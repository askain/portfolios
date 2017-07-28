<!DOCTYPE html>
<html>
  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>Syntax Maker</title>

    <!-- Bootstrap -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
<!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
<script src="js/jquery.min.js"></script>
<script src="js/jquery.cookie.js"></script>
<!-- Include all compiled plugins (below), or include individual files as needed -->
<script src="js/bootstrap.js"></script>
<script src="js/common.js"></script>
<script src="js/syntaxrepository.js"></script>
<script>
	var job_types = ["STORED_PROCEDURE", "PLSQL_BLOCK", "EXECUTABLE"];
	var job_attributes = [
		{	//"STORED_PROCEDURE"
			"job_name"		: "$('#job_name').val()", 
			"job_type"		: "$('#job_type>.btn-primary').text()",
			"job_action"	: "$('#job_action').val()",
			"start_date"	: "$('#start_date').val() + ' ' + $('#start_time').val()",
			"end_date"		: "$('#end_date').val() + ' ' + $('#end_time').val()",
			"repeat_interval": "$('#repeat_interval').val()",
			"enabled"		: "$('#enabled').hasClass('active')",
			"comments"		: "$('#comments').val()"
		},
		{	//"PLSQL_BLOCK"
			"job_name"		: "$('#job_name').val()", 
			"job_type"		: "$('#job_type>.btn-primary').text()",
			"job_action"	: "'BEGIN ' + $('#job_action_PLSQL_BLOCK').val() + ' END;'",
			"start_date"	: "$('#start_date').val() + ' ' + $('#start_time').val()",
			"end_date"		: "$('#end_date').val() + ' ' + $('#end_time').val()",
			"repeat_interval": "$('#repeat_interval').val()",
			"enabled"		: "$('#enabled').hasClass('active')",
			"comments"		: "$('#comments').val()"
		},
		{	//"EXECUTABLE"
			"job_name"		: "$('#job_name').val()", 
			"job_type"		: "$('#job_type>.btn-primary').text()",
			"job_action"	: "$('#job_action').val()",
			"start_date"	: "$('#start_date').val() + ' ' + $('#start_time').val()",
			"end_date"		: "$('#end_date').val() + ' ' + $('#end_time').val()",
			"repeat_interval": "$('#repeat_interval').val()",
			"number_of_arguments": "$('#number_of_arguments').val()",
			"enabled"		: "$('#enabled').hasClass('active')",
			"comments"		: "$('#comments').val()"
		}
	];

	$(function()	{
		var $job_type = $("#job_type");
		for(var i = 0 ; i < job_types.length ; i++)	{
			$job_type.append("<button type='button' class='btn btn-default'>" + job_types[i] + "</button>");
		}
		
		$job_type = $job_type.find("button");
		$job_type.click(function(e)	{
			var $currentTarget = $(e.currentTarget);
			$currentTarget.addClass("btn-primary").addClass("active").siblings().removeClass("btn-primary").removeClass("active");
			
			if($currentTarget.text() === "PLSQL_BLOCK")	{			
				$(".showhide_EXECUTABLE").removeClass("show").addClass("hide");
				$(".showhide_PROGRAM").removeClass("show").addClass("hide");
				$(".showhide_NOT_PLSQL_BLOCK").removeClass("show").addClass("hide");
				$(".showhide_PLSQL_BLOCK").removeClass("hide").addClass("show");
			} else if($currentTarget.text() === "PROGRAM")	{
				$(".showhide_EXECUTABLE").removeClass("show").addClass("hide");
				$(".showhide_PROGRAM").removeClass("hide").addClass("show");
				$(".showhide_NOT_PLSQL_BLOCK").removeClass("show").addClass("hide");
				$(".showhide_PLSQL_BLOCK").removeClass("show").addClass("hide");
			} else if($currentTarget.text() === "EXECUTABLE")	{
				$(".showhide_EXECUTABLE").removeClass("hide").addClass("show");
				$(".showhide_PROGRAM").removeClass("show").addClass("hide");
				$(".showhide_NOT_PLSQL_BLOCK").removeClass("hide").addClass("show");
				$(".showhide_PLSQL_BLOCK").removeClass("show").addClass("hide");
			} else  {
				$(".showhide_EXECUTABLE").removeClass("show").addClass("hide");
				$(".showhide_PROGRAM").removeClass("show").addClass("hide");
				$(".showhide_NOT_PLSQL_BLOCK").removeClass("hide").addClass("show");
				$(".showhide_PLSQL_BLOCK").removeClass("show").addClass("hide");
			}
			
		});
		$job_type.eq(0).click();
	
		$("#number_of_arguments").change(function(e)	{
			var $cTarget = $(e.currentTarget);
			var value = $cTarget.val();
			if( typeof (value%1) === 'number' )	{
				$("[id^=argument_value]").remove();
				for(var i = parseInt(value) ; i > 0 ; i--)	{
					$cTarget.after("<input type='text' id='argument_value"+i+"' class='form-control' placeholder='&lt;파라미터"+i+"&gt;'>");
				}
			}
		});

		$("#make_syntax").click(function(e)	{
			//e.preventDefault();			
			customValidation();
			
			if($(".has-error").length > 0 )
				if(confirm("There is " + $(".has-error").length + " error(s). Do you want to create syntax anyway?") == false)
					return;
			
			var result = makeSyntax();
			$("#result_syntax").text(result).focus();
			
			$("#result_modal").modal();
		});
		
		if(typeof(Storage) === "undefined") {
			$("#save_cookie").removeClass("btn-success").addClass("disabled");
		} else {
		
			var s = SyntaxRepository.get($.urlParam('edit'));
			if(s)	{
				$("#job_name").val(s.job_name);
				for(var i = 0 ; i < job_types.length ; i++)	{
					if(s.job_type === job_types[i])	{
						$("#job_type>button:eq(" + i + ")").click();
						break;
					}
				}
				if(s.job_type === "PLSQL_BLOCK")
					$("#job_action_PLSQL_BLOCK").val($.trim(s.job_action.replace("BEGIN", "").replace("END;", "")));
				else 
					$("#job_action").val(s.job_action);
				$("#start_date").val(s.start_date.split(" ")[0]);
				$("#start_time").val(s.start_date.split(" ")[1]);
				$("#end_date").val(s.end_date.split(" ")[0]);
				$("#end_time").val(s.end_date.split(" ")[1]);
				$("#repeat_interval").val(s.repeat_interval);
				var $setInterval_modal = $("#setInterval_modal");
				var FREQ = s.repeat_interval.split("FREQ=")[1].split(";")[0];
				var INTERVAL = "";
				var BYDAY_num = "";
				var BYDAY_str = "";
				if(s.repeat_interval.indexOf("INTERVAL=") >= 0)
					INTERVAL = s.repeat_interval.split("INTERVAL=")[1].split(";")[0];
				
				if(s.repeat_interval.indexOf("BYDAY=") >= 0)	{
					
					var BYDAY = s.repeat_interval.split("BYDAY=")[1].split(";")[0];
					BYDAY_num = /(-?\d+)/.exec(BYDAY.split(",")[0]);
					if( BYDAY_num )	{
						BYDAY_num = BYDAY_num[0];
						BYDAY_str = BYDAY.replace(BYDAY_num, "").split(",");
					} else {
						BYDAY_str = BYDAY.split(",");
					}
					
				}
				var BYMONTHDAY = "";
				if(s.repeat_interval.indexOf("BYMONTHDAY") >= 0)
					BYMONTHDAY = s.repeat_interval.split("BYMONTHDAY=")[1].split(";")[0].split(",");
					
				var BYHOUR = "";
				if(s.repeat_interval.indexOf("BYHOUR") >= 0)
					BYHOUR = s.repeat_interval.split("BYHOUR=")[1].split(";")[0].split(",");
				
				var BYMINUTE = "";
				if(s.repeat_interval.indexOf("BYMINUTE") >= 0)
					BYMINUTE = s.repeat_interval.split("BYMINUTE=")[1].split(";")[0].split(",");
	
				$setInterval_modal.find("#FREQ").val(FREQ);
				$setInterval_modal.find("#INTERVAL").val(INTERVAL);
				$setInterval_modal.find("#byday_number").val(BYDAY_num);
				$setInterval_modal.find("#BYDAY").val(BYDAY_str);
				$setInterval_modal.find("#BYMONTHDAY").val(BYMONTHDAY);
				$setInterval_modal.find("#BYHOUR").val(BYHOUR);
				$setInterval_modal.find("#BYMINUTE").val(BYMINUTE);
				
				$("#number_of_arguments").val(s.number_of_arguments);
				$("#number_of_arguments").change();
				for(var i = 1 ; i < s.number_of_arguments + 1 ; i++)	{
					$("#argument_value"+i).val(s["argument_value"+i]);
				}
				if(!s.enabled)	$("#enabled").removeClass("active");
				$("#comments").val(s.comments);
			}
		
			$("#save_cookie").click(function()	{
				//e.preventDefault();			
				customValidation();
				
				if($(".has-error").length > 0 )
					if(confirm("There is " + $(".has-error").length + " error(s). Do you want to create syntax anyway?") == false)
						return;
				
				var result = makeJson();
				console.log(result);
				
				SyntaxRepository.add(result);
				
				if(confirm("Syntax 가 저장되었습니다. 확인하시겠습니까?"))	{
					location = "./what_you_saved.php";
				}
			});
		}
	});

	var makeJson = function()	{
		var result = {};
		
		var job_type_text = $("#job_type>.btn-primary").text();
		for(var i = 0 ; i < job_types.length ; i++)	{
			if(job_type_text === job_types[i])	{
				var attributes = job_attributes[i];
				for(var index in attributes) { 
				   if (attributes.hasOwnProperty(index)) {
						var attr = attributes[index];
						var value = eval(attr);
						result[index] = value
				   }
				}
				break;
			}
		}
		if(job_type_text === "EXECUTABLE")	{
			var job_name = $("#job_name").val();
			var number_of_arguments = parseInt($('#number_of_arguments').val());
			for(var i = 1 ; i < number_of_arguments + 1 ; i++)	{
				result["argument_value"+i] = $("#argument_value"+i).val();
			}
		}
		result.isNew = true;
		return result;
	};
	
	var makeSyntax = function()	{
	
		var job_type_text = $("#job_type>.btn-primary").text();
			
		var result = "BEGIN\nDBMS_SCHEDULER.CREATE_JOB (";
		for(var i = 0 ; i < job_types.length ; i++)	{
			if(job_type_text === job_types[i])	{
				var attributes = job_attributes[i];
				for(var index in attributes) { 
				   if (attributes.hasOwnProperty(index)) {
						var attr = attributes[index];
						var value = eval(attr);
						if(value !== "")	{
							if($.type(value) !== "string" || index === "number_of_arguments")	{
								var temp = "\n\t" + index + " => " + value + ",";
							} else {
								var temp = "\n\t" + index + " => '" + value + "',";
							}
							result += temp
						}
				   }
				}
				result = result.substring(0, result.length - 1);
				result += "\n);";
				break;
			}
		}
		if(job_type_text === "EXECUTABLE")	{
			var job_name = $("#job_name").val();
			var number_of_arguments = parseInt($('#number_of_arguments').val());
			for(var i = 1 ; i < number_of_arguments + 1 ; i++)	{
				result += "\t\nDBMS_SCHEDULER.SET_JOB_ARGUMENT_VALUE('" + job_name + "'," + i + ",'" + $("#argument_value"+i).val() + "');";
			}
		}
		
		result += "\nEND;/";
		return result;
	};
	
	var customValidation = function()	{
		$(".form-group").removeClass("has-error");
		//
		var $job_name = $("#job_name");
		var $form_group = $job_name.parent(".form-group");
		if( $job_name.val() === "" )	{
			$form_group.addClass("has-error");
		}
		
		//
		var $job_type_selected = $("#job_type>.btn-primary");
		if($job_type_selected.text() === "STORED_PROCEDURE"
			|| $job_type_selected.text() === "EXECUTABLE")	{
			
			var $job_action = $("#job_action");
			var $form_group = $job_action.parent(".form-group");
			if($job_action.val() === "")	{
				$form_group.addClass("has-error");
			}
		} else if($job_type_selected.text() === "PLSQL_BLOCK")	{
			var $job_action = $("#job_action_PLSQL_BLOCK");
			var $form_group = $job_action.parents(".form-group");
			if($job_action.val() === "")	{
				$form_group.addClass("has-error");
			}
		} else if($job_type_selected.text() === "PROGRAM") {
			var $program_name = $("#program_name");
			var $form_group = $program_name.parent(".form-group");
			if($program_name.val() === "")	{
				$form_group.addClass("has-error");
			}
		}
		
		if($job_type_selected.text() === "EXECUTABLE")	{
			var $number_of_arguments = $("#number_of_arguments");
			$form_group = $number_of_arguments.parent(".form-group");
			var number_of_arguments = parseInt( $number_of_arguments.val() );
			if(number_of_arguments < 0 || isNaN(number_of_arguments))	{
				$form_group.addClass("has-error");
			}
			for(var i = 0 ; i < number_of_arguments ; i++)	{
				var $argument_value = $("#argument_value"+(i+1));
				if(!$argument_value.val())	{
					$form_group.addClass("has-error");
				}
			}
		}
	};

	var tooltips = [
	{
		selector : "#job_name"
		, title_eng : "Name of the job"
		, title_kor : "JOB 이름"
		, datatype : "VARCHAR2"
	}
	,{
		selector : "#start_date"
		, title_eng : "Date to start"
		, title_kor : "시작날짜"
		, datatype : "VARCHAR2"
	}
	,{
		selector : "#end_date"
		, title_eng : "Date to end"
		, title_kor : "종료날짜"
		, datatype : "VARCHAR2"
	}
	]
	$(function()	{
		for(idx in tooltips)	{
			$(tooltips[idx].selector).data("title", tooltips[idx].title_kor).tooltip();
		}
	});
</script>
  </head>
  <body>
	<?php include 'header.php';?>

	<div class="container">
		<form class="form" action="javascript:return;">
			<div class="form-group">
				<input type="text" id="job_name" name="job_name" class="form-control" placeholder="job_name&lt;job이름&gt;" required autofocus>
			</div>

			<div class="btn-group" role="group" id="job_type">
				<!--<button type="button" class="btn btn-default">STORED_PROCEDURE</button>
				<button type="button" class="btn btn-default">PLSQL_BLOCK</button>
				<button type="button" class="btn btn-default">EXECUTABLE</button>
				<button type="button" class="btn btn-default">PROGRAM</button>-->
			</div>
			<div class="form-group"></div>
			<div class="form-group">
				<label class="control-label">시작일시</label>
				<div class="row">
					<div class="col-md-6">
						<input type="date" id="start_date" class="form-control" placeholder="start date" value="1982-01-04">
					</div>
					<div class="col-md-6">
						<input type="time" id="start_time" class="form-control" placeholder="start time" value="00:00">
					</div>
				</div>
			</div>
			<div class="form-group">
				<div class="row">
					<div class="col-md-10">
						<input type="text" id="repeat_interval" class="form-control" placeholder="repeat_interval&lt;반복간격&gt;">
					</div>
					<div class="col-md-2">
						<button id="set_interval" class="btn btn-info btn-block" data-toggle="modal" data-target="#setInterval_modal">set interval</button>
					</div>
				</div>
			</div>
			<div class="form-group">
				<label class="control-label">종료일시</label>
				<div class="row">
					<div class="col-md-6">
						<input type="date" id="end_date" class="form-control" placeholder="end date" value="2082-01-04">
					</div>
					<div class="col-md-6">
						<input type="time" id="end_time" class="form-control" placeholder="end time" value="00:00">
					</div>
				</div>
			</div>
			<div class="form-group showhide_NOT_PLSQL_BLOCK">
				<input type="text" id="job_action" name="job_action" class="form-control" placeholder="job_action&lt;실행명령&gt;">
			</div>
			<div class="form-group showhide_PLSQL_BLOCK">
				<div class="input-group">
				  <div class="input-group-addon">BEGIN </div>
				  <input type="text" id="job_action_PLSQL_BLOCK" class="form-control" placeholder="job_action&lt;실행명령&gt;">
				  <div class="input-group-addon"> END;</div>
				</div>
			</div>
			<div class="form-group showhide_PROGRAM">
				<input type="text" id="program_name" name="program_name" class="form-control" placeholder="program_name&lt;프로그램명&gt;">
			</div>
			<div class="form-group showhide_EXECUTABLE">
				<input type="number" id="number_of_arguments" name="number_of_arguments" class="form-control" placeholder="number_of_arguments&lt;파라미터 갯수&gt;">
			</div>
			<div class="form-group">
				<button type="button" id="enabled" class="btn btn-primary active" data-toggle="button" aria-pressed="false" autocomplete="off">
					Enabled
				</button>
			</div>
			<div class="form-group">
				<input type="text" id="comments" class="form-control" placeholder="comments&lt;비고란&gt;">
			</div>
			<div class="form-group">
				<div class="row">
					<div class="col-md-6">
						<button type="button" id="save_cookie" class="btn btn-success btn-block" type="submit">
							<span class="glyphicon glyphicon-plus" aria-hidden="true"></span> Save Local
						</button>
					</div>
					<div class="col-md-6">
						<button type="button" id="make_syntax" class="btn btn-primary btn-block" type="submit">
							<span class="glyphicon glyphicon-ok" aria-hidden="true"></span> Make Syntax
						</button>
					</div>
				</div>
			</div>
			
<!--
 job_name                       VARCHAR2(100),
  job_class                      VARCHAR2(32),
  job_style                      VARCHAR2(11),
  program_name                   VARCHAR2(100),
  job_action                     VARCHAR2(4000),
  job_type                       VARCHAR2(20),
  schedule_name                  VARCHAR2(65),
  repeat_interval                VARCHAR2(4000),
  schedule_limit                 INTERVAL DAY TO SECOND,
  start_date                     TIMESTAMP WITH TIME ZONE,
  end_date                       TIMESTAMP WITH TIME ZONE,
  event_condition                VARCHAR2(4000),
  queue_spec                     VARCHAR2(100),
  number_of_arguments            NUMBER,
  arguments                      SYS.JOBARG_ARRAY,
  job_priority                   NUMBER,
  job_weight                     NUMBER,
  max_run_duration               INTERVAL DAY TO SECOND,
  max_runs                       NUMBER,
  max_failures                   NUMBER,
  logging_level                  NUMBER,
  restartable                    VARCHAR2(5),
  stop_on_window_close           VARCHAR2(5),
  raise_events                   NUMBER,
  comments                       VARCHAR2(240),
  auto_drop                      VARCHAR2(5),
  enabled                        VARCHAR2(5),
  follow_default_timezone        VARCHAR2(5),
  parallel_instances             VARCHAR2(5),
  aq_job                         VARCHAR2(5),
  instance_id                    NUMBER,
  credential_name                VARCHAR2(65),
  destination                    VARCHAR2(4000),
  database_role                  VARCHAR2(20),
  allow_runs_in_restricted_mode  VARCHAR2(5));
  -->
		</form>
	</div>

<!-- 인터벌 선택창 -->
<script>
var convert2syntax = function()	{
	var result = "FREQ=" + $("#FREQ").val() + "; ";
	
	if($("#INTERVAL").val())	{
		result += "INTERVAL=" + $("#INTERVAL").val() + "; ";
	}
	if($("#BYDAY").val())	{
		result += "BYDAY=" + $("#byday_number").val() + $("#BYDAY").val() + "; ";
	}
	if($("#BYMONTHDAY").val())	{
		result += "BYMONTHDAY=" + $("#BYMONTHDAY").val() + "; ";
	}
	if($("#BYHOUR").val())	{
		result += "BYHOUR=" + $("#BYHOUR").val() + "; ";
	}
	if($("#BYMINUTE").val())	{
		result += "BYMINUTE=" + $("#BYMINUTE").val() + "; ";
	}
	
	$("#repeat_interval").val(result);
};
var reset_interval = function()	{
	$("#INTERVAL").val("")
	$("#byday_number").val("")
	$("#BYDAY").val("")
	$("#BYMONTHDAY").val("")
	$("#BYHOUR").val("")
	$("#BYMINUTE").val("")
};
$(function()	{
	
	var $interval_type = $("#interval_type>li");
		$interval_type.click(function(e)	{
			var $currentTarget = $(e.currentTarget);
			$currentTarget.addClass("active").siblings().removeClass("active");
			if($currentTarget.index() === 0)	{	//정시 실행
				
			} else {
				
			}			
		});
		
});
</script>
<div id="setInterval_modal" class="modal fade bs-example-modal-lg">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title">Set Interval</h4>
      </div>
      <div class="modal-body">
		<form>
			<div class="form-group">
				<label class="control-label">반복:</label>
				<select class="form-control" id="FREQ">
					<option value="YEARLY">년단위</option>
					<option value="MONTHLY">월단위</option>
					<option value="WEEKLY">주단위</option>
					<option value="DAILY">일단위</option>
					<option value="HOURLY">시간단위</option>
					<option value="MINUTELY">분단위</option>
				</select>
			</div>
			<div class="form-group showhide_static">
				<label class="control-label">간격:</label>
				<input type="number" class="form-control" id="INTERVAL" placeholder="INTERVAL">
			</div>
			<div class="form-group">
				<label class="control-label">요일: <kbd><kbd>Ctrl</kbd> + <kbd>Left Click</kbd></kbd>으로 복수선택</label>
				<div class="row">
					<div class="col-md-6">
						<input type="number" class="form-control" id="byday_number" placeholder="byday number">
					</div>
					<div class="col-md-6">
						<select class="form-control" id="BYDAY" multiple>
							<option value="SUN">일요일</option>
							<option value="MON">월요일</option>
							<option value="TUE">화요일</option>
							<option value="WED">수요일</option>
							<option value="THU">목요일</option>
							<option value="FRI">금요일</option>
							<option value="SAT">토요일</option>
						</select>
					</div>
				</div>
			</div>
			
			<div class="form-group">
				<label class="control-label">날짜: <kbd><kbd>Ctrl</kbd> + <kbd>Left Click</kbd></kbd>으로 복수선택</label>
				<select class="form-control" id="BYMONTHDAY" multiple>
					<option value="-3">-3일(월의 마지막날의 전전날)</option>
					<option value="-2">-2일(월의 마지막날의 전날)</option>
					<option value="-1">-1일(월의 마지막날)</option>
					<option value="1">1일(월의 첫째날)</option>
					<option value="2">2일</option>
					<option value="3">3일</option>
					<option value="4">4일</option>
					<option value="5">5일</option>
					<option value="6">6일</option>
					<option value="7">7일</option>
					<option value="8">8일</option>
					<option value="9">9일</option>
					<option value="10">10일</option>
					<option value="11">11일</option>
					<option value="12">12일</option>
					<option value="13">13일</option>
					<option value="14">14일</option>
					<option value="15">15일</option>
					<option value="16">16일</option>
					<option value="17">17일</option>
					<option value="18">18일</option>
					<option value="19">19일</option>
					<option value="20">20일</option>
					<option value="21">21일</option>
					<option value="22">22일</option>
					<option value="23">23일</option>
					<option value="24">24일</option>
					<option value="25">25일</option>
					<option value="26">26일</option>
					<option value="27">27일</option>
					<option value="28">28일</option>
					<option value="29">29일</option>
				</select>
			</div>
			
			<div class="form-group">
				<label class="control-label">시: <kbd><kbd>Ctrl</kbd> + <kbd>Left Click</kbd></kbd>으로 복수선택</label>
				<select class="form-control" id="BYHOUR" multiple>
					<option value="0">0시</option>
					<option value="1">1시</option>
					<option value="2">2시</option>
					<option value="3">3시</option>
					<option value="4">4시</option>
					<option value="5">5시</option>
					<option value="6">6시</option>
					<option value="7">7시</option>
					<option value="8">8시</option>
					<option value="9">9시</option>
					<option value="10">10시</option>
					<option value="11">11시</option>
					<option value="12">12시</option>
					<option value="13">13시</option>
					<option value="14">14시</option>
					<option value="15">15시</option>
					<option value="16">16시</option>
					<option value="17">17시</option>
					<option value="18">18시</option>
					<option value="19">19시</option>
					<option value="20">20시</option>
					<option value="21">21시</option>
					<option value="22">22시</option>
					<option value="23">23시</option>
				</select>
			</div>
			
			<div class="form-group">
				<label class="control-label">분: <kbd><kbd>Ctrl</kbd> + <kbd>Left Click</kbd></kbd>으로 복수선택</label>
				<select class="form-control" id="BYMINUTE" multiple>
					<option value="0">0분</option>
					<option value="1">1분</option>
					<option value="2">2분</option>
					<option value="3">3분</option>
					<option value="4">4분</option>
					<option value="5">5분</option>
					<option value="6">6분</option>
					<option value="7">7분</option>
					<option value="8">8분</option>
					<option value="9">9분</option>
					<option value="10">10분</option>
					<option value="11">11분</option>
					<option value="12">12분</option>
					<option value="13">13분</option>
					<option value="14">14분</option>
					<option value="15">15분</option>
					<option value="16">16분</option>
					<option value="17">17분</option>
					<option value="18">18분</option>
					<option value="19">19분</option>
					<option value="20">20분</option>
					<option value="21">21분</option>
					<option value="22">22분</option>
					<option value="23">23분</option>
					<option value="24">24분</option>
					<option value="25">25분</option>
					<option value="26">26분</option>
					<option value="27">27분</option>
					<option value="28">28분</option>
					<option value="29">29분</option>
					<option value="30">30분</option>
					<option value="31">31분</option>
					<option value="32">32분</option>
					<option value="33">33분</option>
					<option value="34">34분</option>
					<option value="35">35분</option>
					<option value="36">36분</option>
					<option value="37">37분</option>
					<option value="38">38분</option>
					<option value="39">39분</option>
					<option value="40">40분</option>
					<option value="41">41분</option>
					<option value="42">42분</option>
					<option value="43">43분</option>
					<option value="44">44분</option>
					<option value="45">45분</option>
					<option value="46">46분</option>
					<option value="47">47분</option>
					<option value="48">48분</option>
					<option value="49">49분</option>
					<option value="50">50분</option>
					<option value="51">51분</option>
					<option value="52">52분</option>
					<option value="53">53분</option>
					<option value="54">54분</option>
					<option value="55">55분</option>
					<option value="56">56분</option>
					<option value="57">57분</option>
					<option value="58">58분</option>
					<option value="59">59분</option>
				</select>

			</div>
        </form>
      </div>
      <div class="modal-footer">
		<button type="button" class="btn btn-danger" onclick="reset_interval()">Reset</button>
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
		<button type="button" class="btn btn-success" data-dismiss="modal" onclick="convert2syntax()">Set Interval</button>
      </div>
    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->
<!-- /.인터벌 선택창 -->

	
<!-- 결과창 -->
<div id="result_modal" class="modal fade">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title">Syntax Result</h4>
      </div>
      <div class="modal-body">
        <textarea id="result_syntax" class="form-control" style="height:400px;"></textarea>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
      </div>
    </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div><!-- /.modal -->
<!-- /.결과창 -->

  </body>
</html>
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
<style>
.thumbnail.new {
   background-color: #5cb85c;
  -webkit-transition: all 1s ease-in-out;
  -moz-transition: all 1s ease-in-out;
  -o-transition: all 1s ease-in-out;
  transition: all 1s ease-in-out;
}
.thumbnail.old {
   background-color: transparent;
  -webkit-transition: all 1s ease-in-out;
  -moz-transition: all 1s ease-in-out;
  -o-transition: all 1s ease-in-out;
  transition: all 1s ease-in-out;
}
h3	{
	margin-top : 0px;
}
.na {
  color: #4f9fcf;
}
.s {
  color: #d44950;
}
</style>
<script>
	var sample_job = {
		job_name : "sample_job",
		job_type : "PLSQL_BLOCK",
		job_action : "BEGIN select sysdate from dual; END;",
		start_date : "1982-01-04 00:00",
		end_date : "2082-01-04 00:00",
		repeat_interval : "FREQ=MINUTELY; INTERVAL=5; BYDAY=MON,TUE,WED,THU,FRI;",
		enabled : false,
		comments : "토,일요일을 제외한 날에 1분간격으로 sysdate 조회"
	};
	var syntax2summary = function(s)	{
		var r = "DBMS_SCHEDULER.CREATE_JOB (", cnt = 0;
		if(s.job_name && cnt < 5)	{r += "\n<span class='na'>job_name =></span> <span class='s'>'" + s.job_name + "'</span>";cnt++}
		if(s.job_type && cnt < 5)	{r += "\n<span class='na'>job_type =></span> <span class='s'>'" + s.job_type + "'</span>";cnt++}
		if(s.job_action && cnt < 5)	{r += "\n<span class='na'>job_action =></span> <span class='s'>'" + s.job_action.substring(0,27) + "'</span>";cnt++}
		if(s.start_date && cnt < 5)	{r += "\n<span class='na'>start_date =></span> <span class='s'>'" + s.start_date + "'</span>";cnt++}
		if(s.end_date && cnt < 5)	{r += "\n<span class='na'>end_date =></span> <span class='s'>'" + s.end_date + "'</span>";cnt++}
		if(s.repeat_interval && cnt < 5)	{r += "\n<span class='na'>repeat_interval =></span> <span class='s'>'" + s.repeat_interval.substring(0,27) + "'</span>";cnt++}
		if(cnt < 5)	{r += "\n<span class='na'>enabled =></span> <span class='s'>" + s.enabled + "</span>";cnt++}
		
		for(; cnt < 5 ; cnt++)	r += "\n ";
		
		return r;
	};
	var editSampleJob = function()	{
		SyntaxRepository.add(sample_job);
		goEdit(sample_job.job_name);
	};
	var goEdit = function(job_name)	{
		document.location = "./syntax_maker.php?edit=" + job_name;
	}
	$(function()	{
		if(typeof(Storage) === "undefined") {
		} else {
			var $row = $("#row");
			var list = SyntaxRepository.getAll();
			for(var i = 0 ; i < list.length ; i++)	{
				var temp = "<div class='col-sm-6 col-md-4'>"
							+	"<div class='thumbnail" + (list[i].isNew ? " colorthis" : "") + "' job_name='" + list[i].job_name + "'>"
							+		"<div class='highlight'>"
							+			"<pre><code>"
							+				syntax2summary(list[i])
							+			"</code></pre>"
							+		"</div>"
							+		"<div class='caption'>"
							+			"<h3>" + (list[i].job_name ? list[i].job_name : "&lt;Job이름없음&gt;") + "</h3>"
							+			"<p>" + (list[i].comments ? list[i].comments : "&lt;Comments없음&gt;") + "</p>"
							+			"<p><a href='#' class='btn btn-primary func_edit' role='button'>수정</a> <a href='#' class='btn btn-danger func_delete' role='button'>삭제</a></p>"
							+		"</div>"
							+	"</div>"
							+ "</div>";
				$row.prepend(temp);
			}
			
			setTimeout(function()	{
				$(".thumbnail.colorthis").remove("colorthis").addClass("new");
			},100);
			setTimeout(function()	{
				$(".thumbnail.colorthis").addClass("old");
				SyntaxRepository.setAllOld();
			},1000);

		}
		
		$(".func_delete").click(function(e)	{
			if(confirm("해당 syntax를 삭제하겠습니까?") == false) return;
		
			var $cTarget = $(e.currentTarget);
			var $parent = $cTarget.parents(".thumbnail");
			var job_name = $parent.attr("job_name");
			job_name = job_name === "undefined" ? "" : job_name;
			SyntaxRepository.remove(job_name);
			$cTarget.addClass("disabled");
			
			$parent.html("<del>" + $parent.html() + "</del>");
			
			alert("Sytax가 삭제되었습니다.");
		});
		$(".func_edit").click(function(e)	{
			var $cTarget = $(e.currentTarget);
			var job_name = $cTarget.parents(".thumbnail").attr("job_name");
			job_name = job_name === "undefined" ? "" : job_name;
			var s = SyntaxRepository.get(job_name);
			goEdit(job_name);
		});
	});
</script>
  </head>
  <body>
    <?php include 'header.php';?>
	<div class="container">
		<div class="row" id="row">
		  <div class="col-sm-6 col-md-4">
			<div class="thumbnail" job_name="sample_job">
				<div class="highlight">
					<pre><code>DBMS_SCHEDULER.CREATE_JOB (
<span class='na'>job_name =></span> <span class='s'>'sample_job'</span>
<span class='na'>job_type =></span> <span class='s'>'PLSQL_BLOCK'</span>
<span class='na'>job_action =></span> <span class='s'>'BEGIN select sysdate from d</span>
<span class='na'>start_date =></span> <span class='s'>'1982-01-04 00:00'</span>
<span class='na'>end_date =></span> <span class='s'>'2082-01-04 00:00'</span></code></pre>
				</div>
				<div class="caption">
					<h3>샘플</h3>
					<p>샘플 syntax</p>
					<p><a href="#" class="btn btn-primary" role="button" onclick="editSampleJob()">수정</a> <a href="#" class="btn btn-danger disabled" role="button">삭제</a></p>
				</div>
			</div>
		  </div>
		</div>
	</div>
  </body>
</html>
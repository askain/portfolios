//--------------------------------------------------------
// window.userEvents ----    START
//--------------------------------------------------------
(function($) {
		window.userEvents = {
			//----------------- 라이브 챗 START
			live_chat_on : function()	{
				var _liveCnt=$('#liveChatCnt');
				var _closeBtn=$('#liveChatCnt_Close');

				_banner=new module.floatBanner({
					container:_liveCnt
				});
				$('body').on('click','#liveChatCnt_Close',function(event){
					// alert('close');
					$('#liveChatCnt').fadeOut();
					util.common.clearTimer();
					if ($("#today_lvcht").is(":checked")) {
						_banner.setCookie('isOpen','true',1);
					}
					return false;
				});
				/*_closeBtn.bind('click',function(event){
					return false;
		
					//_liveCnt.fadeOut();
					_liveCnt.hide();
					util.common.clearTimer();
					if ($("#today_lvcht").is(":checked")) {
						_banner.setCookie('isOpen','true',1);
					}
					return false;
				});*/
				
				if(_banner.getCookie('isOpen')!='true' && _banner.getCookie('livechatStatus')!='live'){
					util.common.setTimer(_livechat_waiting_time, window.userEvents.callLiveChat);
				}
				
				bindLiveLayerPopupEvent = function(popup, openBtn, closeBtn1, closeBtn2) {
					closeBtn1.bind('click',function(event){
						popup.fadeOut();
						openBtn.focus();
					});
					
					closeBtn2.bind('click',function(event){
						popup.fadeOut();
						openBtn.focus();
					});
				}
				
				bindLiveLayerPopupEvent($('#resultPopup'),$('.rt_submit'),$('.bt_no>a'),$('.bt_exit2>a'));
			},
			live_chat_off : function()	{
				util.common.clearTimer();
				$('#liveChatDisp').hide();
				//$('#liveChatCnt').fadeOut();
				$('#liveChatCnt').hide();
			},
			callLiveChat : function()
			{
				var params = {};
				params.dataType = "json";
				params.cmd = '{';
				params.cmd += '"command":"TALKFLAG"';
				params.cmd += ',"service":"talk"';
				params.cmd += ',"domain_id":"NODE0000000001"';
				params.cmd += '}';
				
				$.ajax({
					type: "POST",
					url: "/livechat",
					dataType: "json",
					data: params,
					async: true,
					success: function(rData){	
//			                	alert("[" + rData.messages + "]");
						if (rData.messages == 'Y') {	 
							$('#liveChatDisp').show();
							$('#liveChatCnt').fadeIn();
							util.common.clearTimer();
						}
					}
				});
			},
			callLiveChatAccept : function()
			{
				if ($("#today_lvcht").is(":checked")) {
					_banner.setCookie('isOpen','true',1);
				}
				
				$('#liveChatDisp').hide();
				$('#liveChatCnt').fadeOut();
				goLiveChatPopup();
			},
			//--------------- 라이브 챗 E N D
			listing : function()	{
				//window.cs_Info.step
				//0 : 휴대폰 리스트
				//1 : 요금제 리스트
				//2 : tgift 리스트
				
				if(window.cs_Info.step == 0)	{
					window.userEvents.listing_phoneList();
				} else if(window.cs_Info.step == 1)	{
					window.userEvents.listing_subscriptionList();
				} else if(window.cs_Info.step == 2)	{
					window.userEvents.listing_tgiftList();
				}
			},
			//
			packing : function(type, obj, goNext)	{
				/*if(_baseGbn == undefined)	alert("_baseGbn을 가져올 수 없습니다.");	
				if(_underAgeFlag == undefined)	alert("underAgeFlag을 가져올 수 없습니다.");
				if(_custTypeCd == undefined)	alert("custTypeCd을 가져올 수 없습니다.");
				if(_userGbn == undefined)	alert("userGbn을 가져올 수 없습니다.");
				if(_nameCert == undefined)	alert("nameCert을 가져올 수 없습니다.");
				if(_custGrade == undefined)	alert("custGrade을 가져올 수 없습니다.");
				*/
				var linq = window.DAO.linq._singletonInstance;
				var layer_id;
				var layer_id_tail;
				
				if(window.cs_Info.step == 0)	{

					var detail = $(".d-select-phone").triggerHandler("getCurrentPhoneDetail");
					
					return window.userEvents.packing_phone(detail);
				} else if(window.cs_Info.step == 1)	{

					var detail = $(".d-select-cost").triggerHandler("getCurrentSubscriptionDetail");
					
					if(type == "list")	{	// 요금제 리스트 페이지에서 바로 Tgift로 이동
						detail = obj.tr.data("detail");
					}
					
					if(detail.SUBSCRIPTION_ID != "NA00004428" && detail.SUBSCRIPTION_ID != "NA00004429") {
						//<--[s]부가서비스 레이업 팝업 컨트롤
						if(obj != undefined){
							if(obj.PREMIUMPASS_AGREE != undefined){
								detail.PREMIUMPASS_AGREE		= obj.PREMIUMPASS_AGREE;
							}
							if(obj.PREMIUMPASS_DUTY_FL != undefined){
								detail.PREMIUMPASS_DUTY_FL	= obj.PREMIUMPASS_DUTY_FL;
							}	
						}
						
						if(window.cs_Info.type === "phone" || window.cs_Info.type === ""){	//휴대폰
							if(detail.PREMIUMPASS_AGREE === "A" 	||		//프리미엄패스+프리클럽 동의
							   detail.PREMIUMPASS_AGREE === "PP"	||		//프리미엄패스 동의
							   detail.PREMIUMPASS_AGREE === "PC"	||		//프리클럽 동의
							   detail.PREMIUMPASS_AGREE === "AL"	||		//프리미엄패스+프리클럽 나중에하기
							   detail.PREMIUMPASS_AGREE === "PL"	||		//프리미엄패스 나중에하기
							   detail.PREMIUMPASS_AGREE === "CL"	||		//프리클럽 나중에하기
							   detail.PREMIUMPASS_DUTY_FL === ""){			//프리미엄패스 or 프리클럽 대상 아닐경우
								//
							} else {
								//프리클럽 대상 휴대폰 => 이하 [프리클럽 휴대폰]으로 표기.
								//스탠다드요금제 이상+프리클럽 휴대폰    => [부가서비스(프리클럽+프리미엄패스) 가입가능 안내] 레이어팝업
								//스탠다드요금제 이상+프리클럽 휴대폰X => [프리미엄패스 가입가능 안내] 레이어팝업
								//스탠다드요금제 이하+프리클럽 휴대폰    => [부가서비스(프리클럽) 가입가능 안내] 레이어팝업
								//스탠다드요금제 이하+프리클럽 휴대폰X => [프리미엄패스 안내] 레이어팝업
								if(detail.PREMIUMPASS_DUTY_FL === "Y"){				//스탠다드요금제 이상
									layer_id_tail		= "1";
								} else if(detail.PREMIUMPASS_DUTY_FL === "N"){		//스탠다드요금제 미만
									layer_id_tail		= "2";
								}	
									
								if(window.cs_Info.packPhoneDetail.FREE_CLUB_FL === "Y"){	//프리클럽 휴대폰일 경우
									//레이어팝업을 띄우기 전에 비체크로 Setting
									$('#premium_free_info0'+layer_id_tail+' #chk_PRCL_agree_'+layer_id_tail).removeAttr("checked");
									$('#premium_free_info0'+layer_id_tail+' #chk_PRPS_agree_'+layer_id_tail).removeAttr("checked");
									
									$('#premium_free_info0'+layer_id_tail+' #sp_mdl_name_'+layer_id_tail).text(window.cs_Info.packPhoneDetail.PRODUCT_GRP_NM);
									$('#premium_free_info0'+layer_id_tail+' #sp_prev_reward_'+layer_id_tail).text(window.cs_Info.packPhoneDetail.PREV_REWARD_AMT);
									
									layer_id = $('#premium_free_info0'+layer_id_tail);
								} else {																			//프리클럽 휴대폰 아닐 경우
									layer_id = $('#premium_pass_info0'+layer_id_tail);
								}	
								
								if(type == "list"){
									layer_id.find('#sel_choice_idx').val(obj.idx);
								}
								$(layer_id).fadeIn();
								
								return false;
							}	
						} else if(window.cs_Info.type === "tablet" && window.cs_Info.packPhoneDetail.WEARABLE_DEVICE === "Y" && detail.DATA_JOIN_NO != "N"){	//태블릿+모회선인증요금제아닐경우
							
							if($("#outdoor_unit_info01 #view_fl").val() != "Y"){
								alert(detail.SUBSCRIPTION_NM + "는 회선인증 없이 가입이 가능합니다.");
								if(type == "list"){
									$("#outdoor_unit_info01").find('#sel_choice_idx').val(obj.idx);
								}
								$("#outdoor_unit_info01 .guide_wrap").attr("style","display:block")
								
								$("#outdoor_unit_info01").fadeIn();
								return false;
							}
						}
						
						detail.eventType	= "pack";	//기프트선택후 퀵변경 이벤트가 발생하면서 myPackSession.getPremiumpassAgree() 를 다시 롤백시켜서 임의로 값 저장.
						//-->[e]부가서비스 레이업 팝업 컨트롤
					}
					
					return window.userEvents.packing_subscription(detail);
				} else if(window.cs_Info.step == 2)	{
					var detail = $(".d-select-tgift").triggerHandler("getCurrentTgiftDetail");
					
					return window.userEvents.packing_tgift(detail);
				} else if(window.cs_Info.step == 3)	{
					return window.userEvents.packing_final(window.cs_Info.packPhoneDetail, window.cs_Info.packSubscriptionDetail, goNext);
				}
			},
			/**
			* UI에서 가져올 파라미터
			* category_id  카테고리 ID
			* company_list 회사 코드 배열
			* sortIndex 정렬프레그
			*     0,	//노출순
			*     1,	//인기순
			*     2,	//최신순
			*     3,	//높은가격순
			*     4,	//낮은가격순
			*
			*/
			listing_phoneList : function()	{
				
				// 선택된 카테고리를 가져옴
				var category_ul = $(".product_step01 ul.category_list");
				var category_id = category_ul.find("[class=on] input").val();
				if(category_id == "on") category_id = undefined;

				// 선택된 회사명을 가져옴
				var subcategory_ul = $(".product_step01 ul.maker_list").find("input:checked");
				var companyList = [];
				for(var i = 0 ; i < subcategory_ul.length ; i++ )	{
					companyList[i] = subcategory_ul.get(i).value;
				}
				
				//$('div.sort_wrap div.select_wrap ul.sort_list>li a')
				var sortIndex =  $("ul.sort_list>li[class=on]").index();
				if(sortIndex == 0)	{	// 노출순
					orderBy = window.DAO.orderByOrderSeq;
				} else if(sortIndex == 1)	{ 
					orderBy = window.DAO.orderByPopularity;	
				} else if(sortIndex == 2)	{ 
					orderBy = window.DAO.orderByRegist;	
				} else if(sortIndex == 3)	{
					orderBy = window.DAO.orderByHighPrice;
				} else if(sortIndex == 4)	{
					orderBy = window.DAO.orderByLowPrice;
				} else if(sortIndex == 5)	{ 
					orderBy = window.DAO.orderByHighDc;
				}
				
				var linq = window.DAO.linq._singletonInstance;
				/*category_id, product_grp_id, product_id, color_seq, company_code_list, orderBy*/
				var phoneList = linq.getPhoneList(
						category_id
						, window.DAO.distinctThis
						, window.DAO.defaultId
						, /*color_seq*/null
						, /*color_hex*/null
						, companyList
						, orderBy);
				
				window.bind.phoneList(phoneList);
				
			},
			detailing_phone : function(product_id, color_hex, entry_cd, phone_capacity)	{
				var linq = window.DAO.linq._singletonInstance;

				var color_seq = null;	// color_hex가 없으면 default_color_seq 로 걸러냄
				
				if(!product_id)	{
					product_id = window.DAO.defaultId;

					if(!color_hex)	{
						color_seq = window.DAO.defaultId;
					}
				}
				
				
				var detail = linq.getPhoneDetail(product_id, color_seq, color_hex, entry_cd, /*commitment_term*/null, /*installment_term*/null);
				/*var detail = linq.getPhoneList(
						category_idnull
						, product_grp_idnull
						, product_id
						, color_seq
						, color_hex
						, company_code_listnull
						, orderBynull
						, entry_cd
						, commitment_termnull
						, installment_termnull
						, phone_capacity);*/

				if(detail.length == 0) {
					alert("존재하지 않는 상품입니다.");
					window.location = "/";
					return;
				}
				
				/*
				// 약정 24개월, 할부 24개월 이 있는지 확인
				var first = Enumerable.From(detail).FirstOrDefault(null, function(x)	{
					return x.COMMITMENT_TERM == 24 && x.INSTALLMET_TERM == 24;
				});
				
				// 약정  24개월, 할부 24개월 이 없으면 약정 24개월만이라도 있는지 확인
				if(first == null)	{
					first = Enumerable.From(detail).FirstOrDefault(detail[0], function(x)	{
						return x.COMMITMENT_TERM == 24;
					});
				}
				*/
				
				// 대표 상품 하나를 가져와서 그 상품을 기준으로 화면을 바인딩함.
				window.bind.phoneDetail(detail[0]);
			},
			listing_subscriptionList : function()	{
				// 선택된 카테고리를 가져옴
				var category_ul = $(".product_step02 ul.category_list");
				var category_id = category_ul.find("[class=on] input").val();
				if(category_id == "on") category_id = undefined;

				// 선택된 회사명을 가져옴
				var subcategory_ul = $(".product_step02 ul.maker_list").find("input:checked");
				var subcategoryList = [];
				for(var i = 0 ; i < subcategory_ul.length ; i++ )	{
					subcategoryList[i] = subcategory_ul.get(i).value;
				}
				
				//$('div.sort_wrap div.select_wrap ul.sort_list>li a')
				var sortIndex =  $("ul.sort_list>li[class=on]").index();
				var orderBy;
				// 태블릿 요금제 정렬(웨어러블 요금제 사용 모델은 휴대폰 요금제 정렬로)
				if( $("#cntLoadType").val() == "tablet" && window.cs_Info.packPhoneDetail.WEARABLE_FL != "Y"){
					if(sortIndex == 0)	{
						orderBy = window.DAO.orderByOrderSeq;
					} else if(sortIndex == 1)	{
						orderBy = window.DAO.orderByPopularity;						
					} else if(sortIndex == 2)	{
						orderBy = window.DAO.orderByLowData;						
					} else if(sortIndex == 3)	{
						orderBy = window.DAO.orderByHighData;						
					} else if(sortIndex == 4)	{
						orderBy = window.DAO.orderByHighPrice;
					} else if(sortIndex == 5)	{
						orderBy = window.DAO.orderByLowPrice;
					}
				}else{ 
					// 휴대폰 요금제 정렬
					if(sortIndex == 0)	{
						orderBy = window.DAO.orderByOrderSeq;
					} else if(sortIndex == 1)	{
						orderBy = window.DAO.orderByPopularity;
					} else if(sortIndex == 2)	{
						orderBy = window.DAO.orderByLowData;
					} else if(sortIndex == 3)	{
						orderBy = window.DAO.orderByHighData;
					} else if(sortIndex == 4)	{
						orderBy = window.DAO.orderByLowTC;
					} else if(sortIndex == 5)	{
						orderBy = window.DAO.orderByHighTC;
					} else if(sortIndex == 6)	{
						orderBy = window.DAO.orderByHighPrice;
					} else if(sortIndex == 7)	{
						orderBy = window.DAO.orderByLowPrice;
					}
				}	
				
				var linq = window.DAO.linq._singletonInstance;
				
				/*category_id, subcategory_id, subscrption_id, subcomm_id, subcomm_term, orderBy*/
				var subscriptionList = linq.getSubscriptionList(category_id, subcategoryList, /*subscrption_id*/window.DAO.distinctThis, /*subcomm_id*/null, /*subcomm_term*/null, orderBy, bestChargeList);
				
				window.bind.subscriptionList(subscriptionList);
				
			},
			detailing_subscription : function(subscription_id)	{
				var linq = window.DAO.linq._singletonInstance;
				
				var detail = linq.getSubscriptionDetail(
						/*category_id*/null, 
						/*subcategory_id*/null, 
						/*subscrption_id*/subscription_id, 
						/*subcomm_id*/null, 
						/*subcomm_term*/null, 
						/*orderBy*/null);
				
				if(detail.length == 0) {
					alert("존재하지 않는 상품입니다.");
					window.location = "/";
					return;
				}
					
				window.bind.subscriptionDetail(detail[0]);
			},
			listing_tgiftList : function()	{
				// 선택된 카테고리를 가져옴
				var category_ul = $(".product_step03 ul.category_list");
				var category_id = category_ul.find("[class=on] input").val();
				if(category_id == "on") category_id = undefined;

				// 선택된 회사명을 가져옴
				var subcategory_ul = $(".product_step03 ul.maker_list").find("input:checked");
				var subcategoryList = [];
				for(var i = 0 ; i < subcategory_ul.length ; i++ )	{
					subcategoryList[i] = subcategory_ul.get(i).value;
				}
				
				//$('div.sort_wrap div.select_wrap ul.sort_list>li a')
				var sortIndex =  $("ul.sort_list>li[class=on]").index();
				var orderBy;
				if(sortIndex == 0)	{	// 노출순
					orderBy = window.DAO.orderByOrderSeq;
				} else if(sortIndex == 1)	{	//인기순
					orderBy = window.DAO.orderByPopularity;
				} else if(sortIndex == 2)	{	//최신순
					orderBy = window.DAO.orderByRegist;
				}
				
				var linq = window.DAO.linq._singletonInstance;
				
				var tgiftList = linq.getTgiftList(/*category_id*/category_id, /*subcategory_id_list*/subcategoryList, /*gift_grp_id*/window.DAO.distinctThis, /*gift_id*/window.DAO.defaultId, /*opt_value*/null, orderBy);
				
				window.bind.tgiftList(tgiftList);
			},
			detailing_tgift : function(gift_id)	{
				var linq = window.DAO.linq._singletonInstance;
				
				var detail;
				if(gift_id)	{
					detail = linq.getTgiftList(/*category_id*/null, /*subcategory_id_list*/null, /*gift_grp_id*/null, /*gift_id*/gift_id);	
				} else {
					detail = linq.getTgiftList(/*category_id*/null, /*subcategory_id_list*/null, /*gift_grp_id*/null, /*gift_id*/window.DAO.defaultId);
				}
				
				if(detail.length == 0) {
					alert("존재하지 않는 상품입니다.");
					window.location = "/";
					return;
				}
				
				// 대표 상품 하나를 가져와서 그 상품을 기준으로 화면을 바인딩함.
				window.bind.tgiftDetail(detail[0]);
				
			},
			optionbar_phone : function(detail, selector, target)	{
				//var phone_final = $("#phone_final");	// 선택한 휴대폰  레이어
				var result_final = $("#result_final");
				var layer_okcash = $("#layer_okcash");	// OK캐쉬백 레이어
				
				var phone_option = ( selector ? selector : $("#phone_optionbar"));
				
				// 쿠폰 할인은 레이어 팝업이 아니라서 적용버튼 클릭시 detail 에 바로 넣어줌
				window.cs_Info.packPhoneDetail = window.cs_Info.packPhoneDetail || {};
				
				// 쿠폰 리셋 최대 2개
				for(i = 0; i < 2 ; i++)	{
					detail["COUPON_ID"			+ i] = undefined;
					detail["COUPON_ISSUE_NO"	+ i] = undefined;
					detail["DC_TYPE"			+ i] = undefined;
					detail["DC_AMT"				+ i] = undefined;
					detail["DC_RATIO_YN"		+ i] = undefined;
					detail["DC_PER_AMT"			+ i] = undefined;
					detail["USE_STATUS"			+ i] = undefined;
					detail["CPN_TYPE"			+ i] = undefined;
					detail["COUPON_USE_TYPE"	+ i] = undefined;
				}
				
				for(var i = 0 ; window.cs_Info.couponList && i < window.cs_Info.couponList.length ; i++)	{
					if(window.cs_Info.couponList[i].DC_TYPE != "1") continue;
					
					detail["COUPON_ID"			+ i] = window.cs_Info.couponList[i].COUPON_ID;
					detail["COUPON_ISSUE_NO"	+ i] = window.cs_Info.couponList[i].COUPON_ISSUE_NO;
					detail["DC_TYPE"			+ i] = window.cs_Info.couponList[i].DC_TYPE;
					detail["DC_AMT"				+ i] = window.cs_Info.couponList[i].DC_AMT;
					detail["DC_RATIO_YN"		+ i] = window.cs_Info.couponList[i].DC_RATIO_YN;
					detail["DC_PER_AMT"			+ i] = window.cs_Info.couponList[i].DC_RATIO_YN;
					detail["USE_STATUS"			+ i] = window.cs_Info.couponList[i].USE_STATUS;
					detail["CPN_TYPE"			+ i] = window.cs_Info.couponList[i].CPN_TYPE;
					detail["COUPON_USE_TYPE"	+ i] = window.cs_Info.couponList[i].COUPON_USE_TYPE;
				}
				
				/*
				window.cs_Info.packPhoneDetail.couponList =	new Array();
				if(!window.cs_Info.couponList || window.cs_Info.couponList.length == 0)	{
					result_final.find("#dis_coupon").prop('checked', false);
					
					window.cs_Info.packPhoneDetail.couponList =	new Array(); 
					
				} else {	// 쿠폰이 있을 경우
					result_final.find("#dis_coupon").prop('checked', true);
					
					for(var i = 0 ; i < window.cs_Info.couponList.length ; i++)	{
						if(window.cs_Info.couponList[i].DC_TYPE == "1")	{	//선할인
							window.cs_Info.packPhoneDetail.couponList.push(window.cs_Info.couponList[i]);
						}
					}
				}*/
				
				if(target == "return"){	//T가족포인트할인에서 호출 =>쿠폰할인금액을 계산해서 쿠폰할인금액을 T가족포인트할인에 Display 
					return detail = getPhonePayment(detail);
					//window.bind.finalResult_tfamily(detail, layerInfo, phone_option);
				} else {
					window.bind.phone_optionbar(detail, phone_option);
				}
			},
			optionbar_subscription : function(detail, selector)	{
				var subscription_final = $("#subscription_final");	// 요금제 파트
				var result_final = $("#result_final");
				var layer_card = $("#layer_card");	// 카드할인 레이어
				
				var WLF_DC_CD = window.cs_Info.WLF_DC_CD;	// 그냥 예전에 설정한 값을 사용
				
				var POINT_11ST_YN = window.cs_Info.POINT_11ST_YN;	//예전에 설정한 값을 사용
				
				// 쿠폰 리셋 최대 1개
				detail["COUPON_ID"			] = undefined;
				detail["COUPON_ISSUE_NO"	] = undefined;
				detail["DC_TYPE"			] = undefined;
				detail["DC_AMT"				] = undefined;
				detail["DC_RATIO_YN"		] = undefined;
				detail["DC_PER_AMT"			] = undefined;
				detail["USE_STATUS"			] = undefined;
				detail["CPN_TYPE"			] = undefined;
				detail["COUPON_USE_TYPE"	] = undefined;
				
				// 쿠폰 할인은 레이어 팝업이 아니라서 적용버튼 클릭시 detail 에 바로 넣어줌 
				window.cs_Info.packSubscriptionDetail = window.cs_Info.packSubscriptionDetail || {};
				for(var i = 0 ; window.cs_Info.couponList && i < window.cs_Info.couponList.length ; i++)	{
					if(window.cs_Info.couponList[i].DC_TYPE != "2")	continue;
					
					//요금할인
					detail["COUPON_ID"			] = window.cs_Info.couponList[i].COUPON_ID;
					detail["COUPON_ISSUE_NO"	] = window.cs_Info.couponList[i].COUPON_ISSUE_NO;
					detail["DC_TYPE"			] = window.cs_Info.couponList[i].DC_TYPE;
					detail["DC_AMT"				] = window.cs_Info.couponList[i].DC_AMT;
					detail["DC_RATIO_YN"		] = window.cs_Info.couponList[i].DC_RATIO_YN;
					detail["DC_PER_AMT"			] = window.cs_Info.couponList[i].DC_RATIO_YN;
					detail["USE_STATUS"			] = window.cs_Info.couponList[i].USE_STATUS;
					detail["CPN_TYPE"			] = window.cs_Info.couponList[i].CPN_TYPE;
					detail["COUPON_USE_TYPE"	] = window.cs_Info.couponList[i].COUPON_USE_TYPE;
				}
				
				// 카드할인 적용
				detail.ASSCARD_DC_CD	= layer_card.find("#select_assocard option:selected").val();
				detail.ASSCARD_DC_AMT	= parseInt( layer_card.find("#select_assocard option:selected").attr("dcCardAmt") );
				
				window.bind.subscription_optionbar(detail, WLF_DC_CD, selector, POINT_11ST_YN);
			},
			// 현재 step에 따라 window.cs_info.href 를 리셋함
			reset_href : function(step)	{
				if(step == 0)	{
					window.cs_Info.href = "";	//파라미터 리셋
					return;
				} 
				if(step == 1)	{
					var newUrl = "";
					var params = window.cs_Info.href.split("&");
					for(var i = 0 ; i < params.length ; i++)	{
						if(i != 0)	newUrl += "&";
						if(params[i].indexOf("SUBSCRIPTION_ID=") == -1
						&& params[i].indexOf("SUBCOMM_TERM=") == -1)	{
							newUrl += params[i];
						}
					}
					window.cs_Info.href = newUrl;
				} 
				if(step == 2){
					var newUrl = "";
					var params = window.cs_Info.href.split("&");
					for(var i = 0 ; i < params.length ; i++)	{
						if(i != 0)	newUrl += "&";
						if(params[i].indexOf("GIFT_GRP_ID=") == -1
						&& params[i].indexOf("GIFT_ID=") == -1)	{
							newUrl += params[i];
						}
					}
					window.cs_Info.href = newUrl;
				}
			},
			/**
			* 휴대폰 상세
			* 휴대폰 값을 재계산함
			*/
			recalculatePrice_phone : function(e)	{
				var detail = $(".d-select-phone").triggerHandler("getCurrentPhoneDetail");
				
				window.bind.phoneDetail_price(detail);
			},
			// 태블릿 회선인증
			chkSktNumberAuth : function()	{
				window.DAO.ajax.chkSktNumberAuth($("#selectPhone1"), $("#selectPhone2"), $("#selectPhone3"));
			},
			//////////////////////////////////////////
			// 휴대폰 패킹 (휴대폰 선택)
			//////////////////////////////////////////
			packing_phone : function(detail)	{
				if(!detail.PRODUCT_ID || !detail.COLOR_SEQ 
						|| !detail.ENTRY_CD || !detail.COMMITMENT_CD
						|| (!detail.COMMITMENT_TERM && detail.COMMITMENT_TERM != 0))	return;
				
				window.userEvents.reset_href(0);
				// 로그인 후 이 화면으로 돌아옴
				document.loginformFloater.returnURL.value = "/handler/CustomShop-ShopMain?step=0&type=detail&PRODUCT_GRP_ID=" + detail.PRODUCT_GRP_ID + "&cntLoadType=" + $("#cntLoadType").val();
				document.loginformFloater.returnURL.value += "&RESUME_YN=Y";
				/* 필요없음
				if (detail.SALE_CD != "01" || detail.SALE_CD2 != "01" ) {
					window.userEvents.sms.openSmsLayer();
					return;
				}
				*/
				if ( _baseGbn == "X" && detail.ENTRY_CD.indexOf("3") == 0 )	{	//로그인하지 않은 상태에서 착기를 선택시
					
					if(detail.ENTRY_CD == "34")
						alert("착한기변 대상 조회 및 정확한 기변정책 적용을 위해 로그인이 필요합니다.\n(ID가 없으시더라도, SMS 간편인증으로도 조회 가능합니다.)");
					
					document.loginformFloater.returnURL.value += "&PRODUCT_ID=" + detail.PRODUCT_ID 
					+ "&COLOR_SEQ=" + detail.COLOR_SEQ 
					+ "&PHONE_CAPACITY=" + detail.PHONE_CAPACITY
					+ "&PRODUCT_GRP_ID=" + detail.PRODUCT_GRP_ID
					+ "&PRODUCT_ID=" + detail.DEFAULT_PRODUCT_ID					
					+ "&MODEL_NICK_NAME=" + detail.TAB_MODEL_NICK_NAME
				;
					
					if(detail.ENTRY_CD == "34" )	{
						document.loginformFloater.returnURL.value += "&ccaLoginYn=Y"
					}
					goTworldLoginPopup();
					return;
				}
						
				if(_baseGbn != "X") {	//로그인인지
					if(( _baseGbn == "N")) {  // 비회원 구매인 경우
						if(detail.ENTRY_CD  == "31" || detail.ENTRY_CD  == "32" || detail.ENTRY_CD == "33" || detail.ENTRY_CD == "34" || detail.ENTRY_CD == "35") {  // 기기변경인 경우
							goTworldLogin();
							return;
						}
					}else if( _baseGbn != "S"){	//실명인증을 제외한 회원 구매 
/*						if( _nameCert != "Y") {  // 실명 인증 안된 고객 체크
							goTworldCertCart(Url);
							return;
						}else{
							*/
						
						/*
						 * 미성년자 여부는 키즈폰 때문에 서버쪽에서 체크 
						 */
							// 기기변경인 경우
							if(detail.ENTRY_CD == "31" || detail.ENTRY_CD == "32" || detail.ENTRY_CD == "33" || detail.ENTRY_CD == "34" || detail.ENTRY_CD == "35") {
								if(!(( _custGrade == "A" || _custGrade == "Y") /*&& _underAgeFlag == "N" */&& _userGbn == "1" &&  _custTypeCd == "01")) {
									alert("기기변경 주문은 본인명의 이동전화로 인증 받으신 인증 회원 등급만 가능합니다.\n회선 인증은 T world 의 회원정보 관리에서 가능합니다.\n(단, 만14세 미만 청소년 및 외국인, 법인 폰 이용 인증 회원, 특수고객(군인 등) 은 주문이 불가능 합니다.)");
									return;
								}
							} else 
							//2011.05.18 신규가입 번호이동 사용하지 않음
							if(detail.ENTRY_CD == "11"|| detail.ENTRY_CD == "20" || detail.ENTRY_CD == "21" || detail.ENTRY_CD == "22" || detail.ENTRY_CD == "23" || detail.ENTRY_CD == "24") {  // 신규가입, 번호이동인 경우
								//미성년자나 외국인,개인,고객타입없는사람
								if( !( /*_underAgeFlag == "N" && */_userGbn == "1" && ( _custTypeCd == "" || _custTypeCd == "01") )) {
									alert("만 14세 미만 청소년 및 법인, 외국인, 특수고객(군인 등) 은 주문이 불가능합니다.");
									return;
								}
							}

/*						}	//실명인증 체크*/
					}	//회원구매
				}	//회원/비회원 구분
				
				if (gbBtnEnabled) {
					gbBtnEnabled = false;	
				} else {
					return;
				}
				$.ajax({
					type : "POST",
					url : "/handler/CustomShop-SetMyPackPhone?" + window.cs_Info.href,
					dataType : "JSON",
					data : {PRODUCT_ID : detail.PRODUCT_ID
						,COLOR_SEQ : detail.COLOR_SEQ
						,ENTRY_CD : detail.ENTRY_CD
						,COMMITMENT_CD : detail.COMMITMENT_CD
						,COMMITMENT_TERM : detail.COMMITMENT_TERM
						,INSTALLMENT_TERM : detail.INSTALLMENT_TERM},
					success : function(d) {
						if(chkJsonError(d) == true)	{
							return;
						}
						if (d.PRODUCT_ID.length > 0) {
							if (d.PRODUCT_ID == "C") {	//요금제가 이미 선택된상태에서, child id 가 없음.
								alert('선택하신 요금제와  맞지 않는 휴대폰 입니다.');
							} /*else if (d.PRODUCT_ID == "N") {	//카테고리가 동일할때
								alert('선택하신 요금제와  맞지 않는 휴대폰 입니다.');
							} */else if (d.PRODUCT_ID == "X") {	//재고가 없음
								//alert('해당상품의 재고가 없습니다.');
								window.userEvents.sms.openSmsLayer();
							} else if (d.PRODUCT_ID == "E") {	//오류
								alert("팩하기중 에러가 발생하였습니다. 다시 시도하여 주십시요.");
							} else if (d.PRODUCT_ID == "A") {	//오류
								alert('휴대폰 오류입니다. 다시 선택하여 주십시오!');
							} else if (d.PRODUCT_ID == "G") {	//착기 대상자가 아님
								alert('착한기변 대상자가 아닙니다.');
							} else if (d.PRODUCT_ID == "L") {	//착기 LITE 대상자가 아님
								alert('로그인하세요.');
							} else if (d.PRODUCT_ID == "K") {	//착기 LITE 대상자가 아님
								alert("기기변경 주문은 본인명의 이동전화로 인증 받으신 인증 회원 등급만 가능합니다.\n회선 인증은 T world 의 회원정보 관리에서 가능합니다.\n(단, 만14세 미만 청소년 및 외국인, 법인 폰 이용 인증 회원, 특수고객(군인 등) 은 주문이 불가능 합니다.)");
							} else if (d.PRODUCT_ID == "J") {	//착기 LITE 대상자가 아님
								alert("만 14세 미만 청소년 및 법인, 외국인, 특수고객(군인 등) 은 주문이 불가능합니다.");
							} else {
								// 성공
								window.userEvents.optionbar_phone(detail);
								window.cs_Info.href = "PRODUCT_GRP_ID=" + detail.PRODUCT_GRP_ID
													+ "&PRODUCT_ID=" + detail.PRODUCT_ID
													+ "&ENTRY_CD=" + $(".d-membership-selectBox option:selected").val()
													;
								window.cs_Info.packPhoneDetail = detail;
								
								
								//웨어러블디바이스면 무조건 온가족할인 비대상
								if(window.cs_Info.packPhoneDetail.WEARABLE_DEVICE === "Y"){
									_W_fmlyDcCd  = "N";
								}
								
								window.bind.final_optionbar();
								
								$("#contents").triggerHandler(window.cs_event.step.process, 1);
							}
						}
					},
				    complete : function(data) {
				    	gbBtnEnabled = true;     
				    },		
					error : function(xhr, status, error) {
						window.cs_Info.packPhoneDetail = null;
						alert("팩하기중 에러가 발생하였습니다. 다시 시도하여 주십시요.");
					}
				});
				
				return true;

			},
			// 원인 : 
			/**
			 * 요금제 패킹
			 * params
			 * 	detail 패킹할 요금제 정보
			 *  phone_optionbar_selector 휴대폰 Optionbar 영역.. 이지만 null 인경우 진짜 optionbar 이고 값이 있으면 final_result.jsp의 휴대폰 영역
			 *  subscription_optionbar_selector 요금제 Optionbar 영역.. 이지만 null 인경우 진짜 optionbar 이고 값이 있으면 final_result.jsp의 요금제 영역 
			 */
			packing_subscription : function(detail, phone_optionbar_selector, subscription_optionbar_selector, goGiftPage)	{
				var autoplan = null;
				window.userEvents.reset_href(1);
				
				if(window.cs_Info.type === "tablet" && $('#container').hasClass('data_together') )	{	// 태블릿 데이터 함께쓰이 일 경우
					if(!$("#inParentServiceNum").val())	{
						alert("데이터함께쓰기 요금제 선택시 반드시 회선인증을 하시기 바랍니다.");
			  			return false;
					}
				} else {
					if(bestChargeList != null)	{
						autoplan = "Y"
					}
				}
				
				/*if (detail.BASIC_CHARGE <= 0) {
					alert('선택 할 수 없는 요금제 입니다.');
					return false;
				}*/

				// 복지할인
				var WLF_DC_CD;
				//--------------클럽T START
				//혹시 요금제 리스트에서 바로 패킹 했을 경우, 게다가 클럽T을 선택한 경우, 
				// select_thWlfDc가 아닌 select_thWlfDc_ClubT에서 복지할인코드를 받어야 한다.
				if(_CLUB_T_SUBSCRIPTION_ID.indexOf(detail.SUBSCRIPTION_ID) >= 0 && $("#select_thWlfDc_ClubT").length > 0)	{	
					WLF_DC_CD = $("#select_thWlfDc_ClubT option:selected").val();	 
				}
				//--------------클럽T E N D 
				else if($("#select_thWlfDc").length > 0)	{
					WLF_DC_CD = $("#select_thWlfDc option:selected").val();	//요금제 리스트에서 바로 Tgift로 이동하는 경우에도 id가 같아서 같이 사용 가능함
				} else {
					WLF_DC_CD = window.cs_Info.WLF_DC_CD;	// 그냥 예전에 설정한 값을 사용
				}
				
				// 11번가 포인트 전환 여부
				var point_11st_yn;
				if($("#point_11st_yn").length > 0)	{
					point_11st_yn = $("#point_11st_yn option:selected").val();
				} else {
					point_11st_yn = window.cs_Info.POINT_11ST_YN;	// 그냥 예전에 설정한 값을 사용
				}
				
				
				if (gbBtnEnabled) {
					gbBtnEnabled = false;
				} else {
					return;
				}
				
				// 추천 맞춤형 요금제인 경우 BASIC_CHARGE 따로 전달
				var my_plan_basic_charge = 0  ;
				var my_plan_add_prod_id  = "" ;
				if( bestChargeList && bestChargeList.length > 0 && bestChargeList[0].MY_PLAN_TYPE == 'MY_PLAN'){
					my_plan_basic_charge = bestChargeList[0].BASIC_CHARGE ;
					my_plan_add_prod_id  = bestChargeList[0].ADD_PROD_ID ;
				}
				
				
				var stepParam = "";
				if(window.cs_Info.step == 1)	stepParam = "&RESET_GIFT_YN=Y";	// 요금제페이지에서 패킹하는 경우 기프트 리셋
				
				$.ajax({
					type : "POST",
					url : "/handler/CustomShop-SetMyPackSubscription?" + window.cs_Info.href + stepParam,
					dataType : "JSON",
					data : {autoplan:autoplan 
							, SUBSCRIPTION_ID		: detail.SUBSCRIPTION_ID
							, SUBCOMM_TERM			: detail.SUBCOMM_TERM
							, SUBCOMM_ID			: detail.SUBCOMM_ID
							, WLF_DC_CD				: WLF_DC_CD 
							, COMMITMENT_CD			: detail.COMMITMENT_CD 
							, point_11st_yn			: point_11st_yn
							, PARENT_SERVICE_NUM	: $("#inParentServiceNum").val()
							, MY_PLAN			    : my_plan_basic_charge 
							, ADD_PROD_ID           : my_plan_add_prod_id
							, PREMIUMPASS_EVENT_TYPE	: detail.eventType
							, PREMIUMPASS_AGREE	: detail.PREMIUMPASS_AGREE},
					success : function(d) {
						if(chkJsonError(d) == true)	{
							return;
						}
						if (d.SUBSCRIPTION_ID.length > 0) {
							if (d.SUBSCRIPTION_ID == "P") {
								alert('휴대폰을 먼저 선택하세요.')					
							} else if (d.SUBSCRIPTION_ID == "C") {	//child id 가 없음. 서로다른 카테고리 선택시
								if ("L" == d.EQP_MTHD_CD) { // lte폰
									alert('선택하신 휴대폰에 따라 LTE요금제만 선택이 가능합니다.');
								} else if ("01" == d.DEVICE_TYPE ) { // 스마트폰
									if (d.SUBCATEGORY_ID == "T" || d.SUBCATEGORY_ID == "S") {	//대부분 스마트폰 요금제일거라 생각하고
										alert('해당요금제는 현재 준비중입니다.');
									} else {
										alert('선택하신 휴대폰에 따라 3G요금제만 선택이 가능합니다.');
									}
								} else if ("02" == d.DEVICE_TYPE) { // 일반폰
									alert('선택하신 휴대폰에 따라 일반 요금제만 선택이 가능합니다.');
								} else {
									alert('해당요금제는 현재 준비중입니다.');
								}
							} else if (d.SUBSCRIPTION_ID == "X") {	//재고가 없음
								alert('해당상품의 재고가 없습니다.');
							} /*else if (d.subscriptionId == "N") {	//카테고리가 동일할때
								alert('해당요금제는 현재 준비중입니다.');
							} else if (d.subscriptionId == "M") {	//유키 시스템 중지로 온가족 대상 확인불가
								alert('시스템 작업으로 온가족 대상확인이 불가능합니다. 약정할인 여부를 다시 선택하십시오.');
							} else if (d.subscriptionId == "W") {	//온가족 대상 아님. (미약정자의 경우)
								alert('온가족 할인 대상자가 아닙니다. 약정할인 여부를 다시 선택하십시오.');
							} */else if (d.SUBSCRIPTION_ID == "E") {	//오류
								alert('해당요금제는 현재 준비중입니다!!');
							} else {
								if(window.cs_Info.step == 1)	{	// 요금제 페이지 인 경우에만 경고 메시지 표시
									if (d.SUBCATEGORY_ID == "T" || d.SUBCATEGORY_ID == "S") {	// Ting/Siver 요금제 일 경우 
										alert("해당 요금제는 특정 대상만 선택 가능하며, 대상이 아닐 경우 주문이 취소될 수 있습니다. \n- 실버 요금제 : 만 65세 이상 고객만 선택 가능\n- 팅 요금제 : 만 18세 미만 고객만 선택 가능");	
									}
									
									/*if (d.SCRP_DC_AMT == "N")	{
										if(!confirm("선택하신 요금제는 스탠다드 등급 미만으로\n요금제 추가 할인이 적용되지 않아 할부원금이 변경되었습니다.\n요금제 추가 할인을 받으시려면 스탠다드 등급 이상의\n요금제를 선택하세요.\nT 기프트로 이동하시겠습니까?"))	{
											return;
										}
									} else {
										alert("요금제가 정상적으로 선택되었습니다.\n선택한 요금제는 스탠다드 등급 이상으로  단말기의 요금제 할인이 추가 할인되었습니다.\n추가할인혜택 : "+ setComma(d.SCRP_DC_AMT) + "원 (해당금액)");
									}*/
								}
								
								if(d.CLUBT == "A")	{
									$("#layer_clubt_dis01").show();	//클럽T선택
								} else if(d.CLUBT == "B")	{
									$("#layer_clubt_dis02").show();	//클럽T해제
								} else if(d.SCRP_DC_AMT_DIFF != "0")	{
									// 요금제 추가할인 금액을 표시하여 준다.
									d.SCRP_DC_AMT_DIFF = parseInt(d.SCRP_DC_AMT_DIFF);
									
									if(d.SCRP_DC_AMT_DIFF < 0)	{
										var layer_dis01 = $("#layer_dis01");
										layer_dis01.find("#scrp_dc_amt_diff").text( setComma(d.SCRP_DC_AMT_DIFF) + "원");
										layer_dis01.show();
									} else {
										var layer_dis02 = $("#layer_dis02");
										layer_dis02.find("#scrp_dc_amt_diff").text( setComma(d.SCRP_DC_AMT_DIFF) + "원");
										layer_dis02.show();
									}
									
								}
								
								//성공
								window.cs_Info.href += "&SUBSCRIPTION_ID=" + d.subscriptionDetail.SUBSCRIPTION_ID;
								
								window.cs_Info.WLF_DC_CD				= d.WLF_DC_CD;		// final result 용으로 저장해둠
								window.cs_Info.POINT_11ST_YN			= d.POINT_11ST_YN;	// final result 용으로 저장해둠
								
								window.cs_Info.packPhoneDetail			= d.phoneDetail;
								window.cs_Info.packSubscriptionDetail	= d.subscriptionDetail;
								
								if(goGiftPage == false)	{
									// 이동안함
								} else if(window.cs_Info.step == 3)	{	//final_result 인 경우
									/*if(d.subscriptionDetail.GIFT_CNT == 0 || d.subscriptionDetail.GIFT_CNT == -1)	{*/
										$("#contents").triggerHandler(window.cs_event.step.process, 2);
										return;
									/*}*/
								} else if(window.cs_Info.step == 1)	{	//요금제 페이지 인 경우 이동
									$("#contents").triggerHandler(window.cs_event.step.process, 2);
									return;
								}
								
								window.userEvents.optionbar_subscription(d.subscriptionDetail);	// optionbar binding
								if(subscription_optionbar_selector)	{	// final_result binding
									window.userEvents.optionbar_subscription(d.subscriptionDetail, subscription_optionbar_selector);
								}
								
								window.userEvents.optionbar_phone(d.phoneDetail);	// optionbar binding 정확한 휴대폰 가격정보로 다시한번 binding
								if(phone_optionbar_selector)	{	// final_result binding
									window.userEvents.optionbar_phone(d.phoneDetail, phone_optionbar_selector);// 정확한 휴대폰 가격정보로 다시한번 binding
								}
								
								window.bind.final_optionbar();
							}
						}
					},
				    complete : function(data) {
				          // 통신이 실패했어도 완료가 되었을 때 이 함수를 타게 된다.
				    	gbBtnEnabled = true;
				    },
					error : function(xhr, status, error) {
						window.cs_Info.packSubscriptionDetail == null;
						alert("팩하기중 에러가 발생하였습니다. 다시 시도하여 주십시요.");
					}
				});	
				
				return true;
			}, 
			// 원인 : tgift 패킹
			packing_tgift : function(detail)	{
				window.userEvents.reset_href(2);
				
				// 실물재고건수체크 (실물은 재고체크를 함)
				if ( ( "0" == detail.GIFT_GB && detail.REAL_GIFT_STOCK < 0 ) ) {
					alert('해당 상품재고가 없습니다.');
					return false;
				}
				
				if (gbBtnEnabled) {
					gbBtnEnabled = false;	
				} else {
					return;
				}
				$.ajax({
					type : "POST",
					url : "/handler/CustomShop-SetMyPackGift?" + window.cs_Info.href,
					dataType : "JSON",
					data : {GIFT_GRP_ID			: detail.GIFT_GRP_ID
						, GIFT_ID				: detail.GIFT_ID
						, PRODUCT_GRP_ID		: window.cs_Info.packPhoneDetail.PRODUCT_GRP_ID
						, SUBSCRIPTION_ID		: window.cs_Info.packSubscriptionDetail.SUBSCRIPTION_ID
						, COMMITMENT_PENALTY	: detail.COMMITMENT_PENALTY},
					success : function(d) {
						if(chkJsonError(d) == true)	{
							return;
						}
						if (d.GIFT_ID.length > 0) {
							if  (d.GIFT_ID == 'E') {
								alert('기프트 팩이 되지 않았습니다. 리프레시후 다시한번 시도하여주시기 바랍니다.');
							} else if(d.GIFT_ID == "C")	{
								alert('선택하신 휴대폰, 요금제와  맞지 않는 기프트 입니다.');
							} else if(d.GIFT_ID == "P")	{
								alert("휴대폰을 먼저 선택하세요.");
							} else if(d.GIFT_ID == "S")	{
								alert("요금제를 먼저 선택 하세요.");
							} else if(d.GIFT_ID == "X")	{
								alert("해당 상품재고가 없습니다.");
							} else if(d.GIFT_ID == "V")	{
								alert('선택하신 T 기프트는 고객등급 골드이상이거나, 스탠다드 요금제 이상일 경우에만 선택이 가능합니다.');
							} else {
								window.cs_Info.packTgiftDetail = detail;
								// 성공
								$("#contents").triggerHandler(window.cs_event.step.process, 3);
							}
						}
					},
				    complete : function(data) {
				        gbBtnEnabled = true;
				    },
					error : function(xhr, status, error) {
						window.cs_Info.packTgiftDetail = null;
						alert("팩하기중 에러가 발생하였습니다. 다시 시도하여 주십시요.");
					}
				});	
			},
			// 원인 : 주문하기 패킹
			packing_final : function(phoneDetail, subscriptionDetail, goNext)	{
				
				// 파라미터 세팅
				var params = {OKCASH_DC_AMT	: phoneDetail.OKCASH_DC_AMT
						      , TFMLY_DC_PT : phoneDetail.TFMLY_DC_PT
							  , ASSCARD_DC_AMT	: subscriptionDetail.ASSCARD_DC_AMT
							  , ASSCARD_DC_CD	: subscriptionDetail.ASSCARD_DC_CD
							  };
				var couponCnt = 0;
				for(; phoneDetail["COUPON_ID" + couponCnt] ; couponCnt++)	{
					params["COUPON_ID"			+ couponCnt] = phoneDetail["COUPON_ID"			+ couponCnt];
					params["CPN_TYPE"			+ couponCnt] = phoneDetail["CPN_TYPE"			+ couponCnt];
					params["COUPON_ISSUE_NO"	+ couponCnt] = phoneDetail["COUPON_ISSUE_NO"	+ couponCnt];
					params["COUPON_USE_TYPE"	+ couponCnt] = phoneDetail["COUPON_USE_TYPE"	+ couponCnt];
					params["USE_STATUS"			+ couponCnt] = phoneDetail["USE_STATUS"			+ couponCnt];
					params["COUPON_DC_AMT"		+ couponCnt] = phoneDetail["COUPON_DC_AMT"		+ couponCnt];
					params["DC_TYPE"			+ couponCnt] = phoneDetail["DC_TYPE"			+ couponCnt];

				}
				
				if(subscriptionDetail.COUPON_ID != null)	{
					params["COUPON_ID"			+ couponCnt] = subscriptionDetail.COUPON_ID;
					params["CPN_TYPE"			+ couponCnt] = subscriptionDetail.CPN_TYPE;
					params["COUPON_ISSUE_NO"	+ couponCnt] = subscriptionDetail.COUPON_ISSUE_NO;
					params["COUPON_USE_TYPE"	+ couponCnt] = subscriptionDetail.COUPON_USE_TYPE;
					params["USE_STATUS"			+ couponCnt] = subscriptionDetail.USE_STATUS;
					params["COUPON_DC_AMT"		+ couponCnt] = subscriptionDetail.COUPON_DC_AMT;
					params["DC_TYPE"			+ couponCnt] = subscriptionDetail.DC_TYPE;
					
				}
				
				
				if (gbBtnEnabled) {
					gbBtnEnabled = false;
				} else {
					return;
				}
				$.ajax({
					type : "POST",
					url : "/handler/CustomShop-SetMyPackFinal",
					dataType : "JSON",
					data : params,
					success : function(d) {
						if(chkJsonError(d) == true)	{
							return;
						}
						if (d.FIANL_CD.length > 0) {
							if  (d.FIANL_CD == 'E') {
								alert('기프트 팩이 되지 않았습니다. 리프레시후 다시한번 시도하여주시기 바랍니다.');
							} else if(d.FIANL_CD == "P")	{
								alert("휴대폰을 먼저 선택하세요.");
							} else if(d.FIANL_CD == "S")	{
								alert("요금제를 먼저 선택 하세요.");
							} else {
								if(goNext == "N")	return;	
									
								window.userEvents.goBuyPage();	// 구매하기ㄱㄱ
							}
						}
					},
				    complete : function(data) {
				        gbBtnEnabled = true;
				    },
					error : function(xhr, status, error) {
						alert("팩하기중 에러가 발생하였습니다. 다시 시도하여 주십시요.");
					}
				});	
			},
			//구매하기 	
			goBuyPage : function(e){
				
				if ( _isLogin 
						&& ( !(_custTypeCd == "" || _custTypeCd == "01")
								|| _isLogin 
								&& ( _sexCd == "5" || _sexCd == "6" || _sexCd == "7" || _sexCd == "8" )
							)
					) {
					alert("만 14세 미만 청소년 및 외국인, 법인 폰 이용 인증 회원, 특수고객(군인 등) 은 주문이 불가능 합니다.");
					return;
				}
				
				//성공
				window.location.href = '/handler/Order-OrderInfo?childId=&disrate=&colorser=&rType=direct';
				return true;
			},
			// 담기
			myWishPack : function(){	//찜하기
				
				if(_isLogin == false)	{
					var form = document.loginformFloater;
					form.returnURL.value = "/handler/CustomShop-ShopMain?type=list&step=3";
					form.returnURL.value += "&RESUME_YN=Y";
					goTworldLogin();
					return;
				}
				
				if (gbBtnEnabled) {
					gbBtnEnabled = false;	
				} else {
					return;
				}			
				$.ajax({
					type : "POST",
					url : "/handler/Common-RgstWishPack",
					dataType : "JSON",
					success : function(d) {
						if(chkJsonError(d) == true)	{
							return;
						}
						if (d.rsltCd == 'A') {
							alert("기기변경 구매를 할수 없는 사용자 입니다. 다시 선택하여 주십시오.");
						} else if(d.rsltCd == "P"){
							alert("휴대폰을 먼저 선택하여 주십시오.");
						} else if(d.rsltCd == "C"){
							alert("요금제을 먼저 선택하여 주십시오.");
						} else if(d.rsltCd == "GN"){
							alert("판매하지 않는 GIFT 상품입니다.");
						} else if(d.rsltCd == "GO"){
							alert("품절된 GIFT상품입니다.");
						} else if(d.rsltCd == "E"){
							alert("담기 중 오류가 발생하였습니다. 다시 시도하여 주십시요.");
						} else if(d.rsltCd == "S"){
							var wishPackConfirm = confirm(d.PRODUCT_GRP_NM + "이 담겼습니다. 담은 상품으로 이동하시겠습니까?");
						}
						
						if(wishPackConfirm)	{
							if( $("#cntLoadType").val() == "tablet" ){
								document.location = "/handler/MyPage-MyWishPackList?pageType=tablet";
							}else{
								document.location = "/handler/MyPage-MyWishPackList";
							}
						}
						
					},
				    complete : function(data) {
				    	gbBtnEnabled = true;
				    },
					error : function(xhr, status, error) {
						alert("담기 중 오류가 발생하였습니다. 다시 시도하여 주십시요.");
					}
				});
			},
			// 퀵변경 : 기프트 갯수 체크
			quickChange : function(PRODUCT_GRP_ID, SUBSCRIPTION_ID, GIFT_ID, SUBCOMM_TERM)	{
				var phone_final = $("#phone_final");	// 휴대폰 파트
				var subscription_final = $("#subscription_final");	// 요금제 파트
				
				$.ajax({
					type : "POST",
					url : "/handler/CustomShop-GetTgiftCnt",
					dataType : "JSON",
					data : {PRODUCT_GRP_ID		: PRODUCT_GRP_ID
							, SUBSCRIPTION_ID	: SUBSCRIPTION_ID
							, GIFT_ID			: GIFT_ID},
					success : function(d) {
						if(chkJsonError(d) == true)	{
							return;
						}
						var goGiftPage = false;

						if(d.tgiftCnt.CURRENT_GIFT_CNT == 0)	{	// 요금제를 변경하면 현제 기프트를 유지할 수 없는 경우
							if(d.tgiftCnt.GIFT_CNT == 0)	{	// 요금제를 변경하면 현제 기프트를 유지할 수도 없고 다른 기프트를 선택할 수도 없음
								
								if(GIFT_ID)	{	//현재 기프트가 있음
									// 근데 이 요금제를 선택하면 아무런 기프트도 선택할 수 없음
									if(confirm("요금제를 변경하시면 선택된 T기프트가 삭제되며 T기프트 를 다시 선택하셔야 합니다. 요금제를 변경하시겠습니까?") == true)	{
										goGiftPage = true;
									} else {
										return;	//요금제 변경하지 않음
									}
								} else { //현재 기프트 조차 없음
									goGiftPage = false;	
								}

							} else {	// 요금제를 변경하면 현제 기프트를 유지할 수도 없지만 다른 기프트를 선택할 수 있음
								if(confirm("요금제를 변경하시면 선택된 T기프트가 삭제되며 T기프트 를 다시 선택하셔야 합니다. 요금제를 변경하시겠습니까?") == true)	{
									goGiftPage = true;
								} else {
									return;	//요금제 변경하지 않음
								}
							}
						} else {	// 요금제를 변경해도 현재 기프트를 유지할 수 있는 경우
							
						}
						
						var linq = window.DAO.linq._singletonInstance; 
						// 추천요금제(맞춤형요금제)를 위해 마지막에 []을 넣음
						var subscriptionDetail = linq.getSubscriptionList(/*category_id*/null, /*subcategory_id_list*/null, SUBSCRIPTION_ID, /*subcomm_id*/null, /*subcomm_term*/SUBCOMM_TERM, /*orderBy*/null, []);
						
						// repacking
						window.userEvents.packing_subscription(subscriptionDetail[0], phone_final, subscription_final, goGiftPage);	// 패킹할때 optionbar 도 binding함
						
					},
				    complete : function(data) {
				    	
				    },
					error : function(xhr, status, error) {
						alert("퀵변경 중 오류가 발생하였습니다. 다시 시도하여 주십시요.");
					}
				});
			},
/*			//T가족포인트 조회/사용/사용취소
			search_tfamily: function(jobCd){

				var layerId;
				var reqPt		 = "";
				var cancleSerNum = "";
				
				layerId = $("#layer_tfamily_apply");
				
				if(jobCd == "APPROVAL") {
					reqPt = $("#use_tfmly_pt").val();	
				} else if(jobCd == "CANCLE") {
					cancleSerNum = $("#cancle_tfmly_pt").val();
				}				
				
				$.ajax({
					type 	 : "POST",
					url 	 : "/handler/Common-GetTFamilyPoint",
					dataType : "JSON",
					data 	 : {jobCd : jobCd, reqPt : reqPt, cancleSerNum : cancleSerNum},
					success : function(d) {
						if(chkJsonError(d) == true)	{
							return;
						}
						
						if(d.rsltMsg != ''){
							alert(d.rsltMsg);
							return;
						} else if (d.rsltTFamily != undefined) {
							var rslt 	  = d.rsltTFamily;
							var allSettle = 0;
							
							if(jobCd == "INFO") {	//조회
								//userEvents.optionbar_phone에서 쿠폰할인금액을 계산 후 bind.finalResult_tfamily에서 T가족포인트레이어에 뿌려준다.   
								window.userEvents.optionbar_phone(window.cs_Info.packPhoneDetail, $("#layer_tfamily_apply"), rslt);
								
							} else if(jobCd == "APPROVAL") {
								$('#cancle_tfmly_pt').text(d.rsltTFamily.pt_icdc_ser_num);	// 포인트증감일련번호
							}
						}
					},
					complete : function(data) {
						  // 통신이 실패했어도 완료가 되었을 때 이 함수를 타게 된다.
						  // TODO
					},
					error : function(xhr, status, error) {
						alert("T가족포인트 사용 중 에러가 발생하였습니다. 다시 시도하여 주십시요.");
					}
				});
				
				return true;
			},*/
			// 할부이자 팝업
			open_InterestView : function()	{
				var addUrl   = "/handler/Order-InterestView";
				  
				  addUrl   += "?salePrice=" + window.cs_Info.packPhoneDetail.SALE_PRICE;
				  addUrl   += "&installmentTerm=" + window.cs_Info.packPhoneDetail.INSTALLMENT_TERM;
				  
				  // salePrice Installment 추가 수정 필요함
				  var newWin = openwindow(addUrl, "InterestView", width=670,height=610, 'no');
				  newWin.focus();
			}
			
		}
})($);
//--------------------------------------------------------
// window.userEvents ----    END
//--------------------------------------------------------
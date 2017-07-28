//--------------------------------------------------------
// window.userEvents ----    START
//--------------------------------------------------------
(function($) {
		window.userEvents = {
			//----------------- ���̺� ê START
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
			//--------------- ���̺� ê E N D
			listing : function()	{
				//window.cs_Info.step
				//0 : �޴��� ����Ʈ
				//1 : ����� ����Ʈ
				//2 : tgift ����Ʈ
				
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
				/*if(_baseGbn == undefined)	alert("_baseGbn�� ������ �� �����ϴ�.");	
				if(_underAgeFlag == undefined)	alert("underAgeFlag�� ������ �� �����ϴ�.");
				if(_custTypeCd == undefined)	alert("custTypeCd�� ������ �� �����ϴ�.");
				if(_userGbn == undefined)	alert("userGbn�� ������ �� �����ϴ�.");
				if(_nameCert == undefined)	alert("nameCert�� ������ �� �����ϴ�.");
				if(_custGrade == undefined)	alert("custGrade�� ������ �� �����ϴ�.");
				*/
				var linq = window.DAO.linq._singletonInstance;
				var layer_id;
				var layer_id_tail;
				
				if(window.cs_Info.step == 0)	{

					var detail = $(".d-select-phone").triggerHandler("getCurrentPhoneDetail");
					
					return window.userEvents.packing_phone(detail);
				} else if(window.cs_Info.step == 1)	{

					var detail = $(".d-select-cost").triggerHandler("getCurrentSubscriptionDetail");
					
					if(type == "list")	{	// ����� ����Ʈ ���������� �ٷ� Tgift�� �̵�
						detail = obj.tr.data("detail");
					}
					
					if(detail.SUBSCRIPTION_ID != "NA00004428" && detail.SUBSCRIPTION_ID != "NA00004429") {
						//<--[s]�ΰ����� ���̾� �˾� ��Ʈ��
						if(obj != undefined){
							if(obj.PREMIUMPASS_AGREE != undefined){
								detail.PREMIUMPASS_AGREE		= obj.PREMIUMPASS_AGREE;
							}
							if(obj.PREMIUMPASS_DUTY_FL != undefined){
								detail.PREMIUMPASS_DUTY_FL	= obj.PREMIUMPASS_DUTY_FL;
							}	
						}
						
						if(window.cs_Info.type === "phone" || window.cs_Info.type === ""){	//�޴���
							if(detail.PREMIUMPASS_AGREE === "A" 	||		//�����̾��н�+����Ŭ�� ����
							   detail.PREMIUMPASS_AGREE === "PP"	||		//�����̾��н� ����
							   detail.PREMIUMPASS_AGREE === "PC"	||		//����Ŭ�� ����
							   detail.PREMIUMPASS_AGREE === "AL"	||		//�����̾��н�+����Ŭ�� ���߿��ϱ�
							   detail.PREMIUMPASS_AGREE === "PL"	||		//�����̾��н� ���߿��ϱ�
							   detail.PREMIUMPASS_AGREE === "CL"	||		//����Ŭ�� ���߿��ϱ�
							   detail.PREMIUMPASS_DUTY_FL === ""){			//�����̾��н� or ����Ŭ�� ��� �ƴҰ��
								//
							} else {
								//����Ŭ�� ��� �޴��� => ���� [����Ŭ�� �޴���]���� ǥ��.
								//���Ĵٵ����� �̻�+����Ŭ�� �޴���    => [�ΰ�����(����Ŭ��+�����̾��н�) ���԰��� �ȳ�] ���̾��˾�
								//���Ĵٵ����� �̻�+����Ŭ�� �޴���X => [�����̾��н� ���԰��� �ȳ�] ���̾��˾�
								//���Ĵٵ����� ����+����Ŭ�� �޴���    => [�ΰ�����(����Ŭ��) ���԰��� �ȳ�] ���̾��˾�
								//���Ĵٵ����� ����+����Ŭ�� �޴���X => [�����̾��н� �ȳ�] ���̾��˾�
								if(detail.PREMIUMPASS_DUTY_FL === "Y"){				//���Ĵٵ����� �̻�
									layer_id_tail		= "1";
								} else if(detail.PREMIUMPASS_DUTY_FL === "N"){		//���Ĵٵ����� �̸�
									layer_id_tail		= "2";
								}	
									
								if(window.cs_Info.packPhoneDetail.FREE_CLUB_FL === "Y"){	//����Ŭ�� �޴����� ���
									//���̾��˾��� ���� ���� ��üũ�� Setting
									$('#premium_free_info0'+layer_id_tail+' #chk_PRCL_agree_'+layer_id_tail).removeAttr("checked");
									$('#premium_free_info0'+layer_id_tail+' #chk_PRPS_agree_'+layer_id_tail).removeAttr("checked");
									
									$('#premium_free_info0'+layer_id_tail+' #sp_mdl_name_'+layer_id_tail).text(window.cs_Info.packPhoneDetail.PRODUCT_GRP_NM);
									$('#premium_free_info0'+layer_id_tail+' #sp_prev_reward_'+layer_id_tail).text(window.cs_Info.packPhoneDetail.PREV_REWARD_AMT);
									
									layer_id = $('#premium_free_info0'+layer_id_tail);
								} else {																			//����Ŭ�� �޴��� �ƴ� ���
									layer_id = $('#premium_pass_info0'+layer_id_tail);
								}	
								
								if(type == "list"){
									layer_id.find('#sel_choice_idx').val(obj.idx);
								}
								$(layer_id).fadeIn();
								
								return false;
							}	
						} else if(window.cs_Info.type === "tablet" && window.cs_Info.packPhoneDetail.WEARABLE_DEVICE === "Y" && detail.DATA_JOIN_NO != "N"){	//�º�+��ȸ������������ƴҰ��
							
							if($("#outdoor_unit_info01 #view_fl").val() != "Y"){
								alert(detail.SUBSCRIPTION_NM + "�� ȸ������ ���� ������ �����մϴ�.");
								if(type == "list"){
									$("#outdoor_unit_info01").find('#sel_choice_idx').val(obj.idx);
								}
								$("#outdoor_unit_info01 .guide_wrap").attr("style","display:block")
								
								$("#outdoor_unit_info01").fadeIn();
								return false;
							}
						}
						
						detail.eventType	= "pack";	//����Ʈ������ ������ �̺�Ʈ�� �߻��ϸ鼭 myPackSession.getPremiumpassAgree() �� �ٽ� �ѹ���Ѽ� ���Ƿ� �� ����.
						//-->[e]�ΰ����� ���̾� �˾� ��Ʈ��
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
			* UI���� ������ �Ķ����
			* category_id  ī�װ� ID
			* company_list ȸ�� �ڵ� �迭
			* sortIndex ����������
			*     0,	//�����
			*     1,	//�α��
			*     2,	//�ֽż�
			*     3,	//�������ݼ�
			*     4,	//�������ݼ�
			*
			*/
			listing_phoneList : function()	{
				
				// ���õ� ī�װ��� ������
				var category_ul = $(".product_step01 ul.category_list");
				var category_id = category_ul.find("[class=on] input").val();
				if(category_id == "on") category_id = undefined;

				// ���õ� ȸ����� ������
				var subcategory_ul = $(".product_step01 ul.maker_list").find("input:checked");
				var companyList = [];
				for(var i = 0 ; i < subcategory_ul.length ; i++ )	{
					companyList[i] = subcategory_ul.get(i).value;
				}
				
				//$('div.sort_wrap div.select_wrap ul.sort_list>li a')
				var sortIndex =  $("ul.sort_list>li[class=on]").index();
				if(sortIndex == 0)	{	// �����
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

				var color_seq = null;	// color_hex�� ������ default_color_seq �� �ɷ���
				
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
					alert("�������� �ʴ� ��ǰ�Դϴ�.");
					window.location = "/";
					return;
				}
				
				/*
				// ���� 24����, �Һ� 24���� �� �ִ��� Ȯ��
				var first = Enumerable.From(detail).FirstOrDefault(null, function(x)	{
					return x.COMMITMENT_TERM == 24 && x.INSTALLMET_TERM == 24;
				});
				
				// ����  24����, �Һ� 24���� �� ������ ���� 24�������̶� �ִ��� Ȯ��
				if(first == null)	{
					first = Enumerable.From(detail).FirstOrDefault(detail[0], function(x)	{
						return x.COMMITMENT_TERM == 24;
					});
				}
				*/
				
				// ��ǥ ��ǰ �ϳ��� �����ͼ� �� ��ǰ�� �������� ȭ���� ���ε���.
				window.bind.phoneDetail(detail[0]);
			},
			listing_subscriptionList : function()	{
				// ���õ� ī�װ��� ������
				var category_ul = $(".product_step02 ul.category_list");
				var category_id = category_ul.find("[class=on] input").val();
				if(category_id == "on") category_id = undefined;

				// ���õ� ȸ����� ������
				var subcategory_ul = $(".product_step02 ul.maker_list").find("input:checked");
				var subcategoryList = [];
				for(var i = 0 ; i < subcategory_ul.length ; i++ )	{
					subcategoryList[i] = subcategory_ul.get(i).value;
				}
				
				//$('div.sort_wrap div.select_wrap ul.sort_list>li a')
				var sortIndex =  $("ul.sort_list>li[class=on]").index();
				var orderBy;
				// �º� ����� ����(����� ����� ��� ���� �޴��� ����� ���ķ�)
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
					// �޴��� ����� ����
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
					alert("�������� �ʴ� ��ǰ�Դϴ�.");
					window.location = "/";
					return;
				}
					
				window.bind.subscriptionDetail(detail[0]);
			},
			listing_tgiftList : function()	{
				// ���õ� ī�װ��� ������
				var category_ul = $(".product_step03 ul.category_list");
				var category_id = category_ul.find("[class=on] input").val();
				if(category_id == "on") category_id = undefined;

				// ���õ� ȸ����� ������
				var subcategory_ul = $(".product_step03 ul.maker_list").find("input:checked");
				var subcategoryList = [];
				for(var i = 0 ; i < subcategory_ul.length ; i++ )	{
					subcategoryList[i] = subcategory_ul.get(i).value;
				}
				
				//$('div.sort_wrap div.select_wrap ul.sort_list>li a')
				var sortIndex =  $("ul.sort_list>li[class=on]").index();
				var orderBy;
				if(sortIndex == 0)	{	// �����
					orderBy = window.DAO.orderByOrderSeq;
				} else if(sortIndex == 1)	{	//�α��
					orderBy = window.DAO.orderByPopularity;
				} else if(sortIndex == 2)	{	//�ֽż�
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
					alert("�������� �ʴ� ��ǰ�Դϴ�.");
					window.location = "/";
					return;
				}
				
				// ��ǥ ��ǰ �ϳ��� �����ͼ� �� ��ǰ�� �������� ȭ���� ���ε���.
				window.bind.tgiftDetail(detail[0]);
				
			},
			optionbar_phone : function(detail, selector, target)	{
				//var phone_final = $("#phone_final");	// ������ �޴���  ���̾�
				var result_final = $("#result_final");
				var layer_okcash = $("#layer_okcash");	// OKĳ���� ���̾�
				
				var phone_option = ( selector ? selector : $("#phone_optionbar"));
				
				// ���� ������ ���̾� �˾��� �ƴ϶� �����ư Ŭ���� detail �� �ٷ� �־���
				window.cs_Info.packPhoneDetail = window.cs_Info.packPhoneDetail || {};
				
				// ���� ���� �ִ� 2��
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
					
				} else {	// ������ ���� ���
					result_final.find("#dis_coupon").prop('checked', true);
					
					for(var i = 0 ; i < window.cs_Info.couponList.length ; i++)	{
						if(window.cs_Info.couponList[i].DC_TYPE == "1")	{	//������
							window.cs_Info.packPhoneDetail.couponList.push(window.cs_Info.couponList[i]);
						}
					}
				}*/
				
				if(target == "return"){	//T��������Ʈ���ο��� ȣ�� =>�������αݾ��� ����ؼ� �������αݾ��� T��������Ʈ���ο� Display 
					return detail = getPhonePayment(detail);
					//window.bind.finalResult_tfamily(detail, layerInfo, phone_option);
				} else {
					window.bind.phone_optionbar(detail, phone_option);
				}
			},
			optionbar_subscription : function(detail, selector)	{
				var subscription_final = $("#subscription_final");	// ����� ��Ʈ
				var result_final = $("#result_final");
				var layer_card = $("#layer_card");	// ī������ ���̾�
				
				var WLF_DC_CD = window.cs_Info.WLF_DC_CD;	// �׳� ������ ������ ���� ���
				
				var POINT_11ST_YN = window.cs_Info.POINT_11ST_YN;	//������ ������ ���� ���
				
				// ���� ���� �ִ� 1��
				detail["COUPON_ID"			] = undefined;
				detail["COUPON_ISSUE_NO"	] = undefined;
				detail["DC_TYPE"			] = undefined;
				detail["DC_AMT"				] = undefined;
				detail["DC_RATIO_YN"		] = undefined;
				detail["DC_PER_AMT"			] = undefined;
				detail["USE_STATUS"			] = undefined;
				detail["CPN_TYPE"			] = undefined;
				detail["COUPON_USE_TYPE"	] = undefined;
				
				// ���� ������ ���̾� �˾��� �ƴ϶� �����ư Ŭ���� detail �� �ٷ� �־��� 
				window.cs_Info.packSubscriptionDetail = window.cs_Info.packSubscriptionDetail || {};
				for(var i = 0 ; window.cs_Info.couponList && i < window.cs_Info.couponList.length ; i++)	{
					if(window.cs_Info.couponList[i].DC_TYPE != "2")	continue;
					
					//�������
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
				
				// ī������ ����
				detail.ASSCARD_DC_CD	= layer_card.find("#select_assocard option:selected").val();
				detail.ASSCARD_DC_AMT	= parseInt( layer_card.find("#select_assocard option:selected").attr("dcCardAmt") );
				
				window.bind.subscription_optionbar(detail, WLF_DC_CD, selector, POINT_11ST_YN);
			},
			// ���� step�� ���� window.cs_info.href �� ������
			reset_href : function(step)	{
				if(step == 0)	{
					window.cs_Info.href = "";	//�Ķ���� ����
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
			* �޴��� ��
			* �޴��� ���� ������
			*/
			recalculatePrice_phone : function(e)	{
				var detail = $(".d-select-phone").triggerHandler("getCurrentPhoneDetail");
				
				window.bind.phoneDetail_price(detail);
			},
			// �º� ȸ������
			chkSktNumberAuth : function()	{
				window.DAO.ajax.chkSktNumberAuth($("#selectPhone1"), $("#selectPhone2"), $("#selectPhone3"));
			},
			//////////////////////////////////////////
			// �޴��� ��ŷ (�޴��� ����)
			//////////////////////////////////////////
			packing_phone : function(detail)	{
				if(!detail.PRODUCT_ID || !detail.COLOR_SEQ 
						|| !detail.ENTRY_CD || !detail.COMMITMENT_CD
						|| (!detail.COMMITMENT_TERM && detail.COMMITMENT_TERM != 0))	return;
				
				window.userEvents.reset_href(0);
				// �α��� �� �� ȭ������ ���ƿ�
				document.loginformFloater.returnURL.value = "/handler/CustomShop-ShopMain?step=0&type=detail&PRODUCT_GRP_ID=" + detail.PRODUCT_GRP_ID + "&cntLoadType=" + $("#cntLoadType").val();
				document.loginformFloater.returnURL.value += "&RESUME_YN=Y";
				/* �ʿ����
				if (detail.SALE_CD != "01" || detail.SALE_CD2 != "01" ) {
					window.userEvents.sms.openSmsLayer();
					return;
				}
				*/
				if ( _baseGbn == "X" && detail.ENTRY_CD.indexOf("3") == 0 )	{	//�α������� ���� ���¿��� ���⸦ ���ý�
					
					if(detail.ENTRY_CD == "34")
						alert("���ѱ⺯ ��� ��ȸ �� ��Ȯ�� �⺯��å ������ ���� �α����� �ʿ��մϴ�.\n(ID�� �����ô���, SMS �����������ε� ��ȸ �����մϴ�.)");
					
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
						
				if(_baseGbn != "X") {	//�α�������
					if(( _baseGbn == "N")) {  // ��ȸ�� ������ ���
						if(detail.ENTRY_CD  == "31" || detail.ENTRY_CD  == "32" || detail.ENTRY_CD == "33" || detail.ENTRY_CD == "34" || detail.ENTRY_CD == "35") {  // ��⺯���� ���
							goTworldLogin();
							return;
						}
					}else if( _baseGbn != "S"){	//�Ǹ������� ������ ȸ�� ���� 
/*						if( _nameCert != "Y") {  // �Ǹ� ���� �ȵ� �� üũ
							goTworldCertCart(Url);
							return;
						}else{
							*/
						
						/*
						 * �̼����� ���δ� Ű���� ������ �����ʿ��� üũ 
						 */
							// ��⺯���� ���
							if(detail.ENTRY_CD == "31" || detail.ENTRY_CD == "32" || detail.ENTRY_CD == "33" || detail.ENTRY_CD == "34" || detail.ENTRY_CD == "35") {
								if(!(( _custGrade == "A" || _custGrade == "Y") /*&& _underAgeFlag == "N" */&& _userGbn == "1" &&  _custTypeCd == "01")) {
									alert("��⺯�� �ֹ��� ���θ��� �̵���ȭ�� ���� ������ ���� ȸ�� ��޸� �����մϴ�.\nȸ�� ������ T world �� ȸ������ �������� �����մϴ�.\n(��, ��14�� �̸� û�ҳ� �� �ܱ���, ���� �� �̿� ���� ȸ��, Ư����(���� ��) �� �ֹ��� �Ұ��� �մϴ�.)");
									return;
								}
							} else 
							//2011.05.18 �ű԰��� ��ȣ�̵� ������� ����
							if(detail.ENTRY_CD == "11"|| detail.ENTRY_CD == "20" || detail.ENTRY_CD == "21" || detail.ENTRY_CD == "22" || detail.ENTRY_CD == "23" || detail.ENTRY_CD == "24") {  // �ű԰���, ��ȣ�̵��� ���
								//�̼����ڳ� �ܱ���,����,��Ÿ�Ծ��»��
								if( !( /*_underAgeFlag == "N" && */_userGbn == "1" && ( _custTypeCd == "" || _custTypeCd == "01") )) {
									alert("�� 14�� �̸� û�ҳ� �� ����, �ܱ���, Ư����(���� ��) �� �ֹ��� �Ұ����մϴ�.");
									return;
								}
							}

/*						}	//�Ǹ����� üũ*/
					}	//ȸ������
				}	//ȸ��/��ȸ�� ����
				
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
							if (d.PRODUCT_ID == "C") {	//������� �̹� ���õȻ��¿���, child id �� ����.
								alert('�����Ͻ� �������  ���� �ʴ� �޴��� �Դϴ�.');
							} /*else if (d.PRODUCT_ID == "N") {	//ī�װ��� �����Ҷ�
								alert('�����Ͻ� �������  ���� �ʴ� �޴��� �Դϴ�.');
							} */else if (d.PRODUCT_ID == "X") {	//��� ����
								//alert('�ش��ǰ�� ��� �����ϴ�.');
								window.userEvents.sms.openSmsLayer();
							} else if (d.PRODUCT_ID == "E") {	//����
								alert("���ϱ��� ������ �߻��Ͽ����ϴ�. �ٽ� �õ��Ͽ� �ֽʽÿ�.");
							} else if (d.PRODUCT_ID == "A") {	//����
								alert('�޴��� �����Դϴ�. �ٽ� �����Ͽ� �ֽʽÿ�!');
							} else if (d.PRODUCT_ID == "G") {	//���� ����ڰ� �ƴ�
								alert('���ѱ⺯ ����ڰ� �ƴմϴ�.');
							} else if (d.PRODUCT_ID == "L") {	//���� LITE ����ڰ� �ƴ�
								alert('�α����ϼ���.');
							} else if (d.PRODUCT_ID == "K") {	//���� LITE ����ڰ� �ƴ�
								alert("��⺯�� �ֹ��� ���θ��� �̵���ȭ�� ���� ������ ���� ȸ�� ��޸� �����մϴ�.\nȸ�� ������ T world �� ȸ������ �������� �����մϴ�.\n(��, ��14�� �̸� û�ҳ� �� �ܱ���, ���� �� �̿� ���� ȸ��, Ư����(���� ��) �� �ֹ��� �Ұ��� �մϴ�.)");
							} else if (d.PRODUCT_ID == "J") {	//���� LITE ����ڰ� �ƴ�
								alert("�� 14�� �̸� û�ҳ� �� ����, �ܱ���, Ư����(���� ��) �� �ֹ��� �Ұ����մϴ�.");
							} else {
								// ����
								window.userEvents.optionbar_phone(detail);
								window.cs_Info.href = "PRODUCT_GRP_ID=" + detail.PRODUCT_GRP_ID
													+ "&PRODUCT_ID=" + detail.PRODUCT_ID
													+ "&ENTRY_CD=" + $(".d-membership-selectBox option:selected").val()
													;
								window.cs_Info.packPhoneDetail = detail;
								
								
								//��������̽��� ������ �°������� ����
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
						alert("���ϱ��� ������ �߻��Ͽ����ϴ�. �ٽ� �õ��Ͽ� �ֽʽÿ�.");
					}
				});
				
				return true;

			},
			// ���� : 
			/**
			 * ����� ��ŷ
			 * params
			 * 	detail ��ŷ�� ����� ����
			 *  phone_optionbar_selector �޴��� Optionbar ����.. ������ null �ΰ�� ��¥ optionbar �̰� ���� ������ final_result.jsp�� �޴��� ����
			 *  subscription_optionbar_selector ����� Optionbar ����.. ������ null �ΰ�� ��¥ optionbar �̰� ���� ������ final_result.jsp�� ����� ���� 
			 */
			packing_subscription : function(detail, phone_optionbar_selector, subscription_optionbar_selector, goGiftPage)	{
				var autoplan = null;
				window.userEvents.reset_href(1);
				
				if(window.cs_Info.type === "tablet" && $('#container').hasClass('data_together') )	{	// �º� ������ �Բ����� �� ���
					if(!$("#inParentServiceNum").val())	{
						alert("�������Բ����� ����� ���ý� �ݵ�� ȸ�������� �Ͻñ� �ٶ��ϴ�.");
			  			return false;
					}
				} else {
					if(bestChargeList != null)	{
						autoplan = "Y"
					}
				}
				
				/*if (detail.BASIC_CHARGE <= 0) {
					alert('���� �� �� ���� ����� �Դϴ�.');
					return false;
				}*/

				// ��������
				var WLF_DC_CD;
				//--------------Ŭ��T START
				//Ȥ�� ����� ����Ʈ���� �ٷ� ��ŷ ���� ���, �Դٰ� Ŭ��T�� ������ ���, 
				// select_thWlfDc�� �ƴ� select_thWlfDc_ClubT���� ���������ڵ带 �޾�� �Ѵ�.
				if(_CLUB_T_SUBSCRIPTION_ID.indexOf(detail.SUBSCRIPTION_ID) >= 0 && $("#select_thWlfDc_ClubT").length > 0)	{	
					WLF_DC_CD = $("#select_thWlfDc_ClubT option:selected").val();	 
				}
				//--------------Ŭ��T E N D 
				else if($("#select_thWlfDc").length > 0)	{
					WLF_DC_CD = $("#select_thWlfDc option:selected").val();	//����� ����Ʈ���� �ٷ� Tgift�� �̵��ϴ� ��쿡�� id�� ���Ƽ� ���� ��� ������
				} else {
					WLF_DC_CD = window.cs_Info.WLF_DC_CD;	// �׳� ������ ������ ���� ���
				}
				
				// 11���� ����Ʈ ��ȯ ����
				var point_11st_yn;
				if($("#point_11st_yn").length > 0)	{
					point_11st_yn = $("#point_11st_yn option:selected").val();
				} else {
					point_11st_yn = window.cs_Info.POINT_11ST_YN;	// �׳� ������ ������ ���� ���
				}
				
				
				if (gbBtnEnabled) {
					gbBtnEnabled = false;
				} else {
					return;
				}
				
				// ��õ ������ ������� ��� BASIC_CHARGE ���� ����
				var my_plan_basic_charge = 0  ;
				var my_plan_add_prod_id  = "" ;
				if( bestChargeList && bestChargeList.length > 0 && bestChargeList[0].MY_PLAN_TYPE == 'MY_PLAN'){
					my_plan_basic_charge = bestChargeList[0].BASIC_CHARGE ;
					my_plan_add_prod_id  = bestChargeList[0].ADD_PROD_ID ;
				}
				
				
				var stepParam = "";
				if(window.cs_Info.step == 1)	stepParam = "&RESET_GIFT_YN=Y";	// ��������������� ��ŷ�ϴ� ��� ����Ʈ ����
				
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
								alert('�޴����� ���� �����ϼ���.')					
							} else if (d.SUBSCRIPTION_ID == "C") {	//child id �� ����. ���δٸ� ī�װ� ���ý�
								if ("L" == d.EQP_MTHD_CD) { // lte��
									alert('�����Ͻ� �޴����� ���� LTE������� ������ �����մϴ�.');
								} else if ("01" == d.DEVICE_TYPE ) { // ����Ʈ��
									if (d.SUBCATEGORY_ID == "T" || d.SUBCATEGORY_ID == "S") {	//��κ� ����Ʈ�� ������ϰŶ� �����ϰ�
										alert('�ش������� ���� �غ����Դϴ�.');
									} else {
										alert('�����Ͻ� �޴����� ���� 3G������� ������ �����մϴ�.');
									}
								} else if ("02" == d.DEVICE_TYPE) { // �Ϲ���
									alert('�����Ͻ� �޴����� ���� �Ϲ� ������� ������ �����մϴ�.');
								} else {
									alert('�ش������� ���� �غ����Դϴ�.');
								}
							} else if (d.SUBSCRIPTION_ID == "X") {	//��� ����
								alert('�ش��ǰ�� ��� �����ϴ�.');
							} /*else if (d.subscriptionId == "N") {	//ī�װ��� �����Ҷ�
								alert('�ش������� ���� �غ����Դϴ�.');
							} else if (d.subscriptionId == "M") {	//��Ű �ý��� ������ �°��� ��� Ȯ�κҰ�
								alert('�ý��� �۾����� �°��� ���Ȯ���� �Ұ����մϴ�. �������� ���θ� �ٽ� �����Ͻʽÿ�.');
							} else if (d.subscriptionId == "W") {	//�°��� ��� �ƴ�. (�̾������� ���)
								alert('�°��� ���� ����ڰ� �ƴմϴ�. �������� ���θ� �ٽ� �����Ͻʽÿ�.');
							} */else if (d.SUBSCRIPTION_ID == "E") {	//����
								alert('�ش������� ���� �غ����Դϴ�!!');
							} else {
								if(window.cs_Info.step == 1)	{	// ����� ������ �� ��쿡�� ��� �޽��� ǥ��
									if (d.SUBCATEGORY_ID == "T" || d.SUBCATEGORY_ID == "S") {	// Ting/Siver ����� �� ��� 
										alert("�ش� ������� Ư�� ��� ���� �����ϸ�, ����� �ƴ� ��� �ֹ��� ��ҵ� �� �ֽ��ϴ�. \n- �ǹ� ����� : �� 65�� �̻� ���� ���� ����\n- �� ����� : �� 18�� �̸� ���� ���� ����");	
									}
									
									/*if (d.SCRP_DC_AMT == "N")	{
										if(!confirm("�����Ͻ� ������� ���Ĵٵ� ��� �̸�����\n����� �߰� ������ ������� �ʾ� �Һο����� ����Ǿ����ϴ�.\n����� �߰� ������ �����÷��� ���Ĵٵ� ��� �̻���\n������� �����ϼ���.\nT ����Ʈ�� �̵��Ͻðڽ��ϱ�?"))	{
											return;
										}
									} else {
										alert("������� ���������� ���õǾ����ϴ�.\n������ ������� ���Ĵٵ� ��� �̻�����  �ܸ����� ����� ������ �߰� ���εǾ����ϴ�.\n�߰��������� : "+ setComma(d.SCRP_DC_AMT) + "�� (�ش�ݾ�)");
									}*/
								}
								
								if(d.CLUBT == "A")	{
									$("#layer_clubt_dis01").show();	//Ŭ��T����
								} else if(d.CLUBT == "B")	{
									$("#layer_clubt_dis02").show();	//Ŭ��T����
								} else if(d.SCRP_DC_AMT_DIFF != "0")	{
									// ����� �߰����� �ݾ��� ǥ���Ͽ� �ش�.
									d.SCRP_DC_AMT_DIFF = parseInt(d.SCRP_DC_AMT_DIFF);
									
									if(d.SCRP_DC_AMT_DIFF < 0)	{
										var layer_dis01 = $("#layer_dis01");
										layer_dis01.find("#scrp_dc_amt_diff").text( setComma(d.SCRP_DC_AMT_DIFF) + "��");
										layer_dis01.show();
									} else {
										var layer_dis02 = $("#layer_dis02");
										layer_dis02.find("#scrp_dc_amt_diff").text( setComma(d.SCRP_DC_AMT_DIFF) + "��");
										layer_dis02.show();
									}
									
								}
								
								//����
								window.cs_Info.href += "&SUBSCRIPTION_ID=" + d.subscriptionDetail.SUBSCRIPTION_ID;
								
								window.cs_Info.WLF_DC_CD				= d.WLF_DC_CD;		// final result ������ �����ص�
								window.cs_Info.POINT_11ST_YN			= d.POINT_11ST_YN;	// final result ������ �����ص�
								
								window.cs_Info.packPhoneDetail			= d.phoneDetail;
								window.cs_Info.packSubscriptionDetail	= d.subscriptionDetail;
								
								if(goGiftPage == false)	{
									// �̵�����
								} else if(window.cs_Info.step == 3)	{	//final_result �� ���
									/*if(d.subscriptionDetail.GIFT_CNT == 0 || d.subscriptionDetail.GIFT_CNT == -1)	{*/
										$("#contents").triggerHandler(window.cs_event.step.process, 2);
										return;
									/*}*/
								} else if(window.cs_Info.step == 1)	{	//����� ������ �� ��� �̵�
									$("#contents").triggerHandler(window.cs_event.step.process, 2);
									return;
								}
								
								window.userEvents.optionbar_subscription(d.subscriptionDetail);	// optionbar binding
								if(subscription_optionbar_selector)	{	// final_result binding
									window.userEvents.optionbar_subscription(d.subscriptionDetail, subscription_optionbar_selector);
								}
								
								window.userEvents.optionbar_phone(d.phoneDetail);	// optionbar binding ��Ȯ�� �޴��� ���������� �ٽ��ѹ� binding
								if(phone_optionbar_selector)	{	// final_result binding
									window.userEvents.optionbar_phone(d.phoneDetail, phone_optionbar_selector);// ��Ȯ�� �޴��� ���������� �ٽ��ѹ� binding
								}
								
								window.bind.final_optionbar();
							}
						}
					},
				    complete : function(data) {
				          // ����� �����߾ �Ϸᰡ �Ǿ��� �� �� �Լ��� Ÿ�� �ȴ�.
				    	gbBtnEnabled = true;
				    },
					error : function(xhr, status, error) {
						window.cs_Info.packSubscriptionDetail == null;
						alert("���ϱ��� ������ �߻��Ͽ����ϴ�. �ٽ� �õ��Ͽ� �ֽʽÿ�.");
					}
				});	
				
				return true;
			}, 
			// ���� : tgift ��ŷ
			packing_tgift : function(detail)	{
				window.userEvents.reset_href(2);
				
				// �ǹ����Ǽ�üũ (�ǹ��� ���üũ�� ��)
				if ( ( "0" == detail.GIFT_GB && detail.REAL_GIFT_STOCK < 0 ) ) {
					alert('�ش� ��ǰ��� �����ϴ�.');
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
								alert('����Ʈ ���� ���� �ʾҽ��ϴ�. ���������� �ٽ��ѹ� �õ��Ͽ��ֽñ� �ٶ��ϴ�.');
							} else if(d.GIFT_ID == "C")	{
								alert('�����Ͻ� �޴���, �������  ���� �ʴ� ����Ʈ �Դϴ�.');
							} else if(d.GIFT_ID == "P")	{
								alert("�޴����� ���� �����ϼ���.");
							} else if(d.GIFT_ID == "S")	{
								alert("������� ���� ���� �ϼ���.");
							} else if(d.GIFT_ID == "X")	{
								alert("�ش� ��ǰ��� �����ϴ�.");
							} else if(d.GIFT_ID == "V")	{
								alert('�����Ͻ� T ����Ʈ�� ����� ����̻��̰ų�, ���Ĵٵ� ����� �̻��� ��쿡�� ������ �����մϴ�.');
							} else {
								window.cs_Info.packTgiftDetail = detail;
								// ����
								$("#contents").triggerHandler(window.cs_event.step.process, 3);
							}
						}
					},
				    complete : function(data) {
				        gbBtnEnabled = true;
				    },
					error : function(xhr, status, error) {
						window.cs_Info.packTgiftDetail = null;
						alert("���ϱ��� ������ �߻��Ͽ����ϴ�. �ٽ� �õ��Ͽ� �ֽʽÿ�.");
					}
				});	
			},
			// ���� : �ֹ��ϱ� ��ŷ
			packing_final : function(phoneDetail, subscriptionDetail, goNext)	{
				
				// �Ķ���� ����
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
								alert('����Ʈ ���� ���� �ʾҽ��ϴ�. ���������� �ٽ��ѹ� �õ��Ͽ��ֽñ� �ٶ��ϴ�.');
							} else if(d.FIANL_CD == "P")	{
								alert("�޴����� ���� �����ϼ���.");
							} else if(d.FIANL_CD == "S")	{
								alert("������� ���� ���� �ϼ���.");
							} else {
								if(goNext == "N")	return;	
									
								window.userEvents.goBuyPage();	// �����ϱ⤡��
							}
						}
					},
				    complete : function(data) {
				        gbBtnEnabled = true;
				    },
					error : function(xhr, status, error) {
						alert("���ϱ��� ������ �߻��Ͽ����ϴ�. �ٽ� �õ��Ͽ� �ֽʽÿ�.");
					}
				});	
			},
			//�����ϱ� 	
			goBuyPage : function(e){
				
				if ( _isLogin 
						&& ( !(_custTypeCd == "" || _custTypeCd == "01")
								|| _isLogin 
								&& ( _sexCd == "5" || _sexCd == "6" || _sexCd == "7" || _sexCd == "8" )
							)
					) {
					alert("�� 14�� �̸� û�ҳ� �� �ܱ���, ���� �� �̿� ���� ȸ��, Ư����(���� ��) �� �ֹ��� �Ұ��� �մϴ�.");
					return;
				}
				
				//����
				window.location.href = '/handler/Order-OrderInfo?childId=&disrate=&colorser=&rType=direct';
				return true;
			},
			// ���
			myWishPack : function(){	//���ϱ�
				
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
							alert("��⺯�� ���Ÿ� �Ҽ� ���� ����� �Դϴ�. �ٽ� �����Ͽ� �ֽʽÿ�.");
						} else if(d.rsltCd == "P"){
							alert("�޴����� ���� �����Ͽ� �ֽʽÿ�.");
						} else if(d.rsltCd == "C"){
							alert("������� ���� �����Ͽ� �ֽʽÿ�.");
						} else if(d.rsltCd == "GN"){
							alert("�Ǹ����� �ʴ� GIFT ��ǰ�Դϴ�.");
						} else if(d.rsltCd == "GO"){
							alert("ǰ���� GIFT��ǰ�Դϴ�.");
						} else if(d.rsltCd == "E"){
							alert("��� �� ������ �߻��Ͽ����ϴ�. �ٽ� �õ��Ͽ� �ֽʽÿ�.");
						} else if(d.rsltCd == "S"){
							var wishPackConfirm = confirm(d.PRODUCT_GRP_NM + "�� �����ϴ�. ���� ��ǰ���� �̵��Ͻðڽ��ϱ�?");
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
						alert("��� �� ������ �߻��Ͽ����ϴ�. �ٽ� �õ��Ͽ� �ֽʽÿ�.");
					}
				});
			},
			// ������ : ����Ʈ ���� üũ
			quickChange : function(PRODUCT_GRP_ID, SUBSCRIPTION_ID, GIFT_ID, SUBCOMM_TERM)	{
				var phone_final = $("#phone_final");	// �޴��� ��Ʈ
				var subscription_final = $("#subscription_final");	// ����� ��Ʈ
				
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

						if(d.tgiftCnt.CURRENT_GIFT_CNT == 0)	{	// ������� �����ϸ� ���� ����Ʈ�� ������ �� ���� ���
							if(d.tgiftCnt.GIFT_CNT == 0)	{	// ������� �����ϸ� ���� ����Ʈ�� ������ ���� ���� �ٸ� ����Ʈ�� ������ ���� ����
								
								if(GIFT_ID)	{	//���� ����Ʈ�� ����
									// �ٵ� �� ������� �����ϸ� �ƹ��� ����Ʈ�� ������ �� ����
									if(confirm("������� �����Ͻø� ���õ� T����Ʈ�� �����Ǹ� T����Ʈ �� �ٽ� �����ϼž� �մϴ�. ������� �����Ͻðڽ��ϱ�?") == true)	{
										goGiftPage = true;
									} else {
										return;	//����� �������� ����
									}
								} else { //���� ����Ʈ ���� ����
									goGiftPage = false;	
								}

							} else {	// ������� �����ϸ� ���� ����Ʈ�� ������ ���� ������ �ٸ� ����Ʈ�� ������ �� ����
								if(confirm("������� �����Ͻø� ���õ� T����Ʈ�� �����Ǹ� T����Ʈ �� �ٽ� �����ϼž� �մϴ�. ������� �����Ͻðڽ��ϱ�?") == true)	{
									goGiftPage = true;
								} else {
									return;	//����� �������� ����
								}
							}
						} else {	// ������� �����ص� ���� ����Ʈ�� ������ �� �ִ� ���
							
						}
						
						var linq = window.DAO.linq._singletonInstance; 
						// ��õ�����(�����������)�� ���� �������� []�� ����
						var subscriptionDetail = linq.getSubscriptionList(/*category_id*/null, /*subcategory_id_list*/null, SUBSCRIPTION_ID, /*subcomm_id*/null, /*subcomm_term*/SUBCOMM_TERM, /*orderBy*/null, []);
						
						// repacking
						window.userEvents.packing_subscription(subscriptionDetail[0], phone_final, subscription_final, goGiftPage);	// ��ŷ�Ҷ� optionbar �� binding��
						
					},
				    complete : function(data) {
				    	
				    },
					error : function(xhr, status, error) {
						alert("������ �� ������ �߻��Ͽ����ϴ�. �ٽ� �õ��Ͽ� �ֽʽÿ�.");
					}
				});
			},
/*			//T��������Ʈ ��ȸ/���/������
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
							
							if(jobCd == "INFO") {	//��ȸ
								//userEvents.optionbar_phone���� �������αݾ��� ��� �� bind.finalResult_tfamily���� T��������Ʈ���̾ �ѷ��ش�.   
								window.userEvents.optionbar_phone(window.cs_Info.packPhoneDetail, $("#layer_tfamily_apply"), rslt);
								
							} else if(jobCd == "APPROVAL") {
								$('#cancle_tfmly_pt').text(d.rsltTFamily.pt_icdc_ser_num);	// ����Ʈ�����Ϸù�ȣ
							}
						}
					},
					complete : function(data) {
						  // ����� �����߾ �Ϸᰡ �Ǿ��� �� �� �Լ��� Ÿ�� �ȴ�.
						  // TODO
					},
					error : function(xhr, status, error) {
						alert("T��������Ʈ ��� �� ������ �߻��Ͽ����ϴ�. �ٽ� �õ��Ͽ� �ֽʽÿ�.");
					}
				});
				
				return true;
			},*/
			// �Һ����� �˾�
			open_InterestView : function()	{
				var addUrl   = "/handler/Order-InterestView";
				  
				  addUrl   += "?salePrice=" + window.cs_Info.packPhoneDetail.SALE_PRICE;
				  addUrl   += "&installmentTerm=" + window.cs_Info.packPhoneDetail.INSTALLMENT_TERM;
				  
				  // salePrice Installment �߰� ���� �ʿ���
				  var newWin = openwindow(addUrl, "InterestView", width=670,height=610, 'no');
				  newWin.focus();
			}
			
		}
})($);
//--------------------------------------------------------
// window.userEvents ----    END
//--------------------------------------------------------
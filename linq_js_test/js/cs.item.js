(function(Class) {
	Class.define("custom.Item", {
		init : function(obj) {
			this.config = {
				cnt : $('#contents'),

				itemWrap : $('div.d-item-wrap'),
				itemList : $('ul.d-item-list'),
				itemUnit : $('ul.d-item-list>li'),
				itemUnitInput : $('ul.d-item-list>li input'),
				itemUnitAnc : $('ul.d-item-list>li .thumb_img>a, ul.d-item-list>li .thumb_tit>a, div.table_wrap tbody>tr td .bt_wrap>span'),
				itemUnitAnc_tableCost : $('div.table_wrap tbody>tr td .bt_wrap>span'),

				//	itemColorSelectAnc : $('.color_wrap>a'),
				//	itemColorSelectList : $('.color_wrap ul>li'),
				itemVolumeSelectAnc : $('.data_choice>a'),

				colorChoice : $('div.color_choice.etc'),
				colorChoiceOpenAnc : $('div.color_choice .btn_open a'),
				colorChoiceCloseAnc : $('div.color_choice .btn_close a'),
				colorChoiceUnit : $('div.color_choice .color_list span'),
				
				adviceBtn : $('span.sc_advice'),
				adviceSubmitBtn : $('span.btn-app-end'),

				c_popup : $('div.d-compare-cnt'),
				c_popup_openBtn : $('div.tip_wrap .que_r'),
				c_popup_closeBtn : $('div.tip_layer button.close'),
				c_execute_btn : $('span.btn_o_ss'),
				c_execute_popup_closeBtn : $('a.d-compare-closeBtn'),

				detailWrap : $('div.detail_wrap'),
				detail_imgListAnc : $('.d-detail-left .small_thumb li'),
				detail_imgListArrow : $('.d-detail-left .thumb_view button'),
				detail_imgListIndex : 0,
				detail_closeBtn : $('.d-detail-close a'),
				detail_membershipSelectBox : $('.d-membership-selectBox'),
				detail_membershipLayer : $('.d-membership-layer'),

				selectPhone : $('.d-select-phone'),
				selectCost : $('.d-select-cost'),
				selectTgift : $('.d-select-tgift'),
				selectCostBtn : $('.sc_choice'),

				noticeStock : $('.sc_inform>button'),
				noticeStockSubmit : $('a.d-noticeStock-next'),
				itemReSelectAnc : $('.btn_resele'),
				costDetailBtn : $('div.box_gray .sc_detail'),

				customizeLayer : $('#my_tariff'),
				customizeAnc : $('div.btn_tariff'),
				customizeTab : $('#my_tariff div.Tab ul>li a'),
				customizeGetBtn : $('#my_tariff span.sc_amount'),
				customizeLayerClose : $('#my_tariff p.btn_close>a'),
				customizeControlBtn : $('#my_tariff span.btn_control'),
				customizeShowTableAnc : $('span.sc_customized , span.sc_recomm_tariff'),
				
				processStep2 : $('#outdoor_unit_info01 .function_agree')	// 141209_T아웃도어 요금제_v0.1
			};
			this.opts = this.opts || {}, this.opts = $.extend(null, this.config, obj);
			//선택이 몇개되었는지
			this.selected = 0;
			this._bind();
		},
		_bind : function() {
			var _self = this, _time, _opts = this.opts, _cnt = _opts.cnt, _d = _opts.detailWrap, _b = $('body');
			//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------리스트모드
			_b.on('click', _opts.colorChoiceOpenAnc.selector, function(event) {// 색상 Layer Open
				event.preventDefault();
				$(this).parents(_opts.colorChoice.selector).addClass('all');
			});
			_b.on('click', _opts.colorChoiceCloseAnc.selector, function(event) {// 색상 Layer Close
				event.preventDefault();
				$(this).parents(_opts.colorChoice.selector).removeClass('all');
			});
			_b.on('click', _opts.colorChoiceUnit.selector, function(event) {// 색상선택하기
				event.preventDefault();
				$(this).addClass('on').siblings().removeClass('on');
				$(this).parents(_opts.colorChoice.selector).removeClass('all');
			});
			_cnt.on('click', _opts.itemUnitInput.selector, function(event) {// 각 아이템 체크박스 선택시
				var _idx = $(this).parent().index();
				var _isChk = $(this).attr('checked');
				_self.selected = $('div.d-item-wrap').find(_opts.itemUnitInput.selector + ":checked").length;
				_self.listAddRemove($(this));
			});
			_cnt.on('click', _opts.c_popup_openBtn.selector, function(event) {//가입비 팝업 열기				
				$(this).parents('.tip_wrap').find('.tip_layer').fadeIn();
			});
			_cnt.on('click', _opts.c_popup_closeBtn.selector, function(event) {//비교하기 알림 팝업닫기
				var _this = $(this).parents('.tip_layer');
				var _chk = _this.prev('input');
				_chk.attr('checked', false);
				var _chked = $('div.d-item-wrap').find(_opts.itemUnitInput.selector + ":checked");
				_self.listAddRemove(_chked.last());
				_this.hide();
			});
			_cnt.on('click', _opts.adviceBtn.selector, function(event) {// 상담신청				
				window.userEvents.counsel.resetCounselLayer();
				$('#smsalert01').fadeIn('fast');
			});
			_b.on('click', _opts.adviceSubmitBtn.selector, function(event) {// 휴대폰 가입 상담 신청 신청완료버튼
				event.preventDefault();
				window.userEvents.counsel.counselRequestInsert();
			});
			_cnt.on('click', _opts.noticeStock.selector, function(event) {// 입고알림신청 팝업열기
				window.userEvents.sms.resetSmsLayer();
				$('#layer_ph_announcement').fadeIn('fast');
			});
			_b.on('click', _opts.noticeStockSubmit.selector, function(event) {// 입고알림신청
				event.preventDefault();
				window.userEvents.sms.soldOutSmsInsert();
			});
			//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------나에게 맞는 요금제 찾기
			_cnt.on('click', _opts.customizeAnc.selector, function(event) {// 나에게 맞는 요금제 찾기 layer open
				event.preventDefault();				
				$('div.product_step02').addClass('mytariff');
				$('div.product_step02 div.product_area').hide();
				$('#tariffTitle').attr('src', '/tws/images/product/tit_recommend_tariff.png'); // 추천요금제로 타이틀 이미지 교체
			});
			_cnt.on('click', _opts.customizeLayerClose.selector, function(event) {// 나에게 맞는 요금제 찾기 layer close
				event.preventDefault();
				// 원래 요금제 리스트로 바인딩
				bestChargeList = null;
				window.userEvents.listing_subscriptionList();
				
				$('div.product_step02').removeClass('mytariff myt_search');
				// ----- 개발 수정 : 처음화면열때 상품이 없어도 상품영역이 보이는 버그 수정 START
				if($('div.product_step02 div.product_area .d-item-list>li').length > 0)	{
					$('div.product_step02 div.product_area').show();
				}
				// ----- 개발 수정 : 처음화면열때 상품이 없어도 상품영역이 보이는 버그 수정  E N D
				//---- 초기화
				$('div.d-customize-cnt1').addClass('search');
				_opts.customizeShowTableAnc.addClass('disable');
				//$('.sc_recomm_tariff').removeClass('disable');
				$('div.product_step02').removeClass('myt_search');
				$('#tariffTitle').attr('src', '/tws/images/product/tit_normal_tariff.png'); // 일반요금제로 타이틀 이미지 교체
			});
			_cnt.on('click', _opts.customizeShowTableAnc.selector, function(event) {// 나에게 맞는 요금제 찾기 결과 table 형태로 보여주기
				var _isChk=$(this).hasClass('disable');
				if(_isChk){
					// disable 클래스 있을경우 작동안되게 해놓음. 14.08.20
					//alert(' not ');
					return false;
				}else{
					event.preventDefault();
					if($(this).hasClass("sc_customized")){					
						window.myTariff.getChargeMyPlan(); //맞춤형 요금제 찾기
					}else{					
						window.myTariff.getChargeRcProd(); //요금제 추천받기
					}
				}				
				//$('div.product_step02').addClass('myt_search');
				//$(this).addClass('disable');
			});
			_cnt.on('click', _opts.customizeTab.selector, function(event) { // 이용량을 바탕으로 추천받기탭 , 맞춤형 요금제 직접 설계하기탭
				// 0 이용량 추천 , 1  맞춤형 요금제
				var _idx = $(this).parents('li').index();
				if(_idx == 0 ){	
					
					if(_isLogin && (_baseGbn == 'L') ){
						if(_svcNum == ""){
							$("#myTariff2").click();    // 맞춤형 요금제 직접설계하기로 이동
							alert('이용량을 바탕으로 추천받기가 불가능합니다.(전화번호가 조회되지 않음!)');
							return;
						}
						window.myTariff.userInfo(); // 사용자 요금 정보 가져오기
						$(".my_info").show();       // 로그인시 안내문구 show
						$(".login_area").hide();    // 로그인시 로그인 영역 hide
						$(".login_mseg").hide(); 
						$(".sc_amount").removeClass("disable");
					}else if(!_isLogin){           
						$(".my_info").hide();       // 비로그인시 내 정보 hide
						$(".login_area").show();    // 비로그인시 로그인 영역 show
						$(".login_mseg").show();    
					}else{
						$(".login_area").hide();    // 로그인시 로그인 영역 hide
						$(".my_info").hide();       // 로그인시 내 정보 hide
						$("#myTariff2").click();    // 맞춤형 요금제 직접설계하기로 이동
						alert("이용량을 바탕으로 추천받기는 SKT 회선 가입자만 이용이 가능하며, 준회원인 경우 맞춤형 요금제 직접 설계하기만 가능합니다.");
						return ;
					}	
					
					$(this).addClass('on').parent().siblings('li').find('a').removeClass('on');
					(_idx === 0) ? $('div.d-customize-cnt1').show() : $('div.d-customize-cnt1').hide();
					(_idx === 0) ? $('div.d-customize-cnt2').hide() : $('div.d-customize-cnt2').show();
					$('div.product_step02').removeClass('myt_search');					
					
					// 화면 초기화
					$(".product_area.d-item-wrap").attr("style", "display:none");
					$(".pnone_list_none").attr("style", "display:none");
					
				}else{
					$(this).addClass('on').parent().siblings('li').find('a').removeClass('on');
					(_idx === 0) ? $('div.d-customize-cnt1').show() : $('div.d-customize-cnt1').hide();
					(_idx === 0) ? $('div.d-customize-cnt2').hide() : $('div.d-customize-cnt2').show();
					$('div.product_step02').removeClass('myt_search');
					
					// 이용량을 바탕으로추천받기 -> 요금제 추천받기 버튼
					$(".sc_recomm_tariff").removeClass('disable');
					
					//화면 초기화
					$(".product_area.d-item-wrap").attr("style", "display:none");
					$(".pnone_list_none").attr("style", "display:none");
								
				}
			});
			_cnt.on('click', _opts.customizeGetBtn.selector, function(event) {// 이용량을 바탕으로 추천받기 에서 "사용량 조회하기"
				
				var _isChk=$(this).hasClass('disable');
				if(_isChk){
					// disable 클래스 있을경우 작동안되게 해놓음. 14.08.20
					//alert(' not ');
					return false;
				}else{
					window.myTariff.getUsedAmt();
				}
				$('div.d-customize-cnt1').removeClass('search');
				$('.sc_recomm_tariff').removeClass('disable');
				_opts.customizeShowTableAnc.removeClass('disable');
			});
			var _isDown1 = false;
			var _isDown2 = false;
			_cnt.on('mousedown mouseup click', _opts.customizeControlBtn.selector, function(event) {// 맞춤형 요금제 직접 설계하기 컨트롤버튼
				var _idx = $(this).parents('li').index();
				if (event.type === 'mousedown') {
					if (_idx === 0) {
						_isDown1 = true;
						_isDown2 = false;
					} else {
						_isDown1 = false;
						_isDown2 = true;
					}
				} else if (event.type === 'mouseup click') {
					_isDown1 = false;
					_isDown2 = false;
				}
			});
			_cnt.on('mouseup', _opts.customizeLayer.selector, function(event) {// 맞춤형 요금제 직접 설계하기 컨트롤버튼
				_isDown1 = false;
				_isDown2 = false;
			});
			_cnt.on('mousemove', _opts.customizeLayer.selector, function(event) {// 맞춤형 요금제 직접 설계하기 컨트롤버튼
				var _g = $('div.d-customize-cnt2 ul.graph_list li');
				//--------------------------   14.09.01   14.09.01 수정 START
				var _ver = -1;
				if (navigator.appName == 'Microsoft Internet Explorer') {
					var ua = navigator.userAgent;
					var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
					if (re.exec(ua) != null)
						_ver = parseFloat(RegExp.$1);
				} else if (navigator.appName == 'Netscape') {
					var ua = navigator.userAgent;
					var re = new RegExp("Trident/.*rv:([0-9]{1,}[\.0-9]{0,})");
					if (re.exec(ua) != null) {
						_ver = parseFloat(RegExp.$1);
					}
				}
				var _mnt = 120;
				//	console.log(' ver  : ' + _ver)
				if (_ver > -1) {// IE Version is ver
					_mnt = 100;
					if (Number(_ver) <= 10) {
						_mnt = 100;
					}
				} else {
					_mnt = 140;
				}
				//------------------------------------------------------------------  14.09.01 수정 END
				var _idx, _top = parseInt($(this).css('height')) - event.pageY + _mnt, _select = Math.round(_top / 40);
				if (_top >= 240 || _top <= 75)
					return false;
				if (_isDown1) {
					_top = _select * 40 + 2;
					_idx = 0;
				}
				if (_isDown2) {
					_top = _select * 40 - 3;
					_idx = 1;
				}
				_g.eq(_idx).find('.png_graph').css('height', _top + 'px');
				var _span = _g.eq(_idx).find('.png_graph span.data_num');
				_span.hide().eq(_select - 2).show();
				var _txt = _span.eq(_select - 2).text();
				(_isDown1) ? $('#d-cus-voice').text(_txt) : null;
				(_isDown2) ? $('#d-cus-data').text(_txt) : null;

			});
			//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------상세보기
			_cnt.on('click', _opts.costDetailBtn.selector, function(event) {
				$('#costDetail').fadeIn('fast');
			});
			_cnt.on('click', _opts.detail_imgListAnc.selector, function(event) {// 상세보기 썸네일 이미지 클릭시
				//-------------------14.08.31
				event.preventDefault();
				
				var _idx = $(this).index();
				$(this).addClass('on').siblings().removeClass('on');
				$(this).parents('.thumb_view').find('.thumb_img img').hide().eq(_idx).show();
			});
			_cnt.on('click', _opts.detail_imgListArrow.selector, function(event) {// 상세보기 썸네일 이미지 좌우 화살표 (4개보다 클경우)
				//-------------------14.08.31
				var _li = $(this).siblings('.small_thumb').find('li');
				var _total = Math.ceil(_li.length / 4);
				var _mode = ($(this).hasClass('btnicon_pre')) ? "prev" : "next";
				if (_mode === "prev") {
					_opts.detail_imgListIndex = (_opts.detail_imgListIndex <= 0) ? 0 : --_opts.detail_imgListIndex;
				} else if (_mode === "next") {
					_opts.detail_imgListIndex = (_opts.detail_imgListIndex >= _total - 1) ? _total - 1 : ++_opts.detail_imgListIndex;
				}
				_li.each(function(idx) {
					if ((_opts.detail_imgListIndex * 4 <= idx) && (idx <= _opts.detail_imgListIndex * 4 + 3)) {
						$(this).show();
					} else {
						$(this).hide();
					}
				});
			});
			_cnt.on('click', _opts.detail_closeBtn.selector, function(event) {// 상세팝업닫기
				
				//각 분기별 처리를 위한 cntLoadType의 value를 gb에 넣음.
				var gb = $("#cntLoadType").val();
				
				if(gb === "phone" || gb === ""){ //핸드폰일때
					gbFamLayerYn = false; //온가족할인 강조 팝업 레이어 팝업 노출 여부
					_is_family_first_list = true;
					_is_family_first_detail = true;
				}
				_cnt.triggerHandler(window.cs_event.view.listView);
				
			});


			// 141030 프리클럽
			_cnt.on('click', _opts.selectPhone.selector, function(event) {// 휴대폰선택
				if ($(".detail_wrap #in_freeclub_fl").val() == "Y"){	//여기에서 핸드폰 조건이 맞을 경우 레이어 띄움
					 event.preventDefault();
					 
					 //in_product_grp_nm
                   $('#freeClubLayer1').show();
                   $('#freeClubLayer1').css('background',  'url("/tws/images/product/popup/modal.png") repeat 0 0');
				}else{
					if(window.userEvents != undefined && window.userEvents.packing != undefined && window.userEvents.packing() != true)	{
						return;	//validation check 실패시 다음화면으로 이동하지 않음
					}
				}
			});

			_b.on('click', "#freeClubLayer1 .function_agree", function(event) {// 프리클럽 레이어 팝업
				$('#freeClubLayer1').hide();
				if(window.userEvents != undefined && window.userEvents.packing != undefined && window.userEvents.packing() != true)	{
					return;	//validation check 실패시 다음화면으로 이동하지 않음
				}
			});	
			// [e] 141030 프리클럽
			                                                                                                   
			// 가입 동의 버튼                                                                                  
			_b.on('click',
				"#premium_pass_info01 .function_agree, #premium_pass_info02 .function_agree, #premium_free_info01 .function_agree, #premium_free_info02 .function_agree", 
				function(event) {
				var _obj = {};
				var _type = "";
				var layer_idx;
				var layer_id;

				if ($('#premium_pass_info01').css("display") != "none"){			//[프리미엄패스 가입가능 안내] 레이어팝업
					layer_idx = 0;
					layer_id	 = '#premium_pass_info01';
				} else if ($('#premium_free_info01').css("display") != "none"){	//[부가서비스(프리클럽+프리미엄패스) 가입가능 안내] 레이어팝업
					layer_idx = 1;
					layer_id	 = '#premium_free_info01';
				}  else if ($('#premium_free_info02').css("display") != "none"){	//[부가서비스(프리클럽) 가입가능 안내] 레이어팝업
					layer_idx = 2;
					layer_id	 = '#premium_free_info02';
				} else if ($('#premium_pass_info02').css("display") != "none"){	//[프리미엄패스 안내] 레이어팝업
					layer_idx = 3;
					layer_id	 = '#premium_pass_info02';
				}			
				
				var _idx	  	= $(layer_id).find('#sel_choice_idx').val();
				var _li = $('div.table_wrap tbody#tbodySubscriptionTable>li').eq(_idx), _tr = $('div.table_wrap tbody#tbodySubscriptionTable>tr').eq(_idx);
				      _obj.li = _li, _obj.tr = _tr, _obj.idx = _idx;
								
			    if (layer_idx === 0){
					if($("#container").find("#tbodySubscriptionTable").length > 0 )	{
						_type	= "list";
						_obj.tr.data().detail.PREMIUMPASS_AGREE	= "PP"; 	//프리미엄패스 동의(리스트)
					} else {
						_obj.PREMIUMPASS_AGREE	= "PP"; 	//프리미엄패스 동의(상세)
					}
			    } else if (layer_idx === 1 || layer_idx === 2){
			    	var checkFl = "N"; 
			    	
			    	if($(layer_id+' #chk_PRCL_agree_'+layer_idx).is(':checked') && $(layer_id+' #chk_PRPS_agree_'+layer_idx).is(':checked')) checkFl = "A"
			    	else if($(layer_id+' #chk_PRCL_agree_'+layer_idx).is(':checked')) checkFl = "PC"
			    	else if($(layer_id+' #chk_PRPS_agree_'+layer_idx).is(':checked')) checkFl = "PP";
			    	
					if (checkFl === "N"){	// 아무것도 체크 하지 않았을 때                                            
						alert('가입을 원하시는 상품에 체크해 주세요.\n가입을 원하지 않을 경우 나중에 하기 버튼을 클릭해 주세요.');
						return;
					} else {	// 체크 했을 때
						if($("#container").find("#tbodySubscriptionTable").length > 0 )	{
							_type	= "list";
							_obj.tr.data().detail.PREMIUMPASS_AGREE	= checkFl; 	//부가서비스 동의(리스트)
						} else {
							_obj.PREMIUMPASS_AGREE	= checkFl; 	//부가서비스 동의(상세)
						}
					}
			    } else if (layer_idx === 3){
					if($("#container").find("#tbodySubscriptionTable").length > 0 )	{
						_type	= "list";
						_obj.tr.data().detail.PREMIUMPASS_DUTY_FL = "";		//다음 step으로 넘기기 위해
					} else {
						_obj.PREMIUMPASS_DUTY_FL	= ""; 	//다음 step으로 넘기기 위해
					}
			    }
			    
				if(window.userEvents != undefined && window.userEvents.packing != undefined && window.userEvents.packing(_type, _obj) != true)	{
					return;	//validation check 실패시 다음화면으로 이동하지 않음
				}			    
			    
			    $(layer_id).hide();
			});                                                                                                
			// 나중에 하기 버튼                                                                                
			_b.on('click',                                                                                     
				"#premium_pass_info01 .function_later, #premium_free_info01 .function_later, #premium_free_info02 .function_later, #premium_free_info02 .d-popup-closeBtn", 
				function(event) {    
				var _obj = {};
				var _type = "";
				var layer_idx;
				var layer_id;
				var checkFl = ""; 

				if ($('#premium_pass_info01').css("display") != "none"){			//[프리미엄패스 가입가능 안내] 레이어팝업
					layer_idx = 0;
					layer_id	 = $('#premium_pass_info01');
				} else if ($('#premium_free_info01').css("display") != "none"){	//[부가서비스(프리클럽+프리미엄패스) 가입가능 안내] 레이어팝업
					layer_idx = 1;
					layer_id	 = $('#premium_free_info01');
				} else if ($('#premium_free_info02').css("display") != "none"){	//[부가서비스(프리클럽) 가입가능 안내] 레이어팝업
					layer_idx = 2;
					layer_id	 = $('#premium_free_info02');
					
					if(event.currentTarget.className === "re-plan-link d-popup-closeBtn" || event.currentTarget.className === "d-popup-closeBtn"){	//요금제 다시 선택하기,닫기
						layer_id.hide();
						return;
					}
				}
				
				var _idx	  	= layer_id.find('#sel_choice_idx').val();
				var _li = $('div.table_wrap tbody#tbodySubscriptionTable>li').eq(_idx), _tr = $('div.table_wrap tbody#tbodySubscriptionTable>tr').eq(_idx);
				      _obj.li = _li, _obj.tr = _tr, _obj.idx = _idx;

				if (layer_idx === 0){
					checkFl = "PL";
				} else if (layer_idx === 1){
					checkFl = "AL";
				} else if (layer_idx === 2){
					checkFl = "CL";
				}
				 
				if($("#container").find("#tbodySubscriptionTable").length > 0 )	{
					_type	= "list";
					_obj.tr.data().detail.PREMIUMPASS_AGREE	= checkFl; 	//나중에하기(리스트)
				} else {
					_obj.PREMIUMPASS_AGREE	= checkFl; 	//나중에하기(상세)
				}
				
				if(window.userEvents != undefined && window.userEvents.packing != undefined && window.userEvents.packing(_type, _obj) != true)	{
					return;	//validation check 실패시 다음화면으로 이동하지 않음
				}
				layer_id.hide();	
			});
			// [e] 141106 부가서비스 가입 안내 레이어 (프리클럽, 프리미엄 패스) 			


			_cnt.on('click', _opts.selectCost.selector, function(event) {// 요금제선택
				if(window.userEvents != undefined && window.userEvents.packing != undefined && window.userEvents.packing() != true)	{
					return;	//validation check 실패시 다음화면으로 이동하지 않음
				}
			});
			_cnt.on('click', _opts.selectTgift.selector, function(event) {// T 기프트선택
				if(window.userEvents != undefined && window.userEvents.packing != undefined && window.userEvents.packing() != true)	{
					return;	//validation check 실패시 다음화면으로 이동하지 않음
				}
			});
			_cnt.on('change', _opts.detail_membershipSelectBox.selector, function(event) {// 번호이동일경우 membership 클래스 적용
				var _val = $(this).val(), _layer = $('div.total_wrap');
				(_val === "20" || _val === "11") ? _layer.addClass('membership') : _layer.removeClass('membership');
			});
			_cnt.on('click', _opts.itemReSelectAnc.selector, function(event) {// 월납부총액에서, 기종/요금제/Tgift 선택시 해당페이지로 이동
				var _ele = $(this).parents('div.d-product');
				var _data = _ele.attr('class').split(' '), _idx;
				for (var i in _data) {
					//150113_재선택IE8오류_v0.1 //if (_data[i].indexOf('product_step0') === 0) {
				     if (_data[i]=="product_step01"||_data[i]=="product_step02"||_data[i]=="product_step03"||_data[i]=="product_step04") {
						_idx = Number(_data[i].split('product_step0')[1]) - 1;
						if(_idx===3){
							var _idx=$(this).index();
						}
						
						if(window.cs_Info.selectSubscriptionType != undefined) window.cs_Info.selectSubscriptionType = "";	//초기화
					}
				}
				
				//휴대폰패킹시 웨어러블디바이스면 무조건 온가족할인 비대상으로 변경한 것을 휴대폰 재선택 시 온가족할인율을 다시 정상적으로 적용시킴
				if(_idx === 0) _W_fmlyDcCd    = _fmlyDcCd;   
				
				if(_idx === 0 || _idx === 1) $("#inParentServiceNum").val("");	//요금제상세의 모상품인증 번호를 초기화
				
				//------------------- 휴대폰/요금제/Tgift 재선택시 레이어팝업 호출이후 "예" 선택시 해당 리스트화면으로 이동
				var _layer = $('#layer_resel0' + Number(_idx + 1)).fadeIn();
				_layer.find('.bt_ok').unbind().bind('click', function(event) {
					_cnt.triggerHandler(window.cs_event.step.process, _idx);
					_layer.hide();
				});
			});
			_cnt.on('click', "#clubT_openBtn", function(event) {// 클럽T 85/100  혜택 자세히보기
				window.userEvents.info.clubtLayer('NA00004428'); // 클럽T 85 : NA00004428
				$('#clubt_info').fadeIn();
			});
			_b.on('click', "#clubt_info .clubTtab li a", function(event) {// 클럽T 85/100  상단탭
				var _idx = $(this).parents('li').index();
				if( _idx == 0 ){
					window.userEvents.info.clubtLayer('NA00004428'); // 클럽T 85 : NA00004428
				}else {
					window.userEvents.info.clubtLayer('NA00004429'); // 클럽T 100 : NA00004429
				}
				$(this).addClass('on').parent().siblings('li').find('a').removeClass('on');
				$('#clubt_info .clubTsubTab li:eq(0)').trigger('click');
			});
			_b.on('click', "#clubt_info .clubTsubTab li", function(event) {// 클럽T 85/100  서브탭
				var _idx = $(this).index();
				$(this).find('>a').addClass('on').parent().siblings('li').find('>a').removeClass('on');
				$('#clubt_info .clubT_guide div').addClass('dHide').eq(_idx).removeClass('dHide').addClass('dShow');

			});
			// 141209_T아웃도어 요금제
			_b.on('click', _opts.processStep2.selector, function(event) {// Tgift 로 이동되는 팝업닫기
				//$('#outdoor_unit_info01').hide();
				//_cnt.triggerHandler(window.cs_event.step.process, 2);
				var _obj = {};
				var _type = "";
				var layer_idx;
				var layer_id;
				var checkFl = ""; 
				
				var _idx	  	= $('#outdoor_unit_info01').find('#sel_choice_idx').val();
				var _li = $('div.table_wrap tbody#tbodySubscriptionTable>li').eq(_idx), _tr = $('div.table_wrap tbody#tbodySubscriptionTable>tr').eq(_idx);
				      _obj.li = _li, _obj.tr = _tr, _obj.idx = _idx;

				if($("#view_fl").val() == "Y"){      
					if($("#container").find("#tbodySubscriptionTable").length > 0 )	{
						_type	= "list";
					}
					
					if(window.userEvents != undefined && window.userEvents.packing != undefined && window.userEvents.packing(_type, _obj) != true)	{
						return;	//validation check 실패시 다음화면으로 이동하지 않음
					}
				
					$("#view_fl").val("N");
					$('#outdoor_unit_info01').hide();
				} else {
					alert("보유하신 스마트폰 기종을 확인 하신 후 가입 동의 해주세요.");
					return;
				}
			});			
		},
		/*-----------------------------------------------------------------------------------------------------------------------------
		 비교하기 단계별 팝업 추가 및 삭제
		 -----------------------------------------------------------------------------------------------------------------------------*/
		listAddRemove : function(target) {
			var _self = this, _opts = this.opts, _i = $('div.d-item-wrap'), _li = $('ul.d-item-list>li'), _ele = '', _chked = target.is(":checked");
			_self.selected = _i.find(_opts.itemUnitInput.selector + ":checked").length;
			//--------------------------------------Remove
			if (this.selected >= 4) {
				target.attr('checked', false);
				_li.find('div.dev_step4').remove();
			} else {
				_li.find('div.dev_step1').add(_li.find('div.dev_step2')).add(_li.find('div.dev_step3')).add(_li.find('div.dev_step4')).remove();
			}
			//--------------------------------------Add
			if (_self.selected == 1) {
				_ele += '<div class="tip_layer dev_step1"><span class="tip"></span><div class="tip_box"><button type="button" class="close" title="비교하기 알림팝업 닫기 및 체크해제"><span class="none">레이어팝업 닫기</span></button>';
				_ele += '<div class="txt_wrap"><span class="txt">상품을 비교하시려면 <strong class="fc_org">2~3개</strong>의<br>기종을 선택해 주세요</span></div><div class="btm"></div></div></div>';
			} else if (_self.selected == 2) {
				_ele += '<div class="tip_layer dev_step2"><span class="tip"></span><div class="tip_box"><button type="button" class="close" title="비교하기 알림팝업 닫기 및 체크해제"><span class="none">레이어팝업 닫기</span></button>';
				_ele += '<div class="txt_wrap"><spawn class="txt">선택하신 <strong class="fc_org">2</strong>개의 기종을<br>비교해 보시겠습니까?</span><span class="txt fc_gray">(3개의 기종을 비교하시려면<br>1개의 기종을 더 선택해 주세요)</span>';
				_ele += '<p class="btn_center"><span class="btn btn_o_ss"><span><a href="#">비교하기</a></span></span></p></div><div class="btm"></div></div></div>';
			} else if (_self.selected == 3) {
				_ele += '<div class="tip_layer dev_step3"><span class="tip"></span><div class="tip_box"><button type="button" class="close" title="비교하기 알림팝업 닫기 및 체크해제"><span class="none">레이어팝업 닫기</span></button><div class="txt_wrap"><span class="txt">선택하신 <strong class="fc_org">3';
				_ele += '</strong>개의 기종을<br>비교해 보시겠습니까?</span>	<p class="btn_center"><span class="btn btn_o_ss"><span><a href="#">비교하기</a></span></span></p></div><div class="btm"></div></div></div>';
			} else if (_self.selected >= 4) {
				_ele += '<div class="tip_layer dev_step4"><span class="tip"></span><div class="tip_box"><button type="button" class="close" title="비교하기 알림팝업 닫기 및 체크해제"><span class="none">레이어팝업 닫기</span></button><div class="txt_wrap">';
				_ele += '<span class="txt"><strong class="fc_org">4</strong>개 이상의 기종을<br>비교하실 수 없습니다</span><span class="txt fc_gray">(상품을 비교하시려면 2~3개의<br>기종을 선택해 주세요)</span></div><div class="btm"></div></div></div>';
			}
			target.parent().append(_ele);
			// 팝업모션
			target.siblings('div.tip_layer').hide().fadeIn('fast');
		},
		/*-----------------------------------------------------------------------------------------------------------------------------
		 카테고리의 선택에 따라 아이템 분류별 filtering
		 -----------------------------------------------------------------------------------------------------------------------------*/
		grouping_filtering : function(ary) {
			var _li = this.opts.itemList.eq(window.cs_Info.step).find('>li');
			var _isAll = (ary.length === 0) ? true : false;
			_li.hide().each(function(idx) {
				var _g = $(this).attr('data-grouping');
				var _c = $(this).attr('data-category');
				if (window.cs_Info.category.groupingCode === _g || window.cs_Info.category.groupingCode === "all") {
					$(this).fadeIn();
					for (var attr in ary) {
						if (_c === ary[attr] || _isAll) {
							$(this).fadeIn();
						} else {
							$(this).hide();
						}
					}
				}
			});
		}
	});
})(fn);

(function($) {
	window.cs_event = {
		sorting : 'sorting',
		filtering : 'filtering',
		layout : {
			normalSize : "layout.normalSize", //-------------------------------------------------------------------------------------------------------------모드타입 일반형
			smallSize : "layout.smallSize" //------------------------------------------------------------------------------------------------------------------모드타입 작게보기
		},
		view : {
			listView : "view.listView", //------------------------------------------------------------------------------------------------------------------------리스트보기
			detailView : "view.detailView" //------------------------------------------------------------------------------------------------------------------상세보기
		},
		step : {
			process : "step.process"//--------------------------------------------------------------------------------------------------------------------------다음STEP 넘어가는형태
		}
	};
	window.cs_Info = {
		//----------  14.08.25
		isAnimate : false,
		step : 0,
		category_idx : 0,
		filtering_idx : [],
		href : '',
		type : "phone",
		list : [],
		detail : [],
		tab : [],
		sizeInfo : ["normal", "normal", "normal", "normal"]
	};
	window.cs = {
		init : function() {
			this.cnt = $('#contents');
			this.item = new custom.Item();
			this.category = new custom.Category();
			this.layout = new custom.Layout();
			this.common = new custom.Common();
			this._setup();
			this._bind();
		},
		_setup : function() {
			window.cs_Info.list = "./data/phoneList.json";
		},
		_bind : function() {
			var _c = this.cnt, _self = this, _top = $('#footer .btn_page_top').hide();
			_c.on(window.cs_event.sorting, function(event, num) {// 인기순/최신순/낮은가격순/높은가격순 클릭시
				if (window.userEvents != undefined && window.userEvents.listing != undefined) {
					window.userEvents.listing();
					//------ 14.08.28
					_self.layout.listSlider("init");
				};
			});
			_c.on(window.cs_event.filtering, function(event, type, filter) {// 카테고리  필터링 적용버튼 클릭시
				if (window.userEvents != undefined && window.userEvents.listing != undefined) {
					window.userEvents.listing();
					_self.layout.resizeHdr($(window).width());
					
					var subScriptionKind		= "";	//요금제 종류
					
					if(window.cs_Info.type === "tablet"){
						if(type.indexOf("함께쓰기") >= 0) subScriptionKind = "tablet_type";	//데이터함께쓰기 요금제 선택시 태블릿요금제 형식으로 노출
					}
					
					//-------------------------------------------------------------------------------------------------- 타블렛 요금제 "데이타 함께쓰기" 일경우 'data_together'  class 추가
					(subScriptionKind === "tablet_type") ? $('#container').addClass('data_together') : $('#container').removeClass('data_together');
					//---------------개발 데이터함께보기 오늘 더이상 보지않기 확인 START				
					var aCookie = document.cookie.split("; ");					
					for (var i=0; i < aCookie.length; i++)  {
						var cPos = aCookie[i].indexOf( "=" );
						var cName = aCookie[i].substring( 0, cPos );						
						if ( cName == "tablet_gift" ) {
							$(".tip_layer").attr("style", "display:none");
						}
					}										
					//---------------개발 데이터함께보기 오늘 더이상 보지않기 확인 END
					if (subScriptionKind === "tablet_type") {
						_self.layout.layoutTypeHdr("normal");
					}
				};
			});
			_c.on(window.cs_event.layout.normalSize, function(event) {// 일반사이즈보기
				_self.layout.layoutTypeHdr("normal");
				_self.layout.listSlider("init");  
				_top.hide();
			});
			_c.on(window.cs_event.layout.smallSize, function(event) {// 작게보기(썸네일로보기) , 요금제에서는 텍스트로 보기
				_self.layout.listSlider("init");
				_self.layout.layoutTypeHdr("small");
				_top.show();
			});
			_c.on(window.cs_event.view.listView, function(event) {// 리스트 페이지 가기									
				var _l = $('div.d-detail-left').eq(window.cs_Info.step), _r = $('div.d-detail-right').eq(window.cs_Info.step);
				var _mount = 1400;
				if (_l.length === 0) {
					_l = $('div.d-detail-left');
				};
				if (_r.length === 0) {
					_r = $('div.d-detail-right');
				};
				//----------------------- 14.08.12.  motion transtion 수정
				_l.stop().animate({
					'left' : -800
				}, 400, function(event) {
					$(this).hide(true);
				});
				_r.stop().animate({
					'right' : -1400
				}, 400, function(event) {
					$(this).hide(true);
					var obj = {};
					obj.type = "list";
					var _w = parseInt($(this).width());
					_self.loadHtml(obj);
					_self.layout.viewTypeHdr(obj.type);
					_self.layout.animateInit();
				});
				_top.hide();
				$('.guide_wrap').hide();
			});
			_c.on(window.cs_event.view.detailView, function(event, data) {// 상세 페이지 가기
				var obj = $.extend({}, data);
				obj.type = "detail";
				var param = data.li.length > 0 ? data.li.data() : data.tr.data(); 
				_self.loadHtml(obj, param);
				_self.layout.viewTypeHdr("detail");			
				_top.show();
			});
			_c.on(window.cs_event.step.process, function(event, num) {// 휴대폰,타블렛 에서  기종선택->요금제->T gift -> 월납부총액  넘어갈때 Step 체크
				window.cs_Info.step = num;
				var obj = {};
				obj.type = "list";
				_self.loadHtml(obj);
				_top.hide();
				_self.layout.viewTypeHdr(obj.type);	   
				//---14.08.26
				//	(num === 3) ? $('body').addClass('proc_step04') : $('body').removeClass('proc_step04');
			});
		},
		loadHtml : function(obj, params, ajax) {// html load 처리
			// 레이어팝업열린부분 닫힘처리
			$('.choice_con').removeClass("more_wrap");
			var _c = this.cnt, _self = this, _step, _type, _url;
			if (ajax != undefined && ajax != null && ajax != '') {//---------------------------------------------------- 3번째 params 가 존재할경우 ajax param를 처리
				_type = (ajax.split('CS_TYPE=')[1]).split('&')[0];
				_step = Number((ajax.split('CS_STEP=')[1]).split('&')[0]);
				_c.removeClass('step01 step02 step03 step04').addClass('step0' + Number(_step + 1) + '');
				_url = ajax;
			} else {
				_step = Number(window.cs_Info.step), _type = obj.type;
				
				_url = window.cs_Info.list;
				
				if(window.cs_Info.packPhoneDetail != undefined){
					if(window.cs_Info.type === "tablet" &&   window.cs_Info.packPhoneDetail.WEARABLE_FL === "Y"){	 //웨어러블 요금제 사용 모델 일경우
						_url = (_type === "list") ? window.cs_Info.chg_list[_step] : window.cs_Info.chg_detail[_step];	//웨어러블요금제 리스트,상세화면을 보여주기위해
						
						if(window.cs_Info.selectSubscriptionType != undefined){
							if(window.cs_Info.selectSubscriptionType === "data_together"){	 //웨어러블 함께쓰기요금제 일경우 모회선 인증 형식으로 노출(cs.item.js의 요금제 리스트에서 table형 보기에서 지정함)
								if(_type === "detail" && _step === 1) $('#container').addClass('data_together'); 
							} else {
								$('#container').removeClass('data_together');
							}
						}
						
						/*if(_type === "detail" && window.cs_Info.packPhoneDetail.WEARABLE_DEVICE ==="Y" ){	//웨어러블 요금제 사용 모델 + 웨어러블 기기 일 경우
							$('#container').addClass('data_together');							//웨어러블 함께쓰기요금제 일경우 모회선 인증 형식으로 노출(요금제리스트 일반사이즈로 들어올 경우)
							window.cs_Info.selectSubscriptionType	= "data_together";	//요금제상세 체크포인트 값 예외처리 위해 Setting(twd_bind_phone.js_subscriptionDetail_checkPointEvent)
						}*/						
					}
				}
			}
			
			/*
			$.when($.ajax({
				url : _url, //+ (_url.indexOf("?") > -1 ? "&" : "?") + encodeURIComponent(window.cs_Info.href),
				data : params,
				dataType : 'json',
				cache : false
			})).done(function(data) {// ---------------------------------------------------------------------------------- data load complete
				*/
				var data = mainData;


				var _p = _c.find('.d-product').eq(_step).find('.d-html-load');
				//var _p = $("#asdf");//_c.find('.d-product').eq(_step).find("#asdf");
				//_p.children().remove();
				//data = jQuery.parseJSON(data);

				var _l = _p.find('div.d-detail-left'), _r = _p.find('div.d-detail-right'), _mount = 800;
				
				if( window.callback != null && window.callback != undefined ){
			        window.callback(data, _type, _step, params);
				}
				
				// url 로 상세화면 가기위해 필요
				_c.removeClass('step01 step02 step03 step04').addClass('step0' + Number(_step + 1) + '');
				if (_type === "list") {					
 				    _c.removeClass('step01 step02 step03 step04').addClass('step0' + Number(_step + 1) + '');
					_self.layout.layoutTypeHdr("set");
					_p.animate({
						'margin-top' : 0
					}, 0, function(event) {
						//	_p.fadeIn('fast');
						_c.removeClass('step01 step02 step03 step04').addClass('step0' + Number(_step + 1) + '');
						_self.layout.scrollHdr(0);
						_self.layout.resizeHdr($(window).width());
						
						//--------------------------------------------------------------- 14.09.22  현업요청 썸네일보기 수정시작
						if(_step!=3) _self.layout.layoutTypeHdr("small");
						//--------------------------------------------------------------- 14.09.22  현업요청 썸네일보기 수정끝
						
	
						//--------------------------------------------------------- 상단옵션바 Step 부분 모션적용  14.08.13
						var _h3 = _p.siblings('.step_tit_bar').find('h3').css('margin-left', -263 + 'px').hide();
						_h3.animate({
							'margin-left' : 0 + 'px',
							'opacity' : "show"
						}, 400);
						//------14.08.26
						(_step === 3) ? $('body').addClass('proc_step04') : $('body').removeClass('proc_step04');
					});
					if(_l)	_l.hide();
					if(_r)	_r.hide();
					//--------------------------------------------------------- 상단옵션바 전체 unchecked  14.08.13
					window.cs_Info.filtering_idx = null;
					//$('.maker_list li').find('input').prop('checked', false);	//개발삭제
					//------ 상단 d-view-type 체크
					var _vNum = (_p.find('.product_area').hasClass('small_list')) ? 1 : 0;
					_p.find('div.d-view-type button').removeClass('on').eq(_vNum).addClass('on');
				}
				/*
			}).fail(function(data, x, e) {
				if (window.console != undefined) {	// parsing 에러가 나면 json string에 날짜형식(REG_DATE)이 들어가 있지 않은지 확인! JsonObject 버그인듯
					console.log('-- fail');
				}
			});*/
		},
		/*
		 *  ajax URL 형태 1개로 하여 변수1&변수2 나누어서  url 로드할수있도록 처리
		 *
		 */
		loadAjax : function(url) {
			this.loadHtml(null, null, url);
			window.cs_Info.step = Number((url.split('CS_STEP=')[1]).split('&')[0]);
			var _type = (url.split('CS_TYPE=')[1]).split('&')[0];
			this.layout.viewTypeHdr(_type);
		}
	};
	$(document).ready(function() {

		window.cs.init();
		
		$('#contents').triggerHandler(window.cs_event.step.process, 0);
		
	});
})($);

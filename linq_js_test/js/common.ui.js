(function(Class) {
	Class.define("common.Ui", {
		init : function(obj) {
			this.config = {
				body : $('body'),
				cnt : $('#contents'),
				topBtn : $('#footer .btn_page_top>a'),

				popup_cnt : $('.d-popup-cnt'),
				popup_layer : $('.d-popup-layer'),
				popup_openBtn : $('.d-popup-openBtn'),
				popup_closeBtn : $('.d-popup-closeBtn'),
				popup_nextBtn : $('.d-popup-nextBtn'),
				popup_chkpoint : $('#chkpoint'),

				selectBoxCnt : $('.d-selectBox-cnt'),
				selectBoxLayer : $('.d-selectBox-layer'),
				selectBoxLayerAnc : $('.d-selectBox-layer li a'),
				selectBoxAnc : $('.d-selectBox-anchor'),

				accordionCnt : $('.d-accordion-cnt'),
				accordionAnc : $('.d-accordion-anc'),
				accordionView : $('.d-accordion-view'),

				adminGnbCnt : $('.d-admin-gnb'),
				adminGnbAnc : $('.d-admin-gnb>li'),

				adminGnbSelect : 0,

				colorSelectCnt : $('.color_wrap'),
				colorSelectAnc : $('.color_wrap>a'),
				colorSelectUnitAnc : $('.color_wrap .color_list ul li'),

				printBtn : $('.d-printBtn'),

				tabCnt : $('.d-tabCnt'),
				tabCntAnc : $('.d-tabCnt h3>a'),

				sliderMenuCnt : $('.d-sliderMenuCnt'),
				sliderMenuBtn : $('.d-sliderMenuCnt .tmenu_page button'),

				selectBoxPopupAnc : $('.tit_wrap a.box_selected'),
				selectBoxPopupUnit : $('.tit_wrap .tit_layer ul>li a'),
				//-------------------14.08.31     기획전 링크 영역수정
				selectBoxPopupCloseAnc : $('.tit_wrap .tit_layer .link_close'),

				swapAmount : $('.d-swap-amount'),
				swapAmountLayer1 : $('.d-swap-amount-layer1'),
				swapAmountLayer2 : $('.d-swap-amount-layer2'),
				swapAmountOpenBtn : $('.d-swap-amount-openBtn'),
				swapAmountExeBtn : $('.d-swap-amount-exeBtn'),

				detailBtnAnc : $('.btn_detailed'),

				footerOpenAnc : $('#footer .btn_foot_open a'),
				footerCloseAnc : $('#footer .btn_foot_close a'),
				familyOpenAnc : $('#footer .bt_off button'),
				familyCloseAnc : $('#footer .bt_on button'),

				smsConfirmAnc : $('.d-smsConfirmAnc'),

				xperiaAnc : $('.xperea_info a'),

				liveChatBtn : $('#liveCntActiveBtn'),
				liveChatCloseBtn : $('#liveChatCnt_Close'),

				goodSwapBtn : $('.banner_wrap>a')
			};
			this.opts = this.opts || {}, this.opts = $.extend(null, this.config, obj);
			this._bind();
		},
		_bind : function() {
			var _self = this, _time, _opts = this.opts, _cnt = _opts.cnt, _b = _opts.body;
			_b.on('click', _opts.footerOpenAnc.selector, function(event) {// footer Layer Open
				$('#footer').addClass('footer_open').removeClass('footer_close').hide().fadeIn('fast');
			});
			_b.on('click', _opts.footerCloseAnc.selector, function(event) {// footer Layer Close
				event.preventDefault();
				$('#footer').addClass('footer_close').removeClass('footer_open');
				_opts.familyCloseAnc.trigger('click');
			});
			_b.on('click', _opts.familyOpenAnc.selector, function(event) {// Family Site  Open
				event.preventDefault();
				$(this).parents().siblings('.family_list').addClass('on');
			});
			_b.on('click', _opts.familyCloseAnc.selector, function(event) {// Family Site Close
				event.preventDefault();
				$(this).parents('.family_list').removeClass('on');
			});
			_b.on('click', _opts.selectBoxAnc.selector + "," + _opts.selectBoxLayerAnc.selector, function(event) {//  별점 & 평점 SelectBox
				//-----14.08.27
				event.preventDefault();
				var _layer = $(this).parents(_opts.selectBoxCnt.selector).find(_opts.selectBoxLayer.selector), _selectCls = $(this).attr('class');
				_opts.selectBoxAnc.removeAttr('class').addClass(_selectCls + " " + _opts.selectBoxAnc.selector.replace('.', ' '));
				(_layer.is(':visible')) ? _layer.hide() : _layer.fadeIn();
			});
			_b.on('click', _opts.accordionAnc.selector, function(event) {//	아코디언 펼침 & 접힘
				var _isOpen = $(this).hasClass('selected');
				var _v = $(this).parent().next(_opts.accordionView.selector);
				(_isOpen) ? $(this).removeClass('selected') : $(this).addClass('selected');
				(_isOpen) ? _v.hide() : _v.fadeIn();
			});
			_b.on('click', _opts.popup_openBtn.selector, function(event) {// Popup Layer Open Button
				event.preventDefault();
				var _p = $(this).parents(_opts.popup_cnt.selector);
				var _layer = _p.find(_opts.popup_layer.selector);
				if (_p.length == 0) {
					if (_opts.popup_layer.length == 1) {
						_layer = _opts.popup_layer;
					}
				} else {
					_layer = _p.find(_opts.popup_layer.selector);
				}
				_layer.fadeIn('fast');

			});
			_b.on('click', _opts.popup_closeBtn.selector, function(event) {// Popup Layer Close Button
				event.preventDefault();
				var _layer = $(this).parents(_opts.popup_cnt.selector).find(_opts.popup_layer.selector);
				_layer.fadeOut('fast');
				(_opts.popup_cnt.find(_opts.popup_layer.length == 0)) ? _opts.popup_layer.fadeOut('fast') : _opts.popup_cnt.find(_opts.popup_layer).fadeOut('fast');
				//_opts.popup_chkpoint.prop('checked', false);
				$('#chkpoint').prop('checked', false);
			});
			_b.on('mouseleave', _opts.popup_layer.selector, function(event) {// Popup Layer mouseleave Event
				var _isOpen = $(this).hasClass('d-mouseleave');
				if (_isOpen) {
					$(this).fadeOut('fast');
				}
			});
			_b.on('mouseleave', _opts.colorSelectCnt.selector, function(event) {// 상품컬러선택
				//-----14.08.27
				event.preventDefault();
				var _layer = $(this).find('.color_list');
				_layer.fadeOut('fast');
				_layer.siblings('a').removeClass('on');
			});
			_b.on('click', _opts.colorSelectAnc.selector, function(event) {// 상품컬러선택
				//-----14.08.27
				event.preventDefault();
				var _layer = $(this).parents(_opts.colorSelectCnt.selector).find('.color_list');
				var _isV = _layer.is(':visible');
				(_isV) ? _layer.fadeOut('fast') : _layer.fadeIn('fast');
				(_isV) ? $(this).removeClass('on') : $(this).addClass('on');
			});
			_b.on('click', _opts.colorSelectUnitAnc.selector, function(event) {// 색상바 선택시 anchor 영역에 선택색상표시
				//-----14.08.27
				event.preventDefault();
				var _idx = $(this).index();
				var _rgb = $(this).find('span.bg_color').css('background-color');
				//	var _rgb = $(this).find('span.bg_color').css('background');
				$(this).parents('.color_wrap').find('>a').removeClass('on').find('span.bg_color').css('background', _rgb);
				$(this).parents('.color_list').fadeOut();
			});
			// admin 활성화
			_opts.adminGnbSelect = $(_opts.adminGnbAnc.selector + ".on").index();
			_b.on('mouseenter mouseleave', _opts.adminGnbAnc.selector, function(event) {//	아코디언 펼침 & 접힘
				if (event.type === 'mouseenter') {
					$(this).addClass('on hover').siblings().removeClass('on hover');
				} else if (event.type === 'mouseleave') {
					$(this).removeClass('on hover');
					_opts.adminGnbAnc.eq(_opts.adminGnbSelect).addClass('on hover');
				}
			});
			_b.on('click', _opts.topBtn.selector, function(event) {// footer영역에 있는 Top버튼
				event.preventDefault();
				$('html, body').stop().animate({
					scrollTop : 0
				}, 600, 'easeInOutExpo');
				return false;
			});
			_b.on('click', _opts.printBtn.selector, function(event) {// 프린트실행detailBtnAnc
				event.preventDefault();
				window.print();
			});
			_b.on('click', _opts.swapAmountOpenBtn.selector, function(event) {// 주문정보입력-수량변경같은 toggle형태
				$(this).parents(_opts.swapAmountLayer1.selector).hide();
				$(this).parents(_opts.swapAmount.selector).find(_opts.swapAmountLayer2).fadeIn();
			});
			_b.on('click', _opts.swapAmountExeBtn.selector, function(event) {// 주문정보입력-수량변경같은 toggle형태
				var _layer2 = $(this).parents(_opts.swapAmountLayer2.selector).hide();
				var _cnt = $(this).parents(_opts.swapAmount.selector);
				_cnt.find(_opts.swapAmountLayer1).fadeIn();
				var _select = _layer2.find('select option:selected').attr('selected', true);
				_select.siblings().attr('selected', false);
				_cnt.find(_opts.swapAmountLayer1.selector).find('.accessory_num').text(_select.text());
			});
			_b.on('click', _opts.tabCntAnc.selector, function(event) {// 마이페이지 탭이동
				event.preventDefault();
				$(this).parents('h3').next('div.mywish_list').show().siblings('div.mywish_list').hide().siblings('h3').find('a').removeClass('on');
				$(this).addClass('on');
			});

			_b.on('click', _opts.selectBoxPopupAnc.selector, function(event) {//기획전 selecBox 팝업열기
				event.preventDefault();
				$(this).next().fadeIn('fast');
			});
			_b.on('click', _opts.selectBoxPopupCloseAnc.selector, function(event) {//기획전 selecBox 팝업닫기
				event.preventDefault();
				$(this).parents('.tit_layer').hide();
			});
			_b.on('click', _opts.selectBoxPopupUnit.selector, function(event) {// 기획전 selectBox 아이템선택
				event.preventDefault();
				var _txt = $(this).text();
				$(this).parents('.tit_layer').siblings('a').text(_txt);
				$(this).parents('.tit_layer').hide();
				$(this).addClass('on').parents('li').siblings('li').find('a').removeClass('on');
			});
			// 기획전 좌우 슬라이드메뉴
			var _slr_li = _opts.sliderMenuCnt.find('li'), _slrIndex = 0, _slr_max = Math.ceil(_slr_li.length / 4) - 1;
			(_slr_li.length > 4) ? _opts.sliderMenuBtn.show() : _opts.sliderMenuBtn.hide();
			_b.on('click', _opts.sliderMenuBtn.selector, function(event) {// 기획전 상단메뉴 이전 다음 Binding
				var _type = ($(this).hasClass('btn_prev')) ? "prev" : "next";
				if (_type === "prev") {
					_slrIndex = (_slrIndex <= 0) ? 0 : --_slrIndex;
				} else if (_type === "next") {
					_slrIndex = (_slrIndex >= _slr_max) ? _slr_max : ++_slrIndex;
				}
				_slr_li.each(function(idx) {
					if ((_slrIndex * 4 <= idx) && (idx <= _slrIndex * 4 + 3)) {
						$(this).show();
					} else {
						$(this).hide();
					}
				});
			});
			_b.on('click', _opts.detailBtnAnc.selector, function(event) {// 기획전 팝업 레이어
				//------------------------- 14.08.30
				event.preventDefault();
				//	var _idx = $(this).parents('.flow_wrap').index();
				var _data = [$('#layer_phone'), $('#layer_cost'), $('#layer_tgift')];
				var _idx = $(this).parent().parent().index();
				var _top = parseInt($(this).offset().top);
				_data[_idx].fadeIn();
				var _layer = _data[_idx].find('.ly_cont');
				var _scrollTop = $(window).scrollTop();
				_layer.css('top', _scrollTop);
			});
			//------------------------------------------------------------------------------------------------------------------------ 월납총액,구매프로세스 마지막 휴대폰할인/요금제할인
			_b.on('click', _opts.popup_nextBtn.selector, function(event) {// 구매시 안내사항 월납부총액 "확인버튼"
				event.preventDefault();
				var _cnt = $(this).parents(_opts.popup_cnt.selector), _layer = _cnt.find(_opts.popup_layer.selector);
				_layer.fadeOut('fast');
				_cnt.find('.txt_normal').hide().siblings('span').show().siblings('input').attr('checked', 'checked');
				//_opts.popup_chkpoint.prop('checked', true);
				$('#chkpoint').prop('checked', true);
			});
			_b.on('change', _opts.popup_chkpoint.selector, function(event) {// 구매시 안내사항 월납부총액 "확인버튼"
				event.preventDefault();
				var _isChk = $(this).prop("checked"), _cnt = $(this).parents(_opts.popup_cnt.selector), _layer = _cnt.find(_opts.popup_layer.selector);
				if (_isChk) {// checked 되면 팝업레이어
					_layer.fadeIn();
				} else {// checked 해제시 기본값으로
					_layer.fadeOut('fast');
					$(this).prop('checked', false).siblings('.txt_normal').show().siblings('.txt_on').hide();
				}
			});
			_cnt.on('click', '#dis_btn_coupon span a', function(event) {// 쿠폰할인
				// window popup 으로 처리
			});
			_cnt.on('click', '#dis_btn_okcash span a', function(event) {// OK캐쉬백할인
				var addurl = "https://www.tworlddirect.com/handler/CustomBoard-ForwardPage?callbackFunctionName=ocbCallback&forwardPage=jsp/common/OCBCommon.jsp";
				var pop = window.open(addurl, "popDetail", "scrollbars=yes, resize=yes, width=500,height=430");
			});
			/*_cnt.on('click', '#dis_btn_tfamily span a', function(event) {// 141117_T가족포인트_v0.1
				$('#layer_tfamily').show();
			});*/
			_cnt.on('click', '#dis_btn_family span a', function(event) {// 착한가족할인
				$('#layer_family').show();
			});
			_cnt.on('click', '#dis_btn_card span a', function(event) {// 제휴카드할인
				$('#layer_card').show();
			});
			_cnt.on('change', '#dis_btn_coupon input', function(event) {// checkbox 쿠폰할인
				if ($(this).is(':checked')) {
					$('#layer_coupon1').show();
				}
			});
			_cnt.on('change', '#dis_btn_okcash input', function(event) {// checkbox OK캐쉬백할인
				if(this.checked == false)	{	//체크해제시
					window.ocbCallback(0);	// 0원으로 처리
				} else {
					_cnt.find('#dis_btn_okcash span a').click();
				}
			});
			/*_cnt.on('change', '#dis_btn_tfamily input', function(event) {// checkbox 141117_T가족포인트_v0.1
				if ($(this).is(':checked')) {
					$('#layer_tfamily').show();
				}
			});*/
			_cnt.on('change', '#dis_btn_family input', function(event) {// checkbox 착한가족할인
				if ($(this).is(':checked')) {
					$('#layer_family').show();
				}
			});
			_cnt.on('change', '#dis_btn_card input', function(event) {// checkbox 제휴카드할인
				if ($(this).is(':checked')) {
					$('#layer_card').show();
				}
			});
			_b.on('click', '.inner .btn_applying', function(event) {// 팝업 레이어 적용하기
				var _layer = $(this).parents('div.d-popup-cnt'), _id = _layer.attr('id');
				if (_id === "layer_coupon1") {
					$('#dis_coupon').att('checked', true);
				} else if (_id === "layer_okcash") {
					$('#dis_okcash').prop('checked', true);
				} else if (_id === "layer_card") {
					$('#dis_asscard').prop('checked', true);
				}
				_layer.fadeOut('fast');
			});
			_b.on('click', '.inner .btn_cpcancel, .d-popup-closeBtn', function(event) {// 팝업 레이어 취소하기
				var _layer = $(this).parents('div.d-popup-cnt'), _id = _layer.attr('id');
				if (_id == "layer_coupon1") {
					$('#dis_coupon').prop('checked', false);
				} else if (_id == "layer_okcash") {
					$('#dis_okcash').prop('checked', false);
				} else if (_id == "layer_card") {
					$('#dis_asscard').prop('checked', false);
				} else if (_id == "sel_24month") {
					$("#sel_24month").hide();
					$("#selectSubcommTerm option[value='0']").prop("selected", true);
					$("#selectSubcommTerm option[value='24']").prop("selected", false);
					$("#select_subcomm_term option[value='0']").prop("selected", true);
					$("#select_subcomm_term option[value='24']").prop("selected", false);
					gbFamLayerYn = false;
					$("#selectSubcommTerm").change();
					$("#select_subcomm_term").change();
					$("#sel_family").hide();
					gbFamLayerYn = true;
				} else if (_id == "sel_family") {
					$("#sel_family").hide();
					$("#selectSubcommTerm option[value='24']").prop("selected", true);
					$("#selectSubcommTerm option[value='0']").prop("selected", false);
					$("#select_subcomm_term option[value='24']").prop("selected", true);
					$("#select_subcomm_term option[value='0']").prop("selected", false);
					gbFamLayerYn = false;
					$("#selectSubcommTerm").change();
					$("#select_subcomm_term").change();
					$("#sel_24month").hide();
					gbFamLayerYn = true;
				}
				$(this).parents('.d-popup-layer').fadeOut('fast');
			});
			_b.on('click', _opts.smsConfirmAnc.selector, function(event) {// SMS 인증팝업
				$('#sms_confirm_cnt').fadeIn('fast');
			});
			_b.on('click', _opts.xperiaAnc.selector, function(event) {// Xperia 구매안내사항 팝업
				$('#xperiaZ').fadeIn('fast');
			});
			_b.on('click', _opts.liveChatBtn.selector, function(event) {// 라이브챗 실행버튼 실행및 모션실행
				var _t = $('#liveChatCnt'), _oldY = 40, _newY = 50;
				_t.css('top', _oldY + '%').fadeIn('fast');
				var _motion = function() {
					_t.animate({
						'top' : _newY + "%"
					}, 1000, 'easeOutBounce', function() {
						$(this).animate({
							'top' : _oldY + "%"
						}, 700, 'easeInOutBack', function(event) {
							if (_motion != undefined) {
								window.liveChatId = setTimeout(_motion, 4000);
							}
						});
					});
				};
				_motion();
			});
			_b.on('click', _opts.liveChatCloseBtn.selector, function(event) {// 라이브챗 실행버튼 닫기
				$('#liveChatCnt').stop().fadeOut('fast');
				clearInterval(window.liveChatId);
			});
			_b.on('click', _opts.goodSwapBtn.selector, function(event) {// 착한 가족할인 버튼 레이어실행
				$('#costDetail').fadeIn();
			});

			_b.on('click', '#purchaseBtn', function(event) {//-------14.08.31  기획전 데이터 함께쓰기 회선인증
				$('#data_confirm').fadeIn();
			});
			//--------------------------------------------------------14.09.23 상담사전용메뉴관련
			_b.on('mouseenter mouseleave', 'ul.gnb_navi li.etc', function(event) {
				var _popup = $('ul.gnb_navi ul.counsellor_navi');
				if (event.type === 'mouseenter') {
					_popup.fadeIn('fast');
				} else if (event.type === 'mouseleave') {
					_popup.hide();
				}
			});
		},
		/*---------------------------------------------------------------------------------------------------------------------
		 function
		 --------------------------------------------------------------------------------------------------------------------*/
		func : function() {

		}
	});
	$(document).ready(function() {
		var _ui = new common.Ui();
	});
})(fn);

/* 150107 T가족포인트 사용_v0.1 */
$(document).ready(function(){
	$('#priority').on({
		'mouseenter' : function(){$('#cont_priority').show()},
		'mouseleave' : function(){$('#cont_priority').hide()}
	});
});
/* //150107 T가족포인트 사용_v0.1 */

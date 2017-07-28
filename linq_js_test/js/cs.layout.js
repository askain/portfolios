(function(Class) {
	Class.define("custom.Layout", {
		init : function(obj) {
			this.config = {
				cnt : $('#contents'),
				pagerCnt : $('div.page_wrap'),
				paging : $('div.paging'),
				itemCnt : $('div.d-item-wrap'),
				itemPrevNextBtn : $('button.d-prev,button.d-next'),
				itemCount : $('.d-list-count'),
				item_detail_left : $('div.d-detail-left'),
				item_detail_right : $('div.d-detail-right'),

				topBtn : $('#topBtn')
			};
			this.vars = {
				nowPage : 0
			};
			//this.isAnimate = false;
			//----------  14.08.25
			window.cs_Info.isAnimate = false;
			this.opts = this.opts || {};
			this.opts = $.extend(null, this.config, obj);
			this._bind();
			//		this.listSlider(null);
		},
		_bind : function() {
			var _self = this, _opts = this.opts, _cnt = _opts.cnt, _setTime, _i = $('.d-item-wrap');
			_cnt.on('mousewheel', _i.find('div.list_wrap').selector, function(event, delta) {//MouseWheel Event
				//-----------------------------------------------------------------------------  14.08.21  mouseWheel 리스트 갯수에 따라 작동
				var _i = $(this).parents('.d-item-wrap');
				if (!_i.hasClass('small_list')) {// 크게보기일때 적용됨.
					var _total = $('#contents').find('div.d-item-wrap').find('li:visible').length;
					var _wrap = $('div.product_area.d-item-wrap');
					if (_wrap.hasClass('w1920') || _wrap.hasClass('w1680')) {
						if (_total <= 6) {
							return true;
						}
					} else if (_wrap.hasClass('w1440') || _wrap.hasClass('wdefault')) {
						if (_total <= 4) {

							return true;
						}
					}
					if (delta === 1) {
						_self.listSlider("prev");
					} else if (delta === -1) {
						_self.listSlider("next");
					}
					return false;
				}
				//---- 14.08.21 수정완료
			});
			_cnt.on('click', _opts.itemPrevNextBtn.selector, function(event) {
				var _mode = ($(this).hasClass('d-prev')) ? "prev" : "next";
				_self.listSlider(_mode);
			});
			$(window).bind('scroll resize', function(event) {// window event handler
				var _evtHdr = function() {
					if (event.type === 'resize') {
						var _w = parseInt($(this).width());
						var _h = parseInt($(this).height());
						_self.resizeHdr(_w, _h);
					} else if (event.type === 'scroll') {
						var _top = parseInt($(this).scrollTop());
						_self.scrollHdr(_top);
					}
				};
				clearTimeout(_setTime);
				_setTime = setTimeout(_evtHdr, 200);
			}).trigger('resize');
		},
		// animation  초기화
		animateInit : function() {
			var _self = this, _setTime;
			_self.isAnimate = false;
			_setTime = setTimeout(function() {
				clearTimeout(_setTime);
				_self.isAnimate = false;
				$(window).trigger('scroll');
				_self.listSlider("init");
			}, 600);

		},
		/*===========================================================
		 리스트 prev , next Slider
		 ===========================================================*/
		listSlider : function(mode) {

			//----------  14.12.03 수정2

			if ($('#contents').find('div.d-item-wrap ul').is(':animated')) {
				return false;
			};

			var _i = $('#contents').find('div.d-item-wrap');
			//	_i = (_i.length > 1) ? _i.eq(_i.length - 1) : _i;
			_i = (_i.length > 1) ? _i.eq(0) : _i;
			
			//-------------------14.08.30
			var _t = _i.find("ul"), _left = _t.css('left'), _count = _i.find(this.opts.itemCount), _vars = this.vars, _opts = this.opts, _self = this, _mount = 263;
			var _c1 = (mode === "next") ? -1 : (mode === "prev") ? 1 : null, _c2 = (_i.hasClass('w1680') || _i.hasClass('w1920')) ? 6 : 4;
			var _left = (_left === "auto") ? 0 : parseInt(_left), _calc = _left + (_mount * _c1 * _c2);
			//-------------------------------------------------------count check

			var _totalTxt = _i.find('li:visible').length, _totalPageTxt = Math.ceil(_totalTxt / _c2), _nowPageTxt = Math.floor(_c2 / _totalTxt);
			var _prevBtn = _opts.cnt.find('.d-prev'), _nextBtn = _opts.cnt.find('.d-next');

			if (mode === "prev") {
				(_vars.nowPage <= 0) ? 0 : --_vars.nowPage;
			} else if (mode === "next") {
				(_vars.nowPage >= _totalPageTxt - 1) ? _totalPageTxt - 1 : ++_vars.nowPage;
			} else if (mode == "init") {
				_vars.nowPage = 0;
				_calc = 0;
			} else if (mode == "set") {
				_calc = -Number(_vars.nowPage * _mount * _c2);
			}
			_calc = (_calc <= -(_totalTxt - _c2) * _mount) ? -(_totalTxt - _c2) * _mount : _calc;
			_calc = (_calc >= 0) ? 0 : _calc;

			_t.stop().animate({
				'left' : _calc
			}, 750, 'easeInOutQuint', function() {
				//	console.log(' --------   motion finished !! ');
			});
			if (_totalTxt <= _c2) {
				_prevBtn.hide();
				_nextBtn.hide();
			} else if (_vars.nowPage <= 0) {
				_prevBtn.hide();
				_nextBtn.show();
			} else if (_vars.nowPage >= _totalPageTxt - 1) {
				_prevBtn.show();
				_nextBtn.hide();
			} else {
				_prevBtn.show();
				_nextBtn.show();
			}
			var _pager = $('.thumb_page');
			_pager.children().remove();	

			for (var i = 0; i < _totalPageTxt; i++) {
				if (i == this.vars.nowPage) {
					_pager.append('<span class="icon_num on"><a href="#"><span class="none">액세서리 리스트 <span>1</span>페이지</span></a></span>');
				} else {
					_pager.append('<span class="icon_num"><a href="#"><span class="none">액세서리 리스트 <span>1</span>페이지</span></a></span>');
				}
			}
			_pager.find('span').unbind().bind('click', function(event) {
				if (_self.isAnimate) {
					return false;
				}
				var _idx = $(this).index();
				_vars.nowPage = _idx;
				_self.listSlider("set");
			});
			//this.isAnimate = true;
			//----------  14.08.25 

			/*
			 _count.find('.d-totalList').text(_totalTxt);
			 _count.find('.d-nowPage').text(this.vars.nowPage + 1);
			 _count.find('.d-totalPage').text(_totalPageTxt);
			 */
		},
		/*===========================================================
		 카테고리 보임/감춤 Toggle function
		 ===========================================================*/
		layerVisible : function(mode) {
			var _mode = Boolean(mode);
			var _opts = this.opts;
			(_mode) ? _opts.category_layer.stop().fadeIn() : _opts.category_layer.hide();
			(_mode) ? _opts.category_toggleBtn.addClass('on') : _opts.category_toggleBtn.removeClass('on');
		},
		/*===========================================================
		 Resize Event Handler  class="w1440", class="w1680", class="w1920"	, 스크롤이 생길경우를 대비해 -20 처리해둠
		 ===========================================================*/
		resizeHdr : function(width) {
			var _i = $('div.d-item-wrap');
			var _width = (width >= 1900) ? 1920 : (width >= 1660) ? 1680 : (width >= 1420) ? 1440 : 'default';
			_i.removeClass('w1920 w1680 w1440 wdefault').addClass('w' + _width);
			this.listSlider("init");
		},
		/*===========================================================
		 Scroll Event Handler
		 ===========================================================*/
		scrollHdr : function(top) {
			//-------------------14.08.31
			var _self = this, _opts = this.opts, _cnt = _opts.cnt, _setTime, _left = $('div.d-detail-left'), _leftClose = $('div.btn_close_left'), _c1, _c2;
			//	var _self = this, _opts = this.opts, _cnt = _opts.cnt, _setTime, _left = $('div.d-detail-left').eq(window.cs_Info.step), _leftClose = $('div.btn_close_left').eq(window.cs_Info.step), _c1, _c2;
			if (window.cs_Info.type === "tablet") {
				_c1 = 70;
				_c2 = 90;
			} else if (window.cs_Info.type === "accessory") {
				_c1 = 160;
				_c2 = 0;
			} else {
				_c1 = 50;
				_c2 = 110;
			}
			// 휴대폰/ 요금제/ tgift  순서
			var _limit = [_c1, 155, 155];
			var _calcLeft = (top <= _limit[window.cs_Info.step]) ? _limit[window.cs_Info.step] : top;
			var _limit_close = [_c2 + _calcLeft, _calcLeft, _calcLeft];
			var _calcClose = (top <= _limit_close[window.cs_Info.step]) ? _limit_close[window.cs_Info.step] : top;
			//------------ 썸네일그룹
			/* 닫기 200px
			 휴대폰 기종/ 태블릿 기종/ 요금제,tgift,액세서리  160/200
			 */
			_left.stop().animate({
				top : _calcLeft
			}, 300);
			_leftClose.stop().animate({
				top : _calcClose
			}, 300);
//------------------ IE8  이하버젼일때 scroll 클래스 적용 안되게
			var agent = window.navigator.userAgent;
			var result = "unknown";
			if (agent == null) {
			} else if (agent.indexOf("MSIE 6.0") > -1) {
				result = "ie6";
			} else if (agent.indexOf("MSIE 7.0") > -1) {
				result = "ie7";
			} else if (agent.indexOf("MSIE 8.0") > -1) {
				result = "ie8";
			} else {
				(top >= 140) ? _cnt.addClass('scroll') : _cnt.removeClass('scroll');
			}
		},
		/*===========================================================
		 layoutType
		 ===========================================================*/
		layoutTypeHdr : function(mode) {
			var _i = $('div.d-item-wrap');
		//	(mode === "set") ? mode = window.cs_Info.sizeInfo[window.cs_Info.step] : _i.hide().delay(100).fadeIn();
			//-- 개발 리스트 진입시 상품(요금제)이 없을 때도 요금제 div를 표시해버리는 버그 수정 START
			//(mode === "set") ? mode = window.cs_Info.sizeInfo[window.cs_Info.step] : _i.show();
			if(mode === "set")	{
				mode = window.cs_Info.sizeInfo[window.cs_Info.step] 
			} else {
				if(_i.is(":visible") == true)	{
					_i.show();
				}
			}
			//-- 개발 리스트 진입시 상품(요금제)이 없을 때도 요금제 div를 표시해버리는 버그 수정 START
			(mode === "normal") ? _i.removeClass('small_list') : _i.addClass('small_list');
			var _idx = (_i.hasClass('small_list')) ? 1 : 0;
			window.cs_Info.sizeInfo[window.cs_Info.step] = mode;
			//카테고리 & 제조사 팝업레이어 닫기
			$('div.btn_wrap a.d-cancel').trigger('click');
			$('.choice_con').removeClass("more_wrap");
			// 레이어팝업열린부분 닫힘처리
		},
		/*===========================================================
		 viewType
		 ===========================================================*/
		viewTypeHdr : function(mode) {
			var _step = $('div.product_step0' + Number(Number(window.cs_Info.step) + 1) + '');
				//-------------------------- transtion 튜닝 14.08.13
			var _time = (mode === "list" && _step.hasClass('detail_choice')) ? 0 : 0;
			var _value = (mode === "list") ? -1000 : 0;
			var _valueOrigin = (mode === "list") ? 0 : -3000;
			var _left = this.opts.item_detail_left.eq(window.cs_Info.step);
			var _right = this.opts.item_detail_right.eq(window.cs_Info.step);
			_left.animate({
				'left' : (mode === "list") ? -200 : 0,
				'opacity' : (mode === "list") ? "hide" : "show"
			}, 0,  function(event) {
			});
			_right.animate({
				'right' : (mode === "list") ? -650 : 0,
				'opacity' : (mode === "list") ? "hide" : "show"
			}, 0,  function(event) {
			});
			setTimeout(function() {
				//-------------------------- transtion 튜닝 14.08.13
				//	(mode === "list") ? _step.hide().removeClass('detail_choice').fadeIn('fast') : _step.addClass('detail_choice');
				(mode === "list") ? _step.removeClass('detail_choice') : _step.addClass('detail_choice');
			}, _time);
		},
		/*===========================================================
		 Sorting
		 ===========================================================*/
		lineUp : function(mode, order) {
			var _ul = $('ul.phone_list');
			var _li = _ul.find('>li');
			var _order = order || "descending";
			_li.sort(function(prev, next) {
				var _prev = $(prev).data(mode) || 0;
				var _next = $(next).data(mode) || 0;
				if (_prev > _next) {//기본형은 내림차순
					return (_order == "descending") ? -1 : 1;
				}
				if (_prev === _next) {
					return 0;
				}
				if (_prev < _next) {
					return (_order == "descending") ? 1 : -1;
				}
			});
			_ul.append(_li);
			_ul.hide().fadeIn();
		}
	});
})(fn);						  


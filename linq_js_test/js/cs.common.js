(function(Class) {
	Class.define("custom.Common", {
		init : function(obj) {
			this.config = {
				cnt : $('#contents'),

				slider_cnt : $('.d-slider-cnt'),
				slider_viewUnit : $('.d-slider-view-unit'),
				slider_itemBtn : $('.d-slider-item-button'),
				slider_playBtn : $('.d-slider-play-button'),
				slider_pauseBtn : $('.d-slider-pause-button'),

				graph_cnt : $('.d-graph-cnt'),
				graphUnit : $('.d-graph-cnt ul li'),
				graph_txt : $('.d-graph-txt'),
				graph_bar : $('.d-graph-bar'),
				// 상품상세쪽 구매후기/상품문의
				board_cnt : $('.d-board-cnt'),
				board_list : $('.d-board-list'),
				board_modify : $('.d-board-modify'),
				board_modifyCnt : $('.d-board-modify-cnt'),
				board_CancelBtn : $('.ui-cancel'),

				// 상품상세 탭
				detail_tab : $('div.d-detail-tab'),
				detail_tabBtn : $('div.d-detail-tab>ul>li>a'),
				detail_tabCnt : $('div.d-detail-cnt'),

				toggleLayerBtn : $('.d-toggle-layer-button'),
				toggleAmountAnc : $('span.btn_more>a')
			};
			this.vars = {
				interval : 0,
				select : 0
			};
			this.opts = this.opts || {}, this.opts = $.extend(null, this.config, obj);
			this._bind();
			this.setGraph();
			this.setRolling(true);
		},
		_bind : function() {
			var _self = this, _time, _opts = this.opts, _cnt = _opts.cnt, _g = _opts.graph_cnt;

			_cnt.on('click', _opts.slider_itemBtn.selector, function(event) {// 추천영역 (슬라이드)
				var _idx = $(this).index();
				_opts.slider_cnt.find(_opts.slider_viewUnit).hide().eq(_idx - 2).show();
				$(this).addClass('on').siblings('button').removeClass('on');
				var _li = $(this).parents('.d-slider-cnt').find('.recomm_list li').hide().eq(_idx - 2).show();
			});
			_cnt.on('click', _opts.slider_playBtn.selector, function(event) {// 추천영역 (시작버튼)
				$('.d-slider-play-button').hide();
				$('.d-slider-pause-button').show();
				_self.setRolling(true);
			});
			_cnt.on('click', _opts.slider_pauseBtn.selector, function(event) {// 추천영역 (정지버튼)
				$('.d-slider-play-button').show();
				$('.d-slider-pause-button').hide();
				_self.setRolling(false);
			});
			_cnt.on('click', _opts.board_list.selector, function(event) {// Accordion toggle
				event.preventDefault();
				var _this = $(this), _isOpen = _this.hasClass('on'), _next = _this.next();
				var _isPrv = _this.hasClass('ui-private');
				var _tr = _this.siblings('.d-board-list');

				// 비밀글일경우 실행되지않음.
				if (_isPrv) {
					return false;
				}
				if (_isOpen) {// close
					_this.removeClass('on');
					_next.addClass('dHide').find('td').addClass('dHide');
				} else {// open
					_tr.removeClass('on');
					_tr.next().addClass('dHide').find('td').addClass('dHide');
					_this.addClass('on');
					_next.removeClass('dHide').find('td').removeClass('dHide');
				}
			});
			_cnt.on('click', _opts.board_CancelBtn.selector, function(event) {// Accordion (상품문의 취소 버튼시)
				event.preventDefault();
				var _p = $(this).parents('.que_m').hide();
				_p.siblings('.que_q').show();
			});
			_cnt.on('click', _opts.board_modify.selector, function(event) {// Accordion toggle
				event.preventDefault();
				var _m = $(this).parents().next(_opts.board_modifyCnt.selector);
				var _q = $(this).parents('.que_q').hide(), _txt = _q.find('p').text();
				(_m.is(":visible")) ? _m.hide() : _m.show();
				//_q.siblings('.d-board-modify-cnt').find('textarea').val(_txt);
			});
			_cnt.on('click', _opts.toggleLayerBtn.selector, function(event) {// toggle layer button	상단 활성화창
				event.preventDefault();
				var _p = $(this).parents('.choice_con');
				(_p.hasClass("more_wrap")) ? _p.removeClass("more_wrap") : _p.addClass("more_wrap");
			});
			_cnt.on('click', _opts.detail_tabBtn.selector, function(event) {// 상품상세 Detail
				event.preventDefault();
				var _idx = $(this).parent().index();
				$(this).addClass('on').parent().siblings('li').find('a').removeClass('on');
				var _p = $(this).parents('.d-detail-tab');
				_p.siblings('div.d-detail-cnt').hide().eq(_idx).show();
				_self.setGraph();
			});
			_cnt.on('click', _opts.toggleAmountAnc.selector, function(event) {
				var _t = $(this).parents('div.amount_wrap');
				(_t.hasClass('scroll')) ? _t.removeClass('scroll') : _t.addClass('scroll');
			});
		},
		setRolling : function(mode) {
			var _time = 5000, _self = this, _opts = this.opts, _cnt = this.opts.cnt, _vars = this.vars;
			var _func = function() {
				_vars.select = _vars.select + 1;
				_vars.select = _vars.select % 3;
				_cnt.find(_opts.slider_itemBtn.selector).eq(_vars.select).trigger('click');
			};
			if (mode) {
				_vars.interval = setInterval(_func, _time);
			} else {
				clearInterval(this.vars.interval);
				this.vars.interval = null;
			}
		},
		/*---------------------------------------------------------------------------------------------------------------------
		 상품상세 연령별통계 & 단말기별 통계 그래프
		 --------------------------------------------------------------------------------------------------------------------*/
		setGraph : function() {
			var _self = this, _time, _opts = this.opts, _cnt = _opts.cnt, _g = _opts.graph_cnt;
			$('div.graph_area ul li').each(function(idx) {
				var _val = parseInt($(this).find('.d-graph-txt').text());
				var _graph = $(this).find('.d-graph-bar').css('height', _val + "%");
			});
		}
	});
})(fn);

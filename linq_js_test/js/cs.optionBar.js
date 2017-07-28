(function(Class) {
	Class.define("custom.Category", {
		init : function(obj) {
			this.config = {
					
				cnt : $('#contents'),
				body : $('body'),

				optionSortType : $('div.sort_wrap .select>a'),
				optionSortPopup : $('div.sort_wrap .select_wrap'),
				optionSortTypeBtn : $('div.sort_wrap div.select_wrap ul.sort_list>li a'),
				optionViewTypeBtn : $('div.d-view-type button'),

				category_cnt : $('div.category_wrap'),
				category_layer : $('div.category_layer'),
				category_toggleBtn : $('p.tit_category>a'),
				category_grouping : $('ul.category_list>li'),
				category_groupingBtn : $('ul.category_list>li>label'),
				category_makerList : $('ul.maker_list>li'),
				category_applyBtn : $('div.btn_wrap a.d-apply'),
				category_cancelBtn : $('div.btn_wrap a.d-cancel'),

				location_cnt : $('div.category_location'),
				location_title : $('p.category'),
				location_make : $('div.maker_wrap>ul'),
				location_pager : $('div.list_page')
			};
			//--------------------- Vars
			this.pager = 0;
			// this.location_max_limit = 5;
			this.location_max_limit = [5, 4, 5, 5];
			this.opts = this.opts || {};
			this.opts = $.extend(null, this.config, obj);
			this._bind();
			this.pagerVisible(false);
		},
		_bind : function() {
			var _self = this, _opts = this.opts, _cnt = _opts.cnt, _b = _opts.body;
			//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ī�װ�
			_b.on('click', _opts.category_toggleBtn.selector, function(event) {// ī�װ� ���̾� open  Toggle ��ư
				var _c = _opts.category_cnt = $(this).parents(_opts.category_cnt.selector), _visible = _c.find(_opts.category_layer.selector).is(":visible");
				_self.layerVisible(!_visible);
			});
			_b.on('click', _opts.category_groupingBtn.selector, function(event) {// ī�װ� ���� �׷��� ��ü����/LTE-A/LTE/����Ʈ/�Ϲ�/���ѱ⺯ ����
				var _txt = $(this).find('span');
				$(this).parent().addClass('on').siblings().removeClass('on');
				// ----- ���� ���� : �󼼿��� ���ƿö� �����ڽ��� üũ �ȵǴ� ���� ���� START 
				$(this).find("input").prop("checked", true);
				//$(this).parent().siblings().find("input").prop("checked", false);
				// ----- ���� ���� : �󼼿��� ���ƿö� �����ڽ��� üũ �ȵǴ� ���� ���� E N D
			});
			_b.on('click', _opts.category_applyBtn.selector + "," + _opts.category_cancelBtn.selector, function(event) {// �����ϱ� , ����ϱ� ��ư Ŭ���� �̺�Ʈó��
				if ($(this).hasClass('d-apply')) {//----����
					_self.itemFiltering(true);
				} else if ($(this).hasClass('d-cancel')) {//----���
					_self.layerVisible(false);
					var _layer = $(this).parents('.category_layer'), _category = _layer.find('.category_list li'), _filter = _layer.find('.maker_list li'), _idx = new Array();
					//---����START : ��ҽ� ������ ����
					var category_id;
					var subcategory_ids;
					if(window.cs_Info.step == 0)	{
						category_id = window.userPreference.phone.CATEGORY_ID;
						subcategory_ids = window.userPreference.phone.SUBCATEGORY_IDs; 
					} else if(window.cs_Info.step == 1)	{
						category_id = window.userPreference.subscription.CATEGORY_ID;
						subcategory_ids = window.userPreference.subscription.SUBCATEGORY_IDs;
					} else if(window.cs_Info.step == 2)	{
						category_id = window.userPreference.tgift.CATEGORY_ID;
						subcategory_ids = window.userPreference.tgift.SUBCATEGORY_IDs;
					} else {
						return;
					}
					for(var i = 0 ; i < _category.length ; i++)	{	//ī�װ�
						if(category_id == _category.eq(i).find("input").val())	{
							_category.eq(i).find("input").click();
							break;
						}
					}
					
					_filter = _filter.find("input");
					_filter.prop('checked', false);
					for(var i = 0 ; i < _filter.length ; i++)	{	//����ī�װ�
						for(var j = 0 ; j < subcategory_ids.length ; j++)	{	//����ī�װ�
							if(subcategory_ids[j] == _filter.eq(i).val())	{
								_filter.eq(i).prop('checked', true);
							}
						}
					}
					//---����E N D
					
					/* �ߺ� ���(�Դٰ� ���׵� ����)- Ŀ��Ʈ �ƿ� 
					_category.each(function(idx) {
						(window.cs_Info.category_idx == idx) ? $(this).addClass('on').find("input").prop("checked", true) : $(this).removeClass('on').find('input').prop('checked', false);
					});*/
					/*_filter.find('input').prop("checked", false);
					for (var i in window.cs_Info.filtering_idx) {
						_filter.eq(window.cs_Info.filtering_idx[i]).find('input').prop('checked', true);
					}*/
				}
			});
			_b.on('click', _opts.location_make.find('button').selector, function(event) {//������Ʈ X ��ư Ŭ���� ����
				var _txt = $(this).parent().find('span').eq(0).text();
				_self.listAddRemove(false, _txt);
				_cnt.triggerHandler(window.cs_event.filtering);
				//				_self.itemFiltering(true);
			});
			_b.on('click', _opts.location_pager.find('button').selector, function(event) {//< > �¿� ī�װ� ����Ʈ ����
				var _idx = $(this).index();
				var _mode = (_idx == 0) ? "prev" : "next";
				_self.pagerSetting(_mode);
			});
			//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------���ξ� ����
			_b.on('mouseenter', _opts.optionSortType.selector, function(event) {//���ξ����� Popup Layer ȣ��
				var _pop = $(this).parent().siblings('div.select_wrap').show();
				//				$(this).show();
			});
			_b.on('mouseleave', _opts.optionSortPopup.selector, function(event) {//���ξ����� Popup Layer ����
				$(this).fadeOut('fast');
			});
			_b.on('click', _opts.optionSortTypeBtn.selector, function(event) {// ���ξ� �ֽż�,�������ݼ�,�������ݼ����ý�
				var _idx = $(this).parent().index(), _txt = $(this).text(), _layer = $(this).parents('div.select_wrap').fadeOut('fast');
				_layer.siblings('.select').find('a').text(_txt);
				$(this).parent().addClass('on').siblings().removeClass('on');
				//----- �� ��ư Ŭ���� �б⼳��
				_cnt.triggerHandler(window.cs_event.sorting, _idx);
			});
			//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------���������� �� �ڼ�������
			_b.on('click', _opts.optionViewTypeBtn.selector, function(event) {// ���������� , �ڼ�������
				var _idx = $(this).index();
				$(this).addClass('on').siblings().removeClass('on');
				/*if ($("#contents .d-item-list li").length == 0) {
					return;
				}*/
				//--------------------------   14.09.01 
				//-------------------14.08.31    �ּ�ǥ��  ������ ������ viewType�� �ٲܼ��־����.
				(_idx === 0) ? _cnt.triggerHandler(window.cs_event.layout.normalSize) : _cnt.triggerHandler(window.cs_event.layout.smallSize);
				
				//--------���� ���� START : ����� ����Ʈ ���̺� ���Ŀ����� ���̺��� ǥ��
				if(window.cs_Info.step == 1)	{	//�����������
					//
					if(_idx == 1)	{	//��������
						// ����û on
						window.userEvents.live_chat_on();
					} else {
						// ����û off
						window.userEvents.live_chat_off();
					}
				}
				//--------���� ���� E N D
			});
		},
		/*===========================================================
		 ī�װ� �׷��� �� ������ ���͸�
		 ===========================================================*/
		itemFiltering : function(mode) {
			var _param = new Array(), _self = this, _opts = this.opts, _cnt = _opts.cnt, _idx = new Array();
			var _t = $('div.d-html-load').eq(window.cs_Info.step), _radioChk = _t.find('ul.category_list>li input:checked'), _radioVal = _radioChk.parent().text();
			var _li = _t.find('ul.maker_list>li');
			var _location = _t.find('.maker_wrap ul');
 
			_t.find('p.category').text(_radioVal);
			if (mode) {//----------------------------------------------------------------------- ī�װ� �������� �����
				if(_radioVal!=="�������Բ�����"){//------- Ÿ���ϰ�� �������Բ������ϰ��  container ���� data_together Ŭ�������� 14.08.14 ����
					$('#container').removeClass('data_together');
				}
				_param.push(_radioVal);
				_location.find('li').remove();
				_li.each(function(idx) {
					var _txt = $(this).find('input:checked').parent().text();
					if (_txt != "") {
						_idx.push(idx);
						_param.push(_txt);
						_self.listAddRemove(true, _txt);
					}
				});
			}
			_self.layerVisible(false);
			window.cs_Info.category_idx = _radioChk.parents('li').index();
			window.cs_Info.filtering_idx = _idx;
			_cnt.triggerHandler(window.cs_event.filtering, _radioVal);
		},
		/*===========================================================
		 ī�װ� ����/���� Toggle function
		 ===========================================================*/
		layerVisible : function(mode) {
			var _mode = Boolean(mode), _opts = this.opts, _layer = _opts.category_cnt.find(_opts.category_layer.selector), _btn = _opts.category_cnt.find(_opts.category_toggleBtn.selector);
			(_mode) ? _layer.stop().fadeIn() : _layer.hide();
			(_mode) ? _btn.addClass('on') : _btn.removeClass('on');
		},
		/*===========================================================
		 üũ�ڽ� Ŭ���� location �� pager �κ� ���� �� ����
		 ===========================================================*/
		listAddRemove : function(mode, txt) {
			var _opts = this.opts, _mode = Boolean(mode);
			var _t = $('div.d-html-load').eq(window.cs_Info.step);
			var _location = _t.find('.maker_wrap ul');
			var _cateogy = _t.find('.maker_list');
			if (_mode) {
				var _ele = $('<li><span>' + txt + '</span>&nbsp;<button type="button" class="btnicon_delete" title="������� ����"><span class="none">����</span></button></li>');
				var _ln = _location.find("li").length;
				_location.append(_ele);
				if (_ln > this.location_max_limit[window.cs_Info.step]) {// �ѹ��� �������� ������ �Ѿ��� �ڵ�����
					_ele.hide();
				}
			} else {
				var _categoy_checked = _cateogy.find("li:contains(" + txt + ")"), _location_checked = _location.find("li:contains(" + txt + ")");
				_categoy_checked.find('input').attr("checked", _mode);
				_location_checked.remove();
				this.pagerSetting("set");
			}
			var _count = _location.find('li').length;
			(_count > this.location_max_limit[window.cs_Info.step]) ? this.pagerVisible(true) : this.pagerVisible(false);
		},
		/*===========================================================
		 location pager ����/���� Toggle function
		 ===========================================================*/
		pagerVisible : function(mode) {
			var _opts = this.opts, _t = $('div.d-html-load').eq(window.cs_Info.step), _in = _t.find('div.list_page');
			(mode) ? _in.fadeIn() : _in.hide();
			if (!mode) {
				this.pager = 0;
			}
			this.pagerSetting("set");
		},
		/*===========================================================
		 ī�װ� pager �� mode�� ���� prev , next ��ɱ���
		 ===========================================================*/
		pagerSetting : function(mode) {
			var _self = this, _opts = this.opts;
			var _t = $('div.d-html-load').eq(window.cs_Info.step);
			var _location = _t.find('.maker_wrap ul');
			var _cateogy = _t.find('.maker_list');
			var _in = _t.find('div.list_page');

			var _li = _location.find("li");
			var _in = _t.find('div.list_page');
			var _total = Math.ceil(_li.length / _self.location_max_limit[window.cs_Info.step]);
			var _nowTxt = _in.find("p.num strong");
			var _totalTxt = _in.find("p.num span");
			if (mode == "prev") {
				(this.pager <= 0) ? 0 : --this.pager;
			} else if (mode == "next") {
				(this.pager >= _total - 1) ? _total - 1 : ++this.pager;
			} else {
				this.pager = Math.floor((_li.length - 1) / _self.location_max_limit[window.cs_Info.step]);
			}
			_li.hide().each(function(idx) {
				if (Number(_self.pager) * _self.location_max_limit[window.cs_Info.step] <= idx && idx < Number(_self.pager + 1) * _self.location_max_limit[window.cs_Info.step]) {
					$(this).show();
				}
			});
			_nowTxt.text(this.pager + 1);
			_totalTxt.text(_total);
		}
	});
})(fn);

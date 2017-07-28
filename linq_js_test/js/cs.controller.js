(function($) {
	window.cs_event = {
		sorting : 'sorting',
		filtering : 'filtering',
		layout : {
			normalSize : "layout.normalSize", //-------------------------------------------------------------------------------------------------------------���Ÿ�� �Ϲ���
			smallSize : "layout.smallSize" //------------------------------------------------------------------------------------------------------------------���Ÿ�� �۰Ժ���
		},
		view : {
			listView : "view.listView", //------------------------------------------------------------------------------------------------------------------------����Ʈ����
			detailView : "view.detailView" //------------------------------------------------------------------------------------------------------------------�󼼺���
		},
		step : {
			process : "step.process"//--------------------------------------------------------------------------------------------------------------------------����STEP �Ѿ������
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
			_c.on(window.cs_event.sorting, function(event, num) {// �α��/�ֽż�/�������ݼ�/�������ݼ� Ŭ����
				if (window.userEvents != undefined && window.userEvents.listing != undefined) {
					window.userEvents.listing();
					//------ 14.08.28
					_self.layout.listSlider("init");
				};
			});
			_c.on(window.cs_event.filtering, function(event, type, filter) {// ī�װ�  ���͸� �����ư Ŭ����
				if (window.userEvents != undefined && window.userEvents.listing != undefined) {
					window.userEvents.listing();
					_self.layout.resizeHdr($(window).width());
					
					var subScriptionKind		= "";	//����� ����
					
					if(window.cs_Info.type === "tablet"){
						if(type.indexOf("�Բ�����") >= 0) subScriptionKind = "tablet_type";	//�������Բ����� ����� ���ý� �º������ �������� ����
					}
					
					//-------------------------------------------------------------------------------------------------- Ÿ�� ����� "����Ÿ �Բ�����" �ϰ�� 'data_together'  class �߰�
					(subScriptionKind === "tablet_type") ? $('#container').addClass('data_together') : $('#container').removeClass('data_together');
					//---------------���� �������Բ����� ���� ���̻� �����ʱ� Ȯ�� START				
					var aCookie = document.cookie.split("; ");					
					for (var i=0; i < aCookie.length; i++)  {
						var cPos = aCookie[i].indexOf( "=" );
						var cName = aCookie[i].substring( 0, cPos );						
						if ( cName == "tablet_gift" ) {
							$(".tip_layer").attr("style", "display:none");
						}
					}										
					//---------------���� �������Բ����� ���� ���̻� �����ʱ� Ȯ�� END
					if (subScriptionKind === "tablet_type") {
						_self.layout.layoutTypeHdr("normal");
					}
				};
			});
			_c.on(window.cs_event.layout.normalSize, function(event) {// �Ϲݻ������
				_self.layout.layoutTypeHdr("normal");
				_self.layout.listSlider("init");  
				_top.hide();
			});
			_c.on(window.cs_event.layout.smallSize, function(event) {// �۰Ժ���(����Ϸκ���) , ����������� �ؽ�Ʈ�� ����
				_self.layout.listSlider("init");
				_self.layout.layoutTypeHdr("small");
				_top.show();
			});
			_c.on(window.cs_event.view.listView, function(event) {// ����Ʈ ������ ����									
				var _l = $('div.d-detail-left').eq(window.cs_Info.step), _r = $('div.d-detail-right').eq(window.cs_Info.step);
				var _mount = 1400;
				if (_l.length === 0) {
					_l = $('div.d-detail-left');
				};
				if (_r.length === 0) {
					_r = $('div.d-detail-right');
				};
				//----------------------- 14.08.12.  motion transtion ����
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
			_c.on(window.cs_event.view.detailView, function(event, data) {// �� ������ ����
				var obj = $.extend({}, data);
				obj.type = "detail";
				var param = data.li.length > 0 ? data.li.data() : data.tr.data(); 
				_self.loadHtml(obj, param);
				_self.layout.viewTypeHdr("detail");			
				_top.show();
			});
			_c.on(window.cs_event.step.process, function(event, num) {// �޴���,Ÿ�� ����  ��������->�����->T gift -> �������Ѿ�  �Ѿ�� Step üũ
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
		loadHtml : function(obj, params, ajax) {// html load ó��
			// ���̾��˾������κ� ����ó��
			$('.choice_con').removeClass("more_wrap");
			var _c = this.cnt, _self = this, _step, _type, _url;
			if (ajax != undefined && ajax != null && ajax != '') {//---------------------------------------------------- 3��° params �� �����Ұ�� ajax param�� ó��
				_type = (ajax.split('CS_TYPE=')[1]).split('&')[0];
				_step = Number((ajax.split('CS_STEP=')[1]).split('&')[0]);
				_c.removeClass('step01 step02 step03 step04').addClass('step0' + Number(_step + 1) + '');
				_url = ajax;
			} else {
				_step = Number(window.cs_Info.step), _type = obj.type;
				
				_url = window.cs_Info.list;
				
				if(window.cs_Info.packPhoneDetail != undefined){
					if(window.cs_Info.type === "tablet" &&   window.cs_Info.packPhoneDetail.WEARABLE_FL === "Y"){	 //����� ����� ��� �� �ϰ��
						_url = (_type === "list") ? window.cs_Info.chg_list[_step] : window.cs_Info.chg_detail[_step];	//��������� ����Ʈ,��ȭ���� �����ֱ�����
						
						if(window.cs_Info.selectSubscriptionType != undefined){
							if(window.cs_Info.selectSubscriptionType === "data_together"){	 //����� �Բ��������� �ϰ�� ��ȸ�� ���� �������� ����(cs.item.js�� ����� ����Ʈ���� table�� ���⿡�� ������)
								if(_type === "detail" && _step === 1) $('#container').addClass('data_together'); 
							} else {
								$('#container').removeClass('data_together');
							}
						}
						
						/*if(_type === "detail" && window.cs_Info.packPhoneDetail.WEARABLE_DEVICE ==="Y" ){	//����� ����� ��� �� + ����� ��� �� ���
							$('#container').addClass('data_together');							//����� �Բ��������� �ϰ�� ��ȸ�� ���� �������� ����(���������Ʈ �Ϲݻ������ ���� ���)
							window.cs_Info.selectSubscriptionType	= "data_together";	//������� üũ����Ʈ �� ����ó�� ���� Setting(twd_bind_phone.js_subscriptionDetail_checkPointEvent)
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
				
				// url �� ��ȭ�� �������� �ʿ�
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
						
						//--------------------------------------------------------------- 14.09.22  ������û ����Ϻ��� ��������
						if(_step!=3) _self.layout.layoutTypeHdr("small");
						//--------------------------------------------------------------- 14.09.22  ������û ����Ϻ��� ������
						
	
						//--------------------------------------------------------- ��ܿɼǹ� Step �κ� �������  14.08.13
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
					//--------------------------------------------------------- ��ܿɼǹ� ��ü unchecked  14.08.13
					window.cs_Info.filtering_idx = null;
					//$('.maker_list li').find('input').prop('checked', false);	//���߻���
					//------ ��� d-view-type üũ
					var _vNum = (_p.find('.product_area').hasClass('small_list')) ? 1 : 0;
					_p.find('div.d-view-type button').removeClass('on').eq(_vNum).addClass('on');
				}
				/*
			}).fail(function(data, x, e) {
				if (window.console != undefined) {	// parsing ������ ���� json string�� ��¥����(REG_DATE)�� �� ���� ������ Ȯ��! JsonObject �����ε�
					console.log('-- fail');
				}
			});*/
		},
		/*
		 *  ajax URL ���� 1���� �Ͽ� ����1&����2 �����  url �ε��Ҽ��ֵ��� ó��
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

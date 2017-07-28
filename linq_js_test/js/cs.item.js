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
				
				processStep2 : $('#outdoor_unit_info01 .function_agree')	// 141209_T�ƿ����� �����_v0.1
			};
			this.opts = this.opts || {}, this.opts = $.extend(null, this.config, obj);
			//������ ��Ǿ�����
			this.selected = 0;
			this._bind();
		},
		_bind : function() {
			var _self = this, _time, _opts = this.opts, _cnt = _opts.cnt, _d = _opts.detailWrap, _b = $('body');
			//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------����Ʈ���
			_b.on('click', _opts.colorChoiceOpenAnc.selector, function(event) {// ���� Layer Open
				event.preventDefault();
				$(this).parents(_opts.colorChoice.selector).addClass('all');
			});
			_b.on('click', _opts.colorChoiceCloseAnc.selector, function(event) {// ���� Layer Close
				event.preventDefault();
				$(this).parents(_opts.colorChoice.selector).removeClass('all');
			});
			_b.on('click', _opts.colorChoiceUnit.selector, function(event) {// �������ϱ�
				event.preventDefault();
				$(this).addClass('on').siblings().removeClass('on');
				$(this).parents(_opts.colorChoice.selector).removeClass('all');
			});
			_cnt.on('click', _opts.itemUnitInput.selector, function(event) {// �� ������ üũ�ڽ� ���ý�
				var _idx = $(this).parent().index();
				var _isChk = $(this).attr('checked');
				_self.selected = $('div.d-item-wrap').find(_opts.itemUnitInput.selector + ":checked").length;
				_self.listAddRemove($(this));
			});
			_cnt.on('click', _opts.c_popup_openBtn.selector, function(event) {//���Ժ� �˾� ����				
				$(this).parents('.tip_wrap').find('.tip_layer').fadeIn();
			});
			_cnt.on('click', _opts.c_popup_closeBtn.selector, function(event) {//���ϱ� �˸� �˾��ݱ�
				var _this = $(this).parents('.tip_layer');
				var _chk = _this.prev('input');
				_chk.attr('checked', false);
				var _chked = $('div.d-item-wrap').find(_opts.itemUnitInput.selector + ":checked");
				_self.listAddRemove(_chked.last());
				_this.hide();
			});
			_cnt.on('click', _opts.adviceBtn.selector, function(event) {// ����û				
				window.userEvents.counsel.resetCounselLayer();
				$('#smsalert01').fadeIn('fast');
			});
			_b.on('click', _opts.adviceSubmitBtn.selector, function(event) {// �޴��� ���� ��� ��û ��û�Ϸ��ư
				event.preventDefault();
				window.userEvents.counsel.counselRequestInsert();
			});
			_cnt.on('click', _opts.noticeStock.selector, function(event) {// �԰�˸���û �˾�����
				window.userEvents.sms.resetSmsLayer();
				$('#layer_ph_announcement').fadeIn('fast');
			});
			_b.on('click', _opts.noticeStockSubmit.selector, function(event) {// �԰�˸���û
				event.preventDefault();
				window.userEvents.sms.soldOutSmsInsert();
			});
			//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------������ �´� ����� ã��
			_cnt.on('click', _opts.customizeAnc.selector, function(event) {// ������ �´� ����� ã�� layer open
				event.preventDefault();				
				$('div.product_step02').addClass('mytariff');
				$('div.product_step02 div.product_area').hide();
				$('#tariffTitle').attr('src', '/tws/images/product/tit_recommend_tariff.png'); // ��õ������� Ÿ��Ʋ �̹��� ��ü
			});
			_cnt.on('click', _opts.customizeLayerClose.selector, function(event) {// ������ �´� ����� ã�� layer close
				event.preventDefault();
				// ���� ����� ����Ʈ�� ���ε�
				bestChargeList = null;
				window.userEvents.listing_subscriptionList();
				
				$('div.product_step02').removeClass('mytariff myt_search');
				// ----- ���� ���� : ó��ȭ�鿭�� ��ǰ�� ��� ��ǰ������ ���̴� ���� ���� START
				if($('div.product_step02 div.product_area .d-item-list>li').length > 0)	{
					$('div.product_step02 div.product_area').show();
				}
				// ----- ���� ���� : ó��ȭ�鿭�� ��ǰ�� ��� ��ǰ������ ���̴� ���� ����  E N D
				//---- �ʱ�ȭ
				$('div.d-customize-cnt1').addClass('search');
				_opts.customizeShowTableAnc.addClass('disable');
				//$('.sc_recomm_tariff').removeClass('disable');
				$('div.product_step02').removeClass('myt_search');
				$('#tariffTitle').attr('src', '/tws/images/product/tit_normal_tariff.png'); // �Ϲݿ������ Ÿ��Ʋ �̹��� ��ü
			});
			_cnt.on('click', _opts.customizeShowTableAnc.selector, function(event) {// ������ �´� ����� ã�� ��� table ���·� �����ֱ�
				var _isChk=$(this).hasClass('disable');
				if(_isChk){
					// disable Ŭ���� ������� �۵��ȵǰ� �س���. 14.08.20
					//alert(' not ');
					return false;
				}else{
					event.preventDefault();
					if($(this).hasClass("sc_customized")){					
						window.myTariff.getChargeMyPlan(); //������ ����� ã��
					}else{					
						window.myTariff.getChargeRcProd(); //����� ��õ�ޱ�
					}
				}				
				//$('div.product_step02').addClass('myt_search');
				//$(this).addClass('disable');
			});
			_cnt.on('click', _opts.customizeTab.selector, function(event) { // �̿뷮�� �������� ��õ�ޱ��� , ������ ����� ���� �����ϱ���
				// 0 �̿뷮 ��õ , 1  ������ �����
				var _idx = $(this).parents('li').index();
				if(_idx == 0 ){	
					
					if(_isLogin && (_baseGbn == 'L') ){
						if(_svcNum == ""){
							$("#myTariff2").click();    // ������ ����� ���������ϱ�� �̵�
							alert('�̿뷮�� �������� ��õ�ޱⰡ �Ұ����մϴ�.(��ȭ��ȣ�� ��ȸ���� ����!)');
							return;
						}
						window.myTariff.userInfo(); // ����� ��� ���� ��������
						$(".my_info").show();       // �α��ν� �ȳ����� show
						$(".login_area").hide();    // �α��ν� �α��� ���� hide
						$(".login_mseg").hide(); 
						$(".sc_amount").removeClass("disable");
					}else if(!_isLogin){           
						$(".my_info").hide();       // ��α��ν� �� ���� hide
						$(".login_area").show();    // ��α��ν� �α��� ���� show
						$(".login_mseg").show();    
					}else{
						$(".login_area").hide();    // �α��ν� �α��� ���� hide
						$(".my_info").hide();       // �α��ν� �� ���� hide
						$("#myTariff2").click();    // ������ ����� ���������ϱ�� �̵�
						alert("�̿뷮�� �������� ��õ�ޱ�� SKT ȸ�� �����ڸ� �̿��� �����ϸ�, ��ȸ���� ��� ������ ����� ���� �����ϱ⸸ �����մϴ�.");
						return ;
					}	
					
					$(this).addClass('on').parent().siblings('li').find('a').removeClass('on');
					(_idx === 0) ? $('div.d-customize-cnt1').show() : $('div.d-customize-cnt1').hide();
					(_idx === 0) ? $('div.d-customize-cnt2').hide() : $('div.d-customize-cnt2').show();
					$('div.product_step02').removeClass('myt_search');					
					
					// ȭ�� �ʱ�ȭ
					$(".product_area.d-item-wrap").attr("style", "display:none");
					$(".pnone_list_none").attr("style", "display:none");
					
				}else{
					$(this).addClass('on').parent().siblings('li').find('a').removeClass('on');
					(_idx === 0) ? $('div.d-customize-cnt1').show() : $('div.d-customize-cnt1').hide();
					(_idx === 0) ? $('div.d-customize-cnt2').hide() : $('div.d-customize-cnt2').show();
					$('div.product_step02').removeClass('myt_search');
					
					// �̿뷮�� ����������õ�ޱ� -> ����� ��õ�ޱ� ��ư
					$(".sc_recomm_tariff").removeClass('disable');
					
					//ȭ�� �ʱ�ȭ
					$(".product_area.d-item-wrap").attr("style", "display:none");
					$(".pnone_list_none").attr("style", "display:none");
								
				}
			});
			_cnt.on('click', _opts.customizeGetBtn.selector, function(event) {// �̿뷮�� �������� ��õ�ޱ� ���� "��뷮 ��ȸ�ϱ�"
				
				var _isChk=$(this).hasClass('disable');
				if(_isChk){
					// disable Ŭ���� ������� �۵��ȵǰ� �س���. 14.08.20
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
			_cnt.on('mousedown mouseup click', _opts.customizeControlBtn.selector, function(event) {// ������ ����� ���� �����ϱ� ��Ʈ�ѹ�ư
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
			_cnt.on('mouseup', _opts.customizeLayer.selector, function(event) {// ������ ����� ���� �����ϱ� ��Ʈ�ѹ�ư
				_isDown1 = false;
				_isDown2 = false;
			});
			_cnt.on('mousemove', _opts.customizeLayer.selector, function(event) {// ������ ����� ���� �����ϱ� ��Ʈ�ѹ�ư
				var _g = $('div.d-customize-cnt2 ul.graph_list li');
				//--------------------------   14.09.01   14.09.01 ���� START
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
				//------------------------------------------------------------------  14.09.01 ���� END
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
			//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------�󼼺���
			_cnt.on('click', _opts.costDetailBtn.selector, function(event) {
				$('#costDetail').fadeIn('fast');
			});
			_cnt.on('click', _opts.detail_imgListAnc.selector, function(event) {// �󼼺��� ����� �̹��� Ŭ����
				//-------------------14.08.31
				event.preventDefault();
				
				var _idx = $(this).index();
				$(this).addClass('on').siblings().removeClass('on');
				$(this).parents('.thumb_view').find('.thumb_img img').hide().eq(_idx).show();
			});
			_cnt.on('click', _opts.detail_imgListArrow.selector, function(event) {// �󼼺��� ����� �̹��� �¿� ȭ��ǥ (4������ Ŭ���)
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
			_cnt.on('click', _opts.detail_closeBtn.selector, function(event) {// ���˾��ݱ�
				
				//�� �б⺰ ó���� ���� cntLoadType�� value�� gb�� ����.
				var gb = $("#cntLoadType").val();
				
				if(gb === "phone" || gb === ""){ //�ڵ����϶�
					gbFamLayerYn = false; //�°������� ���� �˾� ���̾� �˾� ���� ����
					_is_family_first_list = true;
					_is_family_first_detail = true;
				}
				_cnt.triggerHandler(window.cs_event.view.listView);
				
			});


			// 141030 ����Ŭ��
			_cnt.on('click', _opts.selectPhone.selector, function(event) {// �޴�������
				if ($(".detail_wrap #in_freeclub_fl").val() == "Y"){	//���⿡�� �ڵ��� ������ ���� ��� ���̾� ���
					 event.preventDefault();
					 
					 //in_product_grp_nm
                   $('#freeClubLayer1').show();
                   $('#freeClubLayer1').css('background',  'url("/tws/images/product/popup/modal.png") repeat 0 0');
				}else{
					if(window.userEvents != undefined && window.userEvents.packing != undefined && window.userEvents.packing() != true)	{
						return;	//validation check ���н� ����ȭ������ �̵����� ����
					}
				}
			});

			_b.on('click', "#freeClubLayer1 .function_agree", function(event) {// ����Ŭ�� ���̾� �˾�
				$('#freeClubLayer1').hide();
				if(window.userEvents != undefined && window.userEvents.packing != undefined && window.userEvents.packing() != true)	{
					return;	//validation check ���н� ����ȭ������ �̵����� ����
				}
			});	
			// [e] 141030 ����Ŭ��
			                                                                                                   
			// ���� ���� ��ư                                                                                  
			_b.on('click',
				"#premium_pass_info01 .function_agree, #premium_pass_info02 .function_agree, #premium_free_info01 .function_agree, #premium_free_info02 .function_agree", 
				function(event) {
				var _obj = {};
				var _type = "";
				var layer_idx;
				var layer_id;

				if ($('#premium_pass_info01').css("display") != "none"){			//[�����̾��н� ���԰��� �ȳ�] ���̾��˾�
					layer_idx = 0;
					layer_id	 = '#premium_pass_info01';
				} else if ($('#premium_free_info01').css("display") != "none"){	//[�ΰ�����(����Ŭ��+�����̾��н�) ���԰��� �ȳ�] ���̾��˾�
					layer_idx = 1;
					layer_id	 = '#premium_free_info01';
				}  else if ($('#premium_free_info02').css("display") != "none"){	//[�ΰ�����(����Ŭ��) ���԰��� �ȳ�] ���̾��˾�
					layer_idx = 2;
					layer_id	 = '#premium_free_info02';
				} else if ($('#premium_pass_info02').css("display") != "none"){	//[�����̾��н� �ȳ�] ���̾��˾�
					layer_idx = 3;
					layer_id	 = '#premium_pass_info02';
				}			
				
				var _idx	  	= $(layer_id).find('#sel_choice_idx').val();
				var _li = $('div.table_wrap tbody#tbodySubscriptionTable>li').eq(_idx), _tr = $('div.table_wrap tbody#tbodySubscriptionTable>tr').eq(_idx);
				      _obj.li = _li, _obj.tr = _tr, _obj.idx = _idx;
								
			    if (layer_idx === 0){
					if($("#container").find("#tbodySubscriptionTable").length > 0 )	{
						_type	= "list";
						_obj.tr.data().detail.PREMIUMPASS_AGREE	= "PP"; 	//�����̾��н� ����(����Ʈ)
					} else {
						_obj.PREMIUMPASS_AGREE	= "PP"; 	//�����̾��н� ����(��)
					}
			    } else if (layer_idx === 1 || layer_idx === 2){
			    	var checkFl = "N"; 
			    	
			    	if($(layer_id+' #chk_PRCL_agree_'+layer_idx).is(':checked') && $(layer_id+' #chk_PRPS_agree_'+layer_idx).is(':checked')) checkFl = "A"
			    	else if($(layer_id+' #chk_PRCL_agree_'+layer_idx).is(':checked')) checkFl = "PC"
			    	else if($(layer_id+' #chk_PRPS_agree_'+layer_idx).is(':checked')) checkFl = "PP";
			    	
					if (checkFl === "N"){	// �ƹ��͵� üũ ���� �ʾ��� ��                                            
						alert('������ ���Ͻô� ��ǰ�� üũ�� �ּ���.\n������ ������ ���� ��� ���߿� �ϱ� ��ư�� Ŭ���� �ּ���.');
						return;
					} else {	// üũ ���� ��
						if($("#container").find("#tbodySubscriptionTable").length > 0 )	{
							_type	= "list";
							_obj.tr.data().detail.PREMIUMPASS_AGREE	= checkFl; 	//�ΰ����� ����(����Ʈ)
						} else {
							_obj.PREMIUMPASS_AGREE	= checkFl; 	//�ΰ����� ����(��)
						}
					}
			    } else if (layer_idx === 3){
					if($("#container").find("#tbodySubscriptionTable").length > 0 )	{
						_type	= "list";
						_obj.tr.data().detail.PREMIUMPASS_DUTY_FL = "";		//���� step���� �ѱ�� ����
					} else {
						_obj.PREMIUMPASS_DUTY_FL	= ""; 	//���� step���� �ѱ�� ����
					}
			    }
			    
				if(window.userEvents != undefined && window.userEvents.packing != undefined && window.userEvents.packing(_type, _obj) != true)	{
					return;	//validation check ���н� ����ȭ������ �̵����� ����
				}			    
			    
			    $(layer_id).hide();
			});                                                                                                
			// ���߿� �ϱ� ��ư                                                                                
			_b.on('click',                                                                                     
				"#premium_pass_info01 .function_later, #premium_free_info01 .function_later, #premium_free_info02 .function_later, #premium_free_info02 .d-popup-closeBtn", 
				function(event) {    
				var _obj = {};
				var _type = "";
				var layer_idx;
				var layer_id;
				var checkFl = ""; 

				if ($('#premium_pass_info01').css("display") != "none"){			//[�����̾��н� ���԰��� �ȳ�] ���̾��˾�
					layer_idx = 0;
					layer_id	 = $('#premium_pass_info01');
				} else if ($('#premium_free_info01').css("display") != "none"){	//[�ΰ�����(����Ŭ��+�����̾��н�) ���԰��� �ȳ�] ���̾��˾�
					layer_idx = 1;
					layer_id	 = $('#premium_free_info01');
				} else if ($('#premium_free_info02').css("display") != "none"){	//[�ΰ�����(����Ŭ��) ���԰��� �ȳ�] ���̾��˾�
					layer_idx = 2;
					layer_id	 = $('#premium_free_info02');
					
					if(event.currentTarget.className === "re-plan-link d-popup-closeBtn" || event.currentTarget.className === "d-popup-closeBtn"){	//����� �ٽ� �����ϱ�,�ݱ�
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
					_obj.tr.data().detail.PREMIUMPASS_AGREE	= checkFl; 	//���߿��ϱ�(����Ʈ)
				} else {
					_obj.PREMIUMPASS_AGREE	= checkFl; 	//���߿��ϱ�(��)
				}
				
				if(window.userEvents != undefined && window.userEvents.packing != undefined && window.userEvents.packing(_type, _obj) != true)	{
					return;	//validation check ���н� ����ȭ������ �̵����� ����
				}
				layer_id.hide();	
			});
			// [e] 141106 �ΰ����� ���� �ȳ� ���̾� (����Ŭ��, �����̾� �н�) 			


			_cnt.on('click', _opts.selectCost.selector, function(event) {// ���������
				if(window.userEvents != undefined && window.userEvents.packing != undefined && window.userEvents.packing() != true)	{
					return;	//validation check ���н� ����ȭ������ �̵����� ����
				}
			});
			_cnt.on('click', _opts.selectTgift.selector, function(event) {// T ����Ʈ����
				if(window.userEvents != undefined && window.userEvents.packing != undefined && window.userEvents.packing() != true)	{
					return;	//validation check ���н� ����ȭ������ �̵����� ����
				}
			});
			_cnt.on('change', _opts.detail_membershipSelectBox.selector, function(event) {// ��ȣ�̵��ϰ�� membership Ŭ���� ����
				var _val = $(this).val(), _layer = $('div.total_wrap');
				(_val === "20" || _val === "11") ? _layer.addClass('membership') : _layer.removeClass('membership');
			});
			_cnt.on('click', _opts.itemReSelectAnc.selector, function(event) {// �������Ѿ׿���, ����/�����/Tgift ���ý� �ش��������� �̵�
				var _ele = $(this).parents('div.d-product');
				var _data = _ele.attr('class').split(' '), _idx;
				for (var i in _data) {
					//150113_�缱��IE8����_v0.1 //if (_data[i].indexOf('product_step0') === 0) {
				     if (_data[i]=="product_step01"||_data[i]=="product_step02"||_data[i]=="product_step03"||_data[i]=="product_step04") {
						_idx = Number(_data[i].split('product_step0')[1]) - 1;
						if(_idx===3){
							var _idx=$(this).index();
						}
						
						if(window.cs_Info.selectSubscriptionType != undefined) window.cs_Info.selectSubscriptionType = "";	//�ʱ�ȭ
					}
				}
				
				//�޴�����ŷ�� ��������̽��� ������ �°������� �������� ������ ���� �޴��� �缱�� �� �°����������� �ٽ� ���������� �����Ŵ
				if(_idx === 0) _W_fmlyDcCd    = _fmlyDcCd;   
				
				if(_idx === 0 || _idx === 1) $("#inParentServiceNum").val("");	//��������� ���ǰ���� ��ȣ�� �ʱ�ȭ
				
				//------------------- �޴���/�����/Tgift �缱�ý� ���̾��˾� ȣ������ "��" ���ý� �ش� ����Ʈȭ������ �̵�
				var _layer = $('#layer_resel0' + Number(_idx + 1)).fadeIn();
				_layer.find('.bt_ok').unbind().bind('click', function(event) {
					_cnt.triggerHandler(window.cs_event.step.process, _idx);
					_layer.hide();
				});
			});
			_cnt.on('click', "#clubT_openBtn", function(event) {// Ŭ��T 85/100  ���� �ڼ�������
				window.userEvents.info.clubtLayer('NA00004428'); // Ŭ��T 85 : NA00004428
				$('#clubt_info').fadeIn();
			});
			_b.on('click', "#clubt_info .clubTtab li a", function(event) {// Ŭ��T 85/100  �����
				var _idx = $(this).parents('li').index();
				if( _idx == 0 ){
					window.userEvents.info.clubtLayer('NA00004428'); // Ŭ��T 85 : NA00004428
				}else {
					window.userEvents.info.clubtLayer('NA00004429'); // Ŭ��T 100 : NA00004429
				}
				$(this).addClass('on').parent().siblings('li').find('a').removeClass('on');
				$('#clubt_info .clubTsubTab li:eq(0)').trigger('click');
			});
			_b.on('click', "#clubt_info .clubTsubTab li", function(event) {// Ŭ��T 85/100  ������
				var _idx = $(this).index();
				$(this).find('>a').addClass('on').parent().siblings('li').find('>a').removeClass('on');
				$('#clubt_info .clubT_guide div').addClass('dHide').eq(_idx).removeClass('dHide').addClass('dShow');

			});
			// 141209_T�ƿ����� �����
			_b.on('click', _opts.processStep2.selector, function(event) {// Tgift �� �̵��Ǵ� �˾��ݱ�
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
						return;	//validation check ���н� ����ȭ������ �̵����� ����
					}
				
					$("#view_fl").val("N");
					$('#outdoor_unit_info01').hide();
				} else {
					alert("�����Ͻ� ����Ʈ�� ������ Ȯ�� �Ͻ� �� ���� ���� ���ּ���.");
					return;
				}
			});			
		},
		/*-----------------------------------------------------------------------------------------------------------------------------
		 ���ϱ� �ܰ躰 �˾� �߰� �� ����
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
				_ele += '<div class="tip_layer dev_step1"><span class="tip"></span><div class="tip_box"><button type="button" class="close" title="���ϱ� �˸��˾� �ݱ� �� üũ����"><span class="none">���̾��˾� �ݱ�</span></button>';
				_ele += '<div class="txt_wrap"><span class="txt">��ǰ�� ���Ͻ÷��� <strong class="fc_org">2~3��</strong>��<br>������ ������ �ּ���</span></div><div class="btm"></div></div></div>';
			} else if (_self.selected == 2) {
				_ele += '<div class="tip_layer dev_step2"><span class="tip"></span><div class="tip_box"><button type="button" class="close" title="���ϱ� �˸��˾� �ݱ� �� üũ����"><span class="none">���̾��˾� �ݱ�</span></button>';
				_ele += '<div class="txt_wrap"><spawn class="txt">�����Ͻ� <strong class="fc_org">2</strong>���� ������<br>���� ���ðڽ��ϱ�?</span><span class="txt fc_gray">(3���� ������ ���Ͻ÷���<br>1���� ������ �� ������ �ּ���)</span>';
				_ele += '<p class="btn_center"><span class="btn btn_o_ss"><span><a href="#">���ϱ�</a></span></span></p></div><div class="btm"></div></div></div>';
			} else if (_self.selected == 3) {
				_ele += '<div class="tip_layer dev_step3"><span class="tip"></span><div class="tip_box"><button type="button" class="close" title="���ϱ� �˸��˾� �ݱ� �� üũ����"><span class="none">���̾��˾� �ݱ�</span></button><div class="txt_wrap"><span class="txt">�����Ͻ� <strong class="fc_org">3';
				_ele += '</strong>���� ������<br>���� ���ðڽ��ϱ�?</span>	<p class="btn_center"><span class="btn btn_o_ss"><span><a href="#">���ϱ�</a></span></span></p></div><div class="btm"></div></div></div>';
			} else if (_self.selected >= 4) {
				_ele += '<div class="tip_layer dev_step4"><span class="tip"></span><div class="tip_box"><button type="button" class="close" title="���ϱ� �˸��˾� �ݱ� �� üũ����"><span class="none">���̾��˾� �ݱ�</span></button><div class="txt_wrap">';
				_ele += '<span class="txt"><strong class="fc_org">4</strong>�� �̻��� ������<br>���Ͻ� �� �����ϴ�</span><span class="txt fc_gray">(��ǰ�� ���Ͻ÷��� 2~3����<br>������ ������ �ּ���)</span></div><div class="btm"></div></div></div>';
			}
			target.parent().append(_ele);
			// �˾����
			target.siblings('div.tip_layer').hide().fadeIn('fast');
		},
		/*-----------------------------------------------------------------------------------------------------------------------------
		 ī�װ��� ���ÿ� ���� ������ �з��� filtering
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

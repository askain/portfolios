//--------------------------------------------------------
//window.bind ----    START
//--------------------------------------------------------
(function($) {
	
	// ���� �Ѵ�. ���ε�.
	window.bind = {
		// ======================================================================================================
		// ------------------------------------------------------------------------------------------------------
		// ------------------------------------------------------------------------------------------------------
		// ------------------------------------------------------------------------------------------------------
		// �޴���
		// ------------------------------------------------------------------------------------------------------
		// ------------------------------------------------------------------------------------------------------
		// ------------------------------------------------------------------------------------------------------
		// ======================================================================================================
		/** window.bind.phoneList_categoryList
		 * ���ε� Ÿ�� : �޴���/�º� ����Ʈ�� ī�װ� ���
		 * ���� : �޴���/�º� ����Ʈ�� ī�װ��� ���ε��ϰ� �������� ����Ʈ�� �����ϴ� ����� �����Ѵ�.
		 */
		phoneList_categoryList : function(list, up_category_id)	{
			var category_layer = $(".product_step01 ul.category_list");
			category_layer.html("");

			var cateIndex = 1;
			for(var i = 0 ; i < list.length ; i++, cateIndex++)	{
				category_layer.append("<li><label for='cate" + padZero(cateIndex, 2) + "'>" 
										+ "<input type='radio' name='catechk' id='cate" + padZero(cateIndex, 2) + "' class='chk' value='" + list[i].CATEGORY_ID + "'>"
										+ "<span>" + list[i].CATEGORY_NM + "</span></label></li>");
			}
			
			if(category_layer.find("li.on").length == 0)	{
				category_layer.find("#cate01").click();	// �⼱�õ�  ī�װ��� ������  ù��° ���� ����
			}

			$('div.btn_wrap a.d-apply').click();
			
		},
		/** window.bind.phoneList_subcategory
		 *  ���ε� Ÿ�� : �޴���/�º� ����Ʈ�� ī�װ� ��� ���� ȸ��� �κ�
		 */
		phoneList_subcategory : function(list, selector)	{
			var subcategory_layer = $("ul.maker_list");
			subcategory_layer.html("");
			
			var totalIndex = 1;
			
			// ȸ��� subcategory ���ε�
			for(var i = 0 ; i < list.length ; i++, totalIndex++)	{
				subcategory_layer.append("<li><label for='total" + padZero(totalIndex,2) + "'><input type='checkbox' name='maker_total' id='total" + padZero(totalIndex,2) + "' class='chk' value='" + list[i].COMPANY_CODE + "'>" + list[i].COMPANY_NM + "</label></li>");
			}
		},
		/** window.bind.phoneList
		 *  ���ε� Ÿ�� : �޴���/�º� ����Ʈȭ�鿡�� ����ִ� �޴���/�º� ����Ʈ
		 */
		phoneList : function(list, selector)	{
			var ul = $("ul.d-item-list").html("");
			if(list == null || list.length == 0)	{
				$(".pnone_list_none, .tablet_list_none").show().siblings().not(".option_bar").not("#my_tariff").hide();	// ���� �ش��ϴ� �޴����� �����ϴ�.
				return;
			}
			$(".pnone_list_none, .tablet_list_none").hide().siblings().not(".option_bar").not("#my_tariff").show();
			
			// ���ε� �� �޴��� ����Ʈ�� �Ķ���� list�� ����� ������ ���� ���� ����, HOT,NEW,ȿ���� �� ��������
			// ���ؼ��� DAO�� �ʿ���.
			var linq = window.DAO.linq._singletonInstance;
			
			var thumb = "thumb_phone";
			
			for(var i = 0 ; i < list.length ; i++)	{
				// ������ �̵��� ���� data�� �� ����� ��.
				var aPhoneRoot = $("<div class='" + thumb + "'></div>")
									.appendTo(
											$("<li></li>").data("PRODUCT_GRP_ID", list[i].PRODUCT_GRP_ID)
														  .data("PRODUCT_ID", list[i].DEFAULT_PRODUCT_ID)	// ��ǰ �����ǿ�														 
														  .data("MODEL_NICK_NAME", list[i].MODEL_NICK_NAME)	// ��ǰ �����ǿ�
											.appendTo(ul)
									);
				
				// �̹���--------------------------
				var image = $("<p class='thumb_img'><a href='#'><img id='phoneImg' src='" 
						+ (list[i].IMAGE_1 ? _ERUrlProductImgRootCS + list[i].IMAGE_1 : _ERUrlProductImgRootCS + "/160x280.png" ) 
						+ "' alt='" + list[i].IMAGE_1_ALT + "'></a></p>");
				image.appendTo(aPhoneRoot);
				
				// ���� ����Ʈ--------------------------
				var colorList = linq.getColorList(list[i].PRODUCT_GRP_ID);
				
				var divColorPart; // ���⿡ ����
				// 5�� �̻��� ��� class='etc' �߰�, �� ���� Ŭ���� ��� class='all' �߰�
				if(colorList.length > 5)	{
					divColorPart = $("<div class='color_choice etc'></div>");
				} else {
					divColorPart = $("<div class='color_choice'></div>");
				}
				
				divColorPart.append("<p class='btn_open'><a href='#'><span class='none'>�÷����� ��� ����</span></a></p>");
				var temp = $("<div class='color_list'></div>").appendTo(divColorPart);
				
				for(var j = 0 ; j < colorList.length ; j++)	{
					
					
					// default_color_seq �ΰ�� class=on
					// ����� ��� class=white>
					/*-20140722���� ǥ���� ������ �ٲ�  var colorTissue = $("<span class='" + (colorList[j].COLOR_SEQ == colorList[j].DEFAULT_COLOR_SEQ ? "on" : "") + "'><a href='#' " + (colorList[j].COLOR_HEX == "ffffff" || colorList[j].COLOR_HEX == "FFFFFF" ? "class='white'" : "") + " style='background:#" + colorList[j].COLOR_HEX + "' title='" + colorList[j].COLOR_NAME +"' product_id='" + colorList[j].PRODUCT_ID + "' color_seq='" + colorList[j].COLOR_SEQ + "'><span class='none'><span>" + colorList[j].PRODUCT_GRP_NM + " " + colorList[j].COLOR_NAME + "</span>�̸�����</span></a></span>");*/
					var colorTissue = $("<span class='" + (colorList[j].PRODUCT_ID == colorList[j].DEFAULT_PRODUCT_ID && colorList[j].COLOR_SEQ == colorList[j].DEFAULT_COLOR_SEQ ? "on" : "") + "'><a href='#' " + (colorList[j].COLOR_HEX == "ffffff" || colorList[j].COLOR_HEX == "FFFFFF" ? "class='white'" : "") + " style='background:#" + colorList[j].COLOR_HEX + "' title='" + colorList[j].COLOR_NAME +"'><span class='none'><span>" + colorList[j].PRODUCT_GRP_NM + " " + colorList[j].COLOR_NAME + "</span>�̸�����</span></a></span>");
					colorTissue
						.data("src", (colorList[j].IMAGE_1 ? _ERUrlProductImgRootCS + colorList[j].IMAGE_1 : _ERUrlProductImgRootCS + "/160x280.png"))
						.data("alt", colorList[j].IMAGE_1_ALT)
						.data("PRODUCT_ID", colorList[j].PRODUCT_ID)
						.data("COLOR_HEX", colorList[j].COLOR_HEX)
						.click(window.bind.phoneList_imgChange);
					temp.append(colorTissue)
					
					if(j != 4)	{	//5��° �ܿ��� �����Է�
						temp.append("&nbsp;");	
					}
				}
				if(colorList.length >= 5)	{
					divColorPart.append("<p class='btn_close'><a href='#'><span class='none'>�÷����� ��� �ݱ�</span></a></p>")
				}
				
				aPhoneRoot.append(divColorPart);	//�̸� append
				
				for(var j = 0 ; j < colorList.length ; j++)	{
					// Ŭ�� �̺�Ʈ �ɱ�
					if(colorList[j].PRODUCT_ID == colorList[j].DEFAULT_PRODUCT_ID && colorList[j].COLOR_SEQ == colorList[j].DEFAULT_COLOR_SEQ)	{
						temp.find(">span").eq(j).click();
					}					
				}
				
				// ��ǰ��--------------------------
				aPhoneRoot.append("<p class='thumb_tit'><a href='#'>" + list[i].PRODUCT_GRP_NM + "</a></p>");
				
				// ������-------------------------- �ݾ�
				if(list[i].JOB_STATUS == "42") {
					aPhoneRoot.append("<p class='thumb_txt'>���ݰ��� �غ� ��</p>");
				} else {
					aPhoneRoot.append("<p class='thumb_sum'><span>��" + setComma( list[i].SALE_PRICE < 0 ? "0" : list[i].SALE_PRICE ) + "</span>~" + (list[i].INSTALLMENT_TERM == 0 ? "" : "/Month") + "</p>");
				}
				
				// hotNew--------------------------
				var flag = $("<div class='flag'></div>").appendTo(aPhoneRoot)
				var flag_wrap = $("<div class='flag_wrap'></div>").appendTo(flag);
				var flag_list = $("<div class='flag_list'></div>").appendTo(flag_wrap);
				var flag_left = $("<div class='flag-left'></div>").appendTo(aPhoneRoot);
				// product_grp_id�� hot, new, ������, ȿ���� ��������
				var appendTail = false;
				var hotNewCcaList = linq.getHotNewCca(list[i].PRODUCT_GRP_ID);
				for(var j = 0 ; j < hotNewCcaList.length ; j++ )	{
					if(hotNewCcaList[j] == "H")	{
						flag_list.append("<p><span class='hot'>HOT</span></p>");
						appendTail = true;
					} else if(hotNewCcaList[j] == "N")	{
						flag_list.append("<p><span class='new'>NEW</span></p>");
						appendTail = true;
					} else if(hotNewCcaList[j] == "F")	{
						flag_list.append("<p><span class='clubT'>Ŭ��T</span></p>");
						appendTail = true;
					}
				}

				//�������÷���
				if(list[i].RESERVATION_YN == "Y"){
					flag_list.append("<p><span class='in-preorder'>������</span></p>");
					appendTail = true;
				}

				//�Ͻú��÷���
				if(list[i].SINGLEPAYMENT_YN == "Y"){
					flag_list.append("<p><span class='pay-once'>�Ͻú�</span></p>");
					appendTail = true;
				}
				
				if(appendTail == true)	{
					flag.append("<span class='flag_btm'>&nbsp;</span>");
				}
				
				if(list[i].LEFT_UPPER_FLAG == "01")	{
					 flag_left.append("<span class='sale'>Sale</span>");
				}
				
				if(list[i].LEFT_UPPER_FLAG == "02")	{
					flag_left.append("<span class='limited'>����</span>");
				}
				
				if(list[i].LEFT_UPPER_FLAG == "03")	{
					flag_left.append("<span class='event'>Event</span>");
				}
				
				if(list[i].JOB_STATUS == "42") {
				// ��ǰ���ϱ� üũ�ڽ�
					aPhoneRoot.append("<input type='checkbox' disabled name='comparison' id='' class='chk' title='" + list[i].PRODUCT_GRP_NM + " " + list[i].COLOR_NAME  + " ��ǰ SK�ڷ��� �޴��� ������ ���� ������ ������Ʈ ���Դϴ�.' PRODUCT_GRP_ID='" + list[i].PRODUCT_GRP_ID + "'>");
				}else{
					aPhoneRoot.append("<input type='checkbox' name='comparison' id='' class='chk' title='" + list[i].PRODUCT_GRP_NM + " " + list[i].COLOR_NAME  + " ��ǰ ���ϱ�' PRODUCT_GRP_ID='" + list[i].PRODUCT_GRP_ID + "'>");
				}
				
			}
		},
		/** window.bind.phoneList_imgChange
		 *  ���ε� Ÿ�� : �޴���/�º� ����Ʈ�� ����Ʈ ���� �޴���/�º� �̹��� 
		 *  ���� : color tissue �� Ŭ�������� �߻�
		 */
		phoneList_imgChange : function(e)	{
			$(this).parent().parent().parent().find("#phoneImg")
				.attr("src", $(this).data("src"))
				.attr("alt", $(this).data("alt"));	// �׸� �ٲٱ�
			
			$(this).parent().parent().parent().parent()
				.data("PRODUCT_ID", $(this).data("PRODUCT_ID"))
				.data("COLOR_HEX", $(this).data("COLOR_HEX"));	// ���� �� ��ǰ�� Ŭ������ ��쿡 �Ѱ��� �Ķ���͸� ����
		}
	}
})($);
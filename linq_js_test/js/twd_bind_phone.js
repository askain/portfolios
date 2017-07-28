//--------------------------------------------------------
//window.bind ----    START
//--------------------------------------------------------
(function($) {
	
	// 나는 한다. 바인딩.
	window.bind = {
		// ======================================================================================================
		// ------------------------------------------------------------------------------------------------------
		// ------------------------------------------------------------------------------------------------------
		// ------------------------------------------------------------------------------------------------------
		// 휴대폰
		// ------------------------------------------------------------------------------------------------------
		// ------------------------------------------------------------------------------------------------------
		// ------------------------------------------------------------------------------------------------------
		// ======================================================================================================
		/** window.bind.phoneList_categoryList
		 * 바인딩 타겟 : 휴대폰/태블릿 리스트의 카테고리 목록
		 * 설명 : 휴대폰/태블릿 리스트의 카테고리를 바인딩하고 마지막에 리스트를 나열하는 명령을 실행한다.
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
				category_layer.find("#cate01").click();	// 기선택된  카테고리가 없으면  첫번째 것을 선택
			}

			$('div.btn_wrap a.d-apply').click();
			
		},
		/** window.bind.phoneList_subcategory
		 *  바인딩 타겟 : 휴대폰/태블릿 리스트의 카테고리 목록 옆의 회사명 부분
		 */
		phoneList_subcategory : function(list, selector)	{
			var subcategory_layer = $("ul.maker_list");
			subcategory_layer.html("");
			
			var totalIndex = 1;
			
			// 회사명 subcategory 바인딩
			for(var i = 0 ; i < list.length ; i++, totalIndex++)	{
				subcategory_layer.append("<li><label for='total" + padZero(totalIndex,2) + "'><input type='checkbox' name='maker_total' id='total" + padZero(totalIndex,2) + "' class='chk' value='" + list[i].COMPANY_CODE + "'>" + list[i].COMPANY_NM + "</label></li>");
			}
		},
		/** window.bind.phoneList
		 *  바인딩 타겟 : 휴대폰/태블릿 리스트화면에서 가운데있는 휴대폰/태블릿 리스트
		 */
		phoneList : function(list, selector)	{
			var ul = $("ul.d-item-list").html("");
			if(list == null || list.length == 0)	{
				$(".pnone_list_none, .tablet_list_none").show().siblings().not(".option_bar").not("#my_tariff").hide();	// 현재 해당하는 휴대폰이 없습니다.
				return;
			}
			$(".pnone_list_none, .tablet_list_none").hide().siblings().not(".option_bar").not("#my_tariff").show();
			
			// 바인딩 할 휴대폰 리스트는 파라미터 list에 담겨져 있지만 세부 색상 정보, HOT,NEW,효도폰 을 가져오기
			// 위해서는 DAO가 필요함.
			var linq = window.DAO.linq._singletonInstance;
			
			var thumb = "thumb_phone";
			
			for(var i = 0 ; i < list.length ; i++)	{
				// 페이지 이동을 위해 data를 꼭 놓어야 함.
				var aPhoneRoot = $("<div class='" + thumb + "'></div>")
									.appendTo(
											$("<li></li>").data("PRODUCT_GRP_ID", list[i].PRODUCT_GRP_ID)
														  .data("PRODUCT_ID", list[i].DEFAULT_PRODUCT_ID)	// 상품 정보탭용														 
														  .data("MODEL_NICK_NAME", list[i].MODEL_NICK_NAME)	// 상품 정보탭용
											.appendTo(ul)
									);
				
				// 이미지--------------------------
				var image = $("<p class='thumb_img'><a href='#'><img id='phoneImg' src='" 
						+ (list[i].IMAGE_1 ? _ERUrlProductImgRootCS + list[i].IMAGE_1 : _ERUrlProductImgRootCS + "/160x280.png" ) 
						+ "' alt='" + list[i].IMAGE_1_ALT + "'></a></p>");
				image.appendTo(aPhoneRoot);
				
				// 색상 리스트--------------------------
				var colorList = linq.getColorList(list[i].PRODUCT_GRP_ID);
				
				var divColorPart; // 여기에 색상
				// 5개 이상일 경우 class='etc' 추가, 더 보기 클릭일 경우 class='all' 추가
				if(colorList.length > 5)	{
					divColorPart = $("<div class='color_choice etc'></div>");
				} else {
					divColorPart = $("<div class='color_choice'></div>");
				}
				
				divColorPart.append("<p class='btn_open'><a href='#'><span class='none'>컬러선택 모두 보기</span></a></p>");
				var temp = $("<div class='color_list'></div>").appendTo(divColorPart);
				
				for(var j = 0 ; j < colorList.length ; j++)	{
					
					
					// default_color_seq 인경우 class=on
					// 흰색인 경우 class=white>
					/*-20140722색상 표시의 기준을 바꿈  var colorTissue = $("<span class='" + (colorList[j].COLOR_SEQ == colorList[j].DEFAULT_COLOR_SEQ ? "on" : "") + "'><a href='#' " + (colorList[j].COLOR_HEX == "ffffff" || colorList[j].COLOR_HEX == "FFFFFF" ? "class='white'" : "") + " style='background:#" + colorList[j].COLOR_HEX + "' title='" + colorList[j].COLOR_NAME +"' product_id='" + colorList[j].PRODUCT_ID + "' color_seq='" + colorList[j].COLOR_SEQ + "'><span class='none'><span>" + colorList[j].PRODUCT_GRP_NM + " " + colorList[j].COLOR_NAME + "</span>미리보기</span></a></span>");*/
					var colorTissue = $("<span class='" + (colorList[j].PRODUCT_ID == colorList[j].DEFAULT_PRODUCT_ID && colorList[j].COLOR_SEQ == colorList[j].DEFAULT_COLOR_SEQ ? "on" : "") + "'><a href='#' " + (colorList[j].COLOR_HEX == "ffffff" || colorList[j].COLOR_HEX == "FFFFFF" ? "class='white'" : "") + " style='background:#" + colorList[j].COLOR_HEX + "' title='" + colorList[j].COLOR_NAME +"'><span class='none'><span>" + colorList[j].PRODUCT_GRP_NM + " " + colorList[j].COLOR_NAME + "</span>미리보기</span></a></span>");
					colorTissue
						.data("src", (colorList[j].IMAGE_1 ? _ERUrlProductImgRootCS + colorList[j].IMAGE_1 : _ERUrlProductImgRootCS + "/160x280.png"))
						.data("alt", colorList[j].IMAGE_1_ALT)
						.data("PRODUCT_ID", colorList[j].PRODUCT_ID)
						.data("COLOR_HEX", colorList[j].COLOR_HEX)
						.click(window.bind.phoneList_imgChange);
					temp.append(colorTissue)
					
					if(j != 4)	{	//5번째 외에는 공백입력
						temp.append("&nbsp;");	
					}
				}
				if(colorList.length >= 5)	{
					divColorPart.append("<p class='btn_close'><a href='#'><span class='none'>컬러선택 모두 닫기</span></a></p>")
				}
				
				aPhoneRoot.append(divColorPart);	//미리 append
				
				for(var j = 0 ; j < colorList.length ; j++)	{
					// 클릭 이벤트 걸기
					if(colorList[j].PRODUCT_ID == colorList[j].DEFAULT_PRODUCT_ID && colorList[j].COLOR_SEQ == colorList[j].DEFAULT_COLOR_SEQ)	{
						temp.find(">span").eq(j).click();
					}					
				}
				
				// 제품명--------------------------
				aPhoneRoot.append("<p class='thumb_tit'><a href='#'>" + list[i].PRODUCT_GRP_NM + "</a></p>");
				
				// 최저가-------------------------- 금액
				if(list[i].JOB_STATUS == "42") {
					aPhoneRoot.append("<p class='thumb_txt'>가격공시 준비 중</p>");
				} else {
					aPhoneRoot.append("<p class='thumb_sum'><span>￦" + setComma( list[i].SALE_PRICE < 0 ? "0" : list[i].SALE_PRICE ) + "</span>~" + (list[i].INSTALLMENT_TERM == 0 ? "" : "/Month") + "</p>");
				}
				
				// hotNew--------------------------
				var flag = $("<div class='flag'></div>").appendTo(aPhoneRoot)
				var flag_wrap = $("<div class='flag_wrap'></div>").appendTo(flag);
				var flag_list = $("<div class='flag_list'></div>").appendTo(flag_wrap);
				var flag_left = $("<div class='flag-left'></div>").appendTo(aPhoneRoot);
				// product_grp_id로 hot, new, 착한폰, 효도폰 가져오기
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
						flag_list.append("<p><span class='clubT'>클럽T</span></p>");
						appendTail = true;
					}
				}

				//예약중플래그
				if(list[i].RESERVATION_YN == "Y"){
					flag_list.append("<p><span class='in-preorder'>예약중</span></p>");
					appendTail = true;
				}

				//일시불플래그
				if(list[i].SINGLEPAYMENT_YN == "Y"){
					flag_list.append("<p><span class='pay-once'>일시불</span></p>");
					appendTail = true;
				}
				
				if(appendTail == true)	{
					flag.append("<span class='flag_btm'>&nbsp;</span>");
				}
				
				if(list[i].LEFT_UPPER_FLAG == "01")	{
					 flag_left.append("<span class='sale'>Sale</span>");
				}
				
				if(list[i].LEFT_UPPER_FLAG == "02")	{
					flag_left.append("<span class='limited'>한정</span>");
				}
				
				if(list[i].LEFT_UPPER_FLAG == "03")	{
					flag_left.append("<span class='event'>Event</span>");
				}
				
				if(list[i].JOB_STATUS == "42") {
				// 상품비교하기 체크박스
					aPhoneRoot.append("<input type='checkbox' disabled name='comparison' id='' class='chk' title='" + list[i].PRODUCT_GRP_NM + " " + list[i].COLOR_NAME  + " 상품 SK텔레콤 휴대폰 지원금 공지 정보를 업데이트 중입니다.' PRODUCT_GRP_ID='" + list[i].PRODUCT_GRP_ID + "'>");
				}else{
					aPhoneRoot.append("<input type='checkbox' name='comparison' id='' class='chk' title='" + list[i].PRODUCT_GRP_NM + " " + list[i].COLOR_NAME  + " 상품 비교하기' PRODUCT_GRP_ID='" + list[i].PRODUCT_GRP_ID + "'>");
				}
				
			}
		},
		/** window.bind.phoneList_imgChange
		 *  바인딩 타겟 : 휴대폰/태블릿 리스트의 리스트 안의 휴대폰/태블릿 이미지 
		 *  설명 : color tissue 를 클릭했을때 발생
		 */
		phoneList_imgChange : function(e)	{
			$(this).parent().parent().parent().find("#phoneImg")
				.attr("src", $(this).data("src"))
				.attr("alt", $(this).data("alt"));	// 그림 바꾸기
			
			$(this).parent().parent().parent().parent()
				.data("PRODUCT_ID", $(this).data("PRODUCT_ID"))
				.data("COLOR_HEX", $(this).data("COLOR_HEX"));	// 만일 이 제품을 클릭했을 경우에 넘겨줄 파라메터를 변경
		}
	}
})($);
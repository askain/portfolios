$(function() {
	
	// 사용자 preference 저장
	// 반드시 화면이 새로 열릴때만 사용한다.
	// window.bind 에서만 사용
	// window.userEvents 에서 사용 금지 : 저장된 값을 예측할 수 없음!
	window.userPreference = {
		init : function()	{
			// 카테고리 적용버튼 클릭 이벤트
			$("body").on("click", 'div.btn_wrap a.d-apply', function(e)	{
				// 선택된 카테고리를 가져옴
				var category_ul = $("ul.category_list");
				var category_id = category_ul.find("[class=on] input").val();
				if(category_id == "on") category_id = undefined;
				
				// 선택된 회사명/subcategory을 가져옴
				var subcategory_ul = $("ul.maker_list").find("input:checked");
				var subcategoryList = [];
				for(var i = 0 ; i < subcategory_ul.length ; i++ )	{
					subcategoryList[i] = subcategory_ul.get(i).value;
				}
				
				if(window.cs_Info.step == 0)	{
					window.userPreference.phone.CATEGORY_ID = category_id;
					window.userPreference.phone.SUBCATEGORY_IDs = subcategoryList;
				}
			});

			// x버튼 클릭 이벤트
			$("body").on("click", 'button.btnicon_delete', function(e)	{
				var subcategoryList;
				if(window.cs_Info.step == 0)	{
					subcategoryList = window.userPreference.phone.SUBCATEGORY_IDs;
				}
				
				// 해당 서브카테고리ID를 가져옴
				var subcategory_nm = $(this).parent().find("span").eq(0).text();
				var subcategory_id;
				var subcategory_li = $("ul.maker_list>li");
				for(var i = 0 ; i < subcategory_li.length ; i++ )	{
					if(subcategory_li.eq(i).html().indexOf(subcategory_nm) >= 0)	{
						subcategory_id = subcategory_li.eq(i).find("input").val();
						break;
					}
				}
				
				//삭제된 서브카테고리 제외
				var new_subscategoryList = [];
				for(var i = 0 ; i < subcategoryList.length ; i++)	{
					if(subcategoryList[i] == subcategory_id)	{
						
					} else {
						new_subscategoryList.push(subcategoryList[i]);
					}
				}
				
				if(window.cs_Info.step == 0)	{
					window.userPreference.phone.SUBCATEGORY_IDs = new_subscategoryList;
				}
			});
			
			// sorting 이벤트
			$("body").on("click", 'div.sort_wrap div.select_wrap ul.sort_list>li a', function(e)	{
				if(window.cs_Info.step == 0)	{
					window.userPreference.phone.SORT_INDEX = $(e.currentTarget).index("div.sort_wrap div.select_wrap ul.sort_list>li a");	
				}
			});
			
		},
		phone : {
			CATEGORY_ID : ""
			, SUBCATEGORY_IDs : []
			, SORT_INDEX : 0
			, PHONE_CAPACITY : null
			, reset : function()	{
				window.userPreference.phone.PHONE_CAPACITY = null;
			}
		},
		subscription : {
			CATEGORY_ID : ""
			, SUBCATEGORY_IDs : []
			, SORT_INDEX : 0
		},
		tgift : {
			CATEGORY_ID : ""
			, SUBCATEGORY_IDs : []
			, SORT_INDEX : 0
		}
	}
	window.userPreference.init();
});
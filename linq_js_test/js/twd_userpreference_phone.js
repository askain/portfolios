$(function() {
	
	// ����� preference ����
	// �ݵ�� ȭ���� ���� �������� ����Ѵ�.
	// window.bind ������ ���
	// window.userEvents ���� ��� ���� : ����� ���� ������ �� ����!
	window.userPreference = {
		init : function()	{
			// ī�װ� �����ư Ŭ�� �̺�Ʈ
			$("body").on("click", 'div.btn_wrap a.d-apply', function(e)	{
				// ���õ� ī�װ��� ������
				var category_ul = $("ul.category_list");
				var category_id = category_ul.find("[class=on] input").val();
				if(category_id == "on") category_id = undefined;
				
				// ���õ� ȸ���/subcategory�� ������
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

			// x��ư Ŭ�� �̺�Ʈ
			$("body").on("click", 'button.btnicon_delete', function(e)	{
				var subcategoryList;
				if(window.cs_Info.step == 0)	{
					subcategoryList = window.userPreference.phone.SUBCATEGORY_IDs;
				}
				
				// �ش� ����ī�װ�ID�� ������
				var subcategory_nm = $(this).parent().find("span").eq(0).text();
				var subcategory_id;
				var subcategory_li = $("ul.maker_list>li");
				for(var i = 0 ; i < subcategory_li.length ; i++ )	{
					if(subcategory_li.eq(i).html().indexOf(subcategory_nm) >= 0)	{
						subcategory_id = subcategory_li.eq(i).find("input").val();
						break;
					}
				}
				
				//������ ����ī�װ� ����
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
			
			// sorting �̺�Ʈ
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
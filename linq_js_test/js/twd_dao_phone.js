(function($) {
	
	//--------------------------------------------------------
	// window.DAO ----    START
	//--------------------------------------------------------
		window.DAO = {
		
			distinctThis		: "asds4r83902",
			// 순서지정
			orderByOrderSeq		: -1,	//노출순
			orderByPopularity	: 0,	//인기순
			orderByRegist		: 1,	//최신순
			orderByHighPrice	: 2,	//높은가격순
			orderByLowPrice		: 3,	//낮은가격순
			
			// 휴대폰 상세 용 순서시정
			orderByHighDc		: 9,	//DC많이 해주는 순
			
			// 요금제 용
			orderByLowData		: 5,	//DATA용량 적은 순
			orderByHighData		: 6,	//DATA용량 많은 순
			orderByLowTC		: 7,	//음성 적은순
			orderByHighTC		: 8,	//음성 많은 순
			
			// product_default_id 
			defaultId			: "7ybjhl",
			
	//--------------------------------------------------------
	// window.DAO.linq ----    START
	//--------------------------------------------------------
			linq : function(list)	{
				this.list = list;
				if ( arguments.callee._singletonInstance )	return arguments.callee._singletonInstance;
				
				arguments.callee._singletonInstance = this;
				
				// 상품 리스트, 상세를 가져옴
				// param   
				//		category_id			:
				//		product_grp_id		:
				//		product_id			: null 일경우 default_product_id
				//		color_seq			:
				//		color_hex			:
				//		company_code_list	: 제조사 코드 배열
				//		orderBy				: 
				//		entry_cd			: 가입유형
				//		commitment_term		: 약정할부 개월 수 
				//		installment_term	: 휴대폰 할부 개월수
				//		phone_capacity		: 용량
				this.getPhoneList = function(category_id, product_grp_id, product_id, color_seq, color_hex, company_code_list, orderBy, entry_cd, commitment_term, installment_term, phone_capacity)	{
							
					var ret = Enumerable.From(this.list);
					var linq_command_area_val = "Enumerable.From(window.DAO.linq._singletonInstance.list)";
					
					// product_id 가 defualt_product_id 인지 파악하는 것이 가장 중요하기 때문에 가장 먼저. 순서바꾸지 마시오!
					// product_id 적용
					if(product_id == window.DAO.distinctThis)	{
						ret = ret.Distinct(function(x)	{ 
					    	return x.PRODUCT_ID;
					    });
						linq_command_area_val += "\n\t.Distinct(function(x)	{"
												+ "\treturn x.PRODUCT_ID;"
												+ "\t})";
					} else if(product_id == window.DAO.defaultId) {
						ret = ret.Where(function(x)	{ 
							return x.DEFAULT_PRODUCT_ID == x.PRODUCT_ID; /*&& x.COLOR_SEQ == x.DEFAULT_COLOR_SEQ*/;	
						});
						linq_command_area_val += "\n\t.Where(function(x)	{"
												+ "\treturn x.DEFAULT_PRODUCT_ID == x.PRODUCT_ID;"
												+ "\t})";
					} else if(product_id)	{
						ret = ret.Where(function(x)	{ 
							return x.PRODUCT_ID == product_id;	
						});
						linq_command_area_val += "\n\t.Where(function(x)	{"
												+ "\treturn x.PRODUCT_ID == '" + product_id + "';"
												+ "\t})";
					} else {
						
					}
					
					// category_id 적용
					if(category_id)	{
						ret = ret.Where(function(x)	{ 
							return x.CATEGORY_ID == category_id;	
						});
						linq_command_area_val += "\n\t.Where(function(x)	{"
												+ "\treturn x.CATEGORY_ID == '" + category_id + "';"
												+ "\t})";
					} else {
						
					}
					
					// product_grp_id 적용
					if(product_grp_id == window.DAO.distinctThis)	{
						ret = ret.Distinct(function(x)	{ 
					    	return x.PRODUCT_GRP_ID;
					    });
						linq_command_area_val += "\n\t.Distinct(function(x)	{"
												+ "\treturn x.PRODUCT_GRP_ID;"
												+ "\t})";
					} else if(product_grp_id)	{
						ret = ret.Where(function(x)	{ 
							return x.PRODUCT_GRP_ID == product_grp_id;	
						});
						linq_command_area_val += "\n\t.Where(function(x)	{"
												+ "\treturn x.PRODUCT_GRP_ID == '" + product_grp_id + "';"
												+ "\t})";
					} else {

					}
					
					// 색상을 적용
					if(color_seq == window.DAO.defaultId)	{
						ret = ret.Where(function(x)	{ 
							return x.COLOR_SEQ == x.DEFAULT_COLOR_SEQ || x.DEFAULT_COLOR_SEQ == null;	
						});
						linq_command_area_val += "\n\t.Where(function(x)	{"
												+ "\treturn x.COLOR_SEQ == x.DEFAULT_COLOR_SEQ || x.DEFAULT_COLOR_SEQ == null;"
												+ "\t})";
					} else if(color_seq)	{
						ret = ret.Where(function(x)	{ 
							return x.COLOR_SEQ == color_seq;	
						});
						linq_command_area_val += "\n\t.Where(function(x)	{"
												+ "\treturn x.COLOR_SEQ == '" + color_seq + "';"
												+ "\t})";
					} else {
					
					}
					
					if(color_hex)	{
						ret = ret.Where(function(x)	{ 
							return x.COLOR_HEX == color_hex;	
						});
						linq_command_area_val += "\n\t.Where(function(x)	{"
												+ "\treturn x.COLOR_HEX == '" + color_hex + "';"
												+ "\t})";
					} else {
						
					}
					
					// 제조사 적용
					if(company_code_list != null && company_code_list.length > 0)	{
						ret = ret.Where(function(x)	{
							for(var i = 0 ; i < company_code_list.length ; i++)	{
								if(company_code_list[i] == x.COMPANY_CODE )	{
									return true;
								}
							}
							return false;
						});
						
						linq_command_area_val += "\n\t.Where(function(x)	{";
						for(var i = 0 ; i < company_code_list.length ; i++)	{
							linq_command_area_val += "\n\t\tif('" + company_code_list[i] + "' == x.COMPANY_CODE )	{"
													+ "\treturn true;"
													+ "\t\t}"
						}
						linq_command_area_val += "\n\t})";
						
					}
					
					// 가입유형 적용
					if(entry_cd)	{
						ret = ret.Where(function(x)	{ 
							return x.ENTRY_CD == entry_cd;	
						});
						linq_command_area_val += "\n\t.Where(function(x)	{"
												+ "\treturn x.ENTRY_CD == '" + entry_cd + "';"
												+ "\t})";
					} else {

					}
					
					if(commitment_term || commitment_term == 0)	{
						ret = ret.Where(function(x)	{ 
							return x.COMMITMENT_TERM == commitment_term;	
						});
						linq_command_area_val += "\n\t.Where(function(x)	{"
												+ "\treturn x.COMMITMENT_TERM == '" + commitment_term + "';"
												+ "\t})";
					} else {

					}
					
					if(installment_term || installment_term == 0)	{
						ret = ret.Where(function(x)	{ 
							return x.INSTALLMENT_TERM == installment_term;	
						});
						linq_command_area_val += "\n\t.Where(function(x)	{"
												+ "\treturn x.INSTALLMENT_TERM == '" + installment_term + "';"
												+ "\t})";
					} else {
	
					}
					
					if(phone_capacity)	{
						ret = ret.Where(function(x)	{ 
							return x.PHONE_CAPACITY == phone_capacity;	
						});
						linq_command_area_val += "\n\t.Where(function(x)	{"
												+ "\treturn x.PHONE_CAPACITY == '" + phone_capacity + "';"
												+ "\t})";
					} else {

					}
					
					//entry_cd, commitment_term, installment_term 가 지정되지 않으면 디폴트 child_id를 선택
					if(!entry_cd)	{
						if(!commitment_term && commitment_term != 0)	{
							if(!installment_term && installment_term != 0)	{
								// 디폴트 child_id를 가져옴
								temp = ret.Where(function(x)	{ 
									return x.DEFAULT_CHILD_YN == "Y"	
								});
								
								if(temp.Count() > 0)	{	// 디폴트 child_id가 있을경우
									ret = temp;
									linq_command_area_val += "\n\t.Where(function(x)	{"
												+ "\treturn x.DEFAULT_CHILD_YN == 'Y';"
												+ "\t})";
								}
							}
						}
					}
					
					// 나열 순서 지정
					if(orderBy == window.DAO.orderByOrderSeq)	{
						ret = ret.OrderBy(function (x) { 
							return x.ORDER_SEQ; 
						});
						linq_command_area_val += "\n\t.OrderBy(function(x)	{"
												+ "\treturn x.ORDER_SEQ;"
												+ "\t})";
					} else if(orderBy == window.DAO.orderByRegist)	{
						ret = ret.OrderByDescending(function (x) { 
							return x.REG_DATE; 
						});
						linq_command_area_val += "\n\t.OrderByDescending(function(x)	{"
												+ "\treturn x.REG_DATE;"
												+ "\t})";
					} else if(orderBy == window.DAO.orderByHighPrice)	{
						ret = ret.OrderBy(function (x) {
							return x.SALE_PRICE; 
						});
						linq_command_area_val += "\n\t.OrderBy(function(x)	{"
												+ "\treturn x.SALE_PRICE;"
												+ "\t})";
					} else if(orderBy == window.DAO.orderByLowPrice)	{
						ret = ret.OrderByDescending(function (x) { 
							return x.SALE_PRICE; 
						});
						linq_command_area_val += "\n\t.OrderByDescending(function(x)	{"
												+ "\treturn x.SALE_PRICE;"
												+ "\t})";
					} else if(orderBy == window.DAO.orderByHighDc)	{
						ret = ret.OrderByDescending(function (x) {
							return x.AID_AMT + x.AID_CHARGE_MONTH + x.BASIC_DC_AMT + x.SCRP_DC_AMT + x.CCA_DC_AMT + x.VIP_DC_AMT;
						});	
						linq_command_area_val += "\n\t.OrderByDescending(function(x)	{"
												+ "\treturn x.AID_AMT + x.AID_CHARGE_MONTH + x.BASIC_DC_AMT + x.SCRP_DC_AMT + x.CCA_DC_AMT + x.VIP_DC_AMT;"
												+ "\t})";
					} else if(orderBy == window.DAO.orderByPopularity) {
						ret = ret.OrderBy(function (x) {
							return x.O_ORDER_CNT; 
						});
						linq_command_area_val += "\n\t.OrderBy(function(x)	{"
												+ "\treturn x.O_ORDER_CNT;"
												+ "\t})";
					}
					
					$("#linq_command_area").val(linq_command_area_val);
					
					ret = ret.ToArray();
					return ret;
						
				};
					
				// 상품별 색상을 가져옴.
				// product_grp_id : 상품 그룹 ID
				// return : 색상 정보
				this.getColorList = function(product_grp_id)	{
					var ret = Enumerable.From(this.list).Where(function(x)	{ 
						return x.PRODUCT_GRP_ID == product_grp_id;	
						}
					).Distinct(function(x)	{ 
				    	/*-20140722색상 표시의 기준을 바꿈 return x.COLOR_SEQ && x.COLOR_NAME;*/
						return x.COLOR_HEX; 
				    })/* .Select(
						"val, index=>{PRODUCT_GRP_NM:val.PRODUCT_GRP_NM, PRODUCT_ID:val.PRODUCT_ID, COLOR_SEQ:val.COLOR_SEQ, COLOR_NAME:val.COLOR_NAME, COLOR_HEX:val.COLOR_HEX}"
					) */
				    .OrderByDescending(function(x)	{
				    	return x.COLOR_HEX;
				    })
				    
				    .ToArray();
					
					return ret;
				};
				
				// 상품별 용량 가져옴.
				// product_grp_id : 상품 그룹 ID
				// return : 용량 정보
				this.getCapacityList = function(product_grp_id, color_hex)	{
					var ret = Enumerable.From(this.list);
					

					if(product_grp_id)	{
						ret = ret.Where(function(x)	{ 
							return x.PRODUCT_GRP_ID == product_grp_id;	
							}
						);
					}
					
					if(color_hex)	{
						ret = ret.Where(function(x)	{ 
							return x.COLOR_HEX == color_hex;	
							}
						);
					}
					
					
					ret = ret.Distinct(function(x)	{ 
				    	return x.PHONE_CAPACITY && x.PRODUCT_ID;
				    }).OrderBy(function(x)	{
				    	return parseInt(x.PHONE_CAPACITY.replace("G", ""));
				    }).ToArray();
					
					return ret;
				};
				
				// T할부지원,T기본약정 개월 수 가져옴.
				// product_grp_id : 상품 그룹 ID
				// return : 개월수 정보
				this.getCommitmentList = function(product_grp_id, product_id, entry_cd)	{
					var ret = Enumerable.From(this.list);
					
					if(product_grp_id)	{
						ret = ret.Where(function(x)	{ 
							return x.PRODUCT_GRP_ID == product_grp_id;	
							}
						);
					}
					
					if(product_id)	{
						ret = ret.Where(function(x)	{ 
							return x.PRODUCT_ID == product_id;	
							}
						);
					}
					
					if(entry_cd)	{
						ret = ret.Where(function(x)	{ 
							return x.ENTRY_CD == entry_cd;	
							}
						);
					}
					
					ret = ret.Distinct(function(x)	{ 
				    	return x.COMMITMENT_TERM; 
				    }).OrderByDescending(function(x)	{
				    	return x.COMMITMENT_TERM; 
				    }).ToArray();
					
					return ret;
				};
				
				// 할부 개월 수 가져옴.
				// product_grp_id : 상품 그룹 ID
				// return : 개월수 정보
				this.getInstallmentList = function(product_grp_id, product_id, entry_cd, commitment_term)	{
					var ret = Enumerable.From(this.list);
					
					if(product_grp_id)	{
						ret = ret.Where(function(x)	{ 
							return x.PRODUCT_GRP_ID == product_grp_id;	
							}
						);
					}
					
					if(product_id)	{
						ret = ret.Where(function(x)	{ 
							return x.PRODUCT_ID == product_id;	
							}
						);
					}

					if(entry_cd)	{
						ret = ret.Where(function(x)	{ 
							return x.ENTRY_CD == entry_cd;	
							}
						);
					}
					
					if(commitment_term)	{
						ret = ret.Where(function(x)	{ 
							return x.COMMITMENT_TERM == commitment_term;	
							}
						);
					}
					
					ret = ret.Distinct(function(x)	{
				    	return x.INSTALLMENT_TERM;
				    }).OrderByDescending(function(x)	{
				    	return x.INSTALLMENT_TERM;
				    }).ToArray();
					
					return ret;
				};
				
				
				
				//제조사 전체를 가져옴
				this.getCompanyList = function()	{
					var ret = Enumerable.From(this.list).Distinct(function(x)	{ 
					    return x.COMPANY_CODE; 
					}).OrderBy(function(x)	{
						return x.COMPANY_CODE;
					}).Select(
						"val, index=>{COMPANY_CODE:val.COMPANY_CODE, COMPANY_NM:val.COMPANY_NM}"
					).ToArray();

					return ret;
				};

				// product_grp_id로 hot, new, 착한폰, 효도폰 가져오기
				this.getHotNewCca = function(product_grp_id)	{
					var ret = new Array();
						
					var src = Enumerable.From(this.list).Where(function(x)	{ 
						return x.PRODUCT_GRP_ID == product_grp_id;	
						}
					)
			 			
					// hot new 중 무엇인가? 착한기변폰이 있는가? 효도 폰인가?
					var first = null;
					try	{
						first = src.First();

						// H 든 N 이든 넣고 봄
						ret.push(first.HOT_NEW_CD);
						
						// 효보폰 여부를 검색
						if(first.FILIAL_DUTY_YN == "Y")	{
							ret.push("F");
						}
						
											
					}
					catch(e)	{
						// do nothing
					}
					
					first = src.FirstOrDefault(null, function(x)	{ return x.CCA_YN == "Y"; })	//착한 기변 / LITE
					
					if(first)	{
						ret.push("C");
					}

					return ret;
				};
				
				// 가입유형 가져옴.
				// product_grp_id : 상품 그룹 ID
				// return : 가입유형 정보
				this.getEntryList = function(product_grp_id)	{
					var ret = Enumerable.From(this.list).Where(function(x)	{ 
						return x.PRODUCT_GRP_ID == product_grp_id;	
						}
					).Distinct(function(x)	{ 
				    	return x.ENTRY_CD; 
				    }).OrderBy(function(x)	{
				    	return x.ENTRY_NM
				    }).ToArray();

					return ret;
				};
				
			}
	//--------------------------------------------------------
	// window.DAO.linq ----    END
	//--------------------------------------------------------	

		};
	//--------------------------------------------------------
	// window.DAO ----    END
	//--------------------------------------------------------
})($);

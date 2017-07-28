(function($) {
	
	//--------------------------------------------------------
	// window.DAO ----    START
	//--------------------------------------------------------
		window.DAO = {
		
			distinctThis		: "asds4r83902",
			// ��������
			orderByOrderSeq		: -1,	//�����
			orderByPopularity	: 0,	//�α��
			orderByRegist		: 1,	//�ֽż�
			orderByHighPrice	: 2,	//�������ݼ�
			orderByLowPrice		: 3,	//�������ݼ�
			
			// �޴��� �� �� ��������
			orderByHighDc		: 9,	//DC���� ���ִ� ��
			
			// ����� ��
			orderByLowData		: 5,	//DATA�뷮 ���� ��
			orderByHighData		: 6,	//DATA�뷮 ���� ��
			orderByLowTC		: 7,	//���� ������
			orderByHighTC		: 8,	//���� ���� ��
			
			// product_default_id 
			defaultId			: "7ybjhl",
			
	//--------------------------------------------------------
	// window.DAO.linq ----    START
	//--------------------------------------------------------
			linq : function(list)	{
				this.list = list;
				if ( arguments.callee._singletonInstance )	return arguments.callee._singletonInstance;
				
				arguments.callee._singletonInstance = this;
				
				// ��ǰ ����Ʈ, �󼼸� ������
				// param   
				//		category_id			:
				//		product_grp_id		:
				//		product_id			: null �ϰ�� default_product_id
				//		color_seq			:
				//		color_hex			:
				//		company_code_list	: ������ �ڵ� �迭
				//		orderBy				: 
				//		entry_cd			: ��������
				//		commitment_term		: �����Һ� ���� �� 
				//		installment_term	: �޴��� �Һ� ������
				//		phone_capacity		: �뷮
				this.getPhoneList = function(category_id, product_grp_id, product_id, color_seq, color_hex, company_code_list, orderBy, entry_cd, commitment_term, installment_term, phone_capacity)	{
							
					var ret = Enumerable.From(this.list);
					var linq_command_area_val = "Enumerable.From(window.DAO.linq._singletonInstance.list)";
					
					// product_id �� defualt_product_id ���� �ľ��ϴ� ���� ���� �߿��ϱ� ������ ���� ����. �����ٲ��� ���ÿ�!
					// product_id ����
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
					
					// category_id ����
					if(category_id)	{
						ret = ret.Where(function(x)	{ 
							return x.CATEGORY_ID == category_id;	
						});
						linq_command_area_val += "\n\t.Where(function(x)	{"
												+ "\treturn x.CATEGORY_ID == '" + category_id + "';"
												+ "\t})";
					} else {
						
					}
					
					// product_grp_id ����
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
					
					// ������ ����
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
					
					// ������ ����
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
					
					// �������� ����
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
					
					//entry_cd, commitment_term, installment_term �� �������� ������ ����Ʈ child_id�� ����
					if(!entry_cd)	{
						if(!commitment_term && commitment_term != 0)	{
							if(!installment_term && installment_term != 0)	{
								// ����Ʈ child_id�� ������
								temp = ret.Where(function(x)	{ 
									return x.DEFAULT_CHILD_YN == "Y"	
								});
								
								if(temp.Count() > 0)	{	// ����Ʈ child_id�� �������
									ret = temp;
									linq_command_area_val += "\n\t.Where(function(x)	{"
												+ "\treturn x.DEFAULT_CHILD_YN == 'Y';"
												+ "\t})";
								}
							}
						}
					}
					
					// ���� ���� ����
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
					
				// ��ǰ�� ������ ������.
				// product_grp_id : ��ǰ �׷� ID
				// return : ���� ����
				this.getColorList = function(product_grp_id)	{
					var ret = Enumerable.From(this.list).Where(function(x)	{ 
						return x.PRODUCT_GRP_ID == product_grp_id;	
						}
					).Distinct(function(x)	{ 
				    	/*-20140722���� ǥ���� ������ �ٲ� return x.COLOR_SEQ && x.COLOR_NAME;*/
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
				
				// ��ǰ�� �뷮 ������.
				// product_grp_id : ��ǰ �׷� ID
				// return : �뷮 ����
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
				
				// T�Һ�����,T�⺻���� ���� �� ������.
				// product_grp_id : ��ǰ �׷� ID
				// return : ������ ����
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
				
				// �Һ� ���� �� ������.
				// product_grp_id : ��ǰ �׷� ID
				// return : ������ ����
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
				
				
				
				//������ ��ü�� ������
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

				// product_grp_id�� hot, new, ������, ȿ���� ��������
				this.getHotNewCca = function(product_grp_id)	{
					var ret = new Array();
						
					var src = Enumerable.From(this.list).Where(function(x)	{ 
						return x.PRODUCT_GRP_ID == product_grp_id;	
						}
					)
			 			
					// hot new �� �����ΰ�? ���ѱ⺯���� �ִ°�? ȿ�� ���ΰ�?
					var first = null;
					try	{
						first = src.First();

						// H �� N �̵� �ְ� ��
						ret.push(first.HOT_NEW_CD);
						
						// ȿ���� ���θ� �˻�
						if(first.FILIAL_DUTY_YN == "Y")	{
							ret.push("F");
						}
						
											
					}
					catch(e)	{
						// do nothing
					}
					
					first = src.FirstOrDefault(null, function(x)	{ return x.CCA_YN == "Y"; })	//���� �⺯ / LITE
					
					if(first)	{
						ret.push("C");
					}

					return ret;
				};
				
				// �������� ������.
				// product_grp_id : ��ǰ �׷� ID
				// return : �������� ����
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

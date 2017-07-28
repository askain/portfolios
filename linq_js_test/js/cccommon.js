/**
 * Window Open <br>
 * Window�Ӽ� - ȭ�鰡� ��ġ �ϸ� resizable=yes, status=yes, toolbar=no, menubar=no
 * @param url  window�� URL
 * @param name  Window�� ��
 * @param widht window�� (�ȼ�)
 * @param height window���� (�ȼ�)
 * @param scroll scrollbar ���� (yes OR no)
 * @return window object
 */
// openwindow(strURL, "PhotoFind", width=302,height=132, 'no');
function openwindow(mypage, myname, w, h, scroll) {
	var winl = (screen.width - w) / 2;
	var wint = (screen.height - h) / 2;
	winprops = 'height='+h+',width='+w+',top='+wint+',left='+winl+',scrollbars='+scroll+',resizable'
	win = window.open(mypage, myname, winprops)
	if (parseInt(navigator.appVersion) >= 4) { win.window.focus(); }
}

// �Էµ� �� �� ���ڿܿ� ����
function f_onlyNumber(obj){
	if ((event.keyCode<48) || (event.keyCode>57)) event.returnValue=false;
	val=obj.value;
	re=/[^0-9]/gi;
	obj.value=val.replace(re,"");
	//<input type='text' name='htxtNum' onkeypress='SetNum(this)' onblur='SetNum(this)'>	
}

// ��� �������� üũ
function isNumber(str) {
	var i;
	//var	str = obj.value.trim();

	if (str.length == 0)
		return false;

	for (var i=0; i < str.length; i++) {
		if(!('0' <= str.charAt(i) && str.charAt(i) <= '9'))
			return false;
	}
	return true;
}

// �� ���ڶ� �������� üũ
function isNumber1(obj) {
	var i;
	var	str	=	obj.value.trim();

	if (str.length == 0)
		return false;

	for (var i=0; i < str.length; i++) {
		if('0' <= str.charAt(i) && str.charAt(i) <= '9')
			return true;
	}
	return false;
}

// ����üũ
function hasSpace(obj) {
	return IsSpace(obj);
}

// ����üũ
function IsSpace(obj) {
	var i;
	var	str	=	obj.value.trim();

	for(i = 0; i < str.length; i++) {
		if(str.charAt(i) == ' ')
			return true;
	}
	return false;
}

// �ֹι�ȣ �˻�
function isSSN(front, back) {
	var hap = 0;
	var	temp;
	
	for (var i=0; i < 6; i++) {
		var temp = front.charAt(i) * (i+2);
		hap += temp;
	}

	var n1 = back.charAt(0);
	var n2 = back.charAt(1);
	var n3 = back.charAt(2);
	var n4 = back.charAt(3);
	var n5 = back.charAt(4);
	var n6 = back.charAt(5);
	var n7 = back.charAt(6);

	hap += n1*8+n2*9+n3*2+n4*3+n5*4+n6*5;
	hap %= 11;
	hap = 11 - hap;
	hap %= 10;

	if(hap != n7)
		return false;

	return true;
}

// �̸��� üũ
function isEmail(obj) {
	var str = obj.value.trim();

	if(str == "")
		return false;

	var i = str.indexOf("@");
	if(i < 0)
		return false;

	i = str.indexOf(".");
	if(i < 0)
		return false;

	return true;
}


// ���ĺ� ���� üũ
function isAlphabet(obj) {
	var str = obj.value.trim();

	if(str.length == 0)
		return false;

	str = str.toUpperCase();
	for(var i=0; i < str.length; i++) {
		if(!(('A' <= str.charAt(i) && str.charAt(i) <= 'Z') || ('a' <= str.charAt(i) && str.charAt(i) <= 'z')))
			return false;
	}
	return true;
}

// �ѹ��ڶ� ���ĺ����� ���� üũ
function isAlphabet1(obj) {
	var str = obj.value.trim();

	if(str.length == 0)
		return false;

	str = str.toUpperCase();
	for(var i=0; i < str.length; i++) {
		if(('A' <= str.charAt(i) && str.charAt(i) <= 'Z') || ('a' <= str.charAt(i) && str.charAt(i) <= 'z'))
			return true;
	}
	return false;
}


// ���ĺ��� space���� ���� üũ
function isAlphabet2(obj) {
	var str = obj.value.trim();

	if(str.length == 0)
		return false;

	str = str.toUpperCase();
	for(var i=0; i < str.length; i++) {
		if(('A' <= str.charAt(i) && str.charAt(i) <= 'Z') || ('a' <= str.charAt(i) && str.charAt(i) <= 'z') || (str.charAt(i) == ' '))
			return true;
	}
	return false;
}

// �ΰ��� ������ üũ
function isSame(obj1, obj2) {
	var str1 = obj1.value;
	var str2 = obj2.value;

	if(str1.length == 0 || str2.length == 0)
		return false;

	if(str1 == str2)
		return true;
	return false;
}

// ID �� ���ڿ� ����, - , _ �� ���� üũ
function IsID(obj)
{
	obj = obj.toUpperCase();

	for(var i=1; i < obj.length; i++)
	{
		if(!(('A' <= obj.charAt(i) && obj.charAt(i) <= 'Z') ||
			('0' <= obj.charAt(i) && obj.charAt(i) <= '9') ||
			(obj.charAt(i) == '_') || (obj.charAt(i) == '-')))
			return false;
	}
	return true;
}

// Ư������ üũ
function isSpecial(obj) {
	var m_Sp = /[$\\@\\\#%\^\&\*\(\)\[\]\+\_\{\}\'\~\=\|]/;
	var m_val  = obj.value.trim();

	if(m_val.length == 0)
		return false;

	var strLen = m_val.length;
	var m_char = m_val.charAt((strLen) - 1);
	if(m_char.search(m_Sp) != -1)
		return true;

}

// ����, - �� ���� üũ
function IsTel(obj) {
	var i;
	var	str	=	obj.value.trim();

	for(i = 0; i < str.length; i++) {
		if(!('0' <= str.charAt(i) && str.charAt(i) <= '9') && str.charAt(i) != '-')
		{
			return false;
		}
	}
	return true;
}

// ����, , �� ���� üũ
function IsCurrency(obj) {
	var i;
	var	str	=	obj.value.trim();

	for(i = 0; i < str.length; i++) {
		if(!('0' <= str.charAt(i) && str.charAt(i) <= '9') && str.charAt(i) != ',')
		{
			return false;
		}
	}
	return true;
}

// ���ڸ� ���� (onKeyDown="onlyNumber()")
function onlyNumber() {
	if((event.keyCode != 9 && event.keyCode != 8 && event.keyCode != 46) && (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105))
	{
		event.returnValue=false;
	}
}

// 3�ڸ� ���� �޸�(,) ����ִ� ���
function setComma(num) {
	/*var num = num.toString();
	var rtn = "";
	var val = "";
	var j = 0;
	x = num.length;

	for(i=x; i>0; i--) {
		if(num.substring(i,i-1) != ",") 
			val = num.substring(i, i-1) + val;
	}
	x = val.length;
	for(i=x; i>0; i--) {
		if(j%3 == 0 && j!=0) 
			rtn = val.substring(i,i-1)+","+rtn; 
		else 
			rtn = val.substring(i,i-1)+rtn;

		j++;
	}

	return rtn;*/
	
	return Math.floor(parseInt(num)).toLocaleString().split(".")[0];
}

/**
 * �޸� ���� 
 */
function removeComma(str) {
    return(str.replace(/,/g,''));
}

/**
 * üũ�ڽ��� ������ư�� üũ ���¸� Ȯ���Ѵ�.
 * @param 	check_list
 * @return	���� : true
 *          �̼��� : false
 */
function IsChecked(check_list) {
	var is_checked = false;
	var length;

	length = check_list.length;

	if (length > 0) {
		for (i = 0; i < length; i++) {
			if (check_list[i].checked) {
				is_checked = true;
				break;
			}
		}
	} else {
		if (check_list.checked)
		{
			is_checked = true;
		}
	}

	return is_checked;
}

function getCheckedValue(obj) {
	return GetCheckedValue(obj);
}

/**
 * üũ�ڽ��� ������ư�� üũ�� ���� �����´�.
 * @param 	check_list
 * @return	���� : value
 *          �̼��� : ''
 */
function GetCheckedValue(check_list) {
	var sRetVal = '';
	var length;

	if (!check_list) return '';

	length = check_list.length;

	if (length > 0)
	{
		for (i = 0; i < length; i++)
		{
			if (check_list[i].checked)
			{
				sRetVal = check_list[i].value;
				break;
			}
		}
	}
	else
	{
		if (check_list.checked)
		{
			sRetVal = check_list.value;
		}
	}

	return sRetVal;
}

/**
 * üũ�ڽ��� ������ư�� üũ�� �ʱ�ȭ�Ѵ�.
 * @param 	check_list
 * @bFlag 	TRUE/FALSE
 */
function SetCheckedValue(check_list, bFlag)
{
	var length;

	if (!check_list) {
		check_list.checked = bFlag;
		return '';
	}

	length = check_list.length;

	if (length > 0)
	{
		length = check_list.length;

		if (length > 0)
		{
			for (i = 0; i < length; i++)
			{
				check_list[i].checked = bFlag
			}
		}
		else
		{
			check_list.checked = false;
		}
	}
}

// ����Ʈ�ڽ��� ���õ� ���� ��´�.
function getSelectedValue(obj) {
	if (obj.seelctedIndex == -1) return '';
	return obj.options[obj.selectedIndex].value;
}

/**
 * üũ�ڽ��� ������ư�� disabled �� �ʱ�ȭ�Ѵ�..
 * @param 	check_list
 * @bFlag 	TRUE/FALSE
 */
function SetCheckedDisabled(check_list, bFlag)
{
	var length;

	if (!check_list) {
		check_list.disabled = bFlag;
		return '';
	}

	length = check_list.length;

	if (length > 0)
	{
		length = check_list.length;

		if (length > 0)
		{
			for (i = 0; i < length; i++)
			{
				check_list[i].disabled = bFlag
			}
		}
		else
		{
			check_list.disabled = false;
		}
	}
}

// ��ȭ��ȣ check
function dispTelNo (strval) {
	var nRet = true;
	var	s, m, e;

	if (strval.length != 12)
		nRet = false;

	s = strval.mid(0,4);
	m = strval.mid(4,4);
	e = strval.mid(8,4);

	if (nRet)
		document.write (s+'-'+m+'-'+e);
	else
		document.write (strval);

	return nRet;
}

// �����ȣ check
function dispZipNo (strval) {
	var nRet = true;
	var	s, e;

	if (strval.length != 6)
		nRet = false;

	s = strval.mid(0,3);
	e = strval.mid(3,3);

	if (nRet)
		document.write (s+'-'+e);
	else
		document.write (strval);

	return nRet;
}

// ��ȭ��ȣ ���� return
function makeTelNo (strval) {
	var telno;
	telno = strval.arrSplit('-');

	if (telno.length < 3)
		return strval;

	telno[0] = '0000' + telno[0];
	telno[1] = '0000' + telno[1];
	telno[2] = '0000' + telno[2];

	return telno[0].right(4)+telno[1].right(4)+telno[2].right(4);

}

// ��ȭ��ȣ ������ȣ return
function makeTelDDD (strval) {
	if (strval.trim() == '')
		return '';

	var telno, retval = '';
	telno = strval.arrSplit('-');

	if (telno.length > 0  && telno.length < 3)
	{
		if (telno[0].left(1) != '0') {
			retval = '02'
			for (i=0;i<telno.length;i++)
				retval = retval +'-'+ telno[i];
		}
	}

	if (retval == '')
		retval = strval;

	return retval;
}

// �����ȣ ���� return
function makeZip (strval) {
	var zipno

	zipno = strval.arrSplit('-');
	if (zipno.length != 2)
		return strval;

	return zipno[0] + zipno[1];

}

// ������ ��ȯ return
function makeInt (strval) {
	var num, retNum = '';
	num = strval.arrSplit(',');

	for (var i=0; i < num.length ;i++) {
		retNum = retNum + num[i];
	}

	return retNum;

}

// ���ڿ� �������� ����(...)ó��
function dispLeftStr (strval, length) {
	var retval;

	var aaa = strval.arrSplit('\n')

	alert (aaa.length);

	retval = strval.left (length) + '...';
	document.write(retval);
}

//�ѱ� ���� üũ
function getLength(str) {
	return GetLength(str);
}

//�ѱ� ���� üũ
function GetLength(sText) {
	var i;
	var nLength = 0;

	for (i = 0; i < sText.length; i++) 	{
		if (sText.charCodeAt(i) > 128)
			nLength	+= 2;
		else
			nLength	++;
	}

	return nLength;
}

var s1_YesNULL	= '0';
var s1_NoNULL	= '1';
var s2_YesNUM	= '0';
var s2_NoNUM	= '1';
var s2_AllNUM	= '2';
var s3_YesABC	= '0';
var s3_NoABC	= '1';
var s3_AllABC	= '2';

/**
 * CheckTextBoxEx üũ
 * @param 	javaFlag	:	javascript flag (Y or N)
 * @param 	sChkType	:	s1_NoNULL + s2_NoNUM + s3_AllABC ���� ���·� ���յȴ�.
 * 							s1_YesNULL		: TextBox�� Space �� �����ִ�.
 * 							s1_NoNULL		: TextBox�� Space �� ��������.
 * 							s2_YesNUM		: TextBox�� ���ڰ� �����ִ�.
 * 							s2_NoNUM		: TextBox�� ���ڰ� ��������.
 * 							s2_AllNUM		: TextBox�� ���ڸ� �����ִ�.
 * 							s3_YesABC		: TextBox�� ���ĺ��� �����ִ�.
 * 							s3_NoABC		: TextBox�� ���ĺ��� ��������.
 * 							s3_AllABC		: TextBox�� ���ĺ��� �����ִ�.
 * @param 	nLength		:	text length
 * @param 	obj			:	text object
 * @param 	sMsg		:	ErrorString
 * @return 	true or false
 */
function CheckTextBoxEx (javaFlag, sChkType, nLength, obj, sMsg) {

	if (!obj)
	{
		alert ("["+ sMsg + "] Text Object Not Found...");
		return false;
	}

	if (sChkType.length != 3)
	{
		alert ("sChkType Error...!!!");
		return false;
	}

	var	bChk = true;

	if (javaFlag == 'Y') {
	// �ڹٽ�ũ��Ʈ üũ �÷��� TRUE

		if (obj.value.trim().length == 0) {
			alert ("["+sMsg+"]" + " �׸��� �Է��ϼ���.")
			obj.focus();
			return false;
		}
	} else {
	// �ڹٽ�ũ��Ʈ üũ �÷��� FALSE
		if (obj.value.length == 0) {
		// �Է¹��� �ȾҴٸ�
			bChk = false;
		}
	}


	if (bChk) {

		if (GetLength(obj.value) > nLength)
		{
			alert ( "["+sMsg+"]" + " �׸��� ���̴� " + nLength + " ���� �۾ƾ� �մϴ�.")
			obj.focus();
			return false;
		}

		if (sChkType.substring(0,1) == s1_NoNULL)		// ���� üũ
		{
			if (IsSpace(obj)) {
			// ���ڰ� �ԷµǾ����� üũ
				alert ("["+sMsg+"]" + " �׸񿡴� ������ ���� �����ϴ�.");
				obj.focus();
				return false;
			}
		}

		if (sChkType.substring(1,2) == s2_NoNUM)		// ����üũ
		{
			if (isNumber1(obj))
			{
			// ���ڰ� �ԷµǾ����� üũ
				alert ("["+sMsg+"]"+" �׸񿡴� ���ڸ� �Է��� �� �����ϴ�.");
				obj.focus();
				return false;
			}

		}

		if (sChkType.substring(1,2) == s2_AllNUM)		// ����üũ
		{
			if (!isNumber(obj))
			{
			// ���ڰ� �ԷµǾ����� üũ
				alert ("["+sMsg+"]"+" �׸��� ���ڸ� �Է°����մϴ�.");
				obj.focus();
				return false;
			}

		}

		if (sChkType.substring(2,3) == s3_NoABC)		// ���ĺ�üũ
		{
			if (isAlphabet1(obj))
			{
			// ���ĺ��� �ԷµǾ����� üũ
				alert ("["+sMsg+"]"+" �׸񿡴� ���ĺ��� �Է��� �� �����ϴ�.");
				obj.focus();
				return false;
			}
		}

		if (sChkType.substring(2,3) == s3_AllABC)		// ���ĺ�üũ
		{
			if (!isAlphabet(obj))
			{
			// ���ڰ� �ԷµǾ����� üũ
				alert ("["+sMsg+"]"+" �׸��� ���ĺ��� �Է°����մϴ�.");
				obj.focus();
				return false;
			}
		}

	}

	return true;
}

/**
 * Radio, Check üũ
 * @param 	javaFlag	:	javascript flag (Y or N)
 * @param 	obj			:	radio, check object
 * @return 	true or false
 */
function CheckRadioObject (javaFlag, obj, sMsg) {

	if (!obj)
	{
		alert ("["+ sMsg + "] objRadio Object Not Found...");
		return false;
	}

	if (javaFlag == 'Y') {
		if (!IsChecked(obj))
		{
			alert ("["+sMsg+"]" + " �׸��� �����ϼ���.")
			return false;
		}
	}

	return true;
}

/**
 * Combo üũ
 * @param 	javaFlag		:	javascript flag (Y or N)
 * @param 	obj		:	combo object
 * @return 	true or false
 */
function CheckComboObject (javaFlag, obj, sMsg) {

	if (!obj)
	{
		alert ("["+ sMsg + "] objCombo Object Not Found...");
		return false;
	}

	if (javaFlag == 'Y') {
	// �ڹٽ�ũ��Ʈ üũ �÷��� TRUE
		if (obj.options[obj.selectedIndex].value.length == 0) {
			alert ("["+sMsg+"]" + " �׸��� �����ϼ���.")
			obj.focus();
			return false;
		}
	}

	return true;
}

//---------------------------------------------- TRIM() ���� Function START ----------------------------------------------
function _private_arrSplit(split) {
	var tmpStr;
	var i ;
	var iCnt;
	var iEnd;
	tmpStr = this;

	iCnt = 0;
	for( i = 0 ; i < tmpStr.length ; i++) {
		if (tmpStr.charAt(i) == split) {
			iCnt++;
		}
	}
	iCnt++;

	arr_str = new Array(iCnt);

	for (i = 0 ; i < iCnt ; i++)	{
		iEnd = tmpStr.indexOf(split);
		if (iEnd < 0)
			arr_str[i] = tmpStr;
		else{
			arr_str[i] = tmpStr.substring(0,iEnd);
			tmpStr = tmpStr.substring(iEnd+1);
		}
	}

	return arr_str;
}

function _private_trim() {
	var tmpStr, atChar;
	tmpStr = this;

	if (tmpStr.length > 0) atChar = tmpStr.charAt(0);
	while (_private_stringvb_isSpace(atChar)) {
		tmpStr = tmpStr.substring(1, tmpStr.length);
		atChar = tmpStr.charAt(0);
	}
	if (tmpStr.length > 0) atChar = tmpStr.charAt(tmpStr.length-1);
	while (_private_stringvb_isSpace(atChar)) {
		tmpStr = tmpStr.substring(0,( tmpStr.length-1));
		atChar = tmpStr.charAt(tmpStr.length-1);
	}
	return tmpStr;
}

function _private_left(inLen) {
	return this.substring(0,inLen);
}

function _private_right(inLen) {
	return this.substring((this.length-inLen),this.length);
}

function _private_mid(inStart,inLen) {
	var iEnd;
	if (!inLen)
		iEnd = this.length;
	else
		iEnd = inStart + inLen;
	return this.substring(inStart,iEnd);
}

function _private_stringvb_isSpace(inChar) {
	return (inChar == ' ' || inChar == '\t' || inChar == '\n');
}


//����ڵ�Ϲ�ȣ üũ
function check_busino(vencod) {
	var sum = 0;
	var getlist =new Array(10);
	var chkvalue =new Array("1","3","7","1","3","7","1","3","5");
	for(var i=0; i<10; i++) { getlist[i] = vencod.substring(i, i+1); }
	for(var i=0; i<9; i++) { sum += getlist[i]*chkvalue[i]; }
	sum = sum + parseInt((getlist[8]*5)/10);
	sidliy = sum % 10;
	sidchk = 0;
	if(sidliy != 0) { sidchk = 10 - sidliy; } else { sidchk = 0; }
	if(sidchk != getlist[9]) { return false; }
	return true;
}

function chgOnErrorImage(obj, nextImg) {
	var defaultImg = obj.src.substring(0, obj.src.indexOf("root/") ) + "root/product_img/no_product.jpg";
	if ( nextImg != undefined && obj.src.indexOf( nextImg ) == -1) {
		obj.src = nextImg;
	} else {
		obj.src = defaultImg;
	}
	obj.detachEvent('onerror');
}

String.prototype.trim     = _private_trim;
String.prototype.left     = _private_left;
String.prototype.right    = _private_right;
String.prototype.mid      = _private_mid;
String.prototype.arrSplit =_private_arrSplit;
//---------------------------------------------- TRIM() ���� Function END ----------------------------------------------


/** �Ⱓ ����
 *  periodSearch(0)  => ����
 *  periodSearch(7)  => 1����
 *  periodSearch(91) => 1����
 *  periodSearch(1)  => �̴� 
 *  periodSearch(912)=> �ݳ�
 *  periodSearch(12) => 1 year
 */
function periodSearch(iDate){
    var today =	new Date();
    var dateNow	 = today.getDate();
    var monthNow = today.getMonth();
    var yearNow	 = today.getYear();
        
    var dateStart = dateNow;
    var monthStart = monthNow;
    var yearStart = yearNow;    
    switch(iDate){
        case 0:            
            dateStart = dateNow;
            break;    
        case 7:            
            dateStart = dateNow-6;
            break;
        case 1:
            monthStart = monthNow - 1;
            dateStart = dateNow;
            break;
        case 91:
            dateStart = 1;
            break;            
        case 3:
            monthStart = monthNow - 3;
            dateStart = dateNow ;
            break;
        case 6:
            monthStart = monthNow - 6;
            dateStart = dateNow ;
            break;   
        case 12:
            yearStart = yearNow - 1;
            dateStart = dateNow ;
            break;        
        case 912:
            monthStart = 0;
            dateStart = dateNow ;
            dateStart = 1;
            break;                               
        default:
            break;
    }

    var startday = new Date(yearStart, monthStart, dateStart);

    fromYear = startday.getYear();
    fromMonth = padZero(startday.getMonth()+1);
    fromDate = padZero(startday.getDate());

    monthNow = padZero(monthNow+1);
    dateNow = padZero(dateNow);

    oFrom = document.getElementById("startDate");
    oTo = document.getElementById("endDate");

    oFrom.value = fromYear + "-" + fromMonth  + "-" + fromDate;
    oTo.value = yearNow + "-" +  monthNow + "-" + dateNow;
}

function padZero(num) {
    return (num	< 10)? '0' + num : num ;
}

/** �ѱ�,���� length üũ�ϱ�
 *  parameter : obj - �ش� ������Ʈ, slength - �ִ���� �Ѱ��ֱ�
 *  ���� : onblur=getByteLength(this,1000)
 *  @return : boolean
 */
function getByteLength(obj, slength){ 
	var len = 0; 
	s = obj.value;
	hlength =  slength/2;
	if ( s == null ) return '';
	
	for(var i=0;i<s.length;i++) { 
		var c = escape(s.charAt(i)); 
		if ( c.length == 1 ) len ++; 
		else if ( c.indexOf("%u") != -1 ) len += 2; 
		else if ( c.indexOf("%") != -1 ) len += c.length/3; 
	} 

	if (len > slength) {
		alert(len+" Byte:����"+slength+"�ڸ������� �Է��Ҽ� �ֽ��ϴ�.(�ѱ��� "+hlength+"�� �Է�)");
		obj.select();
		return; 
	} 
}     
	
/**
 * ���̳��� ���ϴ� ���� �����ڵ�� ����.
 * ����, �޸��� ū���ڷ� ����� 
 * 
 * 1 -> <span class="num1">1</span>
 * , -> <span class="comma">,</span>
 * 
 * @param c ū���ڷθ��� ����
 */
function getBigChar(c)	{
	if(c == ",")	return "<span class='comma'>,</span>";
	if(isNaN(c) == false)	return "<span class='num" + c + "'>" + c + "</span>";
	else return ""; //���� ������ �����߾�
}
/**
 * ���̳��� ���ϴ� ���� �����ڵ�� ����.
 * ���ڿ��� ū���ڿ��� ����� 
 * 
 * @param str
 * @returns
 */
function getBigChars(str)	{
	var ret = "";
	str = str.toString();
	for (var i = 0; i < str.length; i++) {
		ret += getBigChar(str.charAt(i));
	}
	return ret;
}
/**
 * 
 * @param e
 * @returns {Boolean} true : ����, false : ������ �ƴ�
 */
function chkJsonError(e)	{
	if(e.state != "error")	{
		return false;	//������ �ƴ�
	}
	
	if(e.msg)	alert(e.msg);
	if(e.redirect && e.redirect != "null")	document.location = e.redirect; 
	
	return true;
}

function chkHtmlError(e)	{
	if(e.indexOf("<meta") >= 0)	return true;
	else return false;
		
}

/**
 * SKTȸ�� ���� �޴��� Validation üũ
 * @param phone1
 * 		  phone2
 * 		  phone3
 */
function chkSmsPhone(phone1, phone2, phone3){

	if(phone1.find("option:selected").val() ==""){
		alert("�޴�����ȣ ���ڸ��� �����ϼ���.");
		phone1.focus();
		return false;
	}

	if(!phone2.val()){
		alert("�޴�����ȣ ����ڸ��� �Է��ϼ���.");
		phone2.focus();
		return false;
	}
	
	if(!isNumber(phone2.val())){
		alert("���ڸ� �Է��� �����մϴ�. �ٽ� �ۼ��� �ּ���.");
		phone2.focus();
		return false;
	}
	
	if(!phone3.val()){
		alert("�޴�����ȣ ���ڸ��� �Է��ϼ���.");
		phone3.focus();
		return false;
	} 
	
	if(!isNumber(phone3.val())){
		alert("���ڸ� �Է��� �����մϴ�. �ٽ� �ۼ��� �ּ���.");
		phone3.focus();
		return false;
	}
	
	return true;
}

/** �ѱ�,���� length üũ�ϱ�
 *  parameter : obj - �ش� ������Ʈ, slength - ������Ʈ ��Ī, slength - �ִ���� �Ѱ��ֱ�
 *  @return : boolean
 */
function getByteLengthMsg(obj, objname, slength){ 
	var len = 0; 
	s = obj.value;
	hlength =  slength/2;
	if ( s == null ) {
		alert(objname+"��(��) �������� �ʽ��ϴ�.");
		return false; 
	}
	
	for(var i=0;i<s.length;i++) { 
		var c = escape(s.charAt(i)); 
		if ( c.length == 1 ) len ++; 
		else if ( c.indexOf("%u") != -1 ) len += 2; 
		else if ( c.indexOf("%") != -1 ) len += c.length/3; 
	} 

	if (len > slength) {
		alert(objname+"��(��) ����"+slength+"�ڸ������� �Է��Ҽ� �ֽ��ϴ�.(�ѱ��� "+hlength+"�� �Է�)");
		obj.select();
		return false; 
	}

	return true;
}

/**
 * ���ڿ� ����Ʈ����ŭ �����ְ�, ����ǥ���ϱ�
 * @param str ���ڿ�
 * @param i ����Ʈ��
 * @param trail ���� ���ڿ�. ��) "..."
 * @return String
 */
function cropByte(str, i, trail) {
	if (str==null) return "";
	var tmp = str;
	var slen = 0, blen = 0;
	var c;
	try {
		if(str.length > i){
			while (blen+1 < i) {
				c = tmp.charAt(slen);
				blen++;
				slen++;
				if ( c  > 127 ) blen++;
			}
			tmp=tmp.substring(0,slen)+trail;
		}

	} catch(e) {}
	return tmp;
}

/**
 * ���ڿ� ������ parameter �� �迭 ���·� ��ȯ
 *  
 * @param urlQuery ���ڿ� �Ķ����
 */
function deserialize(urlQuery)	{
	var match,
	urlParamsArray = [],
    pl     = /\+/g,  // Regex for replacing addition symbol with a space
    search = /([^&=]+)=?([^&]*)/g,
    decode = function (s) { return decodeURIComponent(s.replace(pl, " ")); },
    query  = urlQuery.lastIndexOf("?", 0) === 0 ? urlQuery.substring(1) : urlQuery;

	while (match = search.exec(query))
		urlParamsArray.push({name:decode(match[1]), value:decode(match[2]) });

	return urlParamsArray;
}

/**
 * Ŭ��T����� 5.9% �Һ����� ���� �� �Һαݾ�
 * @param salePrice       : �Һο���
 * @param installmentTerm : �ܸ� �Һ� ������
 * @returns
 */
function getClubTSalePriceMonth( salePrice, installmentTerm ) {
	try {
		var interest = 0.059;
		var interestM = interest / 12;
		var t = Math.pow( (1+interestM), Number( installmentTerm ) );
		return Math.round( salePrice * interestM * t / (t -1) );
	} catch(e) {
		return 0;
	}
}
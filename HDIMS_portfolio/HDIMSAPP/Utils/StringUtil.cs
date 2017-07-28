using System;
using System.Text;
using System.Text.RegularExpressions;

namespace HDIMSAPP.Utils
{
    public class StringUtil
    {
        public static object Nvl(object obj, object value)
        {
            if (obj == null || obj.ToString().Equals(""))
                return value;
            else
                return obj;
        }


        #region  문자열 <-> 숫자형
        /// <summary>
        /// 입력된 문자열이 숫자형인지 확인
        /// </summary>
        /// <param name="src">문자열</param>
        /// <returns></returns>
        public static bool IsNumber(string src)
        {
            if (src == null || src.Length < 1 || src.Length > 12)
                return false;

            int len = src.Length;

            for (int i = 0; i < len; i++)
            {
                if (!System.Char.IsNumber(src, i))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 입력된 문자열을 숫자형으로 변환해준다.
        /// </summary>
        /// <param name="src">대상문자열</param>
        /// <param name="retValue">리턴될 숫자</param>
        /// <returns>변환작업의 성공여부</returns>
        /// <example>
        /// ConvertStringToInt("00004", ret) ==> 4
        /// </example>
        public static bool ConvertStringToInt(string src, ref int retValue)
        {
            if (!IsNumber(src))
                return false;

            try
            {
                int i = 0;
                for (; i < src.Length; i++)
                {
                    if (src[i] != '0')
                        break;
                }
                retValue = Int32.Parse(src.Substring(i));
                return true;
            }
            catch
            {
            }

            return false;
        }
        #endregion

        #region 문자를 ANSI코드로 반환
        /// <summary>
        /// 입력된 문자형에 해당하는 int형 값(ANSI 코드)으로 반환해준다. </br>
        /// <pre>
        /// Example
        /// Asc('#');		//returns 35
        /// </pre>
        /// </summary>
        /// <param name="cCharacter"> </param>
        public static int Asc(char cCharacter)
        {
            return (int)cCharacter;
        }

        /// <summary>
        /// Receives an integer ANSI code and returns a character associated with it
        /// <pre>
        /// Example:
        /// StringUtil.Chr(35);		//returns '#'
        /// </pre>
        /// </summary>
        /// <param name="nAnsiCode"> </param>
        public static char Chr(int nAnsiCode)
        {
            return (char)nAnsiCode;
        }
        #endregion

        #region 문자열중에서 지정된 문자/단어가 왼쪽에서 몇번째인지를 반환하는 메소드
        /// <summary>
        /// 문자열중에서 해당 문자가 몇번째에 위치하고 있는지를 반환해 주는 기능</br>
        /// <b>[주의]</b> 위치인덱스 값 0 부터지만 편의상 1부터 시작하도록 설정함 / 대소문자 구분 !
        /// <pre>
        /// Example:
        /// StringUtil.At("D", "Joe Doe");	//returns 5
        /// </pre>
        /// </summary>
        /// <param name="cSearchFor">찾고자하는 문자</param>
        /// <param name="cSearchIn">해당 문자열</param>
        public static int At(string cSearchFor, string cSearchIn)
        {
            return cSearchIn.IndexOf(cSearchFor) + 1;
        }

        /// <summary>
        /// 문자열중 찾고자하는 문자가 여러개 포함되어 있을경우 해당 문자열중에서 </br>
        ///  몇번째 문자인지를 찾아 그 위치를 반환해주는 기능 </br>
        /// <b>[주의]</b> 대소문자 구분 !
        /// <pre>
        /// Example:
        /// StringUtil.At("o", "Joe Doe", 1);	//returns 2
        /// StringUtil.At("o", "Joe Doe", 2);	//returns 6
        /// </pre>
        /// </summary>
        /// <param name="cSearchFor">찾고자하는 문자</param>
        /// <param name="cSearchIn">해당 문자열</param>
        /// <param name="nOccurence">몇번째 문자인지를 설정</param>
        public static int At(string cSearchFor, string cSearchIn, int nOccurence)
        {
            return __at(cSearchFor, cSearchIn, nOccurence, 1);
        }

        /// Private Implementation: This is the actual implementation of the At() and RAt() functions. 
        /// Receives two StringUtil, the expression in which search is performed and the expression to search for. 
        /// Also receives an occurence position and the mode (1 or 0) that specifies whether it is a search
        /// from Left to Right (for At() function)  or from Right to Left (for RAt() function)
        private static int __at(string cSearchFor, string cSearchIn, int nOccurence, int nMode)
        {
            //In this case we actually have to locate the occurence
            int i = 0;
            int nOccured = 0;
            int nPos = 0;
            if (nMode == 1) { nPos = 0; }
            else { nPos = cSearchIn.Length; }

            //Loop through the string and get the position of the requiref occurence
            for (i = 1; i <= nOccurence; i++)
            {
                if (nMode == 1) { nPos = cSearchIn.IndexOf(cSearchFor, nPos); }
                else { nPos = cSearchIn.LastIndexOf(cSearchFor, nPos); }

                if (nPos < 0)
                {
                    //This means that we did not find the item
                    break;
                }
                else
                {
                    //Increment the occured counter based on the current mode we are in
                    nOccured++;

                    //Check if this is the occurence we are looking for
                    if (nOccured == nOccurence)
                    {
                        return nPos + 1;
                    }
                    else
                    {
                        if (nMode == 1) { nPos++; }
                        else { nPos--; }

                    }
                }
            }
            //We never found our guy if we reached here
            return 0;
        }


        /// <summary>
        /// 문자열의 모든 문자를 소문자로 변경후 찾고자하는 문자의 위치를 검색함.
        /// <pre>
        /// Example:
        /// StringUtil.AtC("d", "Joe Doe");	//returns 5
        /// </pre>
        /// </summary>
        /// <param name="cSearchFor">찾고자하는 문자</param>
        /// <param name="cSearchIn">해당 문자열 </param>
        public static int AtC(string cSearchFor, string cSearchIn)
        {
            return cSearchIn.ToLower().IndexOf(cSearchFor.ToLower()) + 1;
        }

        /// <summary>
        /// 문자열의 모든 문자를 소문자로 변경후 찾고자하는 문자의 몇번째에 대당하는지 그 위치를 검색함.
        /// <pre>
        /// Example:
        /// StringUtil.AtC("d", "Joe Doe", 1);	//returns 5
        /// StringUtil.AtC("O", "Joe Doe", 2);	//returns 6
        /// </pre>
        /// </summary>
        /// <param name="cSearchFor">찾고자하는 문자 </param>
        /// <param name="cSearchIn"> 해당 문자열</param>
        /// <param name="nOccurence">몇번째 문자인지를 설정 </param>
        public static int AtC(string cSearchFor, string cSearchIn, int nOccurence)
        {
            return __at(cSearchFor.ToLower(), cSearchIn.ToLower(), nOccurence, 1);
        }

        /// <summary>
        /// 문자열에서 찾고자하는 문자들이 포함된 위치를 반환해 주는 메소드 </br>
        /// <b>[주의]</b>위의 At 메소드는 특정 단어만을 검색하는데 반해 이 메소드는 하나이상의 단어로 검색
        /// <pre>
        /// Example:
        /// StringUtil.Occurs('o', "Joe Doe");		//returns 2
        /// 
        /// Tip: If we have a string say lcString, then lcString[3] gives us the 3rd character in the string
        /// </pre>
        /// </summary>
        /// <param name="cChar"> </param>
        /// <param name="cExpression"> </param>
        public static int Occurs(char tcChar, string cExpression)
        {
            int i, nOccured = 0;

            //Loop through the string
            for (i = 0; i < cExpression.Length; i++)
            {
                //Check if each expression is equal to the one we want to check against
                if (cExpression[i] == tcChar)
                {
                    //if  so increment the counter
                    nOccured++;
                }
            }
            return nOccured;
        }
        /// <summary>
        /// 문자열에서 찾고자하는 문자들이 포함된 위치를 반환해 주는 메소드 </br>
        /// <pre>
        /// Example:
        /// StringUtil.Occurs("oe", "Joe Doe");		//returns 2
        /// StringUtil.Occurs("Joe", "Joe Doe");		//returns 1
        /// 
        /// Tip: String.IndexOf() searches the string (starting from left) for another character or string expression
        /// </pre>
        /// </summary>
        /// <param name="cString"> </param>
        /// <param name="cExpression"> </param>
        public static int Occurs(string cString, string cExpression)
        {
            int nPos = 0;
            int nOccured = 0;
            do
            {
                //Look for the search string in the expression
                nPos = cExpression.IndexOf(cString, nPos);

                if (nPos < 0)
                {
                    //This means that we did not find the item
                    break;
                }
                else
                {
                    //Increment the occured counter based on the current mode we are in
                    nOccured++;
                    nPos++;
                }
            } while (true);

            //Return the number of occurences
            return nOccured;
        }


        #endregion

        #region 문자열중에서 지정된 단어가 오른쪽에서 몇번째인지를 반환하는 메소드
        /// <summary>
        /// 문자열중에서 지정된 단어가 오른쪽에서 몇번째인지를 반환하는 메소드
        /// <pre>
        /// Example:
        /// StringUtil.RAt("o", "Joe Doe");	//returns 6
        /// </pre>
        /// </summary>
        /// <param name="cSearchFor"> </param>
        /// <param name="cSearchIn"> </param>
        public static int RAt(string cSearchFor, string cSearchIn)
        {
            return cSearchIn.LastIndexOf(cSearchFor) + 1;
        }

        /// <summary>
        /// Receives two StringUtil as parameters and an occurence position as parameters. 
        /// The function searches for one string within another and the search is performed 
        /// starting from Right to Left and if found, returns the beginning numeric position 
        /// otherwise returns 0
        /// <pre>
        /// Example:
        /// StringUtil.RAt("o", "Joe Doe", 1);	//returns 6
        /// StringUtil.RAt("o", "Joe Doe", 2);	//returns 2
        /// </pre>
        /// </summary>
        /// <param name="cSearchFor"> </param>
        /// <param name="cSearchIn"> </param>
        /// <param name="nOccurence"> </param>
        public static int RAt(string cSearchFor, string cSearchIn, int nOccurence)
        {
            return __at(cSearchFor, cSearchIn, nOccurence, 0);
        }
        #endregion

        #region 문자열중에서 특정 단어만을 지정된 단어로 변경해주는 메소드
        /// <summary>
        /// 문자열중에서 특정 단어만을 지정된 단어로 변경해주는 메소드 
        /// </summary>
        /// <example>
        /// Console.WriteLine(ChrTran("ABCDEF", "ACE", "XYZ"));  //Displays XBYDZF
        /// Console.WriteLine(ChrTran("ABCD", "ABC", "YZ"));	//Displays YZD
        /// Console.WriteLine(ChrTran("ABCDEF", "ACE", "XYZQRST"));	//Displays XBYDZF
        /// </example>
        /// <param name="cSearchIn">문자열</param>
        /// <param name="cSearchFor">변경하고자 하는 문자들</param>
        /// <param name="cReplaceWith">교체될 문자들</param>
        public static string ChrTran(string cSearchIn, string cSearchFor, string cReplaceWith)
        {
            string lcRetVal = cSearchIn;
            string cReplaceChar;
            for (int i = 0; i < cSearchFor.Length; i++)
            {
                if (cReplaceWith.Length <= i)
                    cReplaceChar = "";
                else
                    cReplaceChar = cReplaceWith[i].ToString();

                lcRetVal = StrTran(lcRetVal, cSearchFor[i].ToString(), cReplaceChar);
            }
            return lcRetVal;
        }

        /// <summary>
        /// 문자열중에서 지정된 단어를 공백으로 변환하는 메소드
        /// <pre>
        /// Example:
        /// StringUtil.StrTran("Joe Doe", "o");		//returns "J e D e" :)
        /// </pre>
        /// </summary>
        /// <param name="cSearchIn"> </param>
        /// <param name="cSearchFor"> </param>
        public static string StrTran(string cSearchIn, string cSearchFor)
        {
            //Create the StringBuilder
            StringBuilder sb = new StringBuilder(cSearchIn);

            //Call the Replace() method of the StringBuilder
            return sb.Replace(cSearchFor, " ").ToString();
        }

        /// <summary>
        /// 문자열에서 지정된 문자를 변경하는 메소드
        /// <pre>
        /// Example:
        /// StringUtil.StrTran("Joe Doe", "o", "ak");		//returns "Jake Dake" 
        /// </pre>
        /// </summary>
        /// <param name="cSearchIn"> </param>
        /// <param name="cSearchFor"> </param>
        /// <param name="cReplaceWith"> </param>
        public static string StrTran(string cSearchIn, string cSearchFor, string cReplaceWith)
        {
            //Create the StringBuilder
            StringBuilder sb = new StringBuilder(cSearchIn);

            //There is a bug in the replace method of the StringBuilder
            sb.Replace(cSearchFor, cReplaceWith);

            //Call the Replace() method of the StringBuilder and specify the string to replace with
            return sb.Replace(cSearchFor, cReplaceWith).ToString();
        }

        /// <summary>
        /// 문자열에서 지정된 캐릭터를 특정 캐릭터로 변환 [이명진추가]
        /// <pre>
        /// Example:
        /// StringUtil.StrTran("Joe Doe", 'o, 'a');		//returns "Jake Dake" 
        /// </pre>
        /// </summary>
        /// <param name="cSearchIn"> </param>
        /// <param name="cCharFor"> </param>
        /// <param name="cReplaceWith"> </param>
        public static string StrTran(string cSearchIn, char cCharFor, char cReplaceWith)
        {
            //Create the StringBuilder
            StringBuilder sb = new StringBuilder(cSearchIn);

            //There is a bug in the replace method of the StringBuilder
            sb.Replace(cCharFor, cReplaceWith);

            //Call the Replace() method of the StringBuilder and specify the string to replace with
            return sb.Replace(cCharFor, cReplaceWith).ToString();
        }

        /// Searches one string into another string and replaces each occurences with
        /// a third string. The fourth parameter specifies the starting occurence and the 
        /// number of times it should be replaced
        /// <pre>
        /// Example:
        /// StringUtil.StrTran("Joe Doe", "o", "ak", 2, 1);		//returns "Joe Dake" 
        /// </pre>
        public static string StrTran(string cSearchIn, string cSearchFor, string cReplaceWith, int nStartoccurence, int nCount)
        {
            //Create the StringBuilder
            StringBuilder sb = new StringBuilder(cSearchIn);

            //There is a bug in the replace method of the StringBuilder
            sb.Replace(cSearchFor, cReplaceWith);

            //Call the Replace() method of the StringBuilder specifying the replace with string, occurence and count
            return sb.Replace(cSearchFor, cReplaceWith, nStartoccurence, nCount).ToString();
        }


        #endregion

        #region 문자열에서 단어의 개수 & 지정된 단어를 반환해 주는 메소드
        /// <summary>
        /// 단어의 개수를 반환해주는 메소드 
        /// <pre>
        /// Example:
        /// string lcString = "Joe Doe is a good man";
        /// StringUtil.GetWordCount(lcString);		//returns 6
        /// </pre>
        /// </summary>
        /// <param name="cString">문자열</param>
        public static long GetWordCount(string cString)
        {
            int i = 0;
            long nLength = cString.Length;
            long nWordCount = 0;

            //Begin by checking for the first word
            if (!Char.IsWhiteSpace(cString[0]))
            {
                nWordCount++;
            }

            //Now look for white spaces and count each word
            for (i = 0; i < nLength; i++)
            {
                //Check for a space to begin counting a word
                if (Char.IsWhiteSpace(cString[i]))
                {
                    //We think we encountered a word
                    //Remove any following white spaces if any after this word
                    do
                    {
                        //Check if we have reached the limit and if so then exit the loop
                        i++;
                        if (i >= nLength) { break; }
                        if (!Char.IsWhiteSpace(cString[i]))
                        {
                            nWordCount++;
                            break;
                        }
                    } while (true);

                }

            }
            return nWordCount;
        }

        /// <summary>
        /// 지정된 위치의 단어를 반환하는 메소드 
        /// <pre>
        /// Example:
        /// string lcString = "Joe Doe is a good man";
        /// StringUtil.GetWordNumber(lcString, 5);		//returns "good"
        /// </pre>
        /// </summary>
        /// <param name="cString"> </param>
        /// <param name="nWordPosition"> </param>
        public static string GetWordNumb(string cString, int nWordPosition)
        {
            int nBeginPos = StringUtil.At(" ", cString, nWordPosition - 1);
            int nEndPos = StringUtil.At(" ", cString, nWordPosition);
            return StringUtil.SubStr(cString, nBeginPos + 1, nEndPos - 1 - nBeginPos);
        }

        /// <summary>
        /// GetWordNumb 메소등 다른 메소드에서 사용하는 메소드로 문자열에서 </br>
        /// 지정된 위치에 있는 단어를 반환하는 메소드
        /// <pre>
        /// string lcName = "Joe Doe";
        /// SubStr(lcName, 1, 3);	//returns "Joe"
        /// SubStr(lcName, 5);	//returns Doe
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nStartPosition"> </param>
        public static string SubStr(string cExpression, int nStartPosition)
        {
            return cExpression.Substring(nStartPosition - 1);
        }

        /// <summary>
        /// Overloaded method for SubStr() that receives starting position and length
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nStartPosition"> </param>
        /// <param name="nLength"> </param>
        public static string SubStr(string cExpression, int nStartPosition, int nLength)
        {
            nStartPosition--;
            if ((nLength + nStartPosition) > cExpression.Length)
                return cExpression.Substring(nStartPosition);
            else
                return cExpression.Substring(nStartPosition, nLength);
        }
        #endregion

        #region 알파벳문자인지를 확인하는 메소드
        /// <summary>
        /// 해당 문자열이 알파벳인지를 확인하는 메소드
        /// <pre>
        /// Example:
        /// StringUtil.IsAlpha("Joe Doe");		//returns true
        /// 
        /// Tip: This method uses Char.IsAlpha(char) to check if it is an alphabet or not. 
        ///      In order to check if the first character is a digit use Char.IsDigit(char)
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        public static bool IsAlpha(string cExpression)
        {
            //Check if the first character is a letter
            return Char.IsLetter(cExpression[0]);
        }
        #endregion

        #region 문자열중 첫번째 문자가 대문자/소문자인지를 확인하는 메소드
        /// <summary>
        /// 
        /// <pre>
        /// Example:
        /// StringUtil.IsLower("MyName");	//returns false
        /// StringUtil.IsLower("mYnAme");	//returns true
        /// </pre>
        /// </summary>
        /// <param name="cExpression">해당문자열</param>
        public static bool IsLower(string cExpression)
        {
            try
            {
                //Get the first character in the string
                string lcString = cExpression.Substring(0, 1);

                //Return a bool indicating if the char is an lowercase or not
                return lcString == lcString.ToLower();
            }
            catch
            {
                //In case of an error return false
                return false;
            }
        }

        /// <summary>
        /// 문자열중 첫번째 문자가 대문자인지를 확인하는 메소드
        /// <pre>
        /// Example:
        /// StringUtil.IsUpper("MyName");	//returns true
        /// StringUtil.IsUpper("mYnAme");	//returns false
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        public static bool IsUpper(string cExpression)
        {
            try
            {
                //Get the first character in the string
                string lcString = cExpression.Substring(0, 1);

                //Return a bool indicating if the char is an uppercase or not
                return lcString == lcString.ToUpper();
            }
            catch
            {
                //In case of an error return false
                return false;
            }
        }
        #endregion

        #region 문자를 정렬시키는 메소드 / 지정된 문자열의 길이를 기준으로
        /// <summary>
        /// 문자를 가운데 정렬시키는 메소드 / 지정된 문자열의 길이를 기준으로 </br>
        /// 남는 공간은 빈공백으로 체워짐
        /// <pre>
        /// Example:
        /// StringUtil.PadL("Joe Doe", 10);		//returns " Joe Doe  "
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nResultSize"> </param>
        public static string PadC(string cExpression, int nResultSize)
        {
            //Determine the number of padding characters
            int nPaddTotal = nResultSize - cExpression.Length;
            int lnHalfLength = (int)(nPaddTotal / 2);

            string lcString = PadL(cExpression, cExpression.Length + lnHalfLength);
            return lcString.PadRight(nResultSize);
        }

        /// <summary>
        /// 문자를 가운데 정렬시키는 메소드 / 지정된 문자열의 길이를 기준으로 </br>
        /// 남는 공간은 지정된 문자로 체워짐
        /// <pre>
        /// Example:
        /// StringUtil.PadL("Joe Doe", 10, 'x');		//returns "xJoe Doexx"
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nResultSize"> </param>
        /// <param name="cPaddingChar"> </param>
        public static string PadC(string cExpression, int nResultSize, char cPaddingChar)
        {
            //Determine the number of padding characters
            int nPaddTotal = nResultSize - cExpression.Length;
            int lnHalfLength = (int)(nPaddTotal / 2);

            string lcString = PadL(cExpression, cExpression.Length + lnHalfLength, cPaddingChar);
            return lcString.PadRight(nResultSize, cPaddingChar);
        }

        /// <summary>
        /// 문자를 왼쪽 정렬시키는 메소드 / 지정된 문자열의 길이를 기준으로 </br>
        /// 남는 공간은 빈공백으로 체워짐
        /// <pre>
        /// Example:
        /// StringUtil.PadL("Joe Doe", 10);		//returns "   Joe Doe"
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nResultSize"> </param>
        public static string PadL(string cExpression, int nResultSize)
        { return cExpression.PadLeft(nResultSize); }

        /// <summary>
        /// 문자를 왼쪽 정렬시키는 메소드 / 지정된 문자열의 길이를 기준으로 </br>
        /// 남는 공간은 지정된 문자로 체워짐
        /// <pre>
        /// Example:
        /// StringUtil.PadL("Joe Doe", 10, 'x');		//returns "xxxJoe Doe"
        /// 
        /// Tip: Use single quote to create a character type data and double quotes for StringUtil
        /// </pre>
        /// </summary>
        public static string PadL(string cExpression, int nResultSize, char cPaddingChar)
        { return cExpression.PadLeft(nResultSize, cPaddingChar); }

        /// <summary>
        /// 문자를 오른쪽 정렬시키는 메소드 / 지정된 문자열의 길이를 기준으로 </br>
        /// 남는 공간은 빈공백으로 체워짐
        /// <pre>
        /// Example:
        /// StringUtil.PadL("Joe Doe", 10);		//returns "Joe Doe   "
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nResultSize"> </param>
        public static string PadR(string cExpression, int nResultSize)
        { return cExpression.PadRight(nResultSize); }


        /// <summary>
        /// 문자를 오른쪽 정렬시키는 메소드 / 지정된 문자열의 길이를 기준으로 </br>
        /// 남는 공간은 지정된 문자로 체워짐
        /// <pre>
        /// Example:
        /// StringUtil.PadL("Joe Doe", 10, 'x');		//returns "Joe Doexxx"
        /// 
        /// Tip: Use single quote to create a character type data and double quotes for StringUtil
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nResultSize"> </param>
        /// <param name="cPaddingChar"> </param>
        public static string PadR(string cExpression, int nResultSize, char cPaddingChar)
        { return cExpression.PadRight(nResultSize, cPaddingChar); }

        #endregion

        
        #region 문자열을 지정된 숫자만큼 반복해 생성하는 메소드
        /// <summary>
        /// 문자열을 지정된 숫자만큼 반복해 생성하는 메소드 
        /// <pre>
        /// Example:
        /// StringUtil.Replicate("Joe", 5);		//returns JoeJoeJoeJoeJoe
        /// 
        /// Tip: Use a StringBuilder when lengthy string manipulations are required.
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nTimes"> </param>
        public static string Replicate(string cExpression, int nTimes)
        {
            //Create a stringBuilder
            StringBuilder sb = new StringBuilder();

            //Insert the expression into the StringBuilder for nTimes
            sb.Insert(0, cExpression, nTimes);

            //Convert it to a string and return it back
            return sb.ToString();
        }
        #endregion

        #region 문자열에서 왼쪽/오른쪽에서부터 지정된 위치에 있는 문자를 반환하는 메소드
        /// <summary>
        /// 문자열에서 오른쪽에서부터 지정된 위치에 있는 문자를 반환하는 메소드
        /// <pre>
        /// Example:
        /// StringUtil.Right("Joe Doe", 3);	//returns "Doe"
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nDigits"> </param>
        public static string Right(string cExpression, int nDigits)
        {
            return cExpression.Substring(cExpression.Length - nDigits);
        }

        /// <summary>
        /// 문자열에서 왼쪽에서부터 지정된 위치에 있는 문자를 반환하는 메소드
        /// <pre>
        /// Example:
        /// StringUtil.Left("Joe Doe", 3);	//returns "Joe"
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nDigits"> </param>
        public static string Left(string cExpression, int nDigits)
        {
            return cExpression.Substring(0, nDigits);
        }
        #endregion

        #region 문자열에서 왼쪽/오른쪽에 있는 빈공백만을 제거하는 메소드
        /// <summary>
        /// 오른쪽에 있는 빈공백만을 제거하는 메소드
        /// </summary>
        /// <example>
        /// StringUtil.RTrim("VFPToolkitNET     "); //returns "VFPToolkitNET"
        /// </example>
        /// <param name="cExpression"> </param>
        public static string RTrim(string cExpression)
        {
            //Hint: Pass null as the first parameter to remove white spaces
            return cExpression.TrimEnd(null);
        }

        /// <summary>
        /// 왼쪽에 있는 빈공백만을 제거하는 메소드
        /// </summary>
        /// <param name="cExpression"> </param>
        public static string LTrim(string cExpression)
        {
            //Hint: Pass null as the first parameter to remove white spaces
            return cExpression.TrimStart(null);
        }

        #endregion

        #region 문자열중 지정된 문자사이에 있는 문자를 반환하는 메소드(Between)
        /// <summary>
        /// 문자열중 지정된 문자사이에 있는 문자를 반환하는 메소드 (Between)
        /// <pre>
        /// Example:
        /// string cExpression = "JoeDoeJoeDoe";
        /// StringUtil.StrExtract(cExpression, "o", "eJ", 1, 0);		//returns "eDo"
        /// </pre>
        /// </summary>
        /// <param name="cSearchExpression">문자열</param>
        /// <param name="cBeginDelim">시작 문자</param>
        /// <param name="cEndDelim">끝 문자</param>
        /// <param name="nBeginOccurence">조건에 맞는 몇번째 단어</param>
        /// <param name="nFlags"></param>
        public static string StrExtract(string cSearchExpression, string cBeginDelim, string cEndDelim, int nBeginOccurence, int nFlags)
        {
            string cstring = cSearchExpression;
            string cb = cBeginDelim;
            string ce = cEndDelim;
            string lcRetVal = "";

            //Check for case-sensitive or insensitive search
            if (nFlags == 1)
            {
                cstring = cstring.ToLower();
                cb = cb.ToLower();
                ce = ce.ToLower();
            }

            //Lookup the position in the string
            int nbpos = At(cb, cstring, nBeginOccurence) + cb.Length - 1;
            int nepos = cstring.IndexOf(ce, nbpos + 1);

            //Extract the part of the strign if we get it right
            if (nepos > nbpos)
            {
                lcRetVal = cSearchExpression.Substring(nbpos, nepos - nbpos);
            }

            return lcRetVal;
        }

        /// <summary>
        /// 문자열중 지정된 문자다음에 있는 모든 문자열을 반환하는 메소드 
        /// <pre>
        /// Example:
        /// string cExpression = "JoeDoeJoeDoe";
        /// StringUtil.StrExtract(cExpression, "o");		//returns "eDoeJoeDoe"
        /// </pre>
        /// </summary>
        /// <param name="cSearchExpression"> </param>
        /// <param name="cBeginDelim"> </param>
        public static string StrExtract(string cSearchExpression, string cBeginDelim)
        {
            int nbpos = At(cBeginDelim, cSearchExpression);
            return cSearchExpression.Substring(nbpos + cBeginDelim.Length - 1);
        }

        /// <summary>
        /// Receives a string along with starting and ending delimiters and returns the 
        /// part of the string between the delimiters
        /// <pre>
        /// Example:
        /// string cExpression = "JoeDoeJoeDoe";
        /// StringUtil.StrExtract(cExpression, "o", "eJ");		//returns "eDo"
        /// </pre>
        /// </summary>
        /// <param name="cSearchExpression"> </param>
        /// <param name="cBeginDelim"> </param>
        /// <param name="cEndDelim"> </param>
        public static string StrExtract(string cSearchExpression, string cBeginDelim, string cEndDelim)
        {
            return StrExtract(cSearchExpression, cBeginDelim, cEndDelim, 1, 0);
        }

        /// <summary>
        /// Receives a string along with starting and ending delimiters and returns the 
        /// part of the string between the delimiters. It also receives a beginning occurence
        /// to begin the extraction from.
        /// <pre>
        /// Example:
        /// string cExpression = "JoeDoeJoeDoe";
        /// StringUtil.StrExtract(cExpression, "o", "eJ", 2);		//returns ""
        /// </pre>
        /// </summary>
        /// <param name="cSearchExpression"> </param>
        /// <param name="cBeginDelim"> </param>
        /// <param name="cEndDelim"> </param>
        /// <param name="nBeginOccurence"> </param>
        public static string StrExtract(string cSearchExpression, string cBeginDelim, string cEndDelim, int nBeginOccurence)
        {
            return StrExtract(cSearchExpression, cBeginDelim, cEndDelim, nBeginOccurence, 0);
        }
        #endregion

        #region 특정단어를 문자열 중간에 삽입시키는 메소드
        /// <summary>
        /// 특정단어를 문자열 중간에 삽입시키는 메소드 
        /// <pre>
        /// Example:
        /// string lcString = "Joe Doe";
        /// string lcReplace = "Foo ";
        /// StringUtil.Stuff(lcString, 5, 0, lcReplace);	//returns "Joe Foo Doe";
        /// StringUtil.Stuff(lcString, 5, 3, lcReplace);	//returns "Joe Foo ";
        /// </pre>
        /// </summary>
        /// <param name="cExpression"> </param>
        /// <param name="nStartReplacement"> </param>
        /// <param name="nCharactersReplaced"> </param>
        /// <param name="cReplacement"> </param>
        public static string Stuff(string cExpression, int nStartReplacement, int nCharactersReplaced, string cReplacement)
        {
            //Create a stringbuilder to work with the string
            StringBuilder sb = new StringBuilder(cExpression);

            if (nCharactersReplaced + nStartReplacement - 1 >= cExpression.Length)
                nCharactersReplaced = cExpression.Length - nStartReplacement + 1;


            //First remove the characters specified in nCharacterReplaced
            if (nCharactersReplaced != 0)
            {
                sb.Remove(nStartReplacement - 1, nCharactersReplaced);
            }

            //Now Add the new string at the right location
            //sb.Insert(0,cExpression,nTimes);
            sb.Insert(nStartReplacement - 1, cReplacement);
            return sb.ToString();
        }
        #endregion

        #region 해당 단어가 몇번째 줄에 있는지를 반환하는 메소드
        /// <summary>
        /// 해당 단어가 몇번째 줄에 있는지를 반환하는 메소드 /대소문작 구별함
        /// <pre>
        /// Example:
        /// StringUtil.AtLine("Is", "Is Life Beautiful? \r\n It sure is");	//returns 1
        /// </pre>
        /// </summary>
        /// <param name="tcSearchExpression"></param>
        /// <param name="tcExpressionSearched"></param>
        /// <returns></returns>
        public static int AtLine(string tcSearchExpression, string tcExpressionSearched)
        {
            string lcString;
            int nPosition;
            int nCount = 0;

            try
            {
                nPosition = StringUtil.At(tcSearchExpression, tcExpressionSearched);
                if (nPosition > 0)
                {
                    lcString = StringUtil.SubStr(tcExpressionSearched, 1, nPosition - 1);
                    nCount = StringUtil.Occurs(@"\r", lcString) + 1;
                }
            }
            catch
            {
                nCount = 0;
            }

            return nCount;
        }

        /// <summary>
        /// Receives a search expression and string to search as parameters and returns an integer specifying
        /// the line where it was found. This function starts it search from the end of the string.
        /// <pre>
        /// Example:
        /// StringUtil.RAtLine("sure", "Is Life Beautiful? \r\n It sure is") 'returns 2
        /// </pre>
        /// </summary>
        /// <param name="tcSearchExpression"></param>
        /// <param name="tcExpressionSearched"></param>
        /// <returns></returns>
        public static int RAtLine(string tcSearchExpression, string tcExpressionSearched)
        {
            string lcString;
            int nPosition;
            int nCount = 0;

            try
            {
                nPosition = StringUtil.RAt(tcSearchExpression, tcExpressionSearched);
                if (nPosition > 0)
                {
                    lcString = StringUtil.SubStr(tcExpressionSearched, 1, nPosition - 1);
                    nCount = StringUtil.Occurs(@"\r", lcString) + 1;
                }
            }
            catch
            {
                nCount = 0;
            }

            return nCount;
        }

        /// <summary>
        /// 대소문자 구분없이 지정된 단어가 몇번째 줄에 있는지를 반환하는 메소드 
        /// another string expression without regard to case (upper or lower)
        /// <pre>
        /// Example:
        /// StringUtil.AtCLine("Is Life Beautiful? \r\n It sure is", "Is");	//returns 1
        /// </pre>
        /// </summary>
        /// <param name="tcSearchExpression"></param>
        /// <param name="tcExpressionSearched"></param>
        /// <returns></returns>
        public static int AtCLine(string tcSearchExpression, string tcExpressionSearched)
        {
            return AtLine(tcSearchExpression.ToLower(), tcExpressionSearched.ToLower());
        }
        #endregion

        #region 문자가 10진수 인지를 구별하는 메소드
        /// <summary>
        /// 문자가 10진수 인지를 구별하는 메소드
        /// <pre>
        /// Example:
        /// if(StringUtil.IsDigit("1Kamal")){...}	//returns true
        /// </pre>
        /// </summary>
        /// <param name="tcExpression"></param>
        /// <returns></returns>
        public static bool IsDigit(string tcString)
        {
            //get the first character in the string
            char c = tcString[0];
            return Char.IsDigit(c);
        }
        #endregion

        #region null string 변화 함수
        /// <summary>
        /// string의 값이 null이면 ""을 리턴한다.
        /// </summary>
        /// <param name="pstr">string 값</param>
        /// <returns>"" 또는 Trim된 문자열값</returns>
        public static string ToString(object obj)
        {
            if ((obj == null) || (obj.ToString().Length == 0))
            {
                return "";
            }
            else
            {
                return obj.ToString().Trim();
            }
        }

        /// <summary>
        /// string의 값이 null이면 "0"을 리턴한다.
        /// </summary>
        /// <param name="pstr">string 값</param>
        /// <returns>"0" 또는 Trim된 문자열값</returns>
        public static string NullStringToZero(string pstr)
        {
            if ((pstr == null) || (pstr.Length == 0))
            {
                return "0";
            }
            else
            {
                return pstr.Trim();
            }
        }

        /// <summary>
        /// string의 값이 "0" 이외의 값이면 true를 리턴한다.
        /// </summary>
        /// <param name="pstr">string 값</param>
        /// <returns>true/false</returns>
        public static bool NullStringToBool(string pstr)
        {
            string s = NullStringToZero(pstr);
            if (s == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion


        #region Null이면 false를, Null이 아니면 true를 리턴
        public static bool IsNull(string sValue)
        {
            if (sValue == null || sValue == string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region EncodeString
        public static string EncodeString(string myString)
        {
            byte[] inArray = System.Text.Encoding.Unicode.GetBytes(myString);
            return Convert.ToBase64String(inArray);
        }
        #endregion

        #region Object의 널값을 체크해서 스트링을 리턴한다.
        public static string NullCheck(object obj)
        {
            string strReturn;
            if (obj == null || obj == DBNull.Value)
            {
                strReturn = "";
            }
            else
            {
                strReturn = obj.ToString();
            }
            return strReturn;
        }
        #endregion

        #region 입력받은 값이 있는지 없는지를 체크해서 Object을 리턴한다. 함수 오버라이드
        public static object NullCheck(string strTmp)
        {
            object objTmp;

            if (strTmp == "")
                objTmp = DBNull.Value;
            else
                objTmp = strTmp;

            return objTmp;
        }

        public static object NullCheck(int iTmp)
        {
            object objTmp;

            if (iTmp == 0)
                objTmp = DBNull.Value;
            else
                objTmp = iTmp;

            return objTmp;
        }
        #endregion

        #region 특정단어를 기준으로 문자열 분할

        public static string[] Tokenizer(string SourceString, char Token)
        {
            if (SourceString != null)
            {
                int nIndex = 0;
                int nLastIndex = SourceString.LastIndexOf(Token);

                int TokenCount = characterCount(SourceString, Token);

                string[] strTemp = null;

                if (TokenCount < 1) return strTemp;

                strTemp = new string[TokenCount + 1];

                int i;
                for (i = 0; nIndex < nLastIndex && nIndex >= 0; i++)
                {
                    strTemp[i] = SourceString.Substring(nIndex, SourceString.IndexOf(Token, nIndex) - nIndex);
                    nIndex = SourceString.IndexOf(Token, nIndex) + 1;
                }
                strTemp[i] = SourceString.Substring(nIndex);

                return strTemp;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region 특정단어의 반복횟수를 반환

        public static int characterCount(string SourceString, char Charactor)
        {
            int nCount = 0;
            char[] charTemp = SourceString.ToCharArray();

            for (int i = 0; i < charTemp.Length; i++)
            {
                if (charTemp[i] == Charactor) nCount++;
            }

            return nCount;
        }

        #endregion

        #region 특정단어의 길이수를 파라메터만큼 줄여서 리턴 [보통 게시판의 리스트에서 제목이 길경우에 사용]

        public static string mnTitleShort(string strTitle, int iReduceNum)
        {
            if (strTitle.Length > iReduceNum)
            {
                strTitle = strTitle.Replace(strTitle, strTitle.Substring(0, iReduceNum - 2) + "...");
                return strTitle;
            }
            else
            {
                return strTitle;
            }
        }

        #endregion


        #region  본문의 HTML 태그를 모두 삭제하고 파라메터 수만큼 줄여서 반환

        public static string mnContentsShort(string strContents, int iReduceNum)
        {
            strContents = strContents.Replace("\r\n", "");

            strContents = Regex.Replace(strContents, "<[^>]*>", "");
            if (strContents.Length > iReduceNum)
            {
                strContents = strContents.Replace(strContents, strContents.Substring(0, iReduceNum - 2) + "...");
                return strContents;
            }
            else
            {
                return strContents;
            }
        }

        #endregion

        #region 요청한 URL에서 프로그램명만가져오기

        public static string mnPgmName(string requestURL)
        {
            string[] URLDirectory;
            string[] Name;
            string PgmName;
            string msg;
            int Count;
            if (requestURL.Length > 0)
            {
                URLDirectory = requestURL.Split('/');
                Count = int.Parse(URLDirectory.Length.ToString()) - 1;
                PgmName = URLDirectory[Count];
                //	PgmName = PgmName.ToLower().Replace("?","aspx");
                Name = PgmName.Split('?');

                return Name[0];
            }
            else
            {
                msg = "잘못된 경로입니다.\r\n" + requestURL;
                return msg;
            }
        }

        #endregion

        #region 랜덤한 문자열 생성
        /// <summary>
        /// 랜덤한 문자열을 원하는 길이만큼 반환합니다. 
        /// </summary>
        /// <param name="length">문자열 길이</param>
        /// <returns>랜덤문자열</returns>
        public static String getRandomString(int length) 
        { 
          StringBuilder buffer = new StringBuilder(); 
          Random random = new Random(); 
  
          char[] chars = {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z' };
  
          for (int i=0 ; i<length ; i++) 
          { 
            buffer.Append(chars[random.Next(chars.Length)]); 
          } 
          return buffer.ToString();
        }
        #endregion
    }
}

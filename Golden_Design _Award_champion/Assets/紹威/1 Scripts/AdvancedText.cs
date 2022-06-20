using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace WEI
{
    public class AdvancedTextPreprocessor : ITextPreprocessor
    {
        public Dictionary<int, float> IntervalDictionary;
        public AdvancedTextPreprocessor()
        {
            IntervalDictionary = new Dictionary<int, float>();
        }
        public string PreprocessText(string text)
        {
            string processingText = text;
            //偵測所有的角括號  *+? = 偵測最近的結果
            string pattern = "<.*?>";
            Match match = Regex.Match(processingText, pattern);
            //如果可以取得到一個文字監控
            while (match.Success)
            {
                //查詢角誇號中有甚麼東西(第一個位置:1，字串長度:match)
                string lable = match.Value.Substring(startIndex: 1, length: match.Length - 2);
                //將字串轉換成浮點數
                //如果結果為true 並把結果存在result
                if (float.TryParse(lable, out float result))
                {
                    //得知所們所要對文字做的動作在浮點數的第幾個後面
                    IntervalDictionary[match.Index - 1] = result;
                }
                //刪除所有的東西
                processingText = processingText.Remove(match.Index, count: match.Length);
                //再去偵測標籤
                match = Regex.Match(processingText, pattern);
            }
            //還原
            processingText = text;
            //"\d"代表一個實際的字符 
            //並且為了不讓轉移字符顯示錯誤、需在前面加上一個@
            //.     代表任意字符!!!
            //*     代表前一個字符出現零次或是多次
            //+     代表前一個字符出現一次或是多次
            //?     代表前一個字符出現零次或是一次
            pattern = @"<(\d+)(\./d+)?>";

            return processingText;
        }
    }

    public class AdvancedText : TextMeshProUGUI
    {
        public AdvancedText()
        {
            textPreprocessor = new AdvancedTextPreprocessor();
        }
    }
}
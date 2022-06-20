using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

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
        string pattern = "<.*?>";
        Match  match = Regex.Match(processingText, pattern);
        //如果可以取得到一個文字監控
        while (match.Success)
        {
            string lable = match.Value.Substring(startIndex: 1, length: match.Length - 2);
        }
        return "Eromouga";
    }
}

public class AdvancedText : TextMeshProUGUI
{
    public AdvancedText()
    {
        textPreprocessor = new AdvancedTextPreprocessor();
    }
}

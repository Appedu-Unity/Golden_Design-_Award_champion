using System.Collections;
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
            pattern = @"<(\d+)(\.\d+)?>";

            processingText = Regex.Replace(processingText, pattern, replacement: "");
            return processingText;
        }
    }

    public class AdvancedText : TextMeshProUGUI
    {
        public AdvancedText()
        {
            textPreprocessor = new AdvancedTextPreprocessor();
        }
        private AdvancedTextPreprocessor SelfPreprocessor => (AdvancedTextPreprocessor)textPreprocessor;

        public void showTextByTyping(string countent)
        {
            SetText(countent);
            StartCoroutine(routine: Typing());
        }

        private int _typingIndex;
        private float _defaultInteravl = 0.06f;

        IEnumerator Typing()
        {
            ForceMeshUpdate();
            for (int i = 0; i < m_characterCount; i++)
            {
                SetSinglecharacterAlpha(i, newAlpha: 128);
            }
            _typingIndex = 0;
            while (_typingIndex < m_characterCount)
            {
                if (textInfo.characterInfo[_typingIndex].isVisible)
                {
                //SetSinglecharacterAlpha(_typingIndex, newAlpha: 255);
                StartCoroutine(routine: FadeInCharacter(_typingIndex));
                }
                if (SelfPreprocessor.IntervalDictionary.TryGetValue(_typingIndex, out float result))
                {
                    yield return new WaitForSecondsRealtime(result);
                }
                else
                {
                    yield return new WaitForSecondsRealtime(_defaultInteravl);
                }
                _typingIndex++;
            }
        }

        // newAlpha範圍是 0~255 ! ! !
        private void SetSinglecharacterAlpha(int index, byte newAlpha)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[index];
            int matIndex = charInfo.materialReferenceIndex;
            int verIndex = charInfo.vertexIndex;
            for (int i = 0; i < 4; i++)
            {
                textInfo.meshInfo[matIndex].colors32[verIndex + i].a = newAlpha;
            }
            UpdateVertexData();
        }
        IEnumerator FadeInCharacter(int index, float duration = 0.2f)
        {
            if (duration <= 0)
            {
                SetSinglecharacterAlpha(index, newAlpha: 255);
            }
            else
            {
                float timer = 0;
                while (timer < duration)
                {
                    timer = Mathf.Min(a: duration, b: timer + Time.unscaledDeltaTime);
                    SetSinglecharacterAlpha(index, newAlpha: (byte)(255 * timer / duration));
                    yield return null;
                }
            }
        }
    }
}
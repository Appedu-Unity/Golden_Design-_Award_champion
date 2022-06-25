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
            //�����Ҧ������A��  *+? = �����̪񪺵��G
            string pattern = "<.*?>";
            Match match = Regex.Match(processingText, pattern);
            //�p�G�i�H���o��@�Ӥ�r�ʱ�
            while (match.Success)
            {
                //�d�ߨ��ظ������ƻ�F��(�Ĥ@�Ӧ�m:1�A�r�����:match)
                string lable = match.Value.Substring(startIndex: 1, length: match.Length - 2);
                //�N�r���ഫ���B�I��
                //�p�G���G��true �ç⵲�G�s�bresult
                if (float.TryParse(lable, out float result))
                {
                    //�o���ҭ̩ҭn���r�����ʧ@�b�B�I�ƪ��ĴX�ӫ᭱
                    IntervalDictionary[match.Index - 1] = result;
                }
                //�R���Ҧ����F��
                processingText = processingText.Remove(match.Index, count: match.Length);
                //�A�h��������
                match = Regex.Match(processingText, pattern);
            }
            //�٭�
            processingText = text;
            //"\d"�N��@�ӹ�ڪ��r�� 
            //�åB���F�����ಾ�r����ܿ��~�B�ݦb�e���[�W�@��@
            //.     �N����N�r��!!!
            //*     �N��e�@�Ӧr�ťX�{�s���άO�h��
            //+     �N��e�@�Ӧr�ťX�{�@���άO�h��
            //?     �N��e�@�Ӧr�ťX�{�s���άO�@��
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

        // newAlpha�d��O 0~255 ! ! !
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
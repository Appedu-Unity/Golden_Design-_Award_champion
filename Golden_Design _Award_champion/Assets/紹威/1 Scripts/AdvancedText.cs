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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI")]
    public Text textLabel;
    public Image faceImage;

    [Header("�奻���")]
    public TextAsset textFile;
    [Header("�s��")]
    public int index;

    [Tooltip("�ЫئW��textList����r�C��")]
    List<string> textList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        GetTxetFormFile(textFile);
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            textLabel.text = textList[index];
            index++;
        }
    }
    /// <summary>
    /// Ū������k
    /// </summary>
    /// <param name="file"></param>
    void GetTxetFormFile(TextAsset file)
    {
        textList.Clear();       //���N�C��M��
        index = 0;              //�N�Ƨ��k0
                                //file.text =>���N�奻�ഫ���r�ŧΪ��ܶq
        var lineDate = file.text.Split('\n');      //�HEnter�N�奻����

        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }
}

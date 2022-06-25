using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI")]
    public Text textLabel;
    public Image faceImage;

    [Header("文本文件")]
    public TextAsset textFile;
    [Header("編號")]
    public int index;

    [Tooltip("創建名為textList的文字列表")]
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
    /// 讀取文件方法
    /// </summary>
    /// <param name="file"></param>
    void GetTxetFormFile(TextAsset file)
    {
        textList.Clear();       //先將列表清空
        index = 0;              //將排序歸0
                                //file.text =>先將文本轉換成字符形的變量
        var lineDate = file.text.Split('\n');      //以Enter將文本切割

        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }
}

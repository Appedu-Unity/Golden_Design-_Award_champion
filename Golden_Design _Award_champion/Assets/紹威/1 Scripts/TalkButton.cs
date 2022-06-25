using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    public GameObject Button;
    public GameObject talkUI;

    private void OnTriggerEnter(Collider other)
    {
        Button.SetActive(true);
        //Debug.Log("¸I¨ì");
    }
    private void OnTriggerExit(Collider other)
    {
        Button.SetActive(false);
    }
    private void Update()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            talkUI.SetActive(true);
        }
        
    }
}

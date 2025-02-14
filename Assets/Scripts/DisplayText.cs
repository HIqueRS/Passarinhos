using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public NodeText nodeText;
    public TextMeshProUGUI text;

    public float textSpeed;

    public int characterPos;

    public UnityEngine.UI.Image background;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = "";
        characterPos = 0;

        StartCoroutine(PassText());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            text.text = "";
            StartCoroutine(PassText());
        }
        if(Input.GetKey(KeyCode.Space))
        {
            textSpeed = 0.01f;
            background.color = new Color(232/255f,231/255f,166/255f,100/255f);
        }
        else{
            textSpeed = 0.2f;
            background.color = new Color(166/255f,222/255f,232/255f,100/255f);
        }
    }



    private IEnumerator PassText()
    {  
        for(; characterPos < nodeText.dialog.Length; characterPos++)
        {   
            if(nodeText.dialog[characterPos] == '\n')
            {
                characterPos++;
                break;
            }
            
            text.text += nodeText.dialog[characterPos];
            yield return new WaitForSeconds(textSpeed);

        }
    }
}

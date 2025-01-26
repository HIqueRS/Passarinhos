using System.Collections;
using TMPro;
using UnityEngine;

public class DisplayText : MonoBehaviour
{
    public NodeText nodeText;
    public TextMeshProUGUI text;

    public float textSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = "";

        StartCoroutine(PassText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PassText()
    {  
        for(int i = 0; i < nodeText.dialog.Length; i++)
        {   
            text.text += nodeText.dialog[i];
            yield return new WaitForSeconds(textSpeed);

        }
    }
}

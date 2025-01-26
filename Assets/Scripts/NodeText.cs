using UnityEngine;

[CreateAssetMenu(fileName = "Cena??" , menuName = "NodeText")]
public class NodeText : ScriptableObject
{
    [TextArea(5,10)]
    public string dialog;
}

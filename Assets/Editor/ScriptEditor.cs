using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ReadText))]
public class ScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Desenha o Inspector padrão
        DrawDefaultInspector();

        // Referência ao script
        ReadText script = (ReadText)target;

        // Botão personalizado
        if (GUILayout.Button("Gerar scriptables"))
        {
            script.ReadTexts();
        }
    }
}

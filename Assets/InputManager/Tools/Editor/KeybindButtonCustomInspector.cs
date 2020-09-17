using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(KeybindPanel))]
public class KeybindButtonCustomInspector : Editor
{
    //Variables

    private KeybindPanel keybindPanel;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        keybindPanel = (KeybindPanel)target;

        GUILayout.Space(10);

        if(keybindPanel.CustomText == true)
        {
            keybindPanel.CustomKeybindActionText = GUILayout.TextField(keybindPanel.CustomKeybindActionText);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad, ExecuteInEditMode]
public class KeybindSettingsTool : EditorWindow
{
    //Variables
    
    [MenuItem("Custom Keybinds/Keybind Settings Tool")]
    public static void GetWindow()
    {
        GetWindow<KeybindSettingsTool>("Keybind Settings Tool");
    }
    
    private void OnGUI()
    {
        GUILayout.Space(10);

        if (GUILayout.Button(new GUIContent("Create Keybinds Script", "...")))
        {
            CreateNewEnumWithAllKeybinds();
        }
    }

    private void CreateNewEnumWithAllKeybinds()
    {
        Debug.Log("Create Cool thing like keybinds bro");

        // Create a new script with an enum that stores all keybind actions etc.
        // 
        //  public enum KeybindActions
        //  {
        //      Forward = 1, Backward = 2, Left = 3, Right = 4, Reload = 5
        //  }
        //
    }
}


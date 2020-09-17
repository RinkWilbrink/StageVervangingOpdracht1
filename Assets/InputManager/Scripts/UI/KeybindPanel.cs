using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindPanel : MonoBehaviour
{
    // Variables
    [SerializeField] public string KeybindAction = "";
    [SerializeField] public int KeybindIndex = 0;

    [Header("Text")]
    [SerializeField] protected TMPro.TextMeshProUGUI Keybind_Key;
    [SerializeField] protected TMPro.TextMeshProUGUI KeybindAction_Text;

    [SerializeField] public bool CustomText = false;
    [HideInInspector] public string CustomKeybindActionText = "";

    public void SetUIWhenNewKeybind(string _keybindKey)
    {
        Keybind_Key.text = _keybindKey;
    }

    private void OnValidate()
    {
        if(CustomText == false)
        {
            KeybindAction_Text.text = KeybindAction;
        }
        else
        {
            KeybindAction_Text.text = CustomKeybindActionText;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindPanel : MonoBehaviour
{
    // Variables
    [SerializeField] public string KeybindAction = "";
    [SerializeField] public int KeybindIndex = 0;

    [Header("Text")]
    [SerializeField] private TMPro.TextMeshProUGUI Keybind_Key;
    [SerializeField] private TMPro.TextMeshProUGUI KeybindAction_Text;

    [Header("Action UI")]
    [SerializeField] private bool CustomText = false;
    [SerializeField] private string CustomKeybindActionText = "";

    public void SetUIWhenNewKeybind(string _keybindKey)
    {
        Keybind_Key.text = _keybindKey;
    }

    private void OnValidate()
    {
        if (CustomText == false)
        {
            KeybindAction_Text.text = KeybindAction;
        }
        else
        {
            KeybindAction_Text.text = CustomKeybindActionText;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindPanel : MonoBehaviour
{
    // Variables
    [SerializeField] public string KeybindAction = "";
    [SerializeField] public int KeybindIndex = 0;

    [SerializeField] private TMPro.TextMeshProUGUI Keybind_Key;

    public void SetUIWhenNewKeybind(string _keybindKey)
    {
        Keybind_Key.text = _keybindKey;
    }
}
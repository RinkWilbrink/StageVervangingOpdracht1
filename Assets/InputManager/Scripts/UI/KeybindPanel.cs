using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindPanel : MonoBehaviour
{
    // Variables
    [SerializeField] public string KeybindAction = "";
    [SerializeField] public int KeybindIndex = 0;

    [Header("Action UI")]
    [SerializeField] private bool CustomText = false;
    [SerializeField] private string CustomKeybindActionText = "";

    /// <summary>Update the Text field that displays the currently assigned key of this action.</summary>
    /// <param name="_keybindKey"></param>
    public void SetUIWhenNewKeybind(string _keybindKey)
    {
        transform.Find("Key_Text").GetComponent<TMPro.TextMeshProUGUI>().text = _keybindKey;
    }

    private void OnValidate()
    {
        TMPro.TextMeshProUGUI ActionText = transform.Find("Keybind_Text").GetComponent<TMPro.TextMeshProUGUI>();

        // Set the action UI text.
        if (CustomText == false)
        {
            ActionText.text = KeybindAction;
        }
        else
        {
            ActionText.text = CustomKeybindActionText;
        }
    }
}
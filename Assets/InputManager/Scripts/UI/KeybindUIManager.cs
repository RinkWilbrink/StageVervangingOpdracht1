using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindUIManager : MonoBehaviour
{
    // Variables
    [Header("All GameObjects related to the Keybind / Rebind menu")]
    [SerializeField] private GameObject ContentObject;

    [Header("Prefabs")]
    [SerializeField] private GameObject KeybindUIPrefab;

    // Private Variables
    
    void Awake()
    {
        InstantiateKeybindButtons(KeybindUIPrefab);
    }

    private void InstantiateKeybindButtons(GameObject _prefab)
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject newKeyBindUI = Instantiate(_prefab, ContentObject.transform);

            newKeyBindUI.name = string.Format("Keybind:{0} = {1})", i, "(Insert Key)");

            // Set UI Text
            newKeyBindUI.gameObject.transform.FindChild("Keybind_Text").GetComponent<TMPro.TextMeshProUGUI>().text = "KeyBind_Action";
            newKeyBindUI.gameObject.transform.FindChild("Key_Text").GetComponent<TMPro.TextMeshProUGUI>().text = "KeyBind_Key";
            newKeyBindUI.gameObject.transform.FindChild("").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { InitiateRebindEvent(string.Format("{0}", i)); });
        }
    }

    public void InitiateRebindEvent(string KeyBind_ID)
    {
        Debug.LogFormat("It's Rebind Time!! ID: {0}", KeyBind_ID);
    }
}

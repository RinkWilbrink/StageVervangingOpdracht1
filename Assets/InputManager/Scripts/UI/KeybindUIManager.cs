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
            newKeyBindUI.gameObject.transform.FindChild("Keybind_Text").GetComponent<TMPro.TextMeshProUGUI>().text = "Banaan";
        }
    }

    public void InitiateRebindEvent()
    {
        Debug.Log("It's Rebind Time!!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeybindUIManager : MonoBehaviour
{
    // Variables
    [Header("All GameObjects related to the Keybind / Rebind menu")]
    [SerializeField] private GameObject ContentObject;

    [Header("Prefabs")]
    [SerializeField] private GameObject KeybindUIPrefab;

    [Header("Input Asset")]
    [SerializeField] private InputActionAsset inputAsset;

    // Private Variables

    void Awake()
    {
        //InstantiateKeybindButtons(KeybindUIPrefab);
    }

    private void InstantiateKeybindButtons(GameObject _prefab)
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject newKeyBindUI = Instantiate(_prefab, ContentObject.transform);

            newKeyBindUI.name = string.Format("Keybind:{0} = {1})", i, "(Insert Key)");

            // Set UI Text
            newKeyBindUI.gameObject.transform.Find("Keybind_Text").GetComponent<TMPro.TextMeshProUGUI>().text = "KeyBind_Action";
            newKeyBindUI.gameObject.transform.Find("Key_Text").GetComponent<TMPro.TextMeshProUGUI>().text = "KeyBind_Key";
            newKeyBindUI.gameObject.transform.Find("InitiateNewKeybindEvent").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { InitiateRebindEvent(string.Format("{0}", i)); });
        }
    }

    public void InitiateRebindEvent(string KeyBind_Action_ID)
    {
        Debug.LogFormat("It's Rebind Time!!");

        //string[] CurrentKeybind = KeyBind_Action_ID.ToLower().Split('_');
        string[] CurrentKeybind = KeyBind_Action_ID.Split('_');

        StartRebindEvent(CurrentKeybind[0], int.Parse(CurrentKeybind[1]));
    }

    public void StartRebindEvent(string KeybindType, int keybindIndex)
    {
        InputAction action = new InputAction();

        action = inputAsset.FindAction(string.Format("Player/{0}", KeybindType /*KeybindType.Replace(KeybindType[0], char.ToUpper(KeybindType[0]))*/));

        action.Disable();

        var rebindOperation = action.PerformInteractiveRebinding(keybindIndex).WithCancelingThrough("").Start();

        rebindOperation.OnComplete((op) => {
            rebindOperation.Dispose();

            Debug.LogFormat("New binding at index 1 = {0}", action.bindings[keybindIndex].effectivePath);

            action.Enable();
        });

        rebindOperation.OnCancel((op) => {
            Debug.Log("Rebind Time Is Cancled!");

            // Re-enable the Binding map
            action.Enable();
        });
    }
}

//rebindOperation.OnApplyBinding((op, path) => {
//    action.ApplyBindingOverride(path);
//    rebindOperation.Dispose();
//
//    Debug.LogFormat("New binding at index 1 = {0}", action.bindings[keybindIndex].effectivePath);
//});
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/* TODO List
 * 
 * - Update the keybind key to properly show the keybind for that keybind action.
 * 
 * Potential Solutions for problems
 * 
 * - Each Keybind Panel/Line/Bar(With its button) can have a script that stores its function and the current keybind and some other information needed for that keybind/action
 * 
*/

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
    private KeybindPanel[] panels;

    private void Awake()
    {
        panels = GameObject.FindObjectsOfType<KeybindPanel>();

        for (int i = 0; i < panels.Length; i++)
        {
            //Debug.LogFormat("Action: {0}_{1} | Name: {2}", panels[i].KeybindAction, panels[i].KeybindIndex ,panels[i].gameObject.name);
            string NewKey = "";

            //action.bindings[keybindPanel.KeybindIndex].effectivePath

            panels[i].SetUIWhenNewKeybind(NewKey);
        }
    }

    public void InitiateRebindEvent(GameObject Button)
    {
        Debug.LogFormat("It's Rebind Time!!");

        StartRebindEvent(Button.GetComponent<KeybindPanel>());
    }

    public void StartRebindEvent(KeybindPanel keybindPanel)
    {
        InputAction action = inputAsset.FindAction(string.Format("Player/{0}", keybindPanel.KeybindAction));

        Debug.LogFormat("{0} {1}", keybindPanel.KeybindAction, keybindPanel.KeybindIndex);

        action.Disable();

        var rebindOperation = action.PerformInteractiveRebinding(keybindPanel.KeybindIndex).WithCancelingThrough("").Start();

        //rebindOperation.OnApplyBinding((op, path) => {
        //
        //    Debug.LogFormat("path = {0}", path);
        //
        //    //Debug.LogFormat("New binding at index 1 = {0}", action.bindings[keybindIndex].effectivePath);
        //});

        rebindOperation.OnComplete((op) => {
            rebindOperation.Dispose();

            Debug.LogFormat("New binding = {0}", action.bindings[keybindPanel.KeybindIndex].effectivePath);

            keybindPanel.SetUIWhenNewKeybind(action.bindings[keybindPanel.KeybindIndex].effectivePath.Replace("<Keyboard>/", "").ToUpper());

            action.Enable();
        });

        rebindOperation.OnCancel((op) => {
            Debug.Log("Rebind Time Is Cancled!");

            // Re-enable the Binding map
            action.Enable();
        });
    }

    private bool IsWhiteNullOrEmpty(string text)
    {
        return string.IsNullOrWhiteSpace(text) || string.IsNullOrEmpty(text);
    }
}
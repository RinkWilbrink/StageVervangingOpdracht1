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

    private void Update()
    {
        
    }

    public void InitiateRebindEvent(GameObject Button)
    {
        Debug.LogFormat("It's Rebind Time!!");

        StartRebindEvent(Button.GetComponent<KeybindPanel>());
    }

    public void StartRebindEvent(KeybindPanel keybindPanel)
    {
        InputAction action = new InputAction();

        action = inputAsset.FindAction(string.Format("Player/{0}", keybindPanel.KeybindAction)); //KeybindType.Replace(KeybindType[0], char.ToUpper(KeybindType[0]))

        action.Disable();

        var rebindOperation = action.PerformInteractiveRebinding(keybindPanel.KeybindIndex).WithCancelingThrough("").Start();

        rebindOperation.OnApplyBinding((op, path) => {
        
            //Debug.LogFormat("New binding at index 1 = {0}", action.bindings[keybindIndex].effectivePath);
        });

        rebindOperation.OnComplete((op) => {
            rebindOperation.Dispose();

            Debug.LogFormat("New binding = {0}", action.bindings[keybindPanel.KeybindIndex].effectivePath);

            keybindPanel.SetUIWhenNewKeybind(action.bindings[keybindPanel.KeybindIndex].effectivePath);

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
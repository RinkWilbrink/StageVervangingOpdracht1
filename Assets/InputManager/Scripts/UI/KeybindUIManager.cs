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

    [Header("Other Script References")]
    [SerializeField] private SaveKeybinds saveKeybinds;

    // Private Variables
    private KeybindPanel[] panels;

    private void Awake()
    {
        saveKeybinds.GetKeybinds();

        SetKeybindsUI();
    }

    public void InitiateRebindEvent(GameObject Button)
    {
        //Debug.LogFormat("It's Rebind Time!!");

        StartRebindEvent(Button.GetComponent<KeybindPanel>());
    }

    public void StartRebindEvent(KeybindPanel keybindPanel)
    {
        InputAction action = inputAsset.FindAction(string.Format("Player/{0}", keybindPanel.KeybindAction));

        action.Disable();

        var rebindOperation = action.PerformInteractiveRebinding(keybindPanel.KeybindIndex).WithCancelingThrough("").Start();

        rebindOperation.OnComplete((op) => {
            rebindOperation.Dispose();

            keybindPanel.SetUIWhenNewKeybind(action.bindings[keybindPanel.KeybindIndex].effectivePath.Replace("<Keyboard>/", "").ToUpper());

            action.Enable();
        });

        rebindOperation.OnCancel((op) => {
            action.Enable();
        });

        //rebindOperation.OnApplyBinding((op, path) => { });
    }

    private void SetKeybindsUI()
    {
        panels = GameObject.FindObjectsOfType<KeybindPanel>();

        for (int i = 0; i < panels.Length; i++)
        {
            string NewKey;

            try
            {
                InputAction action = inputAsset.FindAction(string.Format("Player/{0}", panels[i].KeybindAction));

                NewKey = action.bindings[panels[i].KeybindIndex].effectivePath.Replace("<Keyboard>/", "").ToUpper();
            }
            catch
            {
                NewKey = "Not Found!!!";
            }

            panels[i].SetUIWhenNewKeybind(NewKey);
        }
    }
}
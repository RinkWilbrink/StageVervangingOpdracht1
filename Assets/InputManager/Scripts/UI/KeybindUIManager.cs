using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/* TODO List
 * 
 * 
 * 
 * Potential Solutions for problems
 * 
 * 
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
    private string NewKeybindsString = "";
    private List<string> CustomKeybinds = new List<string>();

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

        InputActionRebindingExtensions.RebindingOperation rebindOperation = action.PerformInteractiveRebinding(keybindPanel.KeybindIndex).WithCancelingThrough("").Start();

        rebindOperation.OnComplete((op) => 
        {
            rebindOperation.Dispose();

            keybindPanel.SetUIWhenNewKeybind(action.bindings[keybindPanel.KeybindIndex].effectivePath.Replace("<Keyboard>/", "").ToUpper());

            StartKeybindSaving();

            action.Enable();
        });

        rebindOperation.OnCancel((op) => {
            action.Enable();
        });

        //rebindOperation.OnApplyBinding((op, path) => { });
    }

    public void StartKeybindSaving()
    {
        for (int i = 0; i < inputAsset.Count(); i++)
        {
            string help = string.Format("{0}/{1}", inputAsset.ToArray()[i].actionMap.ToString().Split(':')[1], inputAsset.ToArray()[i].name);
            InputAction currentAction = inputAsset.FindAction(help);

            for (int actionIndex = 0; actionIndex < currentAction.bindings.Count(); actionIndex++)
            {
                try
                {
                    string BindingText = string.Format("{0}_{1}={2}", help, actionIndex, currentAction.bindings[actionIndex].effectivePath);
                    Debug.LogFormat("{0} | currentBinding = {1}_{2}", BindingText, currentAction.name, currentAction.bindings[actionIndex].name);
                    CustomKeybinds.Add(BindingText);
                }
                catch
                {
                    Debug.LogErrorFormat("help = {0} | actionIndex = {1} | action.bindings[{1}].path = {2} | currentAction.name = {3} | curretnAction.bindings[0].name = {4}",
                        help, actionIndex, currentAction.bindings[actionIndex].effectivePath, currentAction.name, currentAction.bindings[0].name);
                }
            };
        }

        saveKeybinds.SetKeybinds(CustomKeybinds.ToArray());
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

                NewKey = action.bindings[panels[i].KeybindIndex].effectivePath.Split('/')[1].ToUpper();
            }
            catch
            {
                NewKey = "Not Found!!!";
            }

            panels[i].SetUIWhenNewKeybind(NewKey);
        }
    }
}
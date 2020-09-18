using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private SaveKeybinds saveKeybinds;

    // Private Variables
    private KeybindPanel[] panels;
    private string NewKeybindsString = "";
    private List<string> CustomKeybinds = new List<string>();

    private void Awake()
    {
        saveKeybinds = gameObject.GetComponent<SaveKeybinds>();

        saveKeybinds.GetKeybinds();

        SetKeybindsUI();
    }

    /// <summary>Start the rebind event, This function gets called by the Keybind buttons</summary>
    /// <param name="Button"></param>
    public void InitiateRebindEvent(GameObject Button)
    {
        StartRebindEvent(Button.GetComponent<KeybindPanel>());
    }

    //This function is the actual keybind event that lets the user rebind a key for the selected action.
    private void StartRebindEvent(KeybindPanel keybindPanel)
    {
        InputAction action = inputAsset.FindAction(string.Format("Player/{0}", keybindPanel.KeybindAction));

        action.Disable();

        InputActionRebindingExtensions.RebindingOperation rebindOperation = action.PerformInteractiveRebinding(keybindPanel.KeybindIndex).WithCancelingThrough("").Start();

        // Assign the new key, and update the UI and settings file after a succesfull key rebind.
        rebindOperation.OnComplete((op) => 
        {
            rebindOperation.Dispose();

            keybindPanel.SetUIWhenNewKeybind(action.bindings[keybindPanel.KeybindIndex].effectivePath.Replace("<Keyboard>/", "").ToUpper());

            StartKeybindSaving();

            action.Enable();
        });


        // re-enable the action map after a failed rebind event.
        rebindOperation.OnCancel((op) => {
            action.Enable();
        });

        //rebindOperation.OnApplyBinding((op, path) => { });
    }

    /// <summary>This function puts all keybinds and their respective actions into 1 string to write it in the binds.txt file (The file that stores all keybinds).</summary>
    private void StartKeybindSaving()
    {
        for (int i = 0; i < inputAsset.Count(); i++)
        {
            string help = string.Format("{0}/{1}", inputAsset.ToArray()[i].actionMap.ToString().Split(':')[1], inputAsset.ToArray()[i].name);
            InputAction currentAction = inputAsset.FindAction(help);

            for (int actionIndex = 0; actionIndex < currentAction.bindings.Count(); actionIndex++)
            {
                try
                {
                    //Put all keybinds and their actions into a string to be able to save that in a settings file.
                    string BindingText = string.Format("{0}_{1}={2}", help, actionIndex, currentAction.bindings[actionIndex].effectivePath);
                    Debug.LogFormat("{0} | currentBinding = {1}_{2}", BindingText, currentAction.name, currentAction.bindings[actionIndex].name);
                    CustomKeybinds.Add(BindingText);
                }
                catch
                {
                    //Debug.LogErrorFormat("help = {0} | actionIndex = {1} | action.bindings[{1}].path = {2} | currentAction.name = {3} | curretnAction.bindings[0].name = {4}",
                    //    help, actionIndex, currentAction.bindings[actionIndex].effectivePath, currentAction.name, currentAction.bindings[0].name);
                }
            };
        }

        saveKeybinds.SetKeybinds(CustomKeybinds.ToArray());
    }

    /// <summary>This function sets all the UI Keybind rows with their currently assigned keybinds.</summary>
    private void SetKeybindsUI()
    {
        panels = GameObject.FindObjectsOfType<KeybindPanel>();

        for (int i = 0; i < panels.Length; i++)
        {
            string NewKey;

            try
            {
                InputAction action = inputAsset.FindAction(string.Format("Player/{0}", panels[i].KeybindAction));

                // Set the keybind in the UI to propperly display the currently assigned keybind.
                NewKey = action.bindings[panels[i].KeybindIndex].effectivePath.Split('/')[1].ToUpper();
            }
            catch
            {
                // If no key can be found for the current action, this will be set.
                NewKey = "Not Found!!!";
            }

            panels[i].SetUIWhenNewKeybind(NewKey);
        }
    }
}
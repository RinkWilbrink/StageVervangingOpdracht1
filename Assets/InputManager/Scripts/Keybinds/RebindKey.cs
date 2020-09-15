using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindKey : MonoBehaviour
{
    // Variables
    [SerializeField] private InputActionAsset asset;
    [Space(6)]

    public InputActionReference inputActionRef;
    public InputActionAsset inputAsset;

    private bool RebindEventHasHappend = false;
    public int RebindEventCount = 0;

    // When the game starts, instantiate all the keybind rebind menu items.

    public void StartRebindEvent() //InputAction.CallbackContext context
    {
        if(RebindEventHasHappend == false)
        {
            RebindEventCount += 1;

            //inputAsset.Player.Move.actionMap
            InputAction action = inputAsset.FindAction("Player/Move"); //inputActionRef.action;

            action.Disable();

            var rebindOperation = action.PerformInteractiveRebinding(1).OnMatchWaitForAnother(0.1f).WithCancelingThrough("").Start();
            
            rebindOperation.OnApplyBinding((op, path) => { 
                action.ApplyBindingOverride(path); 
                rebindOperation.Dispose();

                // Re-enable the Binding map
                action.Enable();

                Debug.LogFormat("New binding at index 1 = {0}", action.bindings[1].effectivePath);
            });

            rebindOperation.OnComplete((op) =>
            {
                Debug.Log("Completed!");
            });

            rebindOperation.OnCancel((op) =>
            {
                Debug.Log("Cancled!");
            });

            #region redundant
            /*
            for (int i = 0; i < action.bindings.Count; i++)
            {
                Debug.LogFormat("Binding {0}: {1} From {2}", i, action.bindings[i].name, gameObject.name);
            }

            try
            {
                //Debug.LogFormat("Number 1: {0}", action.bindings[1].name);
                if(action != null)
                {
                    //action.ApplyBindingOverride(1, "<keyboard>/q");
                }
                else
                {
                    Debug.Log("Cant rebind because action is null");
                }

                //Debug.LogFormat("Number 2: {0}", action.bindings[1].name);
            }
            catch { Debug.LogFormat("ERROR OFZO!!!!"); }
            */
            #endregion

            Debug.LogFormat("Rebind count: {0}", RebindEventCount);
            RebindEventHasHappend = true;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybindtest : MonoBehaviour
{
	// use Scriptable object InputActionAsset and action.ApplyBindingOverride | Runtime rebinding in the documentation
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendMessage(string Message)
    {
        Debug.Log(Message);
    }

    //public void Fire(InputAction.CallbackContext context)
    //{
    //    Debug.Log("Fire!");
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    [SerializeField] private InputActionAsset asset;
    [SerializeField] private CustomKeybinds controls;

    public void Movement(InputAction.CallbackContext context) //InputAction.CallbackContext context
    {
        Debug.Log("WASD");
        //transform.position += new Vector3(1f, 0, 1f);
    }

    public void Jump()
    {
        Debug.Log("Jump!");
    }
}

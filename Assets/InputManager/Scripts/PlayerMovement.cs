using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    [SerializeField] private InputActionAsset asset;
    public StageVervanging controls;

    void Start()
    {
        //stageVervanging = asset.Inpu;
    }

    /*
    private void OnEnable()
    {
        controls.Player.Move.performed += Movement;
        controls.Player.Move.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= Movement;
        controls.Player.Move.Disable();
    }*/

    public void Movement(InputAction.CallbackContext context) //
    {
        Debug.Log("WASD");
        //transform.position += new Vector3(1f, 0, 1f);
    }
}

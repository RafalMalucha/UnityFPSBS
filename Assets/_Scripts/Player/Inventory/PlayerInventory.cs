using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InputActionAsset InputManager;
    private InputAction _weapon1;
    private InputAction _weapon2;
    private InputAction _weapon3;
    //private List<GameObject> _playerInventory = new List<GameObject>();
    //private List<String> _playerInventoryTest = new List<String>();

    // private void OnEnable() 
    // {
    //     // InputManager.FindActionMap("Weapon1").Enable();
    //     InputManager.FindActionMap("Weapon2").Enable();
    //     InputManager.FindActionMap("Weapon3").Enable();
    // }

    // private void OnDisable() 
    // {
    //     // InputManager.FindActionMap("Weapon1").Disable();
    //     InputManager.FindActionMap("Weapon2").Disable();
    //     InputManager.FindActionMap("Weapon3").Disable();
    // }
    private void Awake() {
        _weapon1 = InputSystem.actions.FindAction("Weapon1");
        _weapon2 = InputSystem.actions.FindAction("Weapon2");
        _weapon3 = InputSystem.actions.FindAction("Weapon3");

        // _playerInventoryTest.Add("item1");
        // _playerInventoryTest.Add("item2");
        // _playerInventoryTest.Add("item3");
    }

    // Update is called once per frame
    void Update()
    {
        if (_weapon1.WasPressedThisFrame())
        {
            Debug.Log("weapon1");
        }
        if (_weapon2.WasPressedThisFrame())
        {
            Debug.Log("weapon2");
        }
        if (_weapon3.WasPressedThisFrame())
        {
            Debug.Log("weapon3");
        }
    }
}

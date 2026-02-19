using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Player/Inventory")]
public class PlayerInventorySO : ScriptableObject
{
    public InputActionAsset InputManager;
    public List<Weapon> _playerWeapons;
}

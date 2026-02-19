using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public PlayerInventorySO playerInventory;
    public GameObject weaponHolder;
    public Transform weaponHolderTransform;
    private InputAction _weapon1_Input;
    private InputAction _weapon2_Input;
    private InputAction _weapon3_Input;
    private InputAction _weapon4_Input;
    private Weapon _currentWeapon;

    void Start()
    {
        Debug.Log(playerInventory._playerWeapons[0].name);
        Debug.Log(playerInventory._playerWeapons[1].name);
        Debug.Log(playerInventory._playerWeapons[2].name);
        Debug.Log(playerInventory._playerWeapons[3].name);

        _weapon1_Input = InputSystem.actions.FindAction("Weapon1");
        _weapon2_Input = InputSystem.actions.FindAction("Weapon2");
        _weapon3_Input = InputSystem.actions.FindAction("Weapon3");
        _weapon4_Input = InputSystem.actions.FindAction("Weapon4");
    }

    // Update is called once per frame
    void Update()
    {
        HandleInventoryInput();
    }

    private void HandleInventoryInput()
    {
        if (_weapon1_Input.WasPressedThisFrame())
        {
            SetCurrentWeapon(playerInventory._playerWeapons[0]);
            SpawnCurrentWeapon();
        }
        if (_weapon2_Input.WasPressedThisFrame())
        {
            SetCurrentWeapon(playerInventory._playerWeapons[1]);
            SpawnCurrentWeapon();
        }
        if (_weapon3_Input.WasPressedThisFrame())
        {
            SetCurrentWeapon(playerInventory._playerWeapons[2]);
            SpawnCurrentWeapon();
        }
        if (_weapon4_Input.WasPressedThisFrame())
        {
            SetCurrentWeapon(playerInventory._playerWeapons[3]);
            SpawnCurrentWeapon();
        }
    }

    public Weapon GetCurrentWeapon()
    {
        return _currentWeapon;
    }

    public void SetCurrentWeapon(Weapon NewWeapon)
    {
        Debug.Log(NewWeapon.name);
        _currentWeapon = NewWeapon;
    }

    private void SpawnCurrentWeapon()
    {
        if(weaponHolder.transform.childCount > 0)
        {
            foreach(Transform child in weaponHolder.transform)
            {
                Destroy(child.gameObject);
            }
            Instantiate(GetCurrentWeapon().model, weaponHolder.transform);
        }
        else
        {
            Instantiate(GetCurrentWeapon().model, weaponHolder.transform);
        }
    }
}

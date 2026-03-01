using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    public float rayDistance = 1f;
    public LayerMask interactableLayer;

    private Ray _ray;
    //private RaycastHit _raycastHit;

    void Update()
    {
        _ray = new Ray(_playerManager.GetMainCamera().transform.position, _playerManager.GetMainCamera().transform.forward);

        if (_playerManager.GetAttackInputAction().WasPressedThisFrame())
        {
            Debug.Log(_playerManager.GetPlayerInventory().GetCurrentWeapon().name);
            
            switch (_playerManager.GetPlayerInventory().GetCurrentWeapon().name)
            {
                case "Pistol":
                    //_playerManager.GetPlayerInventory().GetCurrentWeapon().gameObject.GetComponent<PistolAttack>().Attack(_ray, _raycastHit);
                    _playerManager.GetPlayerInventory().GetCurrentWeapon().gameObject.GetComponent<PistolAttack>().Attack(_ray, interactableLayer); 
                    break;
                case "Rifle":
                    _playerManager.GetPlayerInventory().GetCurrentWeapon().gameObject.GetComponent<RifleAttack>().Attack();
                    break;
                case "T00b":
                    _playerManager.GetPlayerInventory().GetCurrentWeapon().gameObject.GetComponent<T00bAttack>().Attack();
                    break;
                case "LazerBS":
                    _playerManager.GetPlayerInventory().GetCurrentWeapon().gameObject.GetComponent<LazerAttack>().Attack();
                    break;
                default:
                    Debug.Log("no weapon selected");
                    break;
            }
        }
        Debug.DrawRay(_ray.origin, _ray.direction * rayDistance, Color.red);
    }
}

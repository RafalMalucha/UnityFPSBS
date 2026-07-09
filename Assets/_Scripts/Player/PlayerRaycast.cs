using UnityEngine;

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

        if (_playerManager.GetUseInputAction().WasPressedThisFrame())
        {
            _playerManager.GetPlayerInteract().Interact(_ray, interactableLayer);
        }

        if (_playerManager.GetAttackInputAction().IsPressed() && _playerManager.GetPlayerInventory().GetCurrentWeapon().name == "LazerBS")
        {
            _playerManager.GetPlayerInventory().GetCurrentWeapon().gameObject.GetComponent<LazerBehavior>().HandleSingleTickDamage();
        }

        if (_playerManager.GetAttackInputAction().WasPressedThisFrame())
        {
            //Debug.Log(_playerManager.GetPlayerInventory().GetCurrentWeapon().name);
            float timeOfAttack = Time.time;
            //Debug.Log(timeOfAttack);

            switch (_playerManager.GetPlayerInventory().GetCurrentWeapon().name)
            {
                case "Pistol":
                    _playerManager.GetPlayerInventory().GetCurrentWeapon().gameObject.GetComponent<PistolBehavior>().Attack(_ray, interactableLayer, timeOfAttack);
                    break;
                case "Rifle":
                    _playerManager.GetPlayerInventory().GetCurrentWeapon().gameObject.GetComponent<RifleBehavior>().Attack(_ray, interactableLayer, timeOfAttack);
                    break;
                case "T00b":
                    _playerManager.GetPlayerInventory().GetCurrentWeapon().gameObject.GetComponent<T00bBehavior>().Attack(_ray, timeOfAttack);
                    break;
                // case "LazerBS":
                //     _playerManager.GetPlayerInventory().GetCurrentWeapon().gameObject.GetComponent<LazerBehavior>().StartAttack(_ray, interactableLayer, timeOfAttack);
                //     break;
                default:
                    Debug.Log("no weapon selected");
                    break;
            }
        }

        Debug.DrawRay(_ray.origin, _ray.direction * rayDistance, Color.red);
    }
}

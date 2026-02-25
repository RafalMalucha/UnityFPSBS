using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;

    public float rayDistance = 100f;
    public LayerMask interactableLayer;

    void Update()
    {
        Ray ray = new Ray(_playerManager.GetMainCamera().transform.position, _playerManager.GetMainCamera().transform.forward);
        RaycastHit hit;

        if (_playerManager.GetAttackInputAction().WasPressedThisFrame())
        {
            if (_playerManager.GetPlayerInventory().GetCurrentWeapon().gameObject.GetComponent<PistolAttack>())
            {
                Debug.Log(_playerManager.GetPlayerInventory().GetCurrentWeapon().gameObject.name);
                _playerManager.GetPlayerInventory().GetCurrentWeapon().gameObject.GetComponent<PistolAttack>().Attack();
            }
            if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
            {
                Debug.Log("Hit: " + hit.collider.name);
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
    }
}

using UnityEngine;

public class PistolAttack : MonoBehaviour
{
    //public PlayerManager playerManager;
    public Weapon pistol;

    private Ray _pistolRay;
    private RaycastHit _raycastHit;

    private void Start()
    {
        //playerManager = GetComponentInParent<PlayerManager>();
    }

    //public void Attack(Ray ray, RaycastHit raycastHit)
    public void Attack(Ray ray, LayerMask interactableLayer)
    {
        //_pistolRay = new Ray(playerManager.GetMainCamera().transform.position, playerManager.GetMainCamera().transform.forward);
        
        //Debug.Log(playerManager.GetPlayerInventory().GetCurrentWeapon().baseDamage);
        Debug.Log("pistol attack script test");

        Debug.DrawRay(ray.origin, ray.direction * 999, Color.green, 10);

        if (Physics.Raycast(ray, out _raycastHit, 999, interactableLayer))
        {
            Debug.Log("Hit: " + _raycastHit.collider.name);
        }
    }
}

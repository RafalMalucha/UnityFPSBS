using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    private RaycastHit _raycastHit;
    public void Interact(Ray ray, LayerMask interactableLayer)
    {
        Debug.DrawRay(ray.origin, ray.direction * 5, Color.green, 5);

        if (Physics.Raycast(ray, out _raycastHit, 5, interactableLayer))
        {
            if (_raycastHit.collider.transform.GetComponent<ButtonDoorInteract>())
            {
                _raycastHit.collider.transform.GetComponent<ButtonDoorInteract>().OnDoorTrigger();
            }
        }
    }
}

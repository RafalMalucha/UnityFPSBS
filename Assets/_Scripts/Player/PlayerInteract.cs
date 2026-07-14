using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private RaycastHit _raycastHit;
    private int _colletiblesFound = 0;

    public void Interact(Ray ray, LayerMask interactableLayer)
    {
        Debug.DrawRay(ray.origin, ray.direction * 2, Color.green, 5);

        if (Physics.Raycast(ray, out _raycastHit, 2, interactableLayer))
        {
            if (_raycastHit.collider.transform.GetComponent<ButtonDoorInteract>())
            {
                _raycastHit.collider.transform.GetComponent<ButtonDoorInteract>().OnDoorTrigger();
            }
        }
    }

    public void CollectibleFound()
    {
        _colletiblesFound++;
    }

    public int GetCollectiblesFound()
    {
        return _colletiblesFound;
    }
}

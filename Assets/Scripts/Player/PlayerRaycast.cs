using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRaycast : MonoBehaviour
{
    public InputActionAsset InputManager;
    private InputAction _attack;
    public float rayDistance = 100f;
    public LayerMask interactableLayer;
    public Camera camera;

    private void OnEnable() 
    {
        InputManager.FindActionMap("Player").Enable();
    }

    private void Awake()
    {
        _attack = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;

        if (_attack.WasPressedThisFrame())
        {
            if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
            {
                Debug.Log("Hit: " + hit.collider.name);
            }
            Debug.Log("attack");
        }
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
    }
}

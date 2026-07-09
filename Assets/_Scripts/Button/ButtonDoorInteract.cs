using UnityEngine;

public class ButtonDoorInteract : MonoBehaviour
{
    [SerializeField] GameObject doorObject;

    public void OnDoorTrigger()
    {
        Destroy(doorObject);
    }
}

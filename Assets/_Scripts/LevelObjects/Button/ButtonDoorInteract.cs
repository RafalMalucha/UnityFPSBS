using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonDoorInteract : MonoBehaviour
{
    [SerializeField] GameObject doorObject;

    [SerializeField] private bool isActive;
    private bool _isOpen = false;
    private Vector3 doorPosition;

    private void Awake()
    {
        doorPosition = doorObject.transform.position;
    }

    public void SetActive()
    {
        isActive = true;
    }

    public void DoorClose()
    {
        doorObject.transform.position += new Vector3(0f, -3f, 0f);
        _isOpen = false;
    }

    public void DoorOpen()
    {
        doorObject.transform.position += new Vector3(0f, 3f, 0f);
        _isOpen = true;
    }

    public void OnDoorTrigger()
    {
        if(isActive)
        {
            if(_isOpen)
            {
                DoorClose();
            } 
            else
            {
                DoorOpen();
            }
        }
    }

    public bool GetIsOpen()
    {
        return _isOpen;
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    public void SetIsActive(bool newIsActive)
    {
        isActive = newIsActive;
    }
}

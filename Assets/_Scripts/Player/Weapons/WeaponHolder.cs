using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public Camera camera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCurrentWeapon()
    {
        if(transform.childCount > 0)
        {
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            Instantiate(playerInventory.GetCurrentWeapon().gameObject, transform);
        }
        else
        {
            Instantiate(playerInventory.GetCurrentWeapon().gameObject, transform);
        }
    }
}

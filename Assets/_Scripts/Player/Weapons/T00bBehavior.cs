using UnityEngine;

public class T00bBehavior : MonoBehaviour
{
    public Weapon T00b;
    public void Attack(Ray ray, LayerMask interactableLayer, float timeOfAttack)
    {
        Debug.Log("t00b attack script test");
    }
}
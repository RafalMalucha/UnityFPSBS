using UnityEngine;

public class LazerBehavior : MonoBehaviour
{
    public Weapon lazer;
    public void Attack(Ray ray, LayerMask interactableLayer, float timeOfAttack)
    {
        Debug.Log("lazer attack script test");
    }
}

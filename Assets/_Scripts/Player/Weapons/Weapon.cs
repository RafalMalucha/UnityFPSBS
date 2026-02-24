using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    public new string name;
    public GameObject gameObject;
    public int baseDamage;
}

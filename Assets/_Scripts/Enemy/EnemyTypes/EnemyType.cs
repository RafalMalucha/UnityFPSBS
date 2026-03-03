using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Scriptable Objects/EnemyType")]
public class EnemyType : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public int baseDamage;
    public int armor;
    public int speed;
    public int health;
}

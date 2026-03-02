using System;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private SingleEnemyManager _singleEnemyManager;

    private void Awake()
    {
        _singleEnemyManager = transform.GetComponent<SingleEnemyManager>();
    }
    public void OnHit(int baseDamage)
    {
        Debug.Log(transform.name+" got hit");
        _singleEnemyManager.OnHit(baseDamage);
    }
}

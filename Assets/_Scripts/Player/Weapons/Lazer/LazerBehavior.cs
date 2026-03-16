using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LazerBehavior : MonoBehaviour
{
    public Weapon lazer;

    [SerializeField] private LayerMask _interactableLayer;

    public void HandleSingleTickDamage()
    {
        RaycastHit[] _enemiesHit = CheckForEnemiesInLazerRange();

        try
        {
            RaycastHit closestEnemyHit = _enemiesHit[0];
            foreach (RaycastHit raycastHit in _enemiesHit)
            {
                if (raycastHit.distance < closestEnemyHit.distance) { closestEnemyHit = raycastHit; }
            }

            try { closestEnemyHit.transform.GetComponent<EnemyHitbox>().OnHit(0.1f); }
            catch (Exception) { }
        }
        catch (Exception) { }
    }

    private RaycastHit[] CheckForEnemiesInLazerRange()
    {
        return Physics.SphereCastAll(
            PlayerManager.Instance.transform.position,
            5f,
            PlayerManager.Instance.transform.forward,
            7.5f,
            _interactableLayer
        );
    }
}

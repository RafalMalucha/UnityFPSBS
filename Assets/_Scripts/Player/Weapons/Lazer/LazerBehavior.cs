using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LazerBehavior : MonoBehaviour
{
    public Weapon lazer;

    [SerializeField] private LayerMask _interactableLayer;
    //[SerializeField] private int _lazerRange = 20;

    private Transform _cameraPosition;
    private RaycastHit[] _enemiesHit;
    private bool _isAttacking;

    private void Awake()
    {
        gameObject.SetActive(true);
        _isAttacking = false;
        _cameraPosition = PlayerManager.Instance.GetMainCamera().transform;
        // StartCoroutine(Test());
    }

    private void Update()
    {
        //Debug.Log(_isAttacking);

        // if (!_isAttacking)
        // {
        //     CheckForEnemiesInLazerRange();
        // }
        //
        CheckForEnemiesInLazerRange();


        try
        {
            RaycastHit closestEnemyHit = _enemiesHit[0];
            foreach (RaycastHit raycastHit in _enemiesHit)
            {
                if (raycastHit.distance > closestEnemyHit.distance)
                {
                    closestEnemyHit = raycastHit;
                }
            }
            Debug.Log(closestEnemyHit.transform.name);
        }
        catch (Exception) { }
    }

    public void StartAttack(Ray ray, LayerMask interactableLayer, float timeOfAttack)
    {
        Debug.Log("lazer start attack");
        _isAttacking = true;
        //StartCoroutine(HandleDamage());
        Debug.Log(_isAttacking);
    }

    public void EndAttack()
    {
        Debug.Log("lazer end attack");
        _isAttacking = false;
        //StopCoroutine(HandleDamage());
        Debug.Log(_isAttacking);
    }

    public void HandleSingleTickDamage()
    {
        CheckForEnemiesInLazerRange();

        try
        {
            RaycastHit closestEnemyHit = _enemiesHit[0];
            foreach (RaycastHit raycastHit in _enemiesHit)
            {
                if (raycastHit.distance > closestEnemyHit.distance)
                {
                    closestEnemyHit = raycastHit;
                }
            }

            DealSingleTickDamage(closestEnemyHit);
        }
        catch (Exception) { }
    }

    private void CheckForEnemiesInLazerRange()
    {
        _enemiesHit = Physics.SphereCastAll(
            transform.position,
            5f,
            transform.forward,
            7.5f,
            _interactableLayer
        );
    }

    private void DealSingleTickDamage(RaycastHit raycastHit)
    {
        try
        {
            raycastHit.transform.GetComponent<EnemyHitbox>().OnHit(0.1f);
        }
        catch (Exception) { }
    }

    // IEnumerator Test()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(1);
    //         Debug.Log("current attack state" + _isAttacking);
    //     }
    // }

    // IEnumerator HandleDamage()
    // {
    //     while (true)
    //     {
    //         Debug.Log("deal damage");
    //     }
    //     yield return null;
    // }

    void OnDestroy()
    {
        EndAttack();
    }

}

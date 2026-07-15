using System.Collections;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    private bool _playerInMeleeRange = false;
    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Player")
        {
            Debug.Log("Enemy melee attack");
            _playerInMeleeRange = true;
            StartCoroutine(EnemyMeleeAttackCoroutine());
        }
    }

    private void OnTriggerExit(Collider collider) 
    {
        if (collider.tag == "Player")
        {
            _playerInMeleeRange = false;
        }
    }

    IEnumerator EnemyMeleeAttackCoroutine()
    {
        float newRandomAttackDelayTime = Random.Range(0.0f, 0.25f);
        yield return new WaitForSeconds(newRandomAttackDelayTime);

        if(_playerInMeleeRange)
        {
            PlayerManager.Instance.GetPlayerHealth().DamagePlayerHealth(GetComponent<SingleEnemyManager>().GetEnemyType().baseDamage);
        }
    }
}

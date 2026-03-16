using UnityEngine;

public class PistolBehavior : MonoBehaviour
{
    public Weapon pistol;
    private RaycastHit _raycastHit;

    [SerializeField] private float _attackCooldown = 0.25f;
    private static float _lastAttackTime = 0f;

    private void Start()
    {
        //playerManager = GetComponentInParent<PlayerManager>();
    }

    public void Attack(Ray ray, LayerMask interactableLayer, float timeOfAttack)
    {
        float thisAttackTime = timeOfAttack;

        //Debug.Log("last attack time: " + _lastAttackTime);
        //Debug.Log("this attack time: " + thisAttackTime);

        //Debug.Log(_lastAttackTime + _attackCooldown);

        if(thisAttackTime >= _lastAttackTime + _attackCooldown)
        {
            Debug.DrawRay(ray.origin, ray.direction * 999, Color.green, 5);

            AudioManager.Instance.PlaySound(AudioManager.SoundType.Pistol);

            if (Physics.Raycast(ray, out _raycastHit, 999, interactableLayer))
            {
                if (_raycastHit.collider.transform.GetComponent<SingleEnemyManager>())
                {
                    _raycastHit.collider.transform.GetComponent<EnemyHitbox>().OnHit(pistol.baseDamage);
                }
                //Debug.Log("Hit: " + _raycastHit.collider.transform.parent.name);
                //Debug.Log("Hit: " + _raycastHit.collider.transform);
            }

            _lastAttackTime = timeOfAttack;
        }
    }
}

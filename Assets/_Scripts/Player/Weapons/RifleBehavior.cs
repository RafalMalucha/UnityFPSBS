using UnityEngine;

public class RifleBehavior : MonoBehaviour
{
    public Weapon rifle;
    private RaycastHit _raycastHit;

    [SerializeField] private float _attackCooldown = 1f;
    private static float _lastAttackTime = 0f;

    private void Awake()
    {
        _lastAttackTime = 0.0f;
    }

    public void Attack(Ray ray, LayerMask interactableLayer, float timeOfAttack)
    {
        float thisAttackTime = timeOfAttack;

        Debug.Log("last attack time: " + _lastAttackTime);
        Debug.Log("this attack time: " + thisAttackTime);

        Debug.Log(_lastAttackTime + _attackCooldown);

        if(thisAttackTime >= _lastAttackTime + _attackCooldown)
        {
            Debug.DrawRay(ray.origin, ray.direction * 999, Color.purple, 7);

            //TODO change sounds for rifle
            AudioManager.Instance.PlaySound(AudioManager.SoundType.Pistol);

            if (Physics.Raycast(ray, out _raycastHit, 999, interactableLayer))
            {
                if (_raycastHit.collider.transform.GetComponent<SingleEnemyManager>())
                {
                    _raycastHit.collider.transform.GetComponent<EnemyHitbox>().OnHit(rifle.baseDamage);
                }
                Debug.Log("Hit: " + _raycastHit.collider.transform.parent.name);
                Debug.Log("Hit: " + _raycastHit.collider.transform);
            }

            _lastAttackTime = timeOfAttack;
        }
    }
}

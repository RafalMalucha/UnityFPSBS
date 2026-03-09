using UnityEngine;

public class T00bBehavior : MonoBehaviour
{
    public Weapon T00b;
    private RaycastHit _raycastHit;

    [SerializeField] private float _attackCooldown = 3.0f;
    private static float _lastAttackTime = 0f;

    [SerializeField] private GameObject _rocketPrefab;
    [SerializeField] private Transform _rocketSpawnPoint;

    private void Awake()
    {
        _lastAttackTime = 0.0f;
    }

    //public void Attack(Ray ray, LayerMask interactableLayer, float timeOfAttack)
    public void Attack(Ray ray, float timeOfAttack)
    {
        Debug.Log("t00b attack script test");

        float thisAttackTime = timeOfAttack;

        Debug.Log("last attack time: " + _lastAttackTime);
        Debug.Log("this attack time: " + thisAttackTime);

        Debug.Log(_lastAttackTime + _attackCooldown);

        if(thisAttackTime >= _lastAttackTime + _attackCooldown)
        {
            Debug.DrawRay(ray.origin, ray.direction * 999, Color.pink, 15);

            // TODO change sounds
            AudioManager.Instance.PlaySound(AudioManager.SoundType.Pistol);

            //if (Physics.Raycast(ray, out _raycastHit, 999, interactableLayer))
            if (Physics.Raycast(ray, out _raycastHit, 999))
            {
                Vector3 raycastHitPoint = _raycastHit.point;
                Debug.Log("hit position" + _raycastHit.point);
                Debug.Log("rocket origin" + _rocketSpawnPoint.position);

                PlayerAttackHandler.Instance.SpawnRocket(_rocketSpawnPoint, raycastHitPoint);

                if (_raycastHit.collider.transform.GetComponent<SingleEnemyManager>())
                {
                    _raycastHit.collider.transform.GetComponent<EnemyHitbox>().OnHit(T00b.baseDamage);
                }

                Debug.Log("hit position" + _raycastHit.point);
            }

            _lastAttackTime = timeOfAttack;
        }
    }
}
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    [SerializeField]    private float speed = 50.0f;
    [SerializeField]    private GameObject _rocketExplosion;

    private RaycastHit _raycastHit;
    private bool _hasRocketExploded = false;
    private float _sphereCastRadius = 0.1f;
    private float _lifeTime = 5f;
    private float _rocketStartTime;

    private void Awake()
    {
        _rocketStartTime = Time.time;
        Debug.Log("rocket spawned");
        Debug.Log(transform.position);
    }

    void Update()
    {
        if (!_hasRocketExploded)
        {
            if (Time.time > _rocketStartTime + _lifeTime)
            {
                _hasRocketExploded = true;
                RocketExplode();
            }

            transform.position += transform.forward * speed * Time.deltaTime;

            if(Physics.SphereCast(transform.position, _sphereCastRadius, transform.forward, out _raycastHit, 1))
            {
                // Debug.LogWarning("-----------------");
                // Debug.Log("hit object name" + _raycastHit.transform.name);
                // Debug.Log("hit object position" + _raycastHit.transform.position);
                // Debug.Log("distance to hit object" + _raycastHit.distance);
                // Debug.Log("----");
                // Debug.Log("current rocket position" + transform.position);
                // Debug.LogWarning("-----------------");

                if(_raycastHit.distance < 0.25f)
                {
                    _hasRocketExploded = true;
                    RocketExplode();
                }
            }
        }

    }

    private void RocketExplode()
    {
        RaycastHit[] objectsHit = Physics.SphereCastAll(transform.position, 2.5f, transform.forward, 1);
        
        GameObject explosion = Instantiate(
            _rocketExplosion,
            transform.position,
            Quaternion.identity
        );

        AudioManager.Instance.PlaySound(AudioManager.SoundType.Pistol);

        foreach(RaycastHit objectHit in objectsHit)
        {
            try
            {
                var singleEnemyManager = objectHit.transform.GetComponent<SingleEnemyManager>();
                objectHit.transform.GetComponent<SingleEnemyManager>().OnHit(30);
            }catch(Exception){}
        }

        StartCoroutine(WaitForExplosionDestroy(explosion));
    }

    IEnumerator WaitForExplosionDestroy(GameObject explosion)
    {
        yield return new WaitForSeconds(1);
        Destroy(explosion);
        Destroy(gameObject);
    }
}

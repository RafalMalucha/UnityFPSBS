using Unity.VisualScripting;
using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    [SerializeField]    private float speed = 50.0f;

    private BoxCollider _rocketHitBox;
    private Ray _ray;
    private RaycastHit _raycastHit;
    private float _sphereCastRadius = 0.1f;

    private void Awake()
    {
        Debug.Log("rocket spawned");
        Debug.Log(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        transform.position += transform.forward * speed * Time.deltaTime;

        if(Physics.SphereCast(transform.position, _sphereCastRadius, transform.forward, out _raycastHit, 10))
        {
            Debug.Log(_raycastHit.transform.name);
            Debug.Log(_raycastHit.distance);

            if(_raycastHit.distance < 0.1f)
            {
                RocketExplode();
            }
        }
        

        //if (Vector3.Distance(transform.position, _target) < 0.001f)
        // {
        //     RocketExplode();
        // }
    }

    public void SetTarget(Vector3 newTarget)
    {
        //_target = newTarget;    
        Debug.Log("target set");
    }

    private void RocketExplode()
    {
        GameObject testSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        testSphere.transform.position = transform.position;
        testSphere.transform.localScale = new Vector3(5,5,5);

        Destroy(gameObject);
        Debug.Log("rocket explode");
    }
}

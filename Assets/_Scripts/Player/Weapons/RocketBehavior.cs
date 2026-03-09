using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    [SerializeField]    private Vector3 _target;
    [SerializeField]    private float speed = 50.0f;

    private void Awake()
    {
        Debug.Log("rocket spawned");
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _target, step);

        if (Vector3.Distance(transform.position, _target) < 0.001f)
        {
            RocketExplode();
        }
    }

    public void SetTarget(Vector3 newTarget)
    {
        _target = newTarget;    
        Debug.Log("target set");
    }

    private void RocketExplode()
    {
        Debug.Log("rocket explode");
    }
}

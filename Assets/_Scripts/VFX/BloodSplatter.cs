using UnityEngine;
using System.Collections;

public class BloodSplatter : MonoBehaviour
{
    private void Awake() 
    {
        StartCoroutine(BloodSplatterLifetime());
    }

    IEnumerator BloodSplatterLifetime()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}

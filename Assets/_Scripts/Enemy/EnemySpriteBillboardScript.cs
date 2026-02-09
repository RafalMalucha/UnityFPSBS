using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpriteBillboardScript : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Start() {
    }

    private void LateUpdate() {
        Vector3 cameraPosition = _camera.transform.position;

        cameraPosition.y = transform.position.y;
        
        transform.LookAt(cameraPosition);
        transform.Rotate(0f, 180f, 0f);
    }
}

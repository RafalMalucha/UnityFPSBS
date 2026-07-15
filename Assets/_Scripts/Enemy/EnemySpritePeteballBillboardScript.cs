using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpritePeteballBillboardScript : MonoBehaviour
{
    //[SerializeField] private Camera _camera;
    private GameObject _player;

    private void Start() {
    }

    private void LateUpdate() {
        if (GameObject.Find("Player(Clone)"))
        {   
            _player = GameObject.Find("Player(Clone)");
            Vector3 playerPosition = _player.transform.position;
            //playerPosition.y = transform.position.y;
            transform.LookAt(playerPosition);
            transform.Rotate(0f, 180f, 0f);
        }
        // //Vector3 cameraPosition = _camera.transform.position;

        // //cameraPosition.y = transform.position.y;
        
        // //transform.LookAt(cameraPosition);
        // transform.Rotate(0f, 180f, 0f);
    }
}

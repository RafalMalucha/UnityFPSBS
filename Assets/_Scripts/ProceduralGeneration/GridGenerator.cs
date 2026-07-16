using System;
using UnityEngine;

[ExecuteInEditMode]
public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _floor;
    [SerializeField] private GameObject _wall;
    [SerializeField] private int _gridSizeX;
    [SerializeField] private int _gridSizeZ;
    [SerializeField] private int _gridOffset;

    private void OnValidate() {
        BuildGrid();
    }

    void Start()
    {
        BuildGrid();
    }

    private void BuildGrid()
    {
        foreach(Transform child in transform)
        {
            UnityEditor.EditorApplication.delayCall+=()=>
            {
                UnityEditor.Undo.DestroyObjectImmediate(child.gameObject);
            };
        }

        for(int x = 0; x < _gridSizeX; x++)
        {
            for(int z = 0; z < _gridSizeZ; z++)
            {
                Vector3 position = new Vector3(x * _gridOffset, 0, z * _gridOffset);

                GameObject floor = Instantiate(_floor, position, Quaternion.identity);

                floor.transform.SetParent(this.transform);

                float wallSeedX = UnityEngine.Random.Range(0.0f, 4.0f);
                float wallSeedZ = UnityEngine.Random.Range(0.0f, 4.0f);

                if(Math.Floor(wallSeedX) == 0)
                {
                    Debug.Log("No walls");
                }
                if(Math.Floor(wallSeedX) == 1)
                {
                    GameObject wall = Instantiate(_wall, floor.transform.position + new Vector3(10, 3, 0), Quaternion.identity * Quaternion.Euler(0, 90, 0));
                    wall.transform.SetParent(floor.transform);
                }
                if(Math.Floor(wallSeedX) == 2)
                {
                    GameObject wall = Instantiate(_wall, floor.transform.position + new Vector3(-10, 3, 0), Quaternion.identity * Quaternion.Euler(0, 90, 0));
                    wall.transform.SetParent(floor.transform);
                }
                if(Math.Floor(wallSeedX) == 3)
                {
                    GameObject wall1 = Instantiate(_wall, floor.transform.position + new Vector3(-10, 3, 0), Quaternion.identity * Quaternion.Euler(0, 90, 0));
                    wall1.transform.SetParent(floor.transform);

                    GameObject wall2 = Instantiate(_wall, floor.transform.position + new Vector3(10, 3, 0), Quaternion.identity * Quaternion.Euler(0, 90, 0));
                    wall2.transform.SetParent(floor.transform);
                }

                if(Math.Floor(wallSeedZ) == 0)
                {
                    Debug.Log("No walls");
                }
                if(Math.Floor(wallSeedZ) == 1)
                {
                    GameObject wall = Instantiate(_wall, floor.transform.position + new Vector3(0, 3, 10), Quaternion.identity);
                    wall.transform.SetParent(floor.transform);
                }
                if(Math.Floor(wallSeedZ) == 2)
                {
                    GameObject wall = Instantiate(_wall, floor.transform.position + new Vector3(0, 3, -10), Quaternion.identity);
                    wall.transform.SetParent(floor.transform);
                }
                if(Math.Floor(wallSeedZ) == 3)
                {
                    GameObject wall1 = Instantiate(_wall, floor.transform.position + new Vector3(0, 3, -10), Quaternion.identity);
                    wall1.transform.SetParent(floor.transform);

                    GameObject wall2 = Instantiate(_wall, floor.transform.position + new Vector3(0, 3, 10), Quaternion.identity);
                    wall2.transform.SetParent(floor.transform);
                }
            }
        }
    }
}

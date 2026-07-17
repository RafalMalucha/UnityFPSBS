using UnityEngine;

[ExecuteInEditMode]
public class PresetRoomsGrid : MonoBehaviour
{
    [SerializeField] private GameObject[] _rooms;
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

        var counter = 0;

        for(int x = 0; x < _gridSizeX; x++)
        {
            for(int z = 0; z < _gridSizeZ; z++)
            {

                Vector3 position = new Vector3(x * _gridOffset, 0, z * _gridOffset);

                GameObject room = Instantiate(_rooms[Random.Range(0, _rooms.Length)], position, Quaternion.identity);

                room.transform.SetParent(this.transform);
                room.transform.name = "floor_" + x + "_" + z;

                counter++;
            }
        }
    }
}

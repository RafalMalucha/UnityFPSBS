using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class PathVisualizer : MonoBehaviour
{
    [SerializeField] private List<Node> _path;

    private void OnDrawGizmos()
    {
        
        if (_path == null)
            return;

        foreach (Node node in _path)
        {

            var pos = new Vector3(
                this.transform.position.x + (node.GridPosition.x * GetComponent<GridManager>().GetCellSize()), 
                this.transform.position.y, 
                this.transform.position.z + (node.GridPosition.y * GetComponent<GridManager>().GetCellSize())
            );
            Gizmos.color = Color.green;
            Gizmos.DrawCube(pos, new Vector3(0.5f, 0.5f, 0.5f));
        }
    }

    public void SetNewPath(List<Node> newPath)
    {
        _path = new List<Node>();
        _path = newPath;
    }
}

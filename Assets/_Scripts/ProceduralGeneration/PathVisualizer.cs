using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PathVisualizer : MonoBehaviour
{
    [SerializeField] private List<Node> _path;

    private void OnDrawGizmos()
    {
        
        if (_path == null)
            return;

        foreach (Node node in _path)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(node.WorldPosition, new Vector3(0.5f, 0.5f, 0.5f));
        }
    }

    public void SetNewPath(List<Node> newPath)
    {
        _path = new List<Node>();
        _path = newPath;
    }
}

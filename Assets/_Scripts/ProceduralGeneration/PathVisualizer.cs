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
            Gizmos.DrawCube(node.WorldPosition, new Vector3(1.0f, 1.0f, 1.0f));
        }
    }

    public void SetNewPath(List<Node> newPath)
    {
        _path = newPath;
    }
}

using UnityEngine;
using System.Collections.Generic;

public class GraphManager : MonoBehaviour
{
    public List<Node> Nodes;
    public List<Agent> Agents;
    public GameObject nodePrefab;

    private void Start()
    {
        InitializeGraph();
    }

    private void InitializeGraph()
    {
        Nodes = new List<Node>(FindObjectsOfType<Node>());

        Debug.Log("Number of nodes in the graph: " + Nodes.Count);
    }

    public List<Node> FindPath(Node startNode, Node endNode)
    {
        Pathfinding pathfinding = new Pathfinding();
        return pathfinding.FindPath(startNode, endNode);
    }

    private void OnDrawGizmos()
    {
        if (Nodes == null) return;

        int startNodeIndex = 0;
        int endNodeIndex = 1;

        if (startNodeIndex < Nodes.Count && endNodeIndex < Nodes.Count)
        {
            List<Node> path = FindPath(Nodes[startNodeIndex], Nodes[endNodeIndex]);

            if (path != null)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Gizmos.DrawLine(path[i].transform.position, path[i + 1].transform.position);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector2 Position => transform.position;
    //public List<Node> Neighbors { get; private set; }
    public List<Node> Neighbors { get; private set; }
    public Node[] neighbors;

    // A* related properties
    public float gCost;
    public float hCost;
    public Node parent;

    private void Awake()
    {
        Neighbors = new List<Node>(neighbors);
        // Print the number of neighbors for this node
        Debug.Log("Number of neighbors for node " + name + ": " + Neighbors.Count);

    }

    public float fCost
    {
        get { return gCost + hCost; }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.25f);

        if (Neighbors == null || neighbors == null) return;

        //Gizmos.color = Color.white;
        foreach (Node neighbor in neighbors)
        {
            if (neighbor != null)
            {
                //Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Node CurrentNode;
    public List<Node> TargetNodes;
    public float Speed = 2.0f; // Agent's speed (units per second)

    private int currentTargetNodeIndex;
    private List<Node> currentPath;
    private GraphManager graphManager;

    private void Start()
    {
        graphManager = FindObjectOfType<GraphManager>();
        //CurrentNode = graphManager.Nodes[0]; // Set the starting node for the agent
        //TargetNodes = new List<Node> { graphManager.Nodes[0], graphManager.Nodes[1] }; // Set the target nodes
        //StartCoroutine(MoveAlongPath());
    }

    private IEnumerator MoveAlongPath()
    {
        currentTargetNodeIndex = 0;

        while (currentTargetNodeIndex < TargetNodes.Count)
        {
            Node targetNode = TargetNodes[currentTargetNodeIndex];
            currentPath = graphManager.FindPath(CurrentNode, targetNode);

            for (int i = 0; i < currentPath.Count; i++)
            {
                Vector2 startPosition = transform.position;
                Vector2 endPosition = currentPath[i].Position;
                float travelTime = Vector2.Distance(startPosition, endPosition) / Speed;
                float timeElapsed = 0;

                while (timeElapsed < travelTime)
                {
                    timeElapsed += Time.deltaTime;
                    float t = timeElapsed / travelTime;
                    transform.position = Vector2.Lerp(startPosition, endPosition, t);
                    yield return null;
                }

                CurrentNode = currentPath[i];
            }

            currentTargetNodeIndex++;
        }
    }
}

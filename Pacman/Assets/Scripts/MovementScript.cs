using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public GameObject currentNode;
    public string direction = "";
    public float speed = 1f;
    public string previousMovingDirection = "";
    public bool isGhost = false;
    public GameObject pacmanStartNode;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        NodeController nodeController = currentNode.GetComponent<NodeController>();
        transform.position = Vector2.MoveTowards(transform.position, currentNode.transform.position, speed* Time.deltaTime);
        if(transform.position.x == currentNode.transform.position.x && transform.position.y == currentNode.transform.position.y)
        {


            if (isGhost)
            {
                GetComponent<GhostScript>().ReachedCenterOfNode(nodeController);
            }

            if(nodeController.isGhostStartingNode && direction == "down" && (!isGhost || GetComponent<GhostScript>().ghostNode != GhostScript.GhostNodes.Recreate)) 
            {
                direction = previousMovingDirection;
            }
            GameObject newNode = nodeController.GetNodeFromDirection(direction);
            if( newNode != null)
            {
                currentNode = newNode;
                previousMovingDirection = direction;
            }

            else
            {
                direction = previousMovingDirection;
                newNode = nodeController.GetNodeFromDirection(direction);
                if (newNode != null)
                {
                    currentNode = newNode;
                }

            }
        }
    }

    public void SetDirection(string newDirection)
    {
        direction = newDirection;
    }
}

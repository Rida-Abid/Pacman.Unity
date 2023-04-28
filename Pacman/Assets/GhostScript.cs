using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public enum GhostNodes
    {
       Recreate,
       StartNode,
       CenterNode,
       LeftNode,
       RightNode,
       MovingBetweenNodes,

    }

    public GhostNodes ghostNode;
    public GhostNodes recreateState;
    public enum GhostName
    {
        Blinky,
        Inky,
        Pinky,
        Clyde
    }

    public GhostName ghostName;
    
    public GameObject ghostNodeStart;
    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;
    public GameObject ghostNodeCenter;

    public MovementScript movementController;
    public GameObject StartingNode;
    public bool readyToLeaveHome = false;

    public GameControllerScript gameController;

    public bool Recreate = false;

    // Start is called before the first frame update
    void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();

        movementController = GetComponent<MovementScript>();

        if(ghostName == GhostName.Blinky)
        {
            ghostNode = GhostNodes.StartNode;
            recreateState = GhostNodes.CenterNode;
            StartingNode = ghostNodeStart;
            readyToLeaveHome = true;
        }
        else if (ghostName == GhostName.Pinky)
        {
            ghostNode = GhostNodes.CenterNode;
            recreateState = GhostNodes.CenterNode;
            StartingNode = ghostNodeCenter;
        }
        else if (ghostName == GhostName.Inky)
        {
            ghostNode = GhostNodes.LeftNode;
            recreateState = GhostNodes.LeftNode;
            StartingNode = ghostNodeLeft;
        }
        else if (ghostName == GhostName.Clyde)
        {
            ghostNode = GhostNodes.RightNode;
            recreateState = GhostNodes.RightNode;
            StartingNode = ghostNodeRight;
        }

        movementController.currentNode = StartingNode;
        transform.position = StartingNode.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Recreate == true)
        {
            ghostNode = GhostNodes.Recreate;
            Recreate = false;
        }
    } 

    public void ReachedCenterOfNode(NodeController nodeController)
    {
        if(ghostNode == GhostNodes.MovingBetweenNodes)
        {
            if(ghostName == GhostName.Blinky)
            {
                DetermineBlinkyDirection();
            }
        }
        else if(ghostNode == GhostNodes.Recreate)
        {
            string direction = "";

            if(transform.position.x == ghostNodeStart.transform.position.x && transform.position.y == ghostNodeStart.transform.position.y)
            {
                direction = "down";
            }

            else if (transform.position.x == ghostNodeCenter.transform.position.x && transform.position.y == ghostNodeCenter.transform.position.y)
            {
                if(recreateState == GhostNodes.CenterNode)
                {
                    ghostNode = recreateState;
                }
                else if(recreateState == GhostNodes.LeftNode)
                {
                    direction = "left";
                }
                else if (recreateState == GhostNodes.RightNode)
                {
                    direction = "right";
                }
            }

            else if (transform.position.x == ghostNodeLeft.transform.position.x && transform.position.y == ghostNodeLeft.transform.position.y)
            {
                direction = "down";
            }

            else if (transform.position.x == ghostNodeStart.transform.position.x && transform.position.y == ghostNodeStart.transform.position.y)
            {
                direction = "down";
            }

            direction = GetClosestDirection(ghostNodeStart.transform.position);
            movementController.SetDirection(direction);
        }
        else
        {
            if(readyToLeaveHome == true)
            {
                if (ghostNode == GhostNodes.LeftNode)
                {
                    ghostNode = GhostNodes.CenterNode;
                    movementController.SetDirection("right");
                }

                else if (ghostNode == GhostNodes.RightNode)
                {
                    ghostNode = GhostNodes.CenterNode;
                    movementController.SetDirection("left");

                }

                else if(ghostNode == GhostNodes.CenterNode)
                {
                    ghostNode = GhostNodes.StartNode;
                    movementController.SetDirection("up");

                }
                else if(ghostNode == GhostNodes.StartNode)
                {
                    ghostNode = GhostNodes.MovingBetweenNodes;
                    movementController.SetDirection("left");

                }
            }
        }

    }

    void DetermineBlinkyDirection()
    {
        string direction = GetClosestDirection(gameController.pacman.transform.position);
        movementController.SetDirection(direction);
    }

    void DeterminePinkyDirection()
    {

    }

    void DetermineInkyDirection()
    {

    }

    void DetermineClydeDirection()
    {

    }

    string GetClosestDirection(Vector2 target)
    {
        float shortestDistance = 0;
        string previousMovingDirection = movementController.previousMovingDirection;
        string newDirection = "";
        NodeController nodeController = movementController.currentNode.GetComponent<NodeController>();

        if(nodeController.canMoveUp && previousMovingDirection != "down")
        {
            GameObject UpNode = nodeController.upNode;
            float distance = Vector2.Distance(UpNode.transform.position, target);
            if(distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "up";
            }
        }

        if (nodeController.canMoveDown && previousMovingDirection != "up")
        {
            GameObject DownNode = nodeController.downNode;
            float distance = Vector2.Distance(DownNode.transform.position, target);
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "down";
            }
        }

        if (nodeController.canMoveLeft && previousMovingDirection != "right")
        {
            GameObject LeftNode = nodeController.leftNode;
            float distance = Vector2.Distance(LeftNode.transform.position, target);
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "left";
            }
        }

        if (nodeController.canMoveRight && previousMovingDirection != "left")
        {
            GameObject RightNode = nodeController.rightNode;
            float distance = Vector2.Distance(RightNode.transform.position, target);
            if (distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "right";
            }
        }

        return newDirection;
    }
}

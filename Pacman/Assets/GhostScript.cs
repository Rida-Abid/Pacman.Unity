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

    // Start is called before the first frame update
    void Awake()
    {
        movementController = GetComponent<MovementScript>();

        if(ghostName == GhostName.Blinky)
        {
            ghostNode = GhostNodes.StartNode;
            StartingNode = ghostNodeStart;
        }
        else if (ghostName == GhostName.Pinky)
        {
            ghostNode = GhostNodes.CenterNode;
            StartingNode = ghostNodeCenter;
        }
        else if (ghostName == GhostName.Inky)
        {
            ghostNode = GhostNodes.LeftNode;
            StartingNode = ghostNodeLeft;
        }
        else if (ghostName == GhostName.Clyde)
        {
            ghostNode = GhostNodes.RightNode;
            StartingNode = ghostNodeRight;
        }

        movementController.currentNode = StartingNode;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReachedCenterOfNode(NodeController nodeController)
    {
        if(ghostNode == GhostNodes.MovingBetweenNodes)
        {

        }
        else if(ghostNode == GhostNodes.Recreate)
        {

        }
        else
        {
            if(readyToLeaveHome)
            {
                if (ghostNode == GhostNodes.LeftNode)
                {
                    ghostNode = GhostNodes.CenterNode;
                    movementController.SetDirection("right");
                }

                if (ghostNode == GhostNodes.RightNode)
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
}

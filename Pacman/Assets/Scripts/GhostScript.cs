using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.XR;
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
    public PacmanScript pacmanScript;

    public bool Recreate = false;

    public bool isFrightened = false;

    public GameObject[] scatterNodes;
    public int scatterNodeIndex;

    public Animator animator;
    public SpriteRenderer ghostSprite;
    public SpriteRenderer eyesSprite;
    public Color color;



    // Start is called before the first frame update
    void Awake()
    {
        ghostSprite = GetComponent<SpriteRenderer>();
        eyesSprite = GetComponent<SpriteRenderer>();
        scatterNodeIndex = 0;
        gameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        pacmanScript = GameObject.Find("Pacman").GetComponent<PacmanScript>();

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
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isFrightened)
        {
            animator.SetBool("frightened", true);
            eyesSprite.enabled = false;
            ghostSprite.color = new Color(255, 255, 255, 255);
        }
        else
        {
            animator.SetBool("frightened", false);
            ghostSprite.color = color;
        }
        animator.SetBool("moving", true);

        if (!gameController.gameIsRunning)
        {
            return;
        }

       
 
        if (Recreate == true)
        {
            readyToLeaveHome = false;
            ghostNode = GhostNodes.Recreate;
            Recreate = false;
        }
    } 

    public void ReachedCenterOfNode(NodeController nodeController)
    {
        
        if (ghostNode == GhostNodes.MovingBetweenNodes)
        {
            if (gameController.currentGhostMode == GameControllerScript.GhostMode.scatter)
            {
                DetermineGhostScatterModeDirection();
            }
            else if (isFrightened)
            {
                string direction = GetRandomDirection();
                movementController.SetDirection(direction);
            }
            else
            {
                if (ghostName == GhostName.Blinky)
                {
                    DetermineBlinkyDirection();
                }

                else if (ghostName == GhostName.Inky)
                {
                    DetermineInkyDirection();
                }

                else if (ghostName == GhostName.Pinky)
                {
                    DeterminePinkyDirection();
                }

                else if (ghostName == GhostName.Clyde)
                {
                    DetermineClydeDirection();
                }
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

            else if ((transform.position.x == ghostNodeLeft.transform.position.x && transform.position.y == ghostNodeLeft.transform.position.y)
                || (transform.position.x == ghostNodeRight.transform.position.x && transform.position.y == ghostNodeRight.transform.position.y))
            {
                ghostNode = recreateState;
            }

            else 
            {
                direction = GetClosestDirection(ghostNodeStart.transform.position);
            }

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

    string GetRandomDirection()
    {
        List<string> possibleDirections = new List<string>();
        NodeController nodeController = movementController.currentNode.GetComponent<NodeController>();

        if(nodeController.canMoveDown && movementController.previousMovingDirection != "up")
        {
            possibleDirections.Add("down");
        }

        if (nodeController.canMoveUp && movementController.previousMovingDirection != "down")
        {
            possibleDirections.Add("up");
        }

        if (nodeController.canMoveLeft && movementController.previousMovingDirection != "right")
        {
            possibleDirections.Add("left");
        }

        if (nodeController.canMoveRight && movementController.previousMovingDirection != "left")
        {
            possibleDirections.Add("right");
        }

        string direction = "";
        int randomDirectionsIndex = UnityEngine.Random.Range(0, possibleDirections.Count - 1);
        direction = possibleDirections[randomDirectionsIndex];
        return direction;
    }

    void DetermineGhostScatterModeDirection()
    {
       
            if (transform.position.x == scatterNodes[scatterNodeIndex].transform.position.x && transform.position.y == scatterNodes[scatterNodeIndex].transform.position.y)
            {
                scatterNodeIndex++;

                if (scatterNodeIndex == scatterNodes.Length - 1)
                {
                    scatterNodeIndex = 0;
                }
            }
            string direction = GetClosestDirection(scatterNodes[scatterNodeIndex].transform.position);
            movementController.SetDirection(direction);
                
    }
    void DetermineBlinkyDirection()
    {
        string direction = GetClosestDirection(gameController.pacman.transform.position);
        movementController.SetDirection(direction);
    }

    void DeterminePinkyDirection()
    {
        string pacmanDirection = gameController.pacman.GetComponent<MovementScript>().previousMovingDirection;
        float distanceBetweenNodes = 0.35f;

        Vector2 target = gameController.pacman.transform.position;
        if(pacmanDirection == "left")
        {
            target.x -= distanceBetweenNodes * 2;
        }

        else if (pacmanDirection == "right")
        {
            target.x += distanceBetweenNodes * 2;
        }

        else if (pacmanDirection == "up")
        {
            target.y += distanceBetweenNodes * 2;
        }

        else if (pacmanDirection == "down")
        {
            target.y -= distanceBetweenNodes * 2;
        }

        string direction = GetClosestDirection(target);
        movementController.SetDirection(direction);
    }

    void DetermineInkyDirection()
    {
        string pacmanDirection = gameController.pacman.GetComponent<MovementScript>().previousMovingDirection;
        float distanceBetweenNodes = 0.35f;

        Vector2 target = gameController.pacman.transform.position;
        if (pacmanDirection == "left")
        {
            target.x -= distanceBetweenNodes * 2;
        }

        else if (pacmanDirection == "right")
        {
            target.x += distanceBetweenNodes * 2;
        }

        else if (pacmanDirection == "up")
        {
            target.y += distanceBetweenNodes * 2;
        }

        else if (pacmanDirection == "down")
        {
            target.y -= distanceBetweenNodes * 2;
        }

        GameObject blinkyGhost = gameController.blinkyGhost;
        float xDistance = target.x - blinkyGhost.transform.position.x;
        float yDistance = target.y - blinkyGhost.transform.position.y;

        Vector2 inkytarget = new Vector2 (target.x + xDistance, target.y + yDistance);

        string direction = GetClosestDirection(inkytarget);
        movementController.SetDirection(direction);
    }

    void DetermineClydeDirection()
    {
        float distance = Vector2.Distance(gameController.pacman.transform.position, transform.position);
        float distanceBetweenNodes = 0.35f;

        if(distance < 0)
        {
            distance *= -1;
        }

        if ( distance <= distanceBetweenNodes* 8) 
        {
            DetermineBlinkyDirection();
        }
        else
        {
            DetermineGhostScatterModeDirection();
        }

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
    public IEnumerable Setup()
    {
        animator.SetBool("moving", false);
        yield break;
    }

  
}











































































































































































































































































































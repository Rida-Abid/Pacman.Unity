using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeController : MonoBehaviour
{
    public bool canMoveLeft = false;
    public bool canMoveRight = false;
    public bool canMoveUp = false;
    public bool canMoveDown = false;


    public GameObject leftNode;
    public GameObject rightNode;
    public GameObject upNode;
    public GameObject downNode;

    public bool isGhostStartingNode = false;

    public GameControllerScript controller;


    // Start is called before the first frame update
    void Start()
    {
        RaycastHit2D[] hitsDown;

        hitsDown = Physics2D.RaycastAll(transform.position, Vector2.down);

        for (int i = 0; i < hitsDown.Length; i++)
        {
            float distance = Mathf.Abs(hitsDown[i].point.y - transform.position.y);

            if (distance < 0.4f && hitsDown[i].collider.gameObject.CompareTag("Node"))
            {
                canMoveDown = true;
                downNode = hitsDown[i].collider.gameObject;

            }
        }

        RaycastHit2D[] hitsUp;

        hitsUp = Physics2D.RaycastAll(transform.position, Vector2.up);

        for (int i = 0; i < hitsUp.Length; i++)
        {
            float distance = Mathf.Abs(hitsUp[i].point.y - transform.position.y);

            if (distance < 0.4f && hitsUp[i].collider.gameObject.CompareTag("Node"))
            {
                canMoveUp = true;
                upNode = hitsUp[i].collider.gameObject;

            }
        }

        RaycastHit2D[] hitsRight;

        hitsRight = Physics2D.RaycastAll(transform.position, Vector2.right);

        for (int i = 0; i < hitsRight.Length; i++)
        {
            float distance = Mathf.Abs(hitsRight[i].point.x - transform.position.x);

            if (distance < 0.4f && hitsRight[i].collider.gameObject.CompareTag("Node"))
            {
                canMoveRight = true;
                rightNode = hitsRight[i].collider.gameObject;

            }
        }

        RaycastHit2D[] hitsLeft;

        hitsLeft = Physics2D.RaycastAll(transform.position, Vector2.left);

        for (int i = 0; i < hitsLeft.Length; i++)
        {
            float distance = Mathf.Abs(hitsLeft[i].point.x - transform.position.x);

            if (distance < 0.4f && hitsLeft[i].collider.gameObject.CompareTag("Node"))
            {
                canMoveLeft = true;
                leftNode = hitsLeft[i].collider.gameObject;

            }
        }
        
        if(isGhostStartingNode) 
        {
            canMoveDown = true;
            downNode = controller.ghostNodeCenter;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetNodeFromDirection( string direction )
    {
        if (direction == "left" && canMoveLeft)
        {
            return leftNode;
        }

        else if (direction == "right" && canMoveRight)
        {
            return rightNode;
        }

        else if (direction == "up" && canMoveUp)
        {
            return upNode;
        }

        else if (direction == "down" && canMoveDown)
        {
            return downNode;
        }

        else
            return null;
    }
}

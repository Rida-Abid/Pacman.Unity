using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{   public Animator animator;
    public SpriteRenderer sprite;
    public PacmanScript pacman;
    MovementScript movementController;
   
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        pacman = GetComponent<PacmanScript>();
        movementController = GetComponent<MovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("moving" , true);
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            movementController.SetDirection("left");
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            movementController.SetDirection("right");
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            movementController.SetDirection("up");
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            movementController.SetDirection("down");
        }

        bool flipX = false;
        bool flipY = false;


        if (movementController.previousMovingDirection == "left")
        {
            animator.SetInteger("direction", 0);
        }

        else if (movementController.previousMovingDirection == "right")
        {
            animator.SetInteger("direction", 0);
            flipX = true;
        }

        else if (movementController.previousMovingDirection == "up")
        {
            animator.SetInteger("direction", 1);
        }
        else if (movementController.previousMovingDirection == "down")
        {
            animator.SetInteger("direction", 1);
            flipY = true;
        }

        sprite.flipX = flipX;
        sprite.flipY = flipY;

    }
}

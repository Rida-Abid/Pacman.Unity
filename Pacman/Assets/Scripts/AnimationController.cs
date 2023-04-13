using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{   public Animator animator;
    public SpriteRenderer sprite;
    public PacmanScript pacman;
   
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        pacman = GetComponentInChildren<PacmanScript>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("moving" , true);
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pacman.myRigidBody.velocity = Vector2.left * pacman.speed;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pacman.myRigidBody.velocity = Vector2.right * pacman.speed;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pacman.myRigidBody.velocity = Vector2.up * pacman.speed;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pacman.myRigidBody.velocity = Vector2.down * pacman.speed;
        }

        bool flipX = false;
        bool flipY = false;


        if (pacman.lastKeyClick == KeyCode.LeftArrow) {
            animator.SetInteger("direction", 0);
        }

        else if(pacman.lastKeyClick == KeyCode.RightArrow) {
            animator.SetInteger("direction", 0);
            flipX = true;
        }

        else if (pacman.lastKeyClick == KeyCode.UpArrow)
        {
            animator.SetInteger("direction", 1);
        }
        else if (pacman.lastKeyClick == KeyCode.DownArrow)
        {
            animator.SetInteger("direction", 1);
            flipY = true;
        }

        sprite.flipX = flipX;
        sprite.flipY = flipY;

    }
}

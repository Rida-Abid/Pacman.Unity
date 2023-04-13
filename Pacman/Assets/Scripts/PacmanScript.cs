using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class PacmanScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public int speed;
    public int maxLives = 3;
    private float currentOffset;
    private float offset = 0.3f;
    public int currentLives;
    public KeyCode lastKeyClick;
    public bool pacmanIsAlive = true;
    public GameControllerScript controller;
    public LivesScript lives;



    // Start is called before the first frame update
    void Start()
    {
        // Clone Pacman for lives
        //if(gameObject.name == "Pacman")
        //{
        //    currentOffset = offset;
        //    for(int i = 0; i < maxLives; i++)
        //    {
        //        Instantiate(gameObject, new Vector3(transform.position.x +currentOffset, transform.position.y, 0), Quaternion.identity);
        //        currentOffset += offset;
        //    }
        //}

        myRigidBody = GetComponent<Rigidbody2D>();
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        lives = GameObject.FindGameObjectWithTag("Lives").GetComponent<LivesScript>();
        currentLives = maxLives;
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && pacmanIsAlive)
        {
            lastKeyClick = KeyCode.LeftArrow;
            myRigidBody.velocity = Vector2.left * speed;
        }

        if (Input.GetKey(KeyCode.RightArrow) && pacmanIsAlive)
        {
            lastKeyClick = KeyCode.RightArrow;
            myRigidBody.velocity = Vector2.right * speed;
        }

        if (Input.GetKey(KeyCode.UpArrow) && pacmanIsAlive)
        {
            lastKeyClick = KeyCode.UpArrow;
            myRigidBody.velocity = Vector2.up * speed;
        }

        if (Input.GetKey(KeyCode.DownArrow) && pacmanIsAlive)
        {
            lastKeyClick = KeyCode.DownArrow;
            myRigidBody.velocity = Vector2.down * speed;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Node")
        {
            Destroy(collision.gameObject);
            controller.addScore();

        }

        if (collision.gameObject.CompareTag("Deleter") || collision.gameObject.CompareTag("BackGround"))
        {
            if(lastKeyClick == KeyCode.LeftArrow) 
            { 
                /// TODO Add RightArrow KeyClick
            }

            if (lastKeyClick == KeyCode.RightArrow)
            {
                /// TODO Add LeftArrow KeyClick
            }

            myRigidBody.velocity = Vector3.zero;
        }

        if (collision.gameObject.CompareTag("Ghost"))
        {

            currentLives--;

            if (currentLives == 0)
            {
                controller.gameOver();
                pacmanIsAlive = false;
                
            }
            else if (currentLives > 0)
            {
                gameObject.transform.position = Settings.PacmanStartPosition;
                
            }
        }
    }



    

}

public static class Settings
{
    public static readonly Vector3 PacmanStartPosition = new Vector3(-0.172f, -0.65f, -1f);

}










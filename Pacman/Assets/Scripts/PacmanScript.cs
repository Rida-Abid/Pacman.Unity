using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public int maxLives = 3;
    private float currentOffset;
    private float offset = 0.3f;
    public int currentLives;
    public float speed = 4f;
    public KeyCode lastKeyClick;
    public bool pacmanIsAlive = true;
    public GameObject Pacman;
    public GameControllerScript controller;
    public LivesScript lives;
    public MovementScript movementController;
  



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

        movementController = GetComponentInParent<MovementScript>();
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        currentLives = maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
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



    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            currentLives --;
           
            if (currentLives <= 0)
            {
                controller.gameOver();
                pacmanIsAlive = false;
                controller.siren.Pause();
                controller.death.Play();
                controller.PauseGame();
                Pacman.transform.position = Settings.PacmanStartPosition;
            }
            else
            {
                Pacman.transform.position = Settings.PacmanStartPosition;
                //movementController.pacmanStartNode = movementController.currentNode; 
            }


        }








    }
}

public static class Settings
{
    public static readonly Vector3 PacmanStartPosition = new Vector3(0.108f, -2.416f, 0f);

}










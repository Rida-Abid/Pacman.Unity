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
    private LogicScript logic;
    private int currentLives;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        currentLives = maxLives;
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            myRigidBody.velocity = Vector2.left * speed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = Vector2.right * speed;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            myRigidBody.velocity = Vector2.up * speed;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            myRigidBody.velocity = Vector2.down * speed;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Node")
        {
            Destroy(collision.gameObject);
            logic.addScore();

        }

        if (collision.gameObject.CompareTag("Deleter"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
        }

        if (collision.gameObject.CompareTag("BackGround"))
        {
            Debug.Log("DEBUG");
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
        }

        if (collision.gameObject.CompareTag("Ghost"))
        {

            currentLives--;

            if (currentLives == 0)
            {
                logic.gameOver();
            }
            else if (currentLives > 0)
            {
                gameObject.transform.position = new Vector3((float)-0.0172, (float)-0.65, -1);
            }
        }
    }

    

}










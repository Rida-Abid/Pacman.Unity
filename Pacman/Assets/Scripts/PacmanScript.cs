using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) == true)
        {
            myRigidBody.velocity = Vector2.up * 2;

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
        {
            myRigidBody.velocity = Vector2.left * 2;

        }
        if (Input.GetKeyDown(KeyCode.RightArrow) == true)
        {
            myRigidBody.velocity = Vector2.right * 2;

        }
        if (Input.GetKeyDown(KeyCode.DownArrow) == true)
        {
            myRigidBody.velocity = Vector2.down * 2;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public int NumberOfGhosts = 3;
    public  float currentOffset;
    public float offset = 0.3f;
    // Start is called before the first frame update
    void Start()
    {

        if(gameObject.name == "Ghost")
        {
            Debug.Log("debug");
            //currentOffset = offset;
            //for (int i = 0; i < NumberOfGhosts; i++)
            //{
            //    Instantiate(gameObject, new Vector3(transform.position.x + currentOffset, transform.position.y, 0), Quaternion.identity);
            //    currentOffset += offset;
            //}
        }

    }

    // Update is called once per frame
    void Update()
    {
         

    }
}

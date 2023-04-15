using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.name == "Ghost")
        {
            Debug.Log("debug");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

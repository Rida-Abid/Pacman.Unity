using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LivesScript : MonoBehaviour
{
    public PacmanScript PacmanObj;
    public GameObject Life;

    void Start()
    {
        PacmanObj = GameObject.FindGameObjectWithTag("Pacman").GetComponent<PacmanScript>();
        

    }
    void Update()
    {   
        
    }

    
}

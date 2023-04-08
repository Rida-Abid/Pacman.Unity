using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LivesScript : MonoBehaviour
{
    public Sprite pacman;
    //public Sprite emptyHeart;
    public Image[] pacmanLives;
    public PacmanScript PacmanObj;

    void Start()
    {
        PacmanObj = GameObject.FindGameObjectWithTag("Pacman").GetComponent<PacmanScript>();

        for (int i = 0; i < pacmanLives.Length; i++)
        {
            pacmanLives[i].sprite = pacman;
        }

    }
    void Update()
    {   
        for (int i = 0; i < pacmanLives.Length; i++)
        {
            if (i < PacmanObj.maxLives)
            {
                pacmanLives[i].sprite = pacman ;
            }
            else
            {
                pacmanLives[i].sprite = pacman;
            }
        }
    }


    

}

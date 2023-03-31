using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletSpawnScript : MonoBehaviour
{
    public int NumberOfPellets = 29;
    public float PelletSpawn = 0.2f;
    public float CurrentNode;

    // Start is called before the first frame update
    void Start()
    {
       
        CurrentNode = PelletSpawn;
        if(gameObject.name == "Node")
        {
            for (int i = 0; i < NumberOfPellets; i++)
            {
                Instantiate(gameObject, new Vector3(transform.position.x , transform.position.y + CurrentNode, 0), Quaternion.identity);
                CurrentNode += PelletSpawn;
            }

        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

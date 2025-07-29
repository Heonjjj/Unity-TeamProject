using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public bool isEnemySpawn = false;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        if(isEnemySpawn == true)
        {
            isEnemySpawn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objectCount < 20)
        {
            SpawnEnemies();

			objectCount++;
		}
	}

    int objectCount = 0;

    void SpawnEnemies()
    {

    }
}

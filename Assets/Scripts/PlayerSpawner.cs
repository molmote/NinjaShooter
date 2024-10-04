using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

	}

	[SerializeField] float spawnInterval;
	[SerializeField] GameObject enemyType1;
	[SerializeField] GameObject enemyType2;
	float timeDiff = 0;

	// Update is called once per frame
	void Update()
    {

		timeDiff += Time.deltaTime;

		if (timeDiff > spawnInterval)
		{
			Vector3 pos = transform.position;

			if (Random.Range(0, 2) > 1)
			{
				var enemy = Instantiate(enemyType1, pos, Quaternion.identity).GetComponent<EnemyObject>();
				enemy.hitPoint = 100;
				enemy.speed = 20;
			}
			else
			{
				var enemy = Instantiate(enemyType2, pos, Quaternion.identity).GetComponent<EnemyObject>();
				enemy.hitPoint = 100;
				enemy.speed = 20;
			}

			timeDiff = 0;
		}
	}
}

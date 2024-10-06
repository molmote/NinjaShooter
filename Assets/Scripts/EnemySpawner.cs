using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	private static EnemySpawner instance;
	public static EnemySpawner Instance
	{
		get
		{
			return instance;
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		instance = this;

		timeDiff = spawnInterval;
		stageSpeed = stageSpeedInitial;
	}

	public void DestroyObject(EnemyObject obj)
	{
		activeEnemiesList.Remove(obj);
		inactiveEnemiesList.Add(obj);

		obj.gameObject.SetActive(false);
	}

	public EnemyObject GetNearestEnemyObject()
	{
		if (activeEnemiesList != null && activeEnemiesList.Count > 0)
		return activeEnemiesList[0];

		return null;
	}

    [SerializeField] float spawnInterval;
	[SerializeField] GameObject enemyType1;
	[SerializeField] GameObject enemyType2;
	List<EnemyObject> activeEnemiesList = new List<EnemyObject>();
	List<EnemyObject> inactiveEnemiesList = new List<EnemyObject>();

	float timeDiff = 0;
	[SerializeField] float stageSpeedInitial = 0;
	[SerializeField] float stageSpeed = 0;

	// Update is called once per frame
	void Update()
    {
        timeDiff += Time.deltaTime;

        if (timeDiff > spawnInterval)
        {
			Vector3 pos = transform.position;

			var spawnPosition = new Vector3(Random.Range(-2,2), pos.y, pos.z);
			var enemyType = enemyType1;
			if (Random.Range(0,2) > 1)
            {
				enemyType = enemyType2;
			}

			var enemy = Instantiate(enemyType, spawnPosition, Quaternion.identity).GetComponent<EnemyObject>();
			enemy.hitPoint = 100;
			enemy.speed = new Vector2(0, -stageSpeed);

			activeEnemiesList.Add(enemy);

			timeDiff = 0;
		}
	}
}

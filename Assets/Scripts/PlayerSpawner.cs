using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		instance = this;
	}

	public static PlayerSpawner instance;
	public static PlayerSpawner Instance
	{
		get
		{
			return instance;
		}
	}

	public void TakeDamage(int damage)
	{
		hitPoint -= damage;

		if (hitPoint < 0)
		{
			// game ends
		}
	}

	[SerializeField] int hitPoint;
	[SerializeField] float spawnInterval;
	[SerializeField] GameObject weaponType1;
	[SerializeField] GameObject weaponType2;
	[SerializeField] GameObject weaponType3;
	float timeDiff = 0;

	List<EnemyObject> activeProjectileList;
	List<EnemyObject> inactiveProjectileList;

	// Update is called once per frame
	void Update()
    {

		timeDiff += Time.deltaTime;

		if (timeDiff > spawnInterval)
		{
			Vector3 pos = transform.position;

			var nearestEnemy = EnemySpawner.Instance.GetNearestEnemyObject();

			if (nearestEnemy != null)
			{
				var direction = nearestEnemy.transform.position - transform.position;
				if (Random.Range(0, 3) > 1)
				{
					var missile = Instantiate(weaponType1, pos, weaponType1.transform.rotation).GetComponent<ProjectileObject>();
					missile.speed = direction.normalized;
				}
				else
				{
					var missile = Instantiate(weaponType2, pos, weaponType1.transform.rotation).GetComponent<ProjectileObject>();
					missile.speed = direction.normalized;
				}

				timeDiff = 0;
			}
		}
	}
}

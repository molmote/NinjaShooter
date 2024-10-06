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

	private static PlayerSpawner instance;
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

	[SerializeField] Animator anim;
	[SerializeField] int hitPoint;
	[SerializeField] float spawnInterval;
	[SerializeField] GameObject weaponType1;
	[SerializeField] GameObject weaponType2;
	[SerializeField] GameObject weaponType3;
	float timeDiff = 0;

	List<ProjectileObject> activeProjectileList = new List<ProjectileObject>();
	List<ProjectileObject> inactiveProjectileList = new List<ProjectileObject>();

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
				float angle = Vector2.Angle(nearestEnemy.transform.position, transform.position);
				var weaponType = weaponType1;
				int rand = Random.Range(0, 3);
				if (rand >= 2)
				{
					weaponType = weaponType2;
				}
				else if (rand >= 1)
				{
					weaponType = weaponType3;
				}

				var missile = Instantiate(weaponType, pos, Quaternion.identity).GetComponent<ProjectileObject>();
				missile.spriteRenderer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
				missile.speed = direction.normalized;

				timeDiff = 0;

				activeProjectileList.Add(missile);

				anim.SetTrigger("Throw");
			}
		}
	}

	public void DestroyProjectile(ProjectileObject obj)
	{
		activeProjectileList.Remove(obj);
		inactiveProjectileList.Add(obj);

		obj.gameObject.SetActive(false);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    public int hitPoint;
	public Vector3 speed;
	void Update()
	{
		if (hitPoint > 0)
		{
			transform.Translate(speed * Time.deltaTime);

			if (transform.position.y < PlayerSpawner.Instance.transform.position.y)
			{
				EnemySpawner.Instance.DestroyObject(this);

				PlayerSpawner.Instance.TakeDamage(hitPoint);
			}
		}

	}

	// Start is called before the first frame update
	void Start()
    {

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("OnCollisionEnter " + collision.gameObject.name);
		var hit = collision.gameObject.GetComponent<ProjectileObject>();
		if (hit != null)
		{
			hitPoint -= (int)hit.data.damage;

			// also show damage font here
			if (hitPoint <= 0)
			{
				EnemySpawner.Instance.DestroyObject(this);
			}
		}

		/*var player = collision.gameObject.GetComponent<PlayerSpawner>();
		if (player != null)
		{
			// player take damage

			EnemySpawner.Instance.DestroyObject(this);
		}*/
	}
}

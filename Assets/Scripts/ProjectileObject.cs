using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject : MonoBehaviour
{
	// public ProjectileData.WeaponType weaponType;
	public ProjectileData data;
	public Vector3 speed;
	public int life; //penetration 
	public SpriteRenderer spriteRenderer;

	void Update()
	{
		transform.Translate(speed * Time.deltaTime * data.speed);
	}

	void Start()
	{
		life = data.penetration;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("OnCollisionEnter " + collision.gameObject.name);
		var enemy = collision.gameObject.GetComponent<EnemyObject>();
		if (enemy != null)
		{
			life--;

			if (life <= 0)
			{
				PlayerSpawner.Instance.DestroyProjectile(this);
			}
		}
	}

	//private void OnCollisionStay(Collision collision)

	//private void OnCollisionExit(Collision collision)
}

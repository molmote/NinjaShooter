using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject : MonoBehaviour
{
	// public ProjectileData.WeaponType weaponType;
	public ProjectileData data;
	public Vector3 speed;
	public int life; //penetration 

	void Update()
	{
		transform.Translate(speed * Time.deltaTime * data.speed);
	}

	void Start()
	{
		life = 1;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("OnCollisionEnter " + collision.gameObject.name);
		life--;

		if (life <= 0 )
		{
			gameObject.SetActive(false);
		}
	}

	//private void OnCollisionStay(Collision collision)

	//private void OnCollisionExit(Collision collision)
}

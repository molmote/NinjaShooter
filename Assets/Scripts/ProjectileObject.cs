using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Dagger,
    IceSpear,
    Fireball,
    Lightning
}

public class ProjectileObject : MonoBehaviour
{
	public WeaponType weaponType;
	public ProjectileData data;
	public Vector3 speed;
	void Update()
	{
		transform.position += speed * Time.deltaTime;
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("OnCollisionEnter " + collision.gameObject.name);
	}

	//private void OnCollisionStay(Collision collision)

	//private void OnCollisionExit(Collision collision)
}

public class ProjectileData
{
	[SerializeField] WeaponType weaponType;
	public string name;
	public string desc;

	public float speed;
	public float damage;
	public int penetration;
}

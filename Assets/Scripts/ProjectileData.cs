using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "NewWeaponType")]
public class ProjectileData : ScriptableObject
{
	public enum WeaponType
	{
		Dagger,
		IceSpear,
		Fireball,
		Lightning
	}

	[SerializeField] WeaponType weaponType;
	public string name;
	public string desc;

	public float speed;
	public float damage;
	public int penetration;
}
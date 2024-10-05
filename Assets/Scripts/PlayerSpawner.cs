using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

	}

	[SerializeField] float spawnInterval;
	[SerializeField] GameObject weaponType1;
	[SerializeField] GameObject weaponType2;
	[SerializeField] GameObject weaponType3;
	float timeDiff = 0;

	// Update is called once per frame
	void Update()
    {

		timeDiff += Time.deltaTime;

		if (timeDiff > spawnInterval)
		{
			Vector3 pos = transform.position;

			if (Random.Range(0, 3) > 1)
			{
				var missile = Instantiate(weaponType1, pos, Quaternion.identity).GetComponent<ProjectileObject>();
				missile.speed = new Vector3(1, 1);
			}
			else
			{
				var missile = Instantiate(weaponType2, pos, Quaternion.identity).GetComponent<ProjectileObject>();
				missile.speed = new Vector3(1, 1);
			}

			timeDiff = 0;
		}
	}
}

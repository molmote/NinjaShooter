using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    public int hitPoint;
	public Vector3 speed;
	void Update()
	{
		transform.position += speed * Time.deltaTime;
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }
}

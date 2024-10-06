using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		instance = this;
		hitPoint = initialHitPoint;

		UpdateDamageText();
	}

	private static PlayerSpawner instance;
	public static PlayerSpawner Instance
	{
		get
		{
			return instance;
		}
	}

	public IEnumerator Restart()
	{
		yield return new WaitForSeconds(3f);

		SceneManager.LoadScene("SampleScene");
	}

	public bool IsAlive()
	{
		return hitPoint > 0;
	}

	public void TakeDamage(int damage)
	{
		hitPoint -= damage;

		Shake();

		if (hitPoint < 0)
		{
			gameOver.SetActive(true);
			timerShake = 0;

			StartCoroutine("Restart");
		}

		UpdateDamageText();
	}

	void UpdateDamageText()
	{
		playerHPText.text = $"{hitPoint}/{initialHitPoint}";
	}

	[SerializeField] Camera targetCamera;
	[SerializeField] Color flipColor;
	[SerializeField] float shake;
	[SerializeField] Animator anim;
	int hitPoint;
	[SerializeField] int initialHitPoint;
	[SerializeField] float spawnInterval;
	[SerializeField] GameObject weaponType1;
	[SerializeField] GameObject weaponType2;
	[SerializeField] GameObject weaponType3;
	[SerializeField] float coolDown1;
	[SerializeField] float coolDown2;
	[SerializeField] float coolDown3;
	// float timeDiff = 0;

	List<ProjectileObject> activeProjectileList = new List<ProjectileObject>();
	List<ProjectileObject> inactiveProjectileList = new List<ProjectileObject>();

	[SerializeField] GameObject gameOver;
	[SerializeField] TextMeshPro playerHPText;

	[SerializeField] float timerShake;
	[SerializeField] float intensity;
	[SerializeField] float maxTime;
	Vector3 originalPosition;
	float timeShake;

	public void Shake()
	{
		originalPosition = transform.position;
		timerShake = maxTime;
	}

	void FireWeapon(GameObject weaponType)
	{
		Vector3 pos = transform.position;

		var nearestEnemy = EnemySpawner.Instance.GetNearestEnemyObject();

		if (nearestEnemy != null)
		{
			var direction = nearestEnemy.transform.position - transform.position;
			float angle = Vector2.Angle(nearestEnemy.transform.position, transform.position);

			var missile = Instantiate(weaponType, pos, Quaternion.identity).GetComponent<ProjectileObject>();
			missile.spriteRenderer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
			missile.speed = direction.normalized;

			activeProjectileList.Add(missile);

			anim.SetTrigger("Throw");
		}
	}

	// Update is called once per frame
	void Update()
    {
		coolDown1 += Time.deltaTime;
		coolDown2 += Time.deltaTime;
		coolDown3 += Time.deltaTime;

		if (!IsAlive())
		{
			return;
		}

		if (Input.GetKey(KeyCode.A) && coolDown1 > 1)
		{
			FireWeapon(weaponType1);
			coolDown1 = 0;
		}
		if (Input.GetKey(KeyCode.S) && coolDown2 > 1)
		{
			FireWeapon(weaponType2);
			coolDown2 = 0;
		}
		if (Input.GetKey(KeyCode.D) && coolDown3 > 1)
		{
			FireWeapon(weaponType3);
			coolDown3 = 0;
		}

		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
			{
				if (coolDown1 > 1)
				{
					FireWeapon(weaponType1);
					coolDown1 = 0;
				}
				else if (coolDown2 > 1)
				{
					FireWeapon(weaponType2);
					coolDown2 = 0;
				}
				else if (coolDown3 > 1)
				{
					FireWeapon(weaponType3);
					coolDown3 = 0;
				}
			}
		}

		if (timerShake > 0)
		{
			timerShake -= Time.deltaTime;

			transform.position =
				new Vector3(
					originalPosition.x + intensity,
					originalPosition.y + intensity,
					originalPosition.z);

			intensity = -(intensity * timerShake / maxTime);

			FlipColor(intensity > 0);
		}
		else if (PlayerSpawner.Instance != null)
		{
			FlipColor(false);
		}
	}

	public void DestroyProjectile(ProjectileObject obj)
	{
		activeProjectileList.Remove(obj);
		inactiveProjectileList.Add(obj);

		obj.gameObject.SetActive(false);
	}

	public void FlipColor(bool flip)
	{
		/*if (flip)
		{
			targetCamera.backgroundColor = flipColor;
		}
		else
		{
			targetCamera.backgroundColor = new Color(69/255f,121 / 255f, 49 / 255f);
		}*/
	}
}

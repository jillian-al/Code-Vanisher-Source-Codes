using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	#region Public Variables
	public Animator anim;
	public float attackRange;
	public float enemySpeed;
	public float timer;
	public int health;
	int currentHealth;
	public Transform leftLimit;
	public Transform rightLimit;
	public GameObject detector;
	public GameObject triggerAttack;
	[HideInInspector] public Transform target;
	[HideInInspector] public bool inRange;
	#endregion

	#region Private Variables
	private float distance;
	private float intTimer;
	private bool attacking;
	private bool cooling;
	#endregion

	void Start()
	{
		currentHealth = health;
	}

	void Awake()
	{
		TargetLocked();
		intTimer = timer;
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if (!attacking)
		{
			Move();
		}

		if (!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("e1_attack"))
		{
			TargetLocked();
		}

		if (inRange)
		{
			Enemy_behevior();
		}
	}

	public void TakeDamage(int damage)  //Take damage from the player when called
	{
		currentHealth -= damage;
		anim.SetTrigger("hurt");

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		anim.SetBool("isDead", true);
		this.enabled = false;
	}

	private void Deactivate()
	{
		gameObject.SetActive(false);
	}

	void Enemy_behevior()
	{
		distance = Vector2.Distance(transform.position, target.position);

		if (distance > attackRange) //Do not attack if the player is not in range
		{
			StopAttack();
		}
		else if (attackRange >= distance && cooling == false) //Attack if the player is in range
		{
			Attack();
		}

		if (cooling) //Back to cooling
		{
			Cooldown();
			anim.SetBool("isAttacking", false);
		}
	}

	void Move()
	{
		anim.SetBool("canPatrol", true);
		if (!anim.GetCurrentAnimatorStateInfo(0).IsName("e1_attack"))
		{
			Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
			transform.position = Vector2.MoveTowards(transform.position, targetPosition, enemySpeed * Time.deltaTime);
		}
	}

	void Attack()
	{
		timer = intTimer;
		attacking = true;

		anim.SetBool("canPatrol", false);
		anim.SetBool("isAttacking", true);
	}

	void Cooldown()
	{
		timer -= Time.deltaTime;

		if (timer <= 0 && cooling && attacking)
		{
			cooling = false;
			timer = intTimer;
		}
	}

	void StopAttack()
	{
		cooling = false;
		attacking = false;
		anim.SetBool("isAttacking", false);
	}

	public void TriggerCooling()
	{
		cooling = true; //Cool down before attacking again
	}

	private bool InsideofLimits()
	{
		return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
	}

	public void TargetLocked()
	{
		float distanceToLeft = Vector3.Distance(transform.position, leftLimit.position);
		float distanceToRight = Vector3.Distance(transform.position, rightLimit.position);

		if (distanceToLeft > distanceToRight)
		{
			target = leftLimit;
		}
		else
		{
			target = rightLimit;
		}

		Flip();
	}

	public void Flip()
	{
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
}
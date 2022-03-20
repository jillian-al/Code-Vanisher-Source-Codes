using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator myAnim;
    public bool isAttacking = false;
    public static PlayerAttack instance;
    private Player player;

    public float timeBtwAttack; 
    public float startTimeBtwAttack; 

    public Transform attackPos;     //position for the hitbox
    public LayerMask whatIsEnemies; //filters enemies to damage
    public float attackRange;       //radius of hitbox
    public int damage;       

    private Enemy enemyParent1;
    private Enemy2 enemyParent2;

    private void Start()
    {
        myAnim = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    private void Awake()
    {
        enemyParent1 = GetComponentInParent<Enemy>();
        enemyParent2 = GetComponentInParent<Enemy2>();
        instance = this;
    }

    void Update()
    {
        attackAnimation();
    }

    void attackAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking && player.isGrounded)
        {
            myAnim.SetTrigger("attacked");
        }
    }

    public void cooldown()
    {
        if (isAttacking)
        {
            timeBtwAttack = startTimeBtwAttack;
            timeBtwAttack -= Time.deltaTime;

            if (timeBtwAttack <= 0)
            {
                isAttacking = true;
            }
        }
    }
    void attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);  //gets array of enemies in hitbox
        
        foreach (Collider2D enemy in enemiesToDamage)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                enemy.GetComponentInParent<Enemy>().TakeDamage(damage); //Damage for cryptolocker enemies
            }
            else if (enemy.gameObject.CompareTag("Enemy2")) //Damage for trojan enemies
            {
                enemy.GetComponentInParent<Enemy2>().TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPos == null)
        {
            return;
        }

        Gizmos.color = Color.red;   //check gizmos in scenes for debugging
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}

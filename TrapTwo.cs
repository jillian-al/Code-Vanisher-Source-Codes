using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTwo : MonoBehaviour
{
    private Player player;

    public float timer; 
    [SerializeField] private float startTimer; //cooldown timer

    [SerializeField] private float damage; 
    [SerializeField] private float newSpeed; //Player's new movespeed

    public Transform trapPos;  //position of the trap
    public LayerMask whatIsPlayer; //filters player to damage
    [SerializeField] private float rangeX; 
    [SerializeField] private float rangeY;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //get reference for the player
    }

    // Update is called once per frame
    void Update()
    {
        zap(); //call method to zap player
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.modifyMoveSpeed(newSpeed); //change player speed on collision
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.modifyToDefaultMoveSpeed();  //call function from player script to change back to default speed after exiting collision
        }
    }

    void zap()
    {
        if (timer <= 0) //set cooldown after attacking 
        {
            Collider2D[] playerToDamage = Physics2D.OverlapBoxAll(trapPos.position, new Vector2(rangeX, rangeY), 0, whatIsPlayer);  
            
            for (int i = 0; i < playerToDamage.Length; i++)  // deal damage to player
            {
                playerToDamage[i].GetComponent<Player>().updateHealth(-damage);
            }

            timer = startTimer; //start cooldown
        }
        else if (timer > 0) //run cooldown for zap
        {
            timer -= Time.deltaTime;    //reduce cooldown
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;   //check gizmos in scenes for debugging
        Gizmos.DrawWireCube(trapPos.position, new Vector3(rangeX, rangeY)); //draw cube to check hitbox for debugging
    }
}

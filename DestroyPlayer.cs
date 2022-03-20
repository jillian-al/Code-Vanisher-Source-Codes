using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{
    private Player player;
    [SerializeField] private float damage;
   
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject.GetComponent<Player>().transform.localScale.x > 0)
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.right * 2f);
            }

            Debug.Log("Trap activated");
            StartCoroutine(player.Knockback(0f, 0f)); //change argument for the knockback power to determine how far is the knockback
            other.gameObject.GetComponent<Player>().updateHealth(-damage);
        }
    }
}

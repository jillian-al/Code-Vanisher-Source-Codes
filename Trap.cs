using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
public class Trap : MonoBehaviour
{
    public float knockA;
    public float knockB;

    private Player player;
    [SerializeField] private float damage;
 
    // Start is called before the first frame update
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

            StartCoroutine(player.Knockback(knockA, knockB)); //change argument for the knockback power to determine how far is the knockback
            other.gameObject.GetComponent<Player>().updateHealth(-damage);
        }
    }
}

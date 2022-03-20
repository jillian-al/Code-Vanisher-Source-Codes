using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpPot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().updateHealth(5); //Add health value 
            Destroy(gameObject);
        }
    }
}

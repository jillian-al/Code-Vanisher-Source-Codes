using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateScript : MonoBehaviour
{
    private Player player;

    public Transform gatePos;
    public LayerMask whatIsPlayer;
    [SerializeField] private float gateDetectionRangeX;
    [SerializeField] private float gateDetectionRangeY;

    Animator anim;

    bool gateIsApproached = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //get reference for the player
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        gateFunction();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;   //check gizmos in scenes for debugging
        Gizmos.DrawWireCube(gatePos.position, new Vector3(gateDetectionRangeX, gateDetectionRangeY)); //draw cube to check hitbox for debugging
    }

    void gateFunction()
    {
        if (!gateIsApproached)
        {
            Collider2D[] playerToDetect = Physics2D.OverlapBoxAll(gatePos.position, new Vector2(gateDetectionRangeX, gateDetectionRangeY), 0, whatIsPlayer);
            if (playerToDetect.Length > 0)
            {
                anim.SetTrigger("gateApproached");
                gateIsApproached = true;
            }
        }
        else
        {
            Collider2D[] playerToDetect = Physics2D.OverlapBoxAll(gatePos.position, new Vector2(gateDetectionRangeX, gateDetectionRangeY), 0, whatIsPlayer);
            if (playerToDetect.Length > 0)
            {
                anim.SetBool("isByTheGate", true);
            }
            else
            {
                anim.SetBool("isByTheGate", false);
            }
        }
    }
}

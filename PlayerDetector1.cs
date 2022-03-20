using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector1 : MonoBehaviour
{
    private Enemy enemyParent;
    private bool inRange;
    private Animator anim;
    public Transform leftLimit;
    public Transform rightLimit;

    [HideInInspector] public Transform target;

    private void Awake()
    {
        enemyParent = GetComponentInParent<Enemy>();
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = true;

            if (collider.gameObject.CompareTag("boundary"))
            {
                target = rightLimit;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            enemyParent.triggerAttack.SetActive(true);
            enemyParent.inRange = false;
            enemyParent.TargetLocked();
        }
    }
}

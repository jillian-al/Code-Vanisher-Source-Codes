using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAttack1 : MonoBehaviour
{
    private Enemy enemyParent;
    private void Awake()
    {
        enemyParent = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            enemyParent.target = collider.transform;
            enemyParent.inRange = true;
            enemyParent.detector.SetActive(true);
        }
    }
}

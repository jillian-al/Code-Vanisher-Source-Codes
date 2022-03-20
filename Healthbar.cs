using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image fillbar; //put fillbar reference in the inspector

    private Player player;  //reference for player

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //get reference for player
    }

    // Update is called once per frame
    void Update()
    {
        fillbar.fillAmount = player.Heatlh / player.maxHealth;   //base fillbar off of player hp
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{

    [Header("Player Properties")]
    public string shootButton = "Fire1";
    public string suicideButton = "Fire2";



	override protected void Update ()
    {

        base.Update();
        if (Input.GetButtonDown(shootButton))
        {
            Shoot(transform.forward);
            Debug.Log("Shoooot!");
        }

        if (Input.GetButtonDown(suicideButton))
        {
            Die();
            Debug.Log("RIP!");
        }
    }
}

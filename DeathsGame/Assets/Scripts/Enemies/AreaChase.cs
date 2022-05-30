using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChase : MonoBehaviour
{
    public Enemy enemyController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemyController.isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemyController.isChasing = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takingDamage : MonoBehaviour
{
    public HealthManager HM;

    private void OnCollisionEnter2D(Collision2D curr) {
        if (curr.gameObject.tag == "Obstacle")
        {
            HM.DealDamage(gameObject, 5);
        }
    }
}

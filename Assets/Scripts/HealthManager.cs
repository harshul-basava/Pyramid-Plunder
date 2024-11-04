using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float playerHealth;
    public float playerDefense;
    public float playerAttack;

    public GameObject player;
    private float k = 1f;

    void Start() 
    {
        playerHealth = 50f;
        playerDefense = 0f;
        playerAttack = 10f;
    }

    public void DealDamage(GameObject Attacked, float Damage)
    {
        if (Attacked.tag == "Player")
        {
            playerHealth -= Damage * (1 - (playerDefense / 1000));

            player.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
            Invoke(nameof(ResetColor), 0.2f);
        } 
        else if (Attacked.tag == "Enemy")
        {
            Attacked.GetComponent<EnemyController>().TakeDamage(Damage);
        }
    }

    public void ResetColor()
    {
        player.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
    }

    void Update()
    {
        if (playerHealth <= 0 && k > 0)
        {
            playerHealth = 0;
            k = deathSequence(k);
        }
    }

    private float deathSequence(float k)
    {
        if (k > 0) {
            
            Time.timeScale = k;
            k -= 0.005f;

            return k;
        } else {
            Time.timeScale = 0;
            return 0;
        }
    }
}

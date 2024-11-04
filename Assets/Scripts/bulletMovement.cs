using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    private Vector2 player;
    private Rigidbody2D rb;

    public float speed;
    public float damage;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform.position;
        
        Physics2D.IgnoreCollision(transform.parent.gameObject.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        transform.parent = null;

        Vector2 direction = new Vector2(player.x - transform.position.x, player.y - transform.position.y);
        direction = direction.normalized;

        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            HealthManager hm = GameObject.Find("HealthManager").GetComponent<HealthManager>();
            hm.DealDamage(other.gameObject, damage);
        }
        GameObject.Destroy(gameObject);
    }
}

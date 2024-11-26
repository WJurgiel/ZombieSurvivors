using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class Enemy : Damagable, IMoveable
{
    [SerializeField]
    private Transform playerTransform;

    public GameObject HPIndicator;
    
    
    private Rigidbody2D rb2d;
    private bool isCollidingWithPlayer = false;
    private bool isDamagingPlayer = false;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = stats.maxHealth;
    }

    void Update()
    {
        isCollidingWithPlayer = CheckCollision();
        if (isCollidingWithPlayer && !isDamagingPlayer)
        {
            isDamagingPlayer = true;
            StartCoroutine(playerTransform.GetComponent<Player>().TakeDamageCoroutine());
            StartCoroutine(ResetDamagingPlayerFlag());
        }
    }

    void FixedUpdate()
    {
        Move();
    }
    
    // Interface definitions
    public void Move()
    {
        if (isCollidingWithPlayer)
        {
            return;
        }
        Vector3 newPos = Vector3.MoveTowards(transform.position, playerTransform.position,
            stats.speed * Time.fixedDeltaTime);
        rb2d.MovePosition(newPos);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        HPIndicator.transform.localScale = new Vector3(currentHealth / stats.maxHealth, 1, 1);
    }
    
    //Helper functions
    private bool CheckCollision()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player")) return true;
        }

        return false;
    }

    IEnumerator ResetDamagingPlayerFlag()
    {
        yield return new WaitForSeconds(1f);
        isDamagingPlayer = false;
    }
    
    // Collisions
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Collision detected with {other.gameObject.name}"); // Check what it collides with
        if (other.gameObject.CompareTag("Bullet")) 
        {
            Debug.Log("Bullet hit!");
            TakeDamage(Random.Range(2, 5));
            Destroy(other.gameObject);
        }
    }
}

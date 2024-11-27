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
    
    [SerializeField] private GameObject bloodParticles;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private bool isCollidingWithPlayer = false;
    private bool isDamagingPlayer = false;
    
    private HealthBar healthBar;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = stats.maxHealth;
        healthBar = GetComponentInChildren<HealthBar>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        animator.SetTrigger("Hit");
        healthBar.UpdateHealthBar(stats.maxHealth, currentHealth);
    }

    protected override void Die()
    {
        Instantiate(bloodParticles, transform.position, Quaternion.identity);
        animator.SetBool("Dead", true);
        StartCoroutine(PerformDeath());
        
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

    private IEnumerator PerformDeath()
    {
        Color currentColor = spriteRenderer.color;
        float newAlpha = 1f;
        float timeToDisappear = 1f;
        float elapsedTime = 0;
        while (elapsedTime < timeToDisappear)
        {
            elapsedTime += Time.deltaTime;
            newAlpha = Mathf.Lerp(currentColor.a, 0, elapsedTime / timeToDisappear);
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            yield return null;
        }
        if(newAlpha <= 0f) base.Die();
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

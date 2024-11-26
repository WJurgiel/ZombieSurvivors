using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IMoveable, IDamagable
{
    [SerializeField]
    private Transform playerTransform;
    [Range(0,50)]
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField] float health = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if(health <= 0) Destroy(gameObject);
    }
    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("Hit with "+ damage + " dmg, HP: " + health);
        health -= damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Collision detected with {other.gameObject.name}"); // Check what it collides with
        if (other.gameObject.CompareTag("Bullet")) 
        {
            Debug.Log("Bullet hit!");
            TakeDamage(Random.Range(2, 5));
        }
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Player : Damagable, IMoveable
{
    [SerializeField] private float speed;
    [SerializeField] private Tilemap tilemap;

    public Rigidbody2D rb2d;
    public GameObject gunGO;
    private Vector3 targetPosition;
    private bool isMoving;
    void Start()
    {
        gunGO = transform.GetChild(0).gameObject;
        rb2d = GetComponent<Rigidbody2D>();
        speed = stats.speed;
        currentHealth = stats.maxHealth;
    }

    void Update()
    {
        CheckIfMoving();
        GunRotation();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void CheckIfMoving()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePos = tilemap.WorldToCell(worldPos);
            worldPos.z = 0;
            if (tilemap.HasTile(tilePos))
            {
                targetPosition = tilemap.GetCellCenterWorld(tilePos);
                isMoving = true;
            }
        }
    }
    public void Move()
    {
        if (isMoving)
        {
            float step = speed * Time.fixedDeltaTime;
            Vector3 newPos = Vector3.MoveTowards(rb2d.position, targetPosition, step);
            rb2d.MovePosition(newPos);
            if (Vector3.Distance(rb2d.position, targetPosition) < 0.01f)
            {
                rb2d.MovePosition(targetPosition);
                isMoving = false;
            }
        }
    }
    public void GunRotation()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 directionVec = mouseWorldPos - transform.position;

        float angle = Mathf.Atan2(directionVec.y, directionVec.x) * Mathf.Rad2Deg;
        gunGO.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    protected override void Die()
    {
        Debug.Log("Player is dead");
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public IEnumerator TakeDamageCoroutine()
    {
        TakeDamage(Random.Range(5, 10));
        Debug.Log("player hit");
        yield return null;
    }
    
}

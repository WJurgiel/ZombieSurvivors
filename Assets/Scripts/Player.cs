using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour, IMoveable
{
    [Range(1,30)]
    [SerializeField] private float speed = 5f;
    [SerializeField] private Tilemap tilemap;
    public GameObject gunGO;
    private Vector3 targetPosition;
    private bool isMoving;
    void Start()
    {
        gunGO = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        Move();
        GunRotation();
    }

    public void Move()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePos = tilemap.WorldToCell(worldPos);

            if (tilemap.HasTile(tilePos))
            {
                targetPosition = tilemap.GetCellCenterWorld(tilePos);
                isMoving = true;
            }
        }

        if (isMoving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            if(transform.position == targetPosition) isMoving = false;
        }
    }

    public void GunRotation()
    {
        
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 directionVec = mouseWorldPos - transform.position;

        float angle = Mathf.Atan2(directionVec.y, directionVec.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
        gunGO.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
        
    }
}

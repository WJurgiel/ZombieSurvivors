using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(5,50)]
    [SerializeField] private float speed = 5f;
    [Range(5,50)]
    [SerializeField] private float range = 20f;

    private Vector3 startPoint;
    void Start()
    {
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (Vector3.Distance(startPoint, transform.position) > range)
        {
            Destroy(gameObject);
        }
    }
    
    
}

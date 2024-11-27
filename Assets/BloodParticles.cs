using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticles : MonoBehaviour
{
    void Update()
    {
        Destroy(gameObject, 1f);
    }
}

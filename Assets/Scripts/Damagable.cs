using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damagable : MonoBehaviour
{
   [SerializeField] protected float currentHealth;
   [SerializeField] protected CharacterStatsSO stats;
   
   public virtual void TakeDamage(int damage)
   {
      currentHealth -= damage;
      if (currentHealth <= 0) Die();

   }
   protected virtual void Die()
   {
      Destroy(gameObject);
   }
}

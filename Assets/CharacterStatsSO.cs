using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Character statistics", menuName= "Stats/CharacterStats")]
public class CharacterStatsSO : ScriptableObject
{
    public float maxHealth = 100f;
    public float speed = 5f;
    public float armor = 10f;
}

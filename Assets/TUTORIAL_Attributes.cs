using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

[HelpURL("https://www.youtube.com/watch?v=Web_dgDqpx4")]
[RequireComponent(typeof(CircleCollider2D))]
public class TUTORIAL_Attributes : MonoBehaviour
{
    
    //More attributes by importing NaughtyAttributes from AssetStore
    [Header("TUTORIAL Attributes")]
    [ColorUsage(true, true), SerializeField] private Color newColor;
    [SerializeField] [GradientUsage(true)] private Gradient newGradient;
    [HideInInspector] public bool isPlayer;
    [field: SerializeField] public bool isProperty { get; private set; }
    [Space(20f)]
    [Header("2")]
    [Min(0), ContextMenuItem("Assign random number", "AssignRandomNumber")] public float value = 0;
    
    [Range(0, 1)] public float normalized;
    [SerializeField, Tooltip("zip zip zip zip zip zip")] private int zipZip;

    [Space(20f)] [Header("3")] [TextArea(3, 10), SerializeField]
    private string DialogueText; 
    
    public enum FacingDireciton
    {
        [InspectorName("Venstre")]
        Left,
        [InspectorName("Hoyre")]
        Right
    }
    
    private void AssignRandomNumber()
    {
        value = Random.value;
    }
    private void Reset()
    {
        Collider2D collider = GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
    }
    
    public FacingDireciton currentFacingDirection;
}

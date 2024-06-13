using System;
using UnityEngine;

public class ReactionArea : MonoBehaviour
{
    [SerializeField] private DirectionMovement _directionMovement;
    
    public event Action<DirectionMovement, Collider2D> OnTriggerEntered;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEntered?.Invoke(_directionMovement, other);
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    [SerializeField]
    private SnakeMover _snakeMover;
    [SerializeField]
    private List<ReactionArea> _reactionArea;

    public event Action OnItemAdded;
    public event Action OnGameOver;
    
    private void Start()
    {
        _snakeMover.OnItemsSwapped += Lose;
        
        foreach (var reactionArea in _reactionArea)
        {
            reactionArea.OnTriggerEntered += OnReactionAreaEnter;
        }
    }

    private void OnReactionAreaEnter(DirectionMovement directionMovement, Collider2D collider)
    {
        if (directionMovement != _snakeMover.DirectionMovement)
        {
            return;
        }

        if (collider.gameObject.TryGetComponent(out Item item) == false)
        {
            return;
        }
        
        switch (item.ItemType)
        {
            case ItemType.Food:
                AddItem(item);
                break;
            case ItemType.Body:
                Lose(item);
                break;
        }
    }

    private void AddItem(Item item)
    {
        OnItemAdded?.Invoke();
        _snakeMover.AddItem(item);
    }

    private void Lose(Item item)
    {
        OnGameOver?.Invoke();
        _snakeMover.StopMoving();
        item.SetRedColor();
    }
}
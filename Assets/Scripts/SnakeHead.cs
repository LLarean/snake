using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    [SerializeField]
    private SnakeMover _snakeMover;
    [SerializeField]
    private List<ReactionArea> _reactionArea;
    
    private void Start()
    {
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
                _snakeMover.AddItem(item);
                break;
            case ItemType.Body:
                Debug.Log("Lose");
                break;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMover : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private float _speed = 1;
    
    private DirectionMovement _directionMovement;
    private Teleporter _teleporter;
    private float _localScaleX;
    private bool _isMoving;
    
    private List<Item> _bodyItems = new();

    public DirectionMovement DirectionMovement => _directionMovement;

    public void AddItem(Item item)
    {
        _bodyItems.Add(item);
        item.Initialize(_camera);
        item.SetBodyType(ItemType.Body);
    }
    
    private void Start()
    {
        _teleporter = new Teleporter(_camera, transform);
        _localScaleX = gameObject.transform.localScale.x;
        _isMoving = true;
        StartCoroutine(Moving());
    }

    private void Update()
    {
        SetDirectionMovement();
        _teleporter.ChangePosition();

        foreach (var item in _bodyItems)
        {
            item.ChangePosition();
        }
    }

    private void SetDirectionMovement()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _directionMovement = DirectionMovement.Top;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _directionMovement = DirectionMovement.Bot;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _directionMovement = DirectionMovement.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _directionMovement = DirectionMovement.Right;
        }
    }

    private IEnumerator Moving()
    {
        while (_isMoving == true)
        {
            var currentPosition = transform.position;
            MoveHead(currentPosition);
            MoveItems(currentPosition);
            yield return new WaitForSeconds(_speed);
        }
    }

    private void MoveHead(Vector3 currentPosition)
    {
        transform.position = _directionMovement switch
        {
            DirectionMovement.Top => new Vector2(currentPosition.x, currentPosition.y + _localScaleX),
            DirectionMovement.Bot => new Vector2(currentPosition.x, currentPosition.y - _localScaleX),
            DirectionMovement.Right => new Vector2(currentPosition.x + _localScaleX, currentPosition.y),
            DirectionMovement.Left => new Vector2(currentPosition.x - _localScaleX, currentPosition.y),
            _ => currentPosition
        };
    }

    private void MoveItems(Vector3 newPosition)
    {
        foreach (var item in _bodyItems)
        {
            Vector2 nextPosition = item.transform.position;
            item.transform.position = newPosition;
            newPosition = nextPosition;
        }
    }
}
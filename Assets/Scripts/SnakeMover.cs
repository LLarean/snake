using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SnakeMover : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed = 1;

    private List<Item> _bodySections = new();
    private int _bodyCounter = 0;

    private float _minPositionX;
    private float _maxPositionX;
    private float _minPositionY;
    private float _maxPositionY;
    private bool _isMoving;

    private Direction _direction;

    public event Action OnBonusEntered;

    private void Start()
    {
        _minPositionX = _camera.ScreenToWorldPoint(new Vector2(0, 0)).x;
        _maxPositionX = _camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;

        _minPositionY = _camera.ScreenToWorldPoint(new Vector2(0, 0)).y;
        _maxPositionY = _camera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;

        _isMoving = true;
        StartCoroutine(Moving());
    }

    private void Update()
    {
        SetVelocity();
        ChangePosition();

        foreach (var item in _bodySections)
        {
            item.ChangePosition();
        }
    }

    private void SetVelocity()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Direction.Top;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Direction.Bot;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = Direction.Right;
        }
    }

    private void ChangePosition()
    {
        Vector3 screenPoint = _camera.WorldToScreenPoint(transform.position);

        if (screenPoint.x < _minPositionX)
        {
            transform.position = new Vector2(_maxPositionX, transform.position.y);
        }
        else if (screenPoint.x > Screen.width)
        {
            transform.position = new Vector2(_minPositionX, transform.position.y);
        }
        else if (screenPoint.y < 0)
        {
            transform.position = new Vector2(transform.position.x, _maxPositionY);
        }
        else if (screenPoint.y > Screen.height)
        {
            transform.position = new Vector2(transform.position.x, _minPositionY);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Bonus bonus))
        {
            Destroy(bonus.gameObject);
            OnBonusEntered?.Invoke();
        }

        if (other.gameObject.TryGetComponent(out Item item))
        {
            if (item.ItemType == ItemType.Food)
            {
                _bodySections.Add(item);
                item.Initialize(_camera);
            }
        }
    }

    private IEnumerator Moving()
    {
        while (_isMoving == true)
        {
            var lastPosition = transform.position;
            var temp = .13f;

            transform.position = _direction switch
            {
                Direction.Top => new Vector2(transform.position.x, transform.position.y + temp),
                Direction.Bot => new Vector2(transform.position.x, transform.position.y - temp),
                Direction.Right => new Vector2(transform.position.x + temp, transform.position.y),
                Direction.Left => new Vector2(transform.position.x - temp, transform.position.y),
                _ => transform.position
            };

            yield return new WaitForSeconds(_speed);

            MovingBody(lastPosition);
        }
    }

    private void MovingBody(Vector3 newPosition)
    {
        for (int i = 0; i < _bodySections.Count; i++)
        {
            Vector2 nextPosition = _bodySections[i].transform.position;
            _bodySections[i].transform.position = newPosition;
            newPosition = nextPosition;
        }
    }
}
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SnakeMover : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed = 1;
    
    private float _minPositionX;
    private float _maxPositionX;
    
    private float _minPositionY;
    private float _maxPositionY;

    public event Action OnBonusEntered;

    private void Start()
    {
        _minPositionX = _camera.ScreenToWorldPoint(new Vector2(0, 0)).x;
        _maxPositionX = _camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        
        _minPositionY = _camera.ScreenToWorldPoint(new Vector2(0, 0)).y;
        _maxPositionY = _camera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
    }

    private void Update()
    {
        SetVelocity();
        ChangePosition();
    }

    private void SetVelocity()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _rigidbody.velocity = new Vector3(0, _speed);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _rigidbody.velocity = new Vector3(0, -_speed);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _rigidbody.velocity = new Vector3(-_speed, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _rigidbody.velocity = new Vector2(_speed, 0);
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
            transform.position = new Vector2(_minPositionX , transform.position.y);
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
            Debug.Log("+1");
            OnBonusEntered?.Invoke();
        }
    }
}
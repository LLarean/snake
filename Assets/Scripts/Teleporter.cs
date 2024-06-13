using UnityEngine;

public class Teleporter
{
    private readonly Camera _camera;
    private readonly Transform _transform;
    
    private readonly float _minPositionX;
    private readonly float _maxPositionX;
    private readonly float _minPositionY;
    private readonly float _maxPositionY;

    public Teleporter(Camera camera, Transform transform)
    {
        _camera = camera;
        _transform = transform;
        
        _minPositionX = _camera.ScreenToWorldPoint(new Vector2(0, 0)).x;
        _maxPositionX = _camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        _minPositionY = _camera.ScreenToWorldPoint(new Vector2(0, 0)).y;
        _maxPositionY = _camera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
    }
    
    public void ChangePosition()
    {
        Vector3 screenPoint = _camera.WorldToScreenPoint(_transform.position);

        if (screenPoint.x < _minPositionX)
        {
            _transform.position = new Vector2(_maxPositionX, _transform.position.y);
        }
        else if (screenPoint.x > Screen.width)
        {
            _transform.position = new Vector2(_minPositionX , _transform.position.y);
        }
        else if (screenPoint.y < _minPositionY)
        {
            _transform.position = new Vector2(_transform.position.x, _maxPositionY);
        }
        else if (screenPoint.y > Screen.height)
        {
            _transform.position = new Vector2(_transform.position.x, _minPositionY);
        }
    }
}
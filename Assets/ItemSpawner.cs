using System;
using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _interval;
    
    private float _maxPositionX;
    private float _maxPositionY;
    private bool _isActive;
    
    
    private void Start()
    {
        _maxPositionX = _camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        _maxPositionY = _camera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;

        _isActive = true;
        // StartCoroutine(Spawning());
    }

    // private IEnumerator Spawning()
    // {
    //     // while (_isMoving == true)
    //     // {
    //     //     var lastPosition = transform.position;
    //     //     var temp = .13f;
    //     //
    //     //     transform.position = _direction switch
    //     //     {
    //     //         Direction.Top => new Vector2(transform.position.x, transform.position.y + temp),
    //     //         Direction.Bot => new Vector2(transform.position.x, transform.position.y - temp),
    //     //         Direction.Right => new Vector2(transform.position.x + temp, transform.position.y),
    //     //         Direction.Left => new Vector2(transform.position.x - temp, transform.position.y),
    //     //         _ => transform.position
    //     //     };
    //     //
    //     //     yield return new WaitForSeconds(_speed);
    //     //
    //     //     MovingBody(lastPosition);
    //     // }
    // }
}

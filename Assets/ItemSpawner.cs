using System.Collections;
using UnityEngine;
using Random = System.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Item _item;
    [SerializeField]
    [Range(0, 20)]
    private float _interval = 2;
    
    private Random _random;
    private float _minPositionX;
    private float _maxPositionX;
    private float _minPositionY;
    private float _maxPositionY;
    private bool _isActive;


    private void Start()
    {
        _random = new Random();
        
        _minPositionX = _camera.ScreenToWorldPoint(new Vector2(0, 0)).x;
        _maxPositionX = _camera.ScreenToWorldPoint(new Vector2(Screen.width + 100, 0)).x;
        _minPositionY = _camera.ScreenToWorldPoint(new Vector2(0, 100)).y;
        _maxPositionY = _camera.ScreenToWorldPoint(new Vector2(0, Screen.height + 100)).y;

        _isActive = true;
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while (_isActive == true)
        {
            if (_interval != 0)
            {
                Spawn();
            }
            
            yield return new WaitForSeconds(_interval);
        }
    }

    private void Spawn()
    {
        var xPosition = _random.Next((int)_minPositionX, (int)_maxPositionX);
        var yPosition = _random.Next((int)_minPositionY, (int)_maxPositionY);
        var randomPosition = new Vector3(xPosition, yPosition);
        
        if(Physics2D.Raycast(randomPosition, Vector2.zero) == true)
        {
            return;
        }
        
        var item = Instantiate(_item, new Vector3(xPosition, yPosition, 0), Quaternion.identity);
        item.gameObject.transform.SetParent(transform);
    }
}

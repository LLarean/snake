using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _itemType; 
    [SerializeField] private SpriteRenderer _spriteRenderer; 
    [SerializeField] private Collider2D _collider2D; 
    private Teleporter _teleporter;

    public ItemType ItemType => _itemType;
    
    public void Initialize(Camera camera)
    {
        _teleporter = new Teleporter(camera, transform);
    }

    public void ChangePosition() => _teleporter.ChangePosition();

    public void SetBodyType(ItemType itemType)
    {
        _itemType = itemType;
        _spriteRenderer.color = Color.white;

        // if (itemType == ItemType.Neck)
        // {
        //     _collider2D.isTrigger = false;
        //     _spriteRenderer.color = Color.red;
        // }
    }
}
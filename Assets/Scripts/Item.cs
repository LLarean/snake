using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _itemType; 
    [SerializeField] private SpriteRenderer _spriteRenderer; 
    private Teleporter _teleporter;

    public ItemType ItemType => _itemType;
    
    public void Initialize(Camera camera)
    {
        _teleporter = new Teleporter(camera, transform);
        SetBodyType();
    }

    public void ChangePosition() => _teleporter.ChangePosition();

    private void SetBodyType()
    {
        _itemType = ItemType.Body;
        _spriteRenderer.color = Color.white;
    }
}
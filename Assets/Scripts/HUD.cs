using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text _points;
    [SerializeField] private SnakeMover _snakeMover;

    private int _pointsValue;

    private void Start()
    {
        _points.text = _pointsValue.ToString();
        // _snakeMover.OnBonusEntered += AddPoints;
    }

    private void AddPoints()
    {
        _pointsValue++;
        _points.text = _pointsValue.ToString();
    }
}

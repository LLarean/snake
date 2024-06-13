using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private SnakeHead _snakeHead;
    [SerializeField]
    private TMP_Text _scores;

    private int _scoresValue;
    
    public int ScoresValue => _scoresValue;

    private void Start()
    {
        _scores.text = _scoresValue.ToString();
        _snakeHead.OnItemAdded += AddScore;
    }

    private void AddScore()
    {
        _scoresValue++;
        _scores.text = _scoresValue.ToString();
    }
}

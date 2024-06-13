using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private SnakeHead _snakeHead;
    [SerializeField] private HUD _hud;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _scores;
    [SerializeField] private Button _restart;

    private void Start()
    {
        _snakeHead.OnGameOver += ShowGameOver;
        _restart.onClick.AddListener(ReloadScene);
        
        _title.gameObject.SetActive(false);
        _scores.gameObject.SetActive(false);
        _restart.gameObject.SetActive(false);
    }

    private void ShowGameOver()
    {
        _title.gameObject.SetActive(true);
        _scores.gameObject.SetActive(true);
        _restart.gameObject.SetActive(true);
        ShowScores();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ShowScores()
    {
        if (PlayerPrefs.GetInt("Scores", 0) < _hud.ScoresValue)
        {
            PlayerPrefs.SetInt("Scores", _hud.ScoresValue);
            _scores.text = "New Record: " + _hud.ScoresValue;
        }
        else
        {
            _scores.text = "Old Record: " + _hud.ScoresValue;
        }
    }
}
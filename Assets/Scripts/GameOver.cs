using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private SnakeHead _snakeHead;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _toMenu;

    private void Start()
    {
        _snakeHead.OnGameOver += ShowGameOver;
        _restart.onClick.AddListener(ReloadScene);
        _title.gameObject.SetActive(false);
        _restart.gameObject.SetActive(false);
    }

    private void ShowGameOver()
    {
        _title.gameObject.SetActive(true);
        _restart.gameObject.SetActive(true);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
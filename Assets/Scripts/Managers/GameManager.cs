using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : Singleton<GameManager>
{
    GameState state;
    public GameState State{ get => state; }

    [SerializeField] float waitAfterGameOver = 2f;

    protected override void Awake()
    {
        base.Awake();

        state = GameState.MainMenu;
    }

    void OnEnable() => EventHandler.GameOverEvent += GameOver;
    void OnDisable() => EventHandler.GameOverEvent -= GameOver;

    public void GameOver()
    {
        state = GameState.GameOver;
        UIManager.Instance.gameOverText.gameObject.SetActive(true);
        StartCoroutine(OpenMainMenu());
    }

    IEnumerator OpenMainMenu()
    {
        yield return new WaitForSeconds(waitAfterGameOver);

        UIManager.Instance.gameOverText.gameObject.SetActive(false);
        UIManager.Instance.mainMenu.gameObject.SetActive(true);
        UIManager.Instance.highScoreText.text = "High Score: " + PlayerPrefs.GetInt(Strings.strHighScore).ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        state = GameState.MainMenu;
    }

    public void SetGameState(GameState newState)
    {
        state = newState;
    }
}

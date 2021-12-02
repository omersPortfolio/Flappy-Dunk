using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class UIManager : Singleton<UIManager>
{
    [Header("In-Game")]
    public TMP_Text scoreText;
    public TMP_Text swishText;
    public TMP_Text gameOverText;


    [Header("Main Menu")]
    public TMP_Text highScoreText;
    public Image mainMenu;

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        mainMenu.gameObject.SetActive(true);
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt(Strings.strHighScore).ToString();
    }

    void OnEnable() => EventHandler.ScoreAddedEvent += OnScoreAdded;
    void OnDisable() => EventHandler.ScoreAddedEvent -= OnScoreAdded;

    void OnScoreAdded(bool isSwish)
    {
        if (isSwish)
        {
            swishText.gameObject.SetActive(true);
            StartCoroutine(DeactivateSwishText());
        }
    }

    IEnumerator DeactivateSwishText()
    {
        yield return new WaitForSeconds(0.5f);

        swishText.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        mainMenu.gameObject.SetActive(false);
        // Use events instead
        GameManager.Instance.SetGameState(GameState.Running);
    }
}

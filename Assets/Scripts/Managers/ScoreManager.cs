using UnityEngine;

public sealed class ScoreManager : MonoBehaviour
{
    [SerializeField] int score = 0;
    public int Score { get => score; }

    [SerializeField] int swishScoreBonus = 2;

    private void OnEnable()
    {
        EventHandler.ScoreAddedEvent += OnAddScore;
    }

    private void OnDisable()
    {
        EventHandler.ScoreAddedEvent -= OnAddScore;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerPrefs.DeleteAll();
            UIManager.Instance.highScoreText.text = "High Score: " + PlayerPrefs.GetInt(Strings.strHighScore).ToString();
        }
#endif
    }

    public void OnAddScore(bool isGoalSwish)
    {
        score += isGoalSwish ? swishScoreBonus : 1;

        int highScore = PlayerPrefs.GetInt(Strings.strHighScore, 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt(Strings.strHighScore, score);
        }

        // More Events
        UIManager.Instance.scoreText.text = score.ToString();
    }
}

using UnityEngine;

public sealed class ScoreManager : MonoBehaviour
{
    [SerializeField] int score = 0;
    public int Score { get => score; }

    [SerializeField] int swishScoreBonus = 2;

    void Start()
    {
        UIManager.Instance.scoreText.text = "0";
    }

    void OnEnable() => EventHandler.ScoreAddedEvent += OnAddScore;
    void OnDisable() => EventHandler.ScoreAddedEvent -= OnAddScore;

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q pressed");
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

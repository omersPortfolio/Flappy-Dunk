using System;

public static class EventHandler
{
    public static event Action GameOverEvent;
    public static void CallGameOverEvent()
    {
        if (GameOverEvent != null)
            GameOverEvent();
    }

    public static event Action<bool> ScoreAddedEvent;
    public static void CallScoreAddedEvent(bool isGoalSwish)
    {
        if (ScoreAddedEvent != null)
            ScoreAddedEvent(isGoalSwish);
    }
}

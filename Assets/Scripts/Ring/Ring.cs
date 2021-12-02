using System;
using System.Collections;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [SerializeField] GameObject colliders;
    [SerializeField] SpriteRenderer[] renderers;
    [SerializeField] float fadeOutTime = 0.5f;

    bool isGoalSwish = true;
    bool isTriggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Strings.strPlayer))
        {
            Vector2 playerDir = transform.position - other.transform.position;
            if (playerDir.y > 0.15f)
            {
                EventHandler.CallGameOverEvent();
                isTriggered = true;
            }
        }

        if (other.CompareTag(Strings.strWall) && GameManager.Instance.State != GameState.GameOver)
        {
            EventHandler.CallGameOverEvent();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (GameManager.Instance.State != GameState.Running && isTriggered)
            return;

        if (other.CompareTag(Strings.strPlayer))
        {
            float playerYDir = transform.position.y - other.transform.position.y;
            if (playerYDir > 0)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                colliders.SetActive(false);

                EventHandler.CallScoreAddedEvent(isGoalSwish);
                isTriggered = true;

                foreach (SpriteRenderer sr in renderers)
                {
                    StartCoroutine(FadeOut(sr));
                }
            }
        }
    }

    public void SetNoSwish()
    {
        isGoalSwish = false;
    }

    IEnumerator FadeOut(SpriteRenderer sr)
    {
        float currentAlpha = sr.color.a;
        float distance = currentAlpha;

        while (currentAlpha > float.Epsilon)
        {
            currentAlpha -= distance / fadeOutTime * Time.deltaTime;
            sr.color = new Color(1f, 1f, 1f, currentAlpha);
            yield return null;
        }

        sr.color = new Color(1f, 1f, 1f, 0f);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float duration = 0.15f;
    [SerializeField] float magnitude = 0.4f;

    private void OnEnable() =>  EventHandler.ScoreAddedEvent += ShakeOnSwishGoal;
    private void OnDisable() => EventHandler.ScoreAddedEvent -= ShakeOnSwishGoal;

    void ShakeOnSwishGoal(bool isGoalSwish)
    {
        if (isGoalSwish)
            StartCoroutine(Shake(duration, magnitude));
    }

    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}

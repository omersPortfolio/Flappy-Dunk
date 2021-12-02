using UnityEngine;
using UnityEngine.Events;

public class DeathZone : MonoBehaviour
{
    [SerializeField] UnityEvent OnPlayerFallen;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (GameManager.Instance.State != GameState.Running)
            return;

        if (other.collider.CompareTag(Strings.strPlayer))
        {
            //TODO: Get rid of this dependency
            //GameManager.Instance.GameOver();

            OnPlayerFallen?.Invoke();
        }
    }
}

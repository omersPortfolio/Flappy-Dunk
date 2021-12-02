using UnityEngine;
using UnityEngine.Events;

public class RingCollider : MonoBehaviour
{
    [SerializeField] UnityEvent OnPlayerCollided;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag(Strings.strPlayer))
            OnPlayerCollided?.Invoke();
    }
}

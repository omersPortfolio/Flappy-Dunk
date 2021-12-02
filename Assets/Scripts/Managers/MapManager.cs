using UnityEngine;

public sealed class MapManager : MonoBehaviour
{
    [SerializeField] float spaceBetweenRings = 4.5f;
    [SerializeField] float initialRingOffset = 1.25f;
    [SerializeField] float SqrDistanceToSpawnRings = 75f;
    [SerializeField] float gameBounds = 2.5f;

	// References
    Transform playerPos;
    RingObjectPool ringPool;

    Vector2 lastRingPos;

    void Awake()
    {
        ringPool = GetComponent<RingObjectPool>();
        playerPos = FindObjectOfType<Player>().transform;
		
		lastRingPos.x += initialRingOffset;
    }

    void Update()
    {
        if (Vector2.SqrMagnitude(lastRingPos - (Vector2)playerPos.position) < SqrDistanceToSpawnRings)
        {
            SpawnRings(lastRingPos);
        }
    }

    void SpawnRings(Vector2 pos)
    {
        GameObject ring = ringPool.GetFromPool();
        ring.transform.position = pos;

        lastRingPos.x += Random.Range(spaceBetweenRings - 0.5f, spaceBetweenRings);
        lastRingPos.y = Random.Range(-gameBounds, gameBounds);
    }
}

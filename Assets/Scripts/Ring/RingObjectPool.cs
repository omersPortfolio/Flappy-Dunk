using System.Collections.Generic;
using UnityEngine;

public sealed class RingObjectPool : MonoBehaviour
{
    [SerializeField] GameObject[] ringPrefabs;

    Queue<GameObject> availableRings = new Queue<GameObject>();

    void Awake()
    {
        GrowPool();
    }

    void GrowPool()
    {
        for (int i = 0; i < 15; i++)
        {
            var ring = Instantiate(ringPrefabs[Random.Range(0, ringPrefabs.Length)]);
            ring.transform.SetParent(transform);
            AddToPool(ring);
        }
    }

    public GameObject GetFromPool()
    {
        if (availableRings.Count == 0)
            GrowPool();

        var instance = availableRings.Dequeue();
        instance.SetActive(true);
        return instance;
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availableRings.Enqueue(instance);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticlePoolingManager : MonoBehaviour
{

    public GameObject particleGroup;
    public int initialPoolSize = 10;

    private Queue<GameObject> particleGroupPool = new Queue<GameObject>();

    void Start()
    {
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            //GameObject particleGroup = Instantiate(particleGroup);
            particleGroup.SetActive(false);
            particleGroupPool.Enqueue(particleGroup);
        }
    }

    public void SpawnParticleGroup(Vector3 position)
    {
        if (particleGroupPool.Count == 0)
        {
            // If the pool is empty, instantiate a new particle group
            GameObject newParticleGroup = Instantiate(particleGroup, position, new Quaternion(0,0,0,0));
        }
        else
        {
            // Reuse a particle group 
            GameObject recycledParticleGroup = particleGroupPool.Dequeue();
            recycledParticleGroup.transform.position = position;
            recycledParticleGroup.SetActive(true);
        }
    }

    public void ReturnToPool(GameObject pParticleGroup)
    {
        pParticleGroup.SetActive(false);
        particleGroupPool.Enqueue(pParticleGroup);
    }

}

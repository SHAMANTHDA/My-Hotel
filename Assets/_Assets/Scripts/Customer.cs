using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public GameObject[] customerPrefabs; 
    public int numberOfCustomers = 4;
    public float spaceBetweenCustomers = 2.0f; 

    void Start()
    {
        CreateQueue();
    }

    private void CreateQueue()
    {
        List<GameObject> shuffledPrefabs = new List<GameObject>(customerPrefabs);
        Shuffle(shuffledPrefabs);

        for (int i = 0; i < numberOfCustomers; i++)
        {
            int prefabIndex = i % shuffledPrefabs.Count;
            GameObject customerToInstantiate = shuffledPrefabs[prefabIndex];

            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z + i * spaceBetweenCustomers);
            Instantiate(customerToInstantiate, position, Quaternion.identity, transform);
        }
    }

    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            int r = Random.Range(i, n);
            T temp = list[i];
            list[i] = list[r];
            list[r] = temp;
        }
    }
}

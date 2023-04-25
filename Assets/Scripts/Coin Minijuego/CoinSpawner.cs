using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    float timer;
    public GameObject coinPrefab;

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 2f)
        {
            timer = 0;
            float x = Random.Range(-230f, 230);
            Vector3 position = new Vector3(x, 0, 0);
            Quaternion rotation = new Quaternion();
            Instantiate(coinPrefab, position, rotation);
        }
    }
}

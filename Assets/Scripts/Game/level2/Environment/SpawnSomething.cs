using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSomething : MonoBehaviour
{
    public List<GameObject> listOfGO;
    public float pauseTime;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InvokeSpawnSomething", pauseTime, 15f);
    }

    void InvokeSpawnSomething()
    {
        int i;
        float offsetX;
        var rand = new System.Random();
        i = rand.Next()%7;
        offsetX = (float)(rand.Next() % 6) * Mathf.Pow(-1f,(float)rand.Next()) + (rand.Next()%1000) * 0.001f; // отступ по оси Х
        Instantiate(listOfGO[i], new Vector3(transform.position.x + offsetX,10f, 0.1f), transform.rotation);
    }
}

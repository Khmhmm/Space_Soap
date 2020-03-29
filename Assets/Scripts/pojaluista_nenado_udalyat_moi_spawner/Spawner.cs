using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float coordX, coordY;
    public GameObject SpawnIt;
    
    void Awake()
    {
        Instantiate(SpawnIt, new Vector3(coordX, coordY, 0f), Quaternion.Euler(0f,0f,180));
    }

    void Start()
    {
        Invoke("Destroyer", 1f);
    }

    void Destroyer()
    {
        Destroy(gameObject);
    }
}

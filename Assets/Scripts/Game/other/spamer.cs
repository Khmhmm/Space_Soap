using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spamer : MonoBehaviour
{
    public GameObject enemy1;
    public int count1;
    public GameObject enemy2;
    public int count2;
    public GameObject enemy3;
    public int count3;
    public GameObject enemy4;
    public int count4;
    void Start()
    {
        InvokeRepeating("Spawn2", 2.0f, 0.6f);
        InvokeRepeating("Spawn1", 4.0f, 0.6f);
        InvokeRepeating("Spawn3", 8.0f, 0.6f);
        InvokeRepeating("Spawn4", 8.0f, 0.6f);
    }
    void Spawn1()
    {
        if (count1 != 0)
        {
            Instantiate(enemy1, new Vector2(9.5f, transform.position.y - 0.7f), transform.rotation);
            count1 -= 1;
        }
    }
    void Spawn2()
    { 
        if (count2 != 0)
        {
            Instantiate(enemy2, new Vector2(-9.5f, transform.position.y - 2f), transform.rotation);
            count2 -= 1;
        }
    }
    void Spawn3()
    {
        if (count3 != 0)
        {
            Instantiate(enemy3, new Vector2(0, transform.position.y + 0.7f), transform.rotation);
            count3 -= 1;
        }
    }
    void Spawn4()
    {
        if (count4 != 0)
        {
            Instantiate(enemy4, new Vector2(0, transform.position.y + 0.7f), transform.rotation);
            count4 -= 1;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

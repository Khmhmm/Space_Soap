using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Goes_Away : MonoBehaviour
{
    private bool goDestroy = false;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckDestroy", 2f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!goDestroy)
            transform.Translate(new Vector3(0f, -speed, 0f), Space.World);
        else
            Destroy(gameObject);
    }

    void CheckDestroy()
    {
        if (transform.position.y < -21.79f)
            goDestroy = true;
    }
}

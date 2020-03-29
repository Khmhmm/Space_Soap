using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_environment : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyThis", 22.9f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0f, -0.019f), Space.World);
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}

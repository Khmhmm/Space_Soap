using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl1destroy : MonoBehaviour
{
    void Update()
    {
        if (this.transform.position.y <= -20)
        {
            Destroy(gameObject);
        }
    }
}

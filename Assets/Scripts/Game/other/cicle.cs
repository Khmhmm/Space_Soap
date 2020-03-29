using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cicle : MonoBehaviour
{
    void Update()
    {
        if(this.transform.position.y <= -23)
        {
            this.transform.position = new Vector3(0, 36.76f,10f);
        }
    }
}

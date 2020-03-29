using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class back_move : MonoBehaviour
{
    void Update()
    {
        this.transform.position -= new Vector3(0.01f, 0);
        if (this.transform.position.x <= -30)
        {
            this.transform.position = new Vector3(49, 0, 10);
        }
    }
}

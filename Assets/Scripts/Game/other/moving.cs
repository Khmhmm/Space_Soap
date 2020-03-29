using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(0, -1f*Time.deltaTime);
    }
}

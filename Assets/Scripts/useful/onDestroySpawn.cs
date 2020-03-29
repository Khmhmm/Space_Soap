using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onDestroySpawn : MonoBehaviour
{
    public GameObject spamer;
    private void OnDestroy()
    {
        Instantiate(spamer);
    }
}

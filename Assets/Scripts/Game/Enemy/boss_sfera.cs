using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_sfera : MonoBehaviour
{

    private GameObject Boss_helicopter;

    void Awake()
    {
        StartCoroutine("Destroy");
    }
    // Start is called before the first frame update
    void Start()
    {
        Boss_helicopter = GameObject.FindWithTag("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            transform.position = new Vector3(Boss_helicopter.transform.position.x, Boss_helicopter.transform.position.y - 1.71f, Boss_helicopter.transform.position.z);
            if (Boss_helicopter == null)
                throw new System.Exception("Boss was died");
        }
        catch(System.Exception e)
        {
            Debug.Log(e);
            Destroy(gameObject);
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}

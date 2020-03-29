using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private bool donotmove = false;
    public GameObject biglaser,sfera;
    private Rigidbody2D destr_rigid;
    private int count=0;
    // Start is called before the first frame update
    void Start()
    {
       /* Invoke("CreateSphere", 1.5f);
        InvokeRepeating("InstLas", 3f,0.1f); */
        destr_rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int yes = 0;
        if(transform.position.y > 2.5f)
            destr_rigid.MovePosition(new Vector2(transform.position.x, transform.position.y - 0.85f * Time.deltaTime));
        if (transform.position.y < 2.5f)
            yes = 2;
        if (yes == 2)
        {
            InvokeRepeating("InstLas", 3f, 0.35f);
            yes = 1;
        }
            if (yes == 1)
            {
                if (donotmove == false)
                {
                    destr_rigid.MovePosition(new Vector2(transform.position.x, transform.position.y - 0.1f * Time.deltaTime));
                }
                if (count == 500)
                {
                    CancelInvoke("InstLas");
                }
            }

    }

    void InstLas()
    {
        Instantiate(biglaser, new Vector3(transform.position.x, transform.position.y,0f), Quaternion.Euler(0f,0f,0f));
        donotmove = true;
        count++;
    }

}

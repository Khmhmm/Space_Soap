using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy5 : MonoBehaviour
{
    public Collider2D Enemycol;
    public GameObject Laser;
    public float speed = 0.1f;
    float a;
    public int hp = 3;

    private void Start()
    {
        InvokeRepeating("Fire", 0.1f, 0.5f);
    }
    void Update()
    {
        if (this.transform.position.x < 10)
        {
            transform.position += new Vector3(0.25f*speed, -0.25f*speed, 0);
        }
        else
        {
            Destroy(gameObject);
        }
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Fire()
    {
        Instantiate(Laser, new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, 0f), transform.rotation);
        Instantiate(Laser, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, 0f), transform.rotation);
    }
    void OnTriggerEnter2D(Collider2D Enemycol)
    {
        var name = Enemycol.gameObject.name;
        if (name == "laser(Clone)")
        {
            hp -= 1;
        }
    }
}

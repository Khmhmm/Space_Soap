using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy1 : MonoBehaviour
{
    public Collider2D Enemycol;
    public GameObject Laser;
    public float speed = 0.01f;
    float a;
    public int hp = 1;

    private void Start()
    {
        InvokeRepeating("Fire", 0.5f, Random.Range(0.5f, 2f));
    }
    void Update()
    {
        if (this.transform.position.x > -10)
        {
            transform.position -= new Vector3(speed, 0, 0);
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
        Instantiate(Laser, new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, 0f), Quaternion.Euler(0f,0f,0f));
        Instantiate(Laser, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, 0f), Quaternion.Euler(0f, 0f, 0f));
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

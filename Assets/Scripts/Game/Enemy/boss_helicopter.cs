using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_helicopter : MonoBehaviour
{
    private Rigidbody2D Boss;
    private bool vlevo = false;
    public float speed, hp;
    public GameObject laser;
    public GameObject circle;
    public GameObject player;
    private Collider2D BossCollide;

    // Start is called before the first frame update
    void Start()
    {
        Boss = GetComponent<Rigidbody2D>();
        Invoke("State", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        int yes = 0;
        if (transform.position.y > 3.8f)
            Boss.MovePosition(new Vector2(transform.position.x, transform.position.y - 0.85f * Time.deltaTime));
        if (transform.position.y <= 3.8f)
            yes = 1;
        if (yes == 1)
        {
            if (Boss.position.x > 7.65f)
                vlevo = true;
            else if (Boss.position.x < -7.65f)
                vlevo = false;
            if (!vlevo)
            {
                Boss.MovePosition(Boss.position + Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                Boss.MovePosition(Boss.position + Vector2.left * speed * Time.deltaTime);
            }

            CheckDead();
        }
    }

    int Generator_Random_Numbers()
    {
        var rand = new System.Random();
        int result = rand.Next() % 3;
        return result;
    }

    void State()
    {
        int state = Generator_Random_Numbers();
        switch (state) 
        {
            case 0:
                PlasmaFire();
                DoubleFire();
                break;
            case 1:
                PlasmaFire();
                break;
            case 2:
                DoubleFire();
                break;
        }
        StartCoroutine("Restart");
    }

    void PlasmaFire()
    {
       // Instantiate(circle, new Vector3(transform.position.x, 1.53f), transform.rotation); // починить try-catch
        InvokeRepeating("Fire",0.1f,0.3f);
        
        
    }

    void DoubleFire()
    {
      //  Instantiate(circle, new Vector3(transform.position.x, 1.53f), transform.rotation); // починить try-catch
        InvokeRepeating("FireWithTwo", 0.1f,0.3f);
        
    }

    void Fire()
    {
        Instantiate(laser,new Vector3(transform.position.x,transform.position.y-2.15f),Quaternion.Euler(0f,0f,0f));
    }

    void FireWithTwo()
    {
        Instantiate(laser, new Vector3(transform.position.x, transform.position.y - 2.15f), Quaternion.Euler(0f,0f,45f));
        Instantiate(laser, new Vector3(transform.position.x, transform.position.y - 2.15f), Quaternion.Euler(0f, 0f, -45f));
    }

   void OnTriggerEnter2D(Collider2D BossCollide)
    {
        var name = BossCollide.gameObject.tag;
        if(name == "Player_laser")
        {
            var script = BossCollide.GetComponent<LaserScript>();
            var damage = script.damage;
            var rand = new System.Random();
            damage += rand.Next()%10;
            hp -= damage;
            Destroy(BossCollide.gameObject);
        }
    }

    void CheckDead()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1.5f);
        CancelInvoke();
        Invoke("State", 2f);
    }

}

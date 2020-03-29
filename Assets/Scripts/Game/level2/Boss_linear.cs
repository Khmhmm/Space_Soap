using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_linear : MonoBehaviour
{
    public float hp, speed;
    private int i = 0;
    private Rigidbody2D boss_rigid;
    public GameObject laser, player;

   // Start is called before the first frame update
    void Start()
    {
        boss_rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Invoke("State", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        int yes = 0;
        if (transform.position.y > 3.8f)
            boss_rigid.MovePosition(new Vector2(transform.position.x, transform.position.y - 0.85f * Time.deltaTime));
        if (transform.position.y <= 3.8f)
            yes = 1;
        // необходимо, чтобы босс был не слишком высоко по оси Y, поэтому двигаем его вниз
        if (yes == 1)
        {
            if ((Mathf.Abs(transform.position.x - player.transform.position.x)) >= 0.25f) // двигается за игроком
            {
                boss_rigid.MovePosition(new Vector2(transform.position.x + speed*Time.deltaTime*Mathf.Sign(player.transform.position.x - transform.position.x),transform.position.y));
            }
        }
        CheckDead();
        }

    void CheckDead()
    {
        if (hp <= 0)
            Destroy(gameObject);
    }

    int Generator_Random_Numbers()
    {
        var rand = new System.Random();
        int result = rand.Next() % 3;
        return result;
    }

    void State()
    {
        i = 1;
        Debug.Log(i);
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
        InvokeRepeating("Fire", 0.1f, 0.1f);
        i = 0;

    }

    void DoubleFire()
    {
        //  Instantiate(circle, new Vector3(transform.position.x, 1.53f), transform.rotation); // починить try-catch
        InvokeRepeating("FireWithTwo", 0.1f, 0.1f);
        i = 0;
    }

    void Fire()
    {
        Instantiate(laser, new Vector3(transform.position.x, transform.position.y - 2.15f), Quaternion.Euler(0f,0f,0f));
    }

    void FireWithTwo()
    {
        Instantiate(laser, new Vector3(transform.position.x, transform.position.y - 2.15f), Quaternion.Euler(0f, 0f, 45f));
        Instantiate(laser, new Vector3(transform.position.x, transform.position.y - 2.15f), Quaternion.Euler(0f, 0f, -45f));
    }


    void OnTriggerEnter2D(Collider2D BossCollide)
    {
        var name = BossCollide.gameObject.tag;
        if (name == "Player_laser")
        {
            var script = BossCollide.GetComponent<LaserScript>();
            var damage = script.damage;
            var rand = new System.Random();
            damage += rand.Next() % 10;
            hp -= damage;
            Destroy(BossCollide.gameObject);
        }
    }

    public IEnumerator Restart()
    {
        yield return new WaitForSeconds(3f);
        CancelInvoke();
        Invoke("State", 5f);
        Debug.Log("Restarted");
    }

    
}

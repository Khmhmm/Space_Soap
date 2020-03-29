using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    /* Это специальный скрипт для второго уровня*/
    public GUISkin skin;
    private int check = 0,autofires_count = 0;
    public float speed, hp, shield_p, FireRate, timer;
    private Rigidbody2D player;
    public GameObject Laser, autoLaser, dialogue_nobody_to_shoot, dialogue_auto_fire, dialogue_not_ready;
    private Collider2D Playercollide;
    private bool shield_is_damaged = false;
    private float max_hp, max_shield, shields_delay, shield_timer, auto_laser_timer; // нужно для подсчета процентов в полосках

    void Fire() // стрельба
    {
        Instantiate(Laser, new Vector3(player.position.x + 0.6f, player.position.y, 0f), transform.rotation); // выстрел из правой пушки
        Instantiate(Laser, new Vector3(player.position.x - 0.6f, player.position.y, 0f), transform.rotation); // а тут из левой
        Instantiate(Laser, new Vector3(player.position.x, player.position.y + 0.6f, 0f), transform.rotation); // по центру
    }

    void checkDead() // при смерти вызываем экран смерти (можно сделать на нём статистику, но необязательно)
    {
        if (hp <= 0)
        {
            SceneManager.LoadScene("DeadScreen");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = FireRate;
        player = GetComponent<Rigidbody2D>();
        max_hp = hp;
        max_shield = shield_p;
        InvokeRepeating("Plus_timer", 1f, 0.91f); // прибавляем таймер
    }

    // Update is called once per frame
    void Update()
    {
        if (autofires_count > 6)
        {
            CancelInvoke("AutoFire");
            autofires_count = 0;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-speed, 0);
            if (this.transform.position.x <= -8.4)
            {
                this.transform.position += new Vector3(2 * speed, 0);
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(speed, 0);
            if (this.transform.position.x >= 8.4)
            {
                this.transform.position += new Vector3(-2 * speed, 0);
            }
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position += new Vector3(0, speed);
            if (this.transform.position.y >= 1.07f)
            {
                this.transform.position += new Vector3(0, -speed);
            }
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position += new Vector3(0, -speed);
            if (this.transform.position.y <= -3.4)
            {
                this.transform.position += new Vector3(0, speed);
            }
        }
        if (Input.GetKey(KeyCode.F))
        {
            timer += Time.deltaTime;//костыль, но добавляет баланса
            if (timer >= FireRate)
            {
                Fire();
                timer = 0;
            }
        }

        if (Input.GetKey(KeyCode.E)) // самонаводящийся выстрел
        {
            GameObject dialogues = GameObject.FindWithTag("dialogue");
            if (dialogues == null)
            {
                if (auto_laser_timer >= FireRate * 5f)
                {
                    check = 1;
                    Instantiate(dialogue_auto_fire, new Vector3(0f, -3f, 0f), transform.rotation);
                    StartCoroutine("Check_Delay");
                    InvokeRepeating("AutoFire", 1.6f, 0.3f);
                    auto_laser_timer = 0;
                }

                else if ((auto_laser_timer < 2f * FireRate) && (check != 1)) // чек запрещает постоянно вызывать диалог "не готово"
                {
                    check = 1;
                    Instantiate(dialogue_not_ready, new Vector3(0f, -3f), transform.rotation);
                    StartCoroutine("Check_Delay");
                }
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main_Menu");
        }

        if(shield_p < max_shield){ // перезарядка щита
            if(shield_is_damaged)
            {
                StartCoroutine("Shield_Delay"); // дилей, если повредили
            }
            else
            {
                StopCoroutine("Shield_Delay");
                shield_timer += Time.deltaTime;
                if (shield_timer > 0.5f)
                {
                    var rand = new System.Random();
                    shield_p += max_shield / (10f + (float)rand.Next()%9);
                    if (shield_p > max_shield)
                        shield_p = max_shield;
                    shield_timer = 0;
                }
            }
        }

        checkDead();
    }



    void OnGUI() // выводим стату на экран
    {
        GUI.skin = skin;
        int percent = (int)(100 * hp / max_hp);
        string str_percent = percent.ToString() + '%';
        int shield_percent = (int)(100 * shield_p / max_shield);
        string str_shield_percent = shield_percent.ToString() + '%';
        int autolaser_percent = (int)(100 * auto_laser_timer / (FireRate * 5f));
        string str_laser_percent = autolaser_percent.ToString() + " /100";
        GUI.color = Color.red;
        GUI.Box(new Rect(Screen.width / 1000f, 0.95f * Screen.height, percent, 20f), str_percent); // хп в процентах
        GUI.color = Color.blue;
        GUI.Box(new Rect(Screen.width / 1000f, 0.9f * Screen.height, shield_percent, 20f), str_shield_percent); // щиты в процентах
        GUI.color = Color.cyan;
        GUI.Box(new Rect(Screen.width * 0.85f, 0.95f * Screen.height, 100f, 20f), str_laser_percent); // заряд автолазера
    }

    // попадание в нас
    void OnTriggerEnter2D(Collider2D Playercollide)
    {
        var name = Playercollide.gameObject.tag;
        if (name == "Enemy_laser") // если попал лазер - минус хп
        {
            if (shield_p > 0)
            {
                shield_p -= 5f;
                Destroy(Playercollide.gameObject);
                shield_is_damaged = true;
            }
            else
            {
                hp -= 1f;
                Destroy(Playercollide.gameObject);
            }
        }
        else if (name == "enemy")
        {
            if (shield_p > 0)
                shield_p -= 5f;
            else
                hp -= 1f;
            transform.position = new Vector3(transform.position.x, -3.35f);
        }
        else if(name == "Boss")
        {
            hp -= 2*max_hp;
            transform.position = new Vector3(transform.position.x, -3.35f);
        }
    }

    IEnumerator Shield_Delay()
    {
        yield return new WaitForSeconds(3.0f);
        shield_is_damaged = false;
    }

    void AutoFire() // огонь с автонаводкой
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (enemies.Length < 1 )
        {
            check = 1;
            Instantiate(dialogue_nobody_to_shoot, new Vector3(0f, -3f), transform.rotation);
            StartCoroutine("Check_Delay");
            autofires_count = 10; // чтобы остановить инвокрепитинг. Можно поставить и больше, если в апдейте собираемся увеличивать число залпов вот только зачем =)
        }
        else
        {
            
            foreach (var enemy in enemies)
            {
                Instantiate(autoLaser, new Vector3(transform.position.x, transform.position.y + 0.9f), Quaternion.Euler(0f,0f,Mathf.Sign(transform.position.x - enemy.transform.position.x) * (185f+Vector3.Angle(enemy.transform.position-transform.position,transform.up))));
            } 
        }
                autofires_count++;
    }

    void Plus_timer() // нужно для автолазера
    {
        if (auto_laser_timer >= FireRate * 5f)
            auto_laser_timer = FireRate * 5f + 0.01f;
        else
            auto_laser_timer += 0.08f;

    }

    IEnumerator Check_Delay()
    {
        yield return new WaitForSeconds(5f);
        check = 0;
    }
}

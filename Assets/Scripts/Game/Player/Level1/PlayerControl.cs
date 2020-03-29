using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

    /* Это специальный скрипт для первого уровня*/
    public class PlayerControl : MonoBehaviour
    {
        public GUISkin skin;
        public float speed, hp, FireRate, timer;
        private Rigidbody2D player;
        public GameObject Laser;
        private Collider2D Playercollide;
        private GameObject topOfTheScreen;
        private GameObject bottomOfTheScreen;
        private Handler Hand_script;
        private float max_hp; // нужно для подсчета процентов в полоске хп
        private bool DoNotMove = false;
        private GameObject Handling;


        void Fire() // стрельба
        {
            Instantiate(Laser, new Vector3(player.position.x + 0.2f, player.position.y, 0f), transform.rotation); // выстрел из правой пушки
            Instantiate(Laser, new Vector3(player.position.x - 0.2f, player.position.y, 0f), transform.rotation); // а тут из левой
        }

        // Start is called before the first frame update
        void Start()
        {
            timer = FireRate;
            player = GetComponent<Rigidbody2D>();
            topOfTheScreen = GameObject.Find("Top_oftheScreen");
            bottomOfTheScreen = GameObject.Find("Bottom_oftheScreen");
            max_hp = hp;
            Handling = GameObject.Find("Handler");
            Hand_script = Handling.gameObject.GetComponent<Handler>();
        }
        void Update()
        {
            DoNotMove = Hand_script.PlayerDoNotMove; // получаем булеву переменную из класса Handler. Если она тру - мы не двигаемся.
            if (DoNotMove == false)
            {
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

    checkDead();
            }
            else // если DoNotMove
            {
                CheckDeadDoNotMove();
            }
        } // <-- тут закончился Update()

        void pushLeft()
        {
            player.AddForce(new Vector2(0.5f, 0), ForceMode2D.Impulse);
        }

        // попадание в нас
        void OnTriggerEnter2D(Collider2D Playercollide)
        {
            var name = Playercollide.gameObject.tag;
            if (name == "Enemy_laser") // если попал лазер - минус хп
            {
                GameObject Enemy_laser = Playercollide.gameObject;
                hp -= 1f;
                Destroy(Playercollide.gameObject);
            }
            else if(name == "enemy")
            {
                hp -= 1f;
                transform.position = new Vector3(transform.position.x, -3.35f);
            }
        }

        void checkDead() // при смерти вызываем экран смерти (можно сделать на нём статистику, но необязательно)
        {
            if (hp <= 0)
            {  
                SceneManager.LoadScene("DeadScreen");
            }
        }

        void OnGUI() // выводим хп на экран
        {
        GUI.skin = skin;
            int percent = (int)(100 * hp / max_hp);
            string str_percent = percent.ToString() + '%';
            GUI.color = Color.red;
            GUI.Box(new Rect(Screen.width/1000f, 0.9f*Screen.height, percent, 20f), str_percent);
        }

        void CheckDeadDoNotMove()
        {
            if ((hp <= 0) || (transform.position.y < -5f))
            {
                var next = Hand_script.NextScene;
                SceneManager.LoadScene(next);
            }
        }
    };
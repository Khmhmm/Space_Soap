using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Playersuper : MonoBehaviour
{
    public GUISkin skin;
    public float speed=3f;
    private Rigidbody2D player_rigidbody;
    void Start()
    {
        player_rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        /*
        if (Input.GetKey(KeyCode.W))
        {
            this.
        }
        */
        transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y);
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("Main_Menu");
        if (transform.position.x > 9f)
            transform.position = new Vector3(-9.75f, transform.position.y);
    }

    void OnGUI()
    {
        GUI.skin = skin;
        GUI.Label(new Rect(Screen.width * 0.2f, Screen.height * 0.5f, 500f, 25f), "Спасибо за игру. Нажмите Esc, чтобы выйти в главное меню.");
    }
}

using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Print : MonoBehaviour
{
    public Text textUI;
    public string text;
    private int next;
    void Start()
    {
        StartCoroutine("ShowText", text);
#pragma warning disable CS0618 // Тип или член устарел
        next = Application.loadedLevel + 1; // он ругается, но в SceneManager нет нужного метода
#pragma warning restore CS0618 // Тип или член устарел
    }

    void Update()
    {
        if (Input.GetKey("space"))
        {
            SceneManager.LoadScene(next);
        }
    }

    IEnumerator ShowText(string text)
    {
        int i = 0;
        while (i <= text.Length)
        {
            textUI.text = text.Substring(0, i);
            i++;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Communicator_lvl3 : Communicator_lvl2
{
    void Update()
    {
        currentNode = index;        
    }

    void OnGUI()
    {
        GUI.skin = skin;
        GUI.backgroundColor = Color.black;
        GUI.Label(new Rect(0.05f * Screen.width, 0.05f * Screen.height, 300f, 25f), "Пользователь: " + player_name);

        if (index == -1)
        {
            // выводим справа снизу кнопку
            if (GUI.Button(new Rect(0.8f * Screen.width, 0.9f * Screen.height, 100f, 25f), "Продолжить"))
            {
                SceneManager.LoadScene("FinalLvl");
            }
        }

        int[] heights = new int[Nodes.Length];
        
        // вывод списка адресатов
        for (int k = 0; k < Nodes.Length; k++)
        { 
            heights[k] = k;
            if (GUI.Button(new Rect(0.0025f * Screen.width, 0.1f * heights[k] * Screen.height + 100f, Screen.width/2.1f, 25f), "От: " + Nodes[k].author))
            {
                index = heights[k];
            }
        }

        heights = null;

        if (GUI.Button(new Rect(0.05f * Screen.width, 0.9f * Screen.height, 165f, 25f), "Закрыть окно сообщений")) // убирает все лейблы и боксы
        {
            index = -1;
        }

        if (currentNode >= 0) // выводит 2 лейбла и бокс с текстом сообщений
        {
            GUI.Label(new Rect(0.5f * Screen.width, Screen.height - (float)Screen.height * 0.9f, Screen.width / 2f, 100f), "От: " + Nodes[currentNode].author);
            GUI.Box(new Rect(0.5f * Screen.width, Screen.height - (float)Screen.height * 0.85f, Screen.width, 400f), "");
            GUI.Label(new Rect(0.5f * Screen.width, Screen.height - (float)Screen.height * 0.85f, Screen.width / 2f, 400f), Nodes[currentNode].text);
            GUI.color = Color.cyan;
            GUI.Box(new Rect(0.5f * Screen.width, Screen.height - (float)Screen.height * 0.35f, Screen.width / 2f, 200f), "");
            GUI.Label(new Rect(0.5f * Screen.width, Screen.height - (float)Screen.height * 0.35f, Screen.width / 2f, 200f), "Цитата: `` " + Nodes[currentNode].answer + " ``");
        }
    }
};

[System.Serializable]
public class CommunicatorNode_lvl3
{
    public string author, text, answer;
};


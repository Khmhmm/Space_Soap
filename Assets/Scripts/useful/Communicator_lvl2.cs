using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Communicator_lvl2 : MonoBehaviour
{
    public GUISkin skin;
    public CommunicatorNode[] Nodes;
    public int currentNode, index ,link=0;
    protected string player_name;
    protected string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
    // Start is called before the first frame update
    protected void Start()
    {
        if (!Directory.Exists(folder + "\\SpaceSoap\\lvl2_answers\\"))
        {
            Directory.CreateDirectory(folder + "\\SpaceSoap\\lvl2_answers\\");
        }
        index = -1;
        using (FileStream fstream = new FileStream(folder + "\\SpaceSoap\\player_name.txt", FileMode.Open)) // прочитываем имя игрока из файла
        {
            byte[] array = new byte[fstream.Length];
            fstream.Read(array, 0, array.Length);
            player_name = System.Text.Encoding.Default.GetString(array);
        }
    }

    void Update()
    {
            currentNode = index;
        if (link >= 0)
        {
         using (FileStream fstream = new FileStream(folder + "\\SpaceSoap\\lvl2_answers\\" + link.ToString() + ".txt", FileMode.Create))
         {
            byte[] array = System.Text.Encoding.Default.GetBytes(Nodes[link].answer);
            fstream.Write(array, 0, array.Length);
         }
        }
    }

    void OnGUI()
    {
        GUI.skin = skin;
        GUI.Label(new Rect(0.05f * Screen.width,0.05f * Screen.height,300f,25f), "Пользователь: "+player_name);
        if (GUI.Button(new Rect(0.8f*Screen.width,0.9f*Screen.height,100f,25f), "Продолжить")) // выводим справа снизу кнопку
        {
            SceneManager.LoadScene("Level2");
        }
        
        int[] heights = new int[Nodes.Length]; 
        for (int k=0; k<Nodes.Length;k++)
        { 
            // вывод списка адресатов
            heights[k] = k;
            if (GUI.Button(new Rect(0.025f * Screen.width, 0.1f * heights[k] * Screen.height + 100f, 290f, 25f), "От: " + Nodes[k].author)){
                index = heights[k];
            }
        }

        heights = null;


        if (GUI.Button(new Rect(0.05f * Screen.width, 0.9f * Screen.height, 165f, 25f), "Закрыть окно сообщений")) // убирает все лейблы и боксы
        {
            index = -1;
        }

            if (index >= 0) // выводит 2 лейбла и бокс с текстом сообщений
        {
            GUI.Label(new Rect(0.5f * Screen.width, Screen.height - (float)Screen.height * 0.9f, Screen.width/2f, 100f), "От: " + Nodes[currentNode].author);
            GUI.Box(new Rect(0.5f * Screen.width, Screen.height - (float)Screen.height * 0.85f, Screen.width, 400f), "");
            GUI.Label(new Rect(0.5f * Screen.width, Screen.height - (float)Screen.height * 0.85f, Screen.width/2f, 400f), Nodes[currentNode].text);
            if (GUI.Button(new Rect(0.8f * Screen.width, 0.75f * Screen.height, 100f, 25f), "Ответить")){
                 link = currentNode;
                index = -2;
                }
            }   else if (index <= -2) // если нажали кнопку ответа - создали на экране лейбл и поле для входящего потока текста, который записываем в файл
            {
            GUI.Label(new Rect(0.5f * Screen.width, Screen.height - (float)Screen.height * 0.9f, Screen.width, 100f), "Отправить: " + Nodes[link].author);
            Nodes[link].answer = GUI.TextField(new Rect(0.5f * Screen.width, Screen.height - (float)Screen.height * 0.85f, 300, 400f), Nodes[link].answer);
            }   else
                {
                    return;
                }
    }

};

[System.Serializable]
public class CommunicatorNode
{
    public string author, text, answer;
}


using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;

public class InputName : MonoBehaviour
{
    public GUISkin skin;
    private string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
    private string player_name;
    void OnGUI()
    {
        GUI.skin = skin;
        GUI.Label(new Rect(Screen.width * 0.4f, Screen.height* 0.2f , 500f, 100f), "Введите ваше имя:");
        player_name = GUI.TextField(new Rect(Screen.width * 0.4f + 170f, Screen.height *0.2f, 100f, 25f), player_name);

    }

    void Start()
    {
        if(!Directory.Exists(folder + "\\SpaceSoap"))
        {
            Directory.CreateDirectory(folder + "\\SpaceSoap");
            try
            {
                FileStream file = new FileStream(folder + "\\SpaceSoap\\player_name.txt", FileMode.CreateNew, FileAccess.Read);
            }
            catch (IOException e)
            {
                Debug.LogError(e);
            }
        }
        else
        {
            using(FileStream fs = new FileStream(folder + "\\SpaceSoap\\player_name.txt", FileMode.Open, FileAccess.Read))
            {
                byte[] arr = new byte[128];
                fs.Read(arr,0,(int)fs.Length);
                player_name = Encoding.UTF8.GetString(arr);
            }
        }
    }

    void LateUpdate()
    {
        if (player_name != null)
        {
            using (FileStream fstream = new FileStream(folder + "\\SpaceSoap\\player_name.txt", FileMode.Create))
            {
                byte[] array = Encoding.Default.GetBytes(player_name);
                fstream.Write(array, 0, array.Length);
            }
        }
    }

}

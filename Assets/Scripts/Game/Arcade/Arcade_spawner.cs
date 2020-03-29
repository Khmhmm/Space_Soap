using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arcade_spawner : MonoBehaviour
{
    public List<GameObject> Spawn;
    private GameObject[] enemies;
    private GameObject player;
    private string best, last;    
    private string[] results = new string[2];
    public float delay=1f;
    public int spawnIndex=0;
    // Start is called before the first frame update
    void Start()
    {
        var folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("SpawnIt", delay, delay + player.GetComponent<player_arcade>().level);
        using (FileStream fstream = new FileStream(folder + "\\SpaceSoap\\arcade\\last.txt", FileMode.Open))
        {
            byte[] array = new byte[fstream.Length];
            fstream.Read(array, 0, array.Length);
            last = System.Text.Encoding.Default.GetString(array);
        }

        using (FileStream fstream = new FileStream(folder + "\\SpaceSoap\\arcade\\best.txt", FileMode.Open))
        {
            byte[] array = new byte[fstream.Length];
            fstream.Read(array, 0, array.Length);
            best = System.Text.Encoding.Default.GetString(array);
        }
    }

   /* int GenerateRandom()
    {
        System.Random rand = new System.Random();
        return (int)rand.Next() % Spawn.Count;
    } */

    void SpawnIt()
    {
       
            var tempIndex = spawnIndex;
            spawnIndex = player.GetComponent<player_arcade>().level % 10;
            Instantiate(Spawn[spawnIndex - 1], new Vector3(UnityEngine.Random.Range(-3.5f,3.5f),1f), Quaternion.Euler(0f, 0f, 180f));
            if (tempIndex != spawnIndex)
            {
                delay += 3f * player.GetComponent<player_arcade>().level / (float)spawnIndex;
               
            }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width * 0.75f, Screen.height * 0.05f, Screen.width/2f, 50f), "Лучший результат:");
        GUI.Label(new Rect(Screen.width * 0.75f, Screen.height * 0.1f, Screen.width / 2f, 50f), best);
        GUI.Label(new Rect(Screen.width * 0.75f, Screen.height * 0.15f, Screen.width / 2f, 50f), "Прошлый результат:");
        GUI.Label(new Rect(Screen.width * 0.75f, Screen.height * 0.2f, Screen.width / 2f, 50f), last);
    }
}

using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GUISkin skin;
    private string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
    public DialogueBox[] dialogues;
    private int current_dialogue = 0;
    private int index = 0;
    public float interval; // интервал между диалогами
    private string player_name;
    private bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        using (FileStream fstream = new FileStream(folder + "\\SpaceSoap\\player_name.txt", FileMode.Open))
        {
            byte[] array = new byte[fstream.Length];
            fstream.Read(array, 0, array.Length);
            player_name = System.Text.Encoding.Default.GetString(array);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            StartCoroutine("Plus_i");
            start = true;
        }
        if (dialogues[current_dialogue].Dialogue_end)
        {
            StartCoroutine("Destroy_It");
            dialogues[current_dialogue].Dialogue_end = false;
        }
        if (index != dialogues.Length)
            current_dialogue = index;
        if (dialogues[current_dialogue].speaker == "$player")
            dialogues[current_dialogue].speaker = player_name;
    }

    void OnGUI()
    {
        GUI.skin = skin;

        GUI.Label(new Rect(10f, Screen.height - (float)Screen.height * 0.2f, Screen.width, 100f), dialogues[current_dialogue].speaker);
        GUI.Label(new Rect(10f, Screen.height - (float)Screen.height * 0.15f, Screen.width, 100f), dialogues[current_dialogue].text);
    }

    IEnumerator Destroy_It()
    {
        yield return new WaitForSeconds(interval);
        start = false;

        Destroy(gameObject);
    }

    IEnumerator Plus_i()
    {
        yield return new WaitForSeconds(interval);
        index += 1;
        start = false;
    }
};




[System.Serializable]
public class DialogueBox
{
    public string speaker;
    public string text;
    public bool Dialogue_end = false;
};

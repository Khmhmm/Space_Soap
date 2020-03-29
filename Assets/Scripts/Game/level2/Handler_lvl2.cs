using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Handler_lvl2 : MonoBehaviour
{
    public GameObject player, spawner, dialogue1, dialogue2, dialogue3, spawner2, spawner3,dialogue4, dialogue5;
    private int action = 0;
    private GameObject check_enemies, checker_spawners, checker_dialogues, checker_boss;
    private int i = -1,this_stage;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        check_enemies = GameObject.FindWithTag("enemy");
        checker_spawners = GameObject.FindWithTag("Respawn");
        checker_dialogues = GameObject.FindWithTag("dialogue");
        checker_boss = GameObject.FindWithTag("Boss");
        if ((checker_dialogues == null) && (checker_spawners == null) && (check_enemies == null) && (checker_boss == null) && (action == 0))
        {
            i++;
            this_stage = i;
            Invoke("MakeStage", 1f);
            action = 1;
        }
    }

    void MakeStage()
    {
        switch (this_stage)
        {
            case 0:
                Instantiate(dialogue1, new Vector3(0f, -3f, 0f), transform.rotation);
                break;
            case 1:
                Instantiate(spawner, new Vector3(0f,0f,0f),transform.rotation);
                Instantiate(dialogue2, new Vector3(0f, -3f, 0f), transform.rotation);
                break;
            case 2:
                Instantiate(dialogue3, new Vector3(0f, -3f, 0f), transform.rotation);
                Instantiate(spawner2, new Vector3(0f, 0f, 0f), transform.rotation);
                break;
            case 3:
                Instantiate(spawner3, new Vector3(0f, 0f, 0f), transform.rotation);
                Instantiate(dialogue4, new Vector3(0f, -3f, 0f), transform.rotation);
                break;
            case 4:
                Instantiate(dialogue5, new Vector3(0f,-3f,0f), transform.rotation);
                StartCoroutine("NextLevel");
                break;
            default:
                Debug.Log("Error in enum Stage");
                break;
        }
        action = 0;
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(6.5f);
        SceneManager.LoadScene("After_lvl2");
    }
}

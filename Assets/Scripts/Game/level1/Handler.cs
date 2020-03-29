using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



    public class Handler : MonoBehaviour
    {
        public GameObject dialogue1, dialogue2, dialogue3, boss_spawner, destroyer, player;
        [SerializeField]private int action = 0;
        private GameObject check_enemies, checker_spawners, checker_dialogues, checker_boss;
        private int i = 0;
        public bool PlayerDoNotMove = false;
        public string NextScene = "After_lvl1";
    /* Stage
через enum не пошло чета
        none = 0,
        createDialogue2 spawnBoss, = 1
        createdialogue3  goDead, = 2
*/
        private int this_stage = 0;
        // Start is called before the first frame update
        void Start()
        {
        Application.targetFrameRate = 70;
            Instantiate(dialogue1, new Vector3(0f, -3f, 0f), transform.rotation);
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

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main_Menu");
        }
    }

    void MakeStage()
        {
            switch (this_stage)
            {
                case 0:
                    i++;
                    break;
                case 1:
                    Instantiate(dialogue2, new Vector3(0f, -3f, 0f), transform.rotation);
                    Instantiate(boss_spawner, new Vector3(0f, 6f, 0f), transform.rotation);
                    break;
                case 2:
                     Instantiate(dialogue3, new Vector3(0f, -3f, 0f), transform.rotation);
                     PlayerDoNotMove = true;
                     Instantiate(destroyer, new Vector3(player.transform.position.x, 8.5f, 0f), Quaternion.Euler(0f, 0f, 180f));
                    break;
                default:
                    Debug.Log("Error in enum Stage");
                    break;
            }
            action = 0;
        }
    }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goto_1stLevel : MonoBehaviour
{
    public void Before_Firstlvl()
    {
        SceneManager.LoadScene("Before_Level1");
    }
}

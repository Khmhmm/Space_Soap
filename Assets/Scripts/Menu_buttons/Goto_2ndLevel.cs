using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goto_2ndLevel : MonoBehaviour
{
    public void Goto_second()
    {
        SceneManager.LoadScene("Communicator_lvl2");
    }
}

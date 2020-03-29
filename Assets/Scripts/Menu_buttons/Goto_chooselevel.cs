using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Goto_chooselevel : MonoBehaviour
{
   public void Next()
    {
        SceneManager.LoadScene("Choose_Level");
    }
}

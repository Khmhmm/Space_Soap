using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StartCreditsHandler : MonoBehaviour
{
    public bool goNext = false;

    private void Start()
    {
        if (GetComponent<VideoPlayer>().clip == null)
        {
            goNext = true;
        }
        else
        {
            GetComponent<VideoPlayer>().Play();
            StartCoroutine("SetNext");
        }
    }

    private void LateUpdate()
    {
        //skip
        if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Space))
        {
            goNext = true;
        }

        if (goNext)
        {
            SceneManager.LoadScene("Main_Menu");
        }
    }

    IEnumerator SetNext()
    {
        yield return new WaitForSeconds((float)GetComponent<VideoPlayer>().clip.length);
        goNext = true;
    }
}

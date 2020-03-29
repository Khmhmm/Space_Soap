using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public Camera Cam;
    // Start is called before the first frame update
    void Start()
    {
        /* Cam.orthographicSize = (4.062933f * Screen.height / Screen.width)/200f; */ // 4.062933f - ширина фона
        Cam.orthographicSize = 5.02f;
    }

    // Update is called once per frame
    void Update()
    {
    }
}

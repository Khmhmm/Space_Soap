using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LaserScript : MonoBehaviour
{
    public float speed;
    private Rigidbody2D lasersnaryad;
    public float damage=2.45f;
    // Start is called before the first frame update
    void Start()
    {
        lasersnaryad = GetComponent<Rigidbody2D>();
        lasersnaryad.isKinematic = true;
        StartCoroutine("Destroy");

        
    }

    // Update is called once per frame
    void Update()
    {
        lasersnaryad.MovePosition(lasersnaryad.position + Vector2.up*speed*Time.deltaTime*5f); // двигаем
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}

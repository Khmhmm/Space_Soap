using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyLaser : MonoBehaviour
{
    private Rigidbody2D lasersnaryad;
    public float speed = 0.3f;
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
        //lasersnaryad.MovePosition(lasersnaryad.position - Vector2.up * speed * Time.deltaTime * 3f); // двигаем
        gameObject.transform.Translate(new Vector3(0f,-speed,0f), Space.Self);
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}

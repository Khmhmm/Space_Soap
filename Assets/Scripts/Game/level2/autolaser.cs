using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autolaser : MonoBehaviour
{
    public float speed, damage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DestroyIt");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0f, -speed * 0.1f), Space.Self);
    }

    IEnumerator DestroyIt()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}

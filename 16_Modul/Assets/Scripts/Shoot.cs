using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{   
    public float speed = 15;
    
    void Update()
    {
        transform.Translate(Vector3.right * speed *Time.deltaTime);
        Destroy(gameObject, 2f);
    }
}

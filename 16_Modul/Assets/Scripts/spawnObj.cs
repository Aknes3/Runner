using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObj : MonoBehaviour
{   
    public GameObject[] hous;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void spawn()
    {
        Instantiate(hous[Random.Range(0,hous.Length)],transform.position,Quaternion.identity);
    }
}

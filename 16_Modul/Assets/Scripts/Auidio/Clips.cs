using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clips : MonoBehaviour
{   
    private AudioSource _audioCar;
    public AudioClip [] soundToplay;

    
    void Start()
    {
        _audioCar = GetComponent<AudioSource>();
    }
    
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("soundWall"))
        {            
            _audioCar.PlayOneShot(soundToplay[Random.Range(0,soundToplay.Length)]);
        }
    }
}

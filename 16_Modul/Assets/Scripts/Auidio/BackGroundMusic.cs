using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{   
    private AudioSource _audioBackGround;
    public AudioClip [] musicB;
    
    void Start()
    {
        _audioBackGround = GetComponent<AudioSource>();
    }

    
    void Update()
    {   
        if(!_audioBackGround.isPlaying)
        {
            playRandomMusic();
        }
        
    }
    void playRandomMusic() {
        _audioBackGround.clip =  musicB[Random.Range(0,musicB.Length)];
        _audioBackGround.Play();
     }
}

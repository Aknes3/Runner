using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopParticleForBonus : MonoBehaviour
{   
    public ParticleSystem cubeBonus;
    void OnTriggerEnter(Collider col)
    {   
       
        if(col.CompareTag("Player"))
        {              
            gameObject.SetActive(false);          
        }
    }
}

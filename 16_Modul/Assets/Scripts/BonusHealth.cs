using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusHealth : MonoBehaviour
{  
    public Text coins;
    public int coinScore;
    public ParticleSystem bonusEffect ;

    public GameMaster GM; 
    public int bonusNumber;
    public FireCube fireCube;
    
    
    void Start()
    {
        bonusNumber = 0;
        coinScore = 0;
    }

    
    void Update()
    {   
        coins.text = "Coins: " + coinScore.ToString();
        if(Input.GetKeyDown(KeyCode.W) && bonusNumber !=0 && GM.CanPlay)
        {   
            fireCube.SpwanFire();
            bonusNumber --;
            bonusEffect.Stop();
        }
        if(!GM.CanPlay)
        {
           bonusEffect.Stop();
           bonusNumber = 0; 
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Bonus") && bonusNumber == 0)
        {   
            
            bonusNumber ++;
            bonusEffect.Play();
        }

        if(col.CompareTag("Coins") )
        {   
           coinScore ++;             
        }
    }
    
}

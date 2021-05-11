using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlocks : MonoBehaviour
{   
    
    Vector3 moveVec;
    GameMaster GM;
    public GameObject [] coins;
    public AnimationCurve chanceToSpawn; // кривая для шанса появление платформы от времени бега
    public int chanceCoin; // вероятность появление монеток
    
    void Start()
    {   
        GM = FindObjectOfType<GameMaster>();
        moveVec = new Vector3(-1,0,0);
        //цикл на появление монеток на платформе с рандомным шансом
        foreach (var Go in coins)
        {   
            if(chanceCoin <= Random.Range(0,100))
            Go.SetActive(false);
        }
    }

    
    void Update()
    {   
        if(GM.CanPlay == true)
        {               
            transform.Translate(moveVec * Time.deltaTime* GM.platformSpeed );
        }        
    }
}

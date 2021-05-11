using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{ 
    
    public RoadBlocks [] Roads;    
    public Transform player;

    List<RoadBlocks> CurrentPlatform = new List<RoadBlocks>();
    float StartblockXpos = 0;     
    float blockLength = 0;
    int blockCount = 3;    
    float timeToSpawnRoad = 0;
    
    void Start()
    {  
        
        Control.instance.DeathPlayer += StartGame;      // подиска на событие
        StartblockXpos = player.position.x + 25;
        blockLength = 72;
        StartGame(); 
               
    }
    //Запуск-перезапуск игры.
    public void StartGame()
    {   
             
        foreach(var Go in CurrentPlatform)
        {
            Destroy(Go.gameObject);            
        }
        CurrentPlatform.Clear();
        //Control.instance.ResetPosition();
        for(int i = 0 ; i<blockCount; i++)
        {
            SpawnBlocks();
        }
    
    }

    // Update is called once per frame
    void LateUpdate()
    {   
        CheckForSpawn();
        timeToSpawnRoad += Time.deltaTime;
              
    }
    void SpawnBlocks()
    {
        RoadBlocks block = Instantiate(GetRandomRoad(),transform);
        Vector3 BlockPos;
        if(CurrentPlatform.Count>0)
        {
            BlockPos = CurrentPlatform[CurrentPlatform.Count-1].transform.position + new Vector3(blockLength,0,0);
        }
        else
        {
            BlockPos = new Vector3(StartblockXpos,0 , 0);
        }
        block.transform.position = BlockPos;
        CurrentPlatform.Add(block);
    }
    //Дабвление платформ
    void CheckForSpawn()
    {           
        if(CurrentPlatform[0].transform.position.x  < -60)
        {  
            for(int i = 0 ; i<2; i++)
            {
                 SpawnBlocks();
            }             
            //SpawnBlocks();
            DestroyBlocks();
        }
    }
    void DestroyBlocks()
    {
        Destroy(CurrentPlatform[0].gameObject);
        CurrentPlatform.RemoveAt(0);
    }
    
    private RoadBlocks GetRandomRoad()
    {
        List<float> chance = new List<float>();
        for(int i =0; i< Roads.Length; i++)
        {
            chance.Add(Roads[i].chanceToSpawn.Evaluate(timeToSpawnRoad));
        }

        float value = Random.Range(0,chance.Sum());
        float sum = 0;
        for(int i =0; i< chance.Count; i++)
        {
            sum += chance[i];
            if(value<sum)
            {   
                
                return Roads[i];
                
            }
        }
        return Roads[Roads.Length-1];
    }
}

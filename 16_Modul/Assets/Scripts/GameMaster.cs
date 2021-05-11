using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{      
    public Text text;
    float score ;
    public static GameMaster instance;
    public Spawner sp;
    
   
   public bool CanPlay = true;
   public float platformSpeed = 5;    

    private void Awake()
    {   
        
        if(GameMaster.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        GameMaster.instance = this;
        
    }
    void Start()
    {
        platformSpeed = Mathf.Clamp(platformSpeed, 5 , 15);
        score = 0;
    }
    
    private void OnDestroy()
    {
        GameMaster.instance = null;
    }
    public void StartLevel()
   {
       sp.StartGame();
   }
    
   private void Update()
   {   
       if(CanPlay)
       {
           score += Time.deltaTime * 3;       
       } 
       text.text = "Score: " + ((int)score).ToString();
       
       platformSpeed += .05f* Time.deltaTime;
        
        if(!CanPlay)
        {
            platformSpeed = 6;
            score = 0f;
        }         
   }
   
}

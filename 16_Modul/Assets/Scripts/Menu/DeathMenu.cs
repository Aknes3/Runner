using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathMenu : MonoBehaviour
{   
    public GameObject deathMenu;
    public GameMaster GameMaster; 
    void Start()
    {
        GameMaster = FindObjectOfType<GameMaster>();
    }
    void Update()
    {
        if(!GameMaster.CanPlay)
        {
            deathMenu.SetActive(true);
        }
    }
    

}

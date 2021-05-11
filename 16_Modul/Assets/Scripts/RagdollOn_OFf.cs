using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollOn_OFf : MonoBehaviour
{   
    
    private Animator animator;
    
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        setRigidbodyState(false);
        setColliderState(true);        
    }

    
    void Update()
    {
        if(GameMaster.instance.CanPlay == false)
        {                      
            StartCoroutine(SetRagdollState());            
        }        
    }

    public void SetColAndRigState(bool StateBoth)
    {   
        
       animator.enabled = !StateBoth;
        
       GetComponent<BoxCollider>().enabled = !StateBoth;
       setRigidbodyState(StateBoth);
       setColliderState(!StateBoth);
       
        
    }

    //включаем, выключаем гравитацию на персонаже
    void setRigidbodyState (bool state)
    {
        Rigidbody [] body = GetComponentsInChildren<Rigidbody>();
        
        foreach (Rigidbody rigidbody in body)
        {   
            if(rigidbody.gameObject != this.gameObject)
            {
                rigidbody.useGravity = state;
            }            
        }
        
    }

    //вкл выкл триггера на персонаже
    void setColliderState (bool state)
    {          
        Collider [] collider = GetComponentsInChildren<Collider>();
        foreach (Collider col in collider)
        {   
            if(col.gameObject != this.gameObject)
            {
                col.isTrigger = state;                
            }            
        }        
    }

    IEnumerator SetRagdollState()
    {
        SetColAndRigState(true);
        yield return null;          
    }
}

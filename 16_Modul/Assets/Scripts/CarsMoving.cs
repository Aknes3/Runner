using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsMoving : MonoBehaviour
{   
    
    public ParticleSystem carUpParticle;
    GameMaster GM;
     float _carSpeed = 30f;
    // Update is called once per frame
    void Update()
    {   
        if(!GameMaster.instance.CanPlay)
        {
            StartCoroutine(Crush());
        }
        transform.Translate(Vector3.forward * (_carSpeed+Time.deltaTime) *Time.deltaTime);
        Destroy(gameObject, 10f);
    }
    void Start()
    {
        _carSpeed = Mathf.Clamp(_carSpeed, 30f , 50f);
    }
    void OnTriggerEnter (Collider col)
    {   

        if(col.CompareTag("bullet"))
        {   
            _carSpeed = 8;            
            carUpParticle.Play();
            
            StartCoroutine(CarLevitate());
        }        
    }
    IEnumerator Crush()
    {
        _carSpeed = 0f;
        carUpParticle.Stop();
        Destroy(gameObject, 2f);
        yield return null;
    }
    IEnumerator CarLevitate()
    {   
        float timeOn = 0;
        float timeOff = 1f;
        while(timeOn<timeOff)
        {   
            timeOn +=Time.deltaTime;
            transform.position =  new Vector3(transform.position.x , Mathf.Lerp(transform.position.y, transform.position.y+0.5f,20f * Time.deltaTime ), transform.position.z);
            yield return null;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinParticle : MonoBehaviour
{   
    private AudioSource audioCoin;
    float speed = 100f;
    public ParticleSystem _particle;
    
    MeshRenderer coinMesh;
    // Start is called before the first frame update
    void Start()
    {   
        audioCoin = GetComponent<AudioSource>();
        coinMesh = GetComponent<MeshRenderer>();
        _particle.Stop();
    }
    void Update()
    {
        transform.Rotate(Vector3.forward*speed * Time.deltaTime);  
        
    }
   
    void OnTriggerEnter(Collider col)
    {   
       
        if(col.CompareTag("Player"))        {   
                        
            _particle.Play();
            StartCoroutine(Coin());
            audioCoin.PlayOneShot(audioCoin.clip);
        }
    }
    IEnumerator Coin()
    {
        coinMesh.enabled = false;
        yield return new WaitForSeconds(.6f);
        
        Destroy(transform.parent.gameObject);        
    }
}

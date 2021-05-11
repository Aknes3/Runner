using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCars : MonoBehaviour
{   
    public Transform [] spawnSpace;
    public GameObject [] cars;
    float timeToCarSpawn = 3f;
    // Start is called before the first frame update
    void Start()
    {   
        timeToCarSpawn = Mathf.Clamp(timeToCarSpawn, 1f , 3f);
        StartCoroutine(SpawnerCars());
    }

    // Update is called once per frame
    void Update()
    {
       timeToCarSpawn -= Time.deltaTime;
       if(timeToCarSpawn <= 1.2f) timeToCarSpawn = 1.2f; 
    }
    IEnumerator SpawnerCars ()
    {
        while(GameMaster.instance.CanPlay)
        {
            yield return new WaitForSeconds(3f);
            Instantiate( cars[Random.Range(0,cars.Length)], spawnSpace[Random.Range(0, spawnSpace.Length)].position, Quaternion.Euler(0,-90,0));
        }
    }
}

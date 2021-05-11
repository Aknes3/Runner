using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCube : MonoBehaviour
{   
    public GameObject firePoint;
    public List<GameObject> fireEffect = new List<GameObject>();
    
    // появление снаряда
    public void SpwanFire()
    {   
        GameObject _partycle;
        if(firePoint != null)
        {
            _partycle = Instantiate(fireEffect[0], firePoint.transform.position,Quaternion.identity);
        }
        else 
        Debug.Log("нет точки спавна");
    }
}

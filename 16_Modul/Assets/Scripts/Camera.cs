using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{   
    public Transform target;
    Vector3 _distance , _moveVec;
    // Start is called before the first frame update
    void Start()
    {
        _distance = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        _moveVec = target.position + _distance;
        _moveVec.y = _distance.y;
        _moveVec.z = 0f; 
        transform.position = _moveVec;
    }
}

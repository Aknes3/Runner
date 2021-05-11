using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{   
    int _blockCount = 5;
    public GameObject [] commonPlatforms;
    public GameObject [] hardPlatforms;
    public Transform boxOfPlatform;
    private Transform lastPlatform = null;
    List<GameObject> CurrentBlocks = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<_blockCount;i++)
        createCommonPlatform();        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(sp());
    }
    void spawnBlocks()
    {
        for(int i = 0; i<_blockCount;i++)
        {
            createCommonPlatform();
        }
    }
    private void createCommonPlatform()
    {
        Vector3 pos = (lastPlatform == null) ?  boxOfPlatform.position : lastPlatform.GetComponent<PosPlatfom>().endPos.position;
        
        int _index = Random.Range(0, commonPlatforms.Length);
        GameObject _platform = Instantiate(commonPlatforms[_index],pos , Quaternion.identity, boxOfPlatform );
        lastPlatform = _platform.transform;
        CurrentBlocks.Add(_platform);
    }
    IEnumerator sp()
    {
        yield return new WaitForSeconds(10f);
        spawnBlocks();
    }
}

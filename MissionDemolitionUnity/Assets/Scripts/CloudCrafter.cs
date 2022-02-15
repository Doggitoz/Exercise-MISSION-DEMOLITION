/****
 * Created by: Coleton Wheeler
 * Created on: 2/14/22
 * 
 * Edited by: N/A
 * Last Edited: N/A
 * 
 * Description: Generate multiple clouds
 ****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{

    [SerializeField] private int _numberOfClouds = 40;
    [SerializeField] private GameObject _cloudPrefab;
    [SerializeField] private Vector3 _cloudPositionMin = new Vector3(-50, -5, 10);
    [SerializeField] private Vector3 _cloudPositionMax = new Vector3(150, 100, 10);
    [SerializeField] private float _cloudScaleMin = 1;
    [SerializeField] private float _cloudScaleMax = 3;
    [SerializeField] private float _cloudSpeedMultiplier = 0.5f;

    private GameObject[] _cloudInstances;

    private void Awake()
    {
        _cloudInstances = new GameObject[_numberOfClouds];
        GameObject anchor = GameObject.Find("CloudAnchor");

        GameObject cloud;

        for (int i = 0; i < _numberOfClouds; i++)
        {
            cloud = Instantiate<GameObject>(_cloudPrefab);
            //POsition cloud
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(_cloudPositionMin.x, _cloudPositionMax.x);
            cPos.y = Random.Range(_cloudPositionMin.y, _cloudPositionMax.y);

            //Scale clouds
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(_cloudScaleMin, _cloudScaleMax, scaleU);

            cPos.y = Mathf.Lerp(_cloudPositionMin.y, cPos.y, scaleU);
            cPos.z = 100 - 90 * scaleU;
            
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;

            cloud.transform.SetParent(anchor.transform);
            _cloudInstances[i] = cloud;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

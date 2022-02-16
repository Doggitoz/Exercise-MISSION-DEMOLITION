/****
 * Created by: Coleton Wheeler
 * Created on: 2/14/22
 * 
 * Edited by: N/A
 * Last Edited: N/A
 * 
 * Description: Generates random cloud
 ****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    [Header("Set in Inspector")]
    [SerializeField] private GameObject _cloudSphere;
    [SerializeField] private int _numberOfSpheresMin = 6;
    [SerializeField] private int _numberOfSpheresMax = 10;
    [SerializeField] private Vector2 _sphereScalerangeX = new Vector2(4, 8);
    [SerializeField] private Vector2 _sphereScalerangeY = new Vector2(3, 4);
    [SerializeField] private Vector2 _sphereScalerangeZ = new Vector2(2, 4);
    [SerializeField] private Vector3 _sphereOffsetScale = new Vector3(5, 2, 1);
    [SerializeField] float _scaleYmin = 2f;

    private List<GameObject> _spheres;


    // Start is called before the first frame update
    void Start()
    {
        createCloud();
    }

    private void createCloud()
    {
        _spheres = new List<GameObject>();

        int num = Random.Range(_numberOfSpheresMin, _numberOfSpheresMax);

        for (int i = 0; i < num; i++)
        {
            GameObject sp = Instantiate<GameObject>(_cloudSphere);
            _spheres.Add(sp);

            Transform spTrans = sp.transform;
            spTrans.SetParent(this.transform);

            //Randomly assign a position
            Vector3 offset = Random.insideUnitSphere;
            offset.x *= _sphereOffsetScale.x;
            offset.y *= _sphereOffsetScale.y;
            offset.z *= _sphereOffsetScale.z;
            spTrans.localPosition = offset;

            Vector3 scale = Vector3.one;
            scale.x = Random.Range(_sphereScalerangeX.x, _sphereScalerangeX.y);
            scale.y = Random.Range(_sphereScalerangeY.x, _sphereScalerangeY.y);
            scale.z = Random.Range(_sphereScalerangeZ.x, _sphereScalerangeZ.y);

            //Adjust y scale by x distance from core
            scale.y *= 1 - (Mathf.Abs(offset.x) / _sphereOffsetScale.x);
            scale.y = Mathf.Max(scale.y, _scaleYmin);
            spTrans.localScale = scale;

        }
    }

    // Update is called once per frame
    void Update()
    {
        //Restart();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
        }
    }

    private void Restart()
    {
        foreach (GameObject sp in _spheres)
        {
            Destroy(sp);
        }

        createCloud();
    }
}

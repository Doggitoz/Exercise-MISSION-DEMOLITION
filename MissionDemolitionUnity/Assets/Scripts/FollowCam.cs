/****
 * Created by: Coleton Wheeler
 * Created on: N/A
 * 
 * Edited by: Coleton Wheeler
 * Last Edited: 2/14/22
 * 
 * Description: Follows the projectile
 ****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;

    [Header("Set in Inspector")]
    [SerializeField] private float _easing = 0.05f;
    [SerializeField] private Vector2 _minXY = Vector2.zero;

    [Header("Set Dynamically")]
    [SerializeField] private float _camz;

    // Start is called before the first frame update

    private void Awake()
    {
        _camz = this.transform.position.z;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (POI == null) return;

        Vector3 destination = POI.transform.position;

        destination.x = Mathf.Max(_minXY.x, destination.x);
        destination.y = Mathf.Max(_minXY.y, destination.y);

        destination = Vector3.Lerp(transform.position, destination, _easing);
        destination.z = _camz;
        transform.position = destination;

        Camera.main.orthographicSize = destination.y + 10;
    }
}

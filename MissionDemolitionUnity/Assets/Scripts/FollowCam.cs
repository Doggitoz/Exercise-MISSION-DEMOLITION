using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;

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
        destination.z = _camz;
        transform.position = destination;
    }
}

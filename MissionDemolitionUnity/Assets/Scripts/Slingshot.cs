/****
 * Created by: Coleton Wheeler
 * Created on: 2/9/22
 * 
 * Last Edited: N/A
 * Last Edited by: N/A
 * 
 * Description: Slingshot script
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    
    [Header ("Set in Inspector")]
    [SerializeField] private GameObject _launchPoint;
    [Header ("Set Dynamically")]
    [SerializeField] private GameObject _prefabProjectile;
    private Vector3 _launchPos;
    private GameObject _projectile; //Instance of projectile
    private bool _aimingMode; //Is playing aiming slingshot
    private Rigidbody _projectileRB;
    private float _velocilyMultiplier = 9f;

    private void Awake()
    {
        Transform LaunchPointTrans = transform.Find("LaunchPoint");
        _launchPoint = LaunchPointTrans.gameObject;
        _launchPoint.SetActive(false);
        _launchPos = LaunchPointTrans.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_aimingMode) return;

        //get mouse pos from 2D coordinates
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 mouseDelta = mousePos3D - _launchPos;

        //limit mouseDelta to slingshot collider radisu
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if(mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        //Move projectile to new position
        Vector3 projectilePos = _launchPos + mouseDelta;
        _projectile.transform.position = projectilePos;

        Debug.Log(Input.GetMouseButtonUp(0));

        if(Input.GetMouseButtonUp(0))
        {
            //mouse button has been released
            _aimingMode = false;
            _projectileRB.isKinematic = false;
            _projectileRB.velocity = -mouseDelta * _velocilyMultiplier;
            FollowCam.POI = _projectile;
            _projectile = null; //empties reference to instance projectile
        }
    }

    private void OnMouseEnter()
    {
        _launchPoint.SetActive(true);
    }
    private void OnMouseExit()
    {
        //print("Slingshot: OnMouseExit");
        _launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        _aimingMode = true;
        _projectile = Instantiate(_prefabProjectile) as GameObject;
        _projectile.transform.position = _launchPos;
        _projectileRB = _projectile.GetComponent<Rigidbody>();
        _projectileRB.isKinematic = true;
        

    }
}

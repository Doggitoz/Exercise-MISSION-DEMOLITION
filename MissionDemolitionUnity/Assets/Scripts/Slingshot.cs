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

    static private Slingshot S;
    
    [Header ("Set in Inspector")]
    [SerializeField] private GameObject _launchPoint;
    [Header ("Set Dynamically")]
    [SerializeField] private GameObject _prefabProjectile;
    public Vector3 launchPos;
    private GameObject _projectile; //Instance of projectile
    private bool _aimingMode; //Is playing aiming slingshot
    private Rigidbody _projectileRB;
    private float _velocilyMultiplier = 9f;

    static public Vector3 Launch_Pos
    {
        get
        {
            if (S == null)
            {
                return Vector3.zero;
            }
            return S.launchPos;
        }
    }

    private void Awake()
    {
        S = this;
        Transform LaunchPointTrans = transform.Find("LaunchPoint");
        _launchPoint = LaunchPointTrans.gameObject;
        _launchPoint.SetActive(false);
        launchPos = LaunchPointTrans.position;
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
        Vector3 mouseDelta = mousePos3D - launchPos;

        //limit mouseDelta to slingshot collider radisu
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if(mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        //Move projectile to new position
        Vector3 projectilePos = launchPos + mouseDelta;
        _projectile.transform.position = projectilePos;

        if(Input.GetMouseButtonUp(0))
        {
            //mouse button has been released
            _aimingMode = false;
            _projectileRB.isKinematic = false;
            _projectileRB.velocity = -mouseDelta * _velocilyMultiplier;
            _projectile.GetComponent<SphereCollider>().isTrigger = false;
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
        _projectile.GetComponent<SphereCollider>().isTrigger = true;
        _projectile.transform.position = launchPos;
        _projectileRB = _projectile.GetComponent<Rigidbody>();
        _projectileRB.isKinematic = true;
        

    }
}

/****
 * Created by: Coleton Wheeler
 * Created on: 2/16/22
 * 
 * Edited by: N/A
 * Last Edited: N/A
 * 
 * Description: Put rigid body to sleep (1 frame)
 ****/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class RigidSleep : MonoBehaviour
{
    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.Sleep();

    }
}

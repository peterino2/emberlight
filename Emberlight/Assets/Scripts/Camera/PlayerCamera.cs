using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject FollowTarget;
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    private void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            FollowTarget.transform.position, 0.05f);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            FollowTarget.transform.rotation, Time.deltaTime*4.0f);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is an average camera follow method, for this project it doesn't need much improvement 
public class CameraFollow : MonoBehaviour
{
    private Transform target;
    //offset vector determines how far the camera will follow the player object
    [SerializeField] private Vector3 offset;
    public float chaseSpeed = 5;

    //We use the PlayerMovement script to find the player object but any other method would also work just fine.
    void Start()
    {
        if (!target)
        {
            target = GameObject.FindObjectOfType<PlayerMovement>().transform;
        }
    }
    //always update camera movement in late update
    private void LateUpdate()
    {
        TransformCamPos();
    }
    void TransformCamPos()
    {
        transform.position=Vector3.Lerp(transform.position,target.position+offset,chaseSpeed*Time.deltaTime);
    }
}
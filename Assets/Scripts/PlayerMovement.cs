using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator playerAnimator;
    Vector3 firstPos, endPos;
    [SerializeField] private float _xLimit, sidewaySpeed;
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        Borders();
        ForwardMovement();
    }
    void ForwardMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        playerAnimator.SetBool("run",true);

        if(Input.GetMouseButtonDown(0))
        {
            firstPos = Input.mousePosition;
        }
        else if(Input.GetMouseButton(0))
        {
            endPos = Input.mousePosition;
            float xDistance = endPos.x - firstPos.x;
            transform.Translate(xDistance/ sidewaySpeed * Time.deltaTime, 0, 0);
        }

        if(Input.GetMouseButtonUp(0))
        {
            firstPos = Vector3.zero;
            endPos = Vector3.zero;
        }
    }
    void Borders()
    {
        if(transform.position.x >= _xLimit)
        {
            transform.position = new Vector3(_xLimit, transform.position.y, transform.position.z); 
        }
        else if(transform.position.x <= -_xLimit)
        {
            transform.position = new Vector3(-_xLimit, transform.position.y, transform.position.z); 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D currentBallRb;
    [SerializeField] private SpringJoint2D currentBallSj;
    [SerializeField] private float detachDelay;

    private Camera mainCamera; //to convert to world space 
    private bool isDraggin;
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if(currentBallRb == null) return;
        
        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (isDraggin)
            {
                LaunchBall();
            }

            isDraggin = false;
            return;
        }

        isDraggin = true;
        currentBallRb.isKinematic = true;

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
        //even tho it's a vector 3 it will 0 out the Z coordinates and make it as a vector 2 value

        currentBallRb.position = worldPosition; 
        //when we only use this line ball goes crazy because it's position is also effected by the gravity and the spring joint
    }

    private void LaunchBall()
    {
        currentBallRb.isKinematic = false;
        currentBallRb = null;
        
        //Invoke("DetachBall", detachDelay); 
        Invoke(nameof(DetachBall), detachDelay); 
    }

    private void DetachBall()
    {
        currentBallSj.enabled = false;
        currentBallSj = null;
    }
}

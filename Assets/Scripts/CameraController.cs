using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private Transform LookAtTarget;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    UIManager uiManager;

    bool isPlayerDead;
    bool isPlayerSucceeded;

    private void Awake()
    {
        uiManager.OnPlayerDied += SetOffsetToLookFromBehind;
        playerController.OnLevelEnded += SetOffsetToLookFromFromt;
    }
  
   

    private void Start()
    {
        isPlayerDead = false;
        isPlayerSucceeded = false;
        offset = new Vector3(0, 4.5f, -2.91f);
    }

    private void SetOffsetToLookFromFromt()
    {
        isPlayerSucceeded = true;
        offset = new Vector3(0, 2.5f, 4);
    }

    private void SetOffsetToLookFromBehind()
    {
        isPlayerDead = true;
        offset = new Vector3(-4, 4, 0);
    }

    void LateUpdate()
    {
        if (isPlayerDead)
        {
            SlowlyMoveCamera(transform.position, playerTransform.position, offset, 1);
            
        }
        else
        {
            transform.position = playerTransform.position + offset;
        }
        
        if (isPlayerSucceeded)
        {
            SlowlyMoveCamera(transform.position, playerTransform.position, offset, 1);
        }

        transform.LookAt(LookAtTarget, Vector3.up);
    }

    void SlowlyMoveCamera(Vector3 from, Vector3 to, Vector3 offset, float speed)
    {
       transform.position =  Vector3.Lerp(from, to + offset, Time.deltaTime * speed);
    }
}

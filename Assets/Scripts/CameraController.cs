using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    private const float MIN_FOLLOW_OFFSET_Y = 2f;
    private const float MAX_FOLLOW_OFFSET_Y = 12f;    

    private float moveSpeed = 5f;
    private float rotationSpeed = 100f;
    private float zoomSpeed = 5f;
    private float zoomAmount = 1f;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineTransposer cinemachineTransposer;
    private Vector3 targetFollowOffset;
    private void Start() {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }

    private void Update() {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleMovement(){
        Vector3 inputMoveDir = new Vector3(0,0,0);
        if(Input.GetKey(KeyCode.W)){
            inputMoveDir.z = 1f;
        }

        if(Input.GetKey(KeyCode.A)){
            inputMoveDir.x = -1f;
        }

         if(Input.GetKey(KeyCode.S)){
            inputMoveDir.z = -1f;
        }

         if(Input.GetKey(KeyCode.D)){
            inputMoveDir.x = 1f;
        }

        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += inputMoveDir * moveSpeed * Time.deltaTime;

    }

    private void HandleRotation(){
        Vector3 rotationVector = new Vector3(0,0,0);
        if(Input.GetKey(KeyCode.Q)){
            rotationVector.y = 1f;
        }
        if(Input.GetKey(KeyCode.E)){
            rotationVector.y = -1f;
        }
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

    private void HandleZoom(){
        if(Input.mouseScrollDelta.y > 0){
            targetFollowOffset.y -= zoomAmount;
        }
        if(Input.mouseScrollDelta.y < 0){
            targetFollowOffset.y += 1f;
        }

        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_OFFSET_Y,MAX_FOLLOW_OFFSET_Y);

        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset,targetFollowOffset,Time.deltaTime * zoomSpeed); 
    }
}

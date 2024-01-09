using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Move : MonoBehaviour
{
    

    [SerializeField]
    private float lookSensitivity;


    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0;


    [SerializeField]
    private Camera theCamera;

    private Rigidbody myRigid;


    // Use this for initialization
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }




    // Update is called once per frame
    void Update()
    {
        
        Move();
        CameraRotation();
        CharacterRotation();

    }
    
    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * 30.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * 30.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * 30.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * 30.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * 30.0f * Time.deltaTime);
        }
    }


    private void CharacterRotation()
    {
        // 좌우 캐릭터 회전
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
        Debug.Log(myRigid.rotation);
        Debug.Log(myRigid.rotation.eulerAngles);
    }

    private void CameraRotation()
    {
        // 상하 카메라 회전
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

}

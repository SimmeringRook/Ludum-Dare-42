using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private float cameraSpeed = 1.0f;
    public float CameraSpeed
    {
        get
        {
            return cameraSpeed;
        }
        set
        {
            cameraSpeed = Mathf.Clamp(value, 0.0f, 1.0f);
        }
    }

    private float minimumFOV = 15f;
    private float maximumFOV = 90f;
    private float zoomSensitivity = 10f;

    private bool isRotating = false;
    private Vector2 initialMousePosition;

    private Camera Camera;
    private Vector3 centerOfMap;

    // Use this for initialization
    void Start ()
    {
        this.Camera = this.gameObject.GetComponent<Camera>();
        this.centerOfMap = GameObject.FindObjectOfType<Map>().GetMapCenter();
        this.gameObject.transform.LookAt(this.centerOfMap);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Pan();
        Zoom();
        Rotate();
	}

    void Pan()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.gameObject.transform.position += (Vector3.forward * this.CameraSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this.gameObject.transform.position += (Vector3.back * this.CameraSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.position += (Vector3.left * this.CameraSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.position += (Vector3.right * this.CameraSpeed);
        }
    }

    private void Zoom()
    {
        float fov = this.Camera.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        fov = Mathf.Clamp(fov, minimumFOV, maximumFOV);
        this.Camera.fieldOfView = fov;
    }

    private Vector2 lastPosition;
    private Vector2 currentMousePosition;
    private void Rotate()
    {
        if (Input.GetMouseButton(1))
        {

            if (isRotating)
            {
                lastPosition = currentMousePosition;
                currentMousePosition = Input.mousePosition;
                
                float differenceInX = currentMousePosition.x - lastPosition.x;
                float differenceInY = currentMousePosition.y - lastPosition.y;


                //TODO tweak this to resolve stuttering when rotating
                if (differenceInX != 0 && differenceInY != 0)
                {
                    Vector3 axis = new Vector3
                        (
                        x: 0/*Vector3.Lerp(lastPosition, currentMousePosition, Time.deltaTime * differenceInY).x*/,
                        y: differenceInX/*Vector3.Lerp(lastPosition, currentMousePosition, Time.deltaTime * differenceInX).y*/,
                        z: 0
                        );

                    this.gameObject.transform.LookAt(new Vector3(5.5f, 0f, 5.5f));
                    this.gameObject.transform.RotateAround(new Vector3(5.5f, 0f, 5.5f), axis, 100 * Time.deltaTime);
                }

            }
            else
            {
                initialMousePosition = Input.mousePosition;
                currentMousePosition = initialMousePosition;
                isRotating = true;
            }
        }
        else
        {
            isRotating = false;
        }
    }
}

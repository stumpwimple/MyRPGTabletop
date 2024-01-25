using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbittingCamera : MonoBehaviour
{
    public GameController gameController;
    public GameObject cameraTarget;
    public float rotationSpeed;

    private Vector3 targetCenter;
    // Use this for initialization
    void Start()
    {
        if (cameraTarget)
        {
            targetCenter = cameraTarget.transform.position;
        }
        else
        {
            targetCenter = this.transform.position;
        }

        transform.LookAt(targetCenter);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController && gameController.selectedObject) {
            targetCenter = gameController.selectedObject.transform.position;            
        }


        if (Input.GetMouseButton(2) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift)))
        {
            transform.RotateAround(targetCenter, Vector3.up, Input.GetAxis("Mouse X") * rotationSpeed);
            float xRotation = transform.localRotation.eulerAngles.x;
            if (xRotation > 180)
            {
                xRotation -= 360f;
            }

            float yMouseAxis = Input.GetAxis("Mouse Y");

            if (xRotation < 80 && xRotation > -80)
            {
                transform.RotateAround(targetCenter, -Camera.main.transform.right, yMouseAxis * rotationSpeed);
            }
            else if ((xRotation >= 80 && yMouseAxis > 0) || (xRotation <= -80 && yMouseAxis < 0))
            {
                transform.RotateAround(targetCenter, -Camera.main.transform.right, yMouseAxis * rotationSpeed);
            }
        }
        else
        if (Input.GetMouseButton(2))
        {
            Debug.Log("Camera up" + Camera.main.transform.up);
            Debug.Log("Camera right" + Camera.main.transform.right);
            float yMouseAxis = Input.GetAxis("Mouse Y");
            float xMouseAxis = Input.GetAxis("Mouse X");

            transform.position -= Camera.main.transform.up * yMouseAxis + Camera.main.transform.right* xMouseAxis;

            //transform.Translate(Camera.main.transform.up * yMouseAxis);
            //transform.Translate(Camera.main.transform.right * xMouseAxis);
        }

        float zoomScroll = Input.GetAxis("Mouse ScrollWheel");
        float speedZoom=1;
        if(Input.GetKey(KeyCode.LeftShift)|| Input.GetKey(KeyCode.RightShift)) { speedZoom = 3; }
        else { speedZoom = 1; }
        if (zoomScroll!=0&&Camera.main.orthographic) {
            Camera.main.orthographicSize -= zoomScroll*10f*speedZoom;
        }
        if (zoomScroll != 0 && !Camera.main.orthographic)
        {
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView-zoomScroll * 10f * speedZoom, 5f, 75);
        }
    }
}

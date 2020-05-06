using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject cam;
    public float speed, jumpForce, jumpRaycastDist, mouseSensitivity;
    private float hor, ver, jump, xmouse, ymouse, xrotation;

    private PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!photonView.IsMine) return;
        foreach(GameObject camera in GameObject.FindGameObjectsWithTag("Camera"))
        {
            if(camera == cam)
            {
                camera.SetActive(true);
            }
            else
            {
                camera.SetActive(false);
            }
        }

        cam.GetComponent<Camera>();
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        if(Physics.Raycast(transform.position, Vector3.down, transform.localScale.y / 2 + jumpRaycastDist)){
            
            rb.AddForce(new Vector3(0, jump, 0) * jumpForce);
            jump = Input.GetAxis("Jump");

        }
        xmouse = Input.GetAxis("Mouse X") * mouseSensitivity;
        ymouse = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        xrotation -= ymouse;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f);
        rb.AddRelativeForce(new Vector3(hor, 0, ver) * speed);
        cam.transform.localRotation = Quaternion.Euler(xrotation, 0, 0);
        transform.Rotate(Vector3.up, xmouse);
    }
}

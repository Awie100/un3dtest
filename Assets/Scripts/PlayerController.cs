using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Camera cam;
    public GameObject bullet;
    public float bullSpeed = 10f;
    public float speed = 5;
    public float gravity = 10;
    private float vy = 0;
    private float dpi = 2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();

        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }


    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        

        Vector3 dPos = (transform.forward * v + transform.right * h) * speed + transform.up * vy;
        characterController.Move(dPos * Time.deltaTime);

        if(characterController.isGrounded)
        {
            vy = Input.GetKey(KeyCode.Space) ? 10 : 0;
        } 
        else
        {
            vy -= gravity * Time.deltaTime;
        }
    }

    private void Rotate()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        transform.Rotate(0, h * dpi, 0);

        cam.transform.Rotate(-v * dpi, 0, 0);
        float lRot = cam.transform.localRotation.eulerAngles.x;
        lRot = (lRot >= 180) ? lRot - 360 : lRot;
        lRot = Mathf.Clamp(lRot, -80, 80);
        cam.transform.localRotation = Quaternion.Euler(lRot, 0, 0);
    }


    private void Shoot()
    {
        GameObject bull = Instantiate(bullet, cam.transform.position, cam.transform.rotation);
        bull.GetComponent<Rigidbody>().velocity = cam.transform.forward * bullSpeed;
    }
}

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    Transform  cameraTransform;         //相机的Transform
    CharacterController controller;  
    public float speed = 8.0f;          //移动的速度
    public float gravity = 10.0f;       //重力加速度

    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)))
        {
            if (Input.GetKey(KeyCode.W))
            {
                //根据主相机的朝向决定人物的移动方向，下同
                controller.transform.eulerAngles = new Vector3(0, cameraTransform.transform.eulerAngles.y, 0);
            }

            if (Input.GetKey(KeyCode.S))
            {
                controller.transform.eulerAngles = new Vector3(0, cameraTransform.transform.eulerAngles.y + 180f, 0);
            }

            if (Input.GetKey(KeyCode.A))
            {
                controller.transform.eulerAngles = new Vector3(0, cameraTransform.transform.eulerAngles.y + 270f, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                controller.transform.eulerAngles = new Vector3(0, cameraTransform.transform.eulerAngles.y + 90f, 0);
            }

            controller.Move(transform.forward * Time.deltaTime * speed);
        }
        else
        {
            //静止状态
        }
        if (Input.GetButton("Jump"))
        {
            controller.Move(transform.up * Time.deltaTime * speed);
        }
        if (!controller.isGrounded)
        {
            //模拟简单重力，每秒下降10米，当然你也可以写成抛物线
            controller.Move(new Vector3(0, -gravity * Time.deltaTime, 0));
        }
    }
}

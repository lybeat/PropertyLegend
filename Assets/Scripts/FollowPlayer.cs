using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;
    public Vector3 playerOffset = Vector3.zero;

    public float distance = 10f;
    public float minDistance = 2f;
    public float maxDistance = 15f;
    public float zoomSpeed = 1f;

    // 在X轴上的移动速度
    public float xSpeed = 200.0f;
    // 在Y轴上的移动速度
    public float ySpeed = 200.0f;

    public bool allowYTilt = true;
    public float yMinLimit = 20f;
    public float yMaxLimit = 80f;

    // 需要旋转的角度
    private float x = 0f;
    private float y = 0f;
    // 旋转的目标角度
    private float targetX = 0f;
    private float targetY = 0f;
    private float targetDistance = 0f;
    // 旋转速度
    private float xVelocity = 1f;
    private float yVelocity = 1f;
    // 缩放速度
    private float zoomVelocity = 1f;
    
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        targetX = angles.x;
        targetY = ClampAngle(angles.y, yMinLimit, yMaxLimit);
        targetDistance = distance;
    }

    void LateUpdate()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            targetDistance -= zoomSpeed;
        }
        else if (scroll < 0f)
        {
            targetDistance += zoomSpeed;
        }
        targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);

        if (Input.GetMouseButton(1) || (Input.GetMouseButton(0) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))))
        {
            targetX += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            if (allowYTilt)
            {
                targetY -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                targetY = ClampAngle(targetY, yMinLimit, yMaxLimit);
            }
        }
        x = Mathf.SmoothDampAngle(x, targetX, ref xVelocity, 0.3f);
        if (allowYTilt)
        {
            y = Mathf.SmoothDampAngle(y, targetY, ref yVelocity, 0.3f);
        }
        else
        {
            y = targetY;
        }

        //Debug.Log("targetX: " + targetX + ", targetY: " + targetY);
        //Debug.Log("x: " + x + ", y: " + y);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        distance = Mathf.SmoothDamp(distance, targetDistance, ref zoomVelocity, 0.5f);

        Vector3 position = rotation * new Vector3(0, 0, -distance) + player.position + playerOffset;
        transform.rotation = rotation;
        transform.position = position;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}

using UnityEngine;
using System.Collections;
using UnityEditor;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    float x = 0.0f;
    float y = 0.0f;

    private Vector2 mouseDelta;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        Player.Controls.Player.MouseRight.performed += context =>
        {
            Cursor.lockState = CursorLockMode.Locked;
        };
        
        Player.Controls.Player.MouseRight.canceled += context =>
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        };
        
        Player.Controls.Player.MouseMove.performed += context =>
        {
            var input = context.ReadValue<Vector2>();
            mouseDelta = new Vector3(input.x, input.y);
        };
        Player.Controls.Player.MouseMove.canceled += context => mouseDelta = Vector2.zero;
    }

    void LateUpdate()
    {
        if (target)
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                x += mouseDelta.x * xSpeed * distance * 0.02f;
                y -= mouseDelta.y * ySpeed * 0.02f;
            }

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
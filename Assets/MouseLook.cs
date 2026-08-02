using System.Collections;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private Vector3 center;

    void Start()
    {
        center = Vector3.zero;
        center.z = CreateMaze.size / 2.0f;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            transform.RotateAround(center, new Vector3(mouseY, -mouseX, 0), rotationSpeed * Time.deltaTime);
        }
    }
}
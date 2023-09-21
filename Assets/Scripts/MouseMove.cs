using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float mouseSens = 100f;
    private float _mouseX, _mouseY = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //скрыть курсор
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void Update()
    {
        if (GameController.IsOver) return;
        Move();
    }

    private void Move()
    {
        _mouseX += Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        _mouseY += Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        transform.rotation = Quaternion.Euler(-_mouseY, _mouseX, 0f);
    }
}
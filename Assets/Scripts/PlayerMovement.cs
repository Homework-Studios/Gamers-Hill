using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] public float dpi = 100;
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpVelocity;

    private Camera _camera;

    private CharacterController _controller;
    private Entity _entity;
    private StatsManager _statsManager;


    private float _xRotation;

    // Start is called before the first frame update
    private void Start()
    {
        _camera = Camera.allCameras[0];
        _statsManager = GetComponent<StatsManager>();
        _entity = GetComponent<Entity>();
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        var rightclick = Input.GetMouseButtonDown(1);
        var moveX = Input.GetAxis("Horizontal");
        var moveY = Input.GetAxis("Vertical");

        var mouseX = Input.GetAxis("Mouse X") * dpi * Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * dpi * Time.deltaTime;

        _controller.Move(new Vector3(0, gravity * Time.deltaTime, 0));

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        var move = transform.right * moveX + transform.forward * moveY;

        _controller.Move(move * (Time.deltaTime * moveSpeed * _statsManager.speedMultiplier));
        _controller.transform.Rotate(Vector3.up * mouseX);

        var rotation = _controller.transform.rotation.normalized;
        _camera.transform.rotation = Quaternion.Euler(_xRotation, rotation.eulerAngles.y, rotation.eulerAngles.z);

        if (rightclick) _entity.weapon.Shoot(_camera.transform);
    }
}
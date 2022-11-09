//using UnityEngine;
//​
///// <summary>
///// By Dustin S.
///// Premade for XR Dev Section 1.
///// Simple Controls for a first & third person character using a CharacterController.
///// WASD        - move forward/back and strafe left/right.
///// Mouse       - Rotate body and Look up/down.
///// Mouse Wheel - Zoom in/out. (Zoom in fully for first person view)
///// Space       - Press to jump.
///// Shift       - Hold to run.
///// </summary>
//[RequireComponent(typeof(CharacterController))]
//public class PlayerMove : MonoBehaviour
//{
//    const float DEFAULT_CAM_ZOOM = 3.6f;
//    const float DEFAULT_CAM_ANGLE = 30f;
//    const float MIN_LOOK_UP_ANGLE = -37f;
//    const float MAX_LOOK_DOWN_ANGLE = 85f; 
//    const float ZOOM_SPEED = 50;
//    const float FIRST_PERSON_SNAP_DISTANCE = 1.13f;
//    const float MAX_ZOOM_DISTANCE = 10;​
//    public float jumpPower = 10f;
//    public float gravity = 25;
//    public float moveSpeed = 4;
//    public float sprintMoveSpeedMultiplier = 2.0f;
//    public float rotationSpeed = 150;
//    public float cameraLerpSpeed = 8;
//    public float cameraTargetHeight = 1;
//​
//    private float currentVerticalSpeed;
//    private Transform _characterTransform;
//    private Transform _camLookTransform;
//    private Transform _camTransform;
//    private CharacterController _characterController;​
//    private float _currentZoomLevel = DEFAULT_CAM_ZOOM;
//    private float _currentCameraAngle = DEFAULT_CAM_ANGLE;​
//    private Vector3 CameraLocalDirection => Quaternion.AngleAxis(_currentCameraAngle, Vector3.right) * Vector3.back;
//​
//    private void Awake()
//    {
//        if (!TryGetCamera(out Camera mainCam))
//        {
//            enabled = false;
//            Debug.LogError("Disabling. Unable to find the main camera.");
//            return;
//        }
//​
//        // Cache & Unparent Camera
//        _camTransform = mainCam.transform;
//        _camTransform.parent = null;
//​
//        // Cache local Components.
//        _characterTransform = transform;
//        _characterController = GetComponent<CharacterController>();
//​
//        // Create camera target
//        _camLookTransform = new GameObject("Camera Look Target").transform;
//        _camLookTransform.parent = _characterTransform;
//​
//        Cursor.lockState = CursorLockMode.Confined;
//    }
//​
//    private void Update()
//    {
//        // Set CameraTarget position in case cameraTargetHeight is changed at run time.
//        _camLookTransform.localPosition = new Vector3(0, cameraTargetHeight);
//​
//        Cursor.visible = false;
//​
//        CalculateGravity();
//​
//        if (IsGrounded())
//            JumpInput();
//​
//        MovementInput();
//        PlayerRotationInput(); // Mouse X
//        LookRotationInput(); // Mouse Y
//        ZoomInput();
//​
//        UpdateCamera();
//    }
//​
//    private void UpdateCamera()
//    {
//        bool enableFirstPerson = _currentZoomLevel < FIRST_PERSON_SNAP_DISTANCE;
//​
//        // Snap Camera zoom
//        float currentZoom = enableFirstPerson ? 0.01f : _currentZoomLevel;
//​
//        // Camera Position.
//        Vector3 localCamOffset = CameraLocalDirection * currentZoom;
//        Vector3 targetCamPosition = _camLookTransform.TransformPoint(localCamOffset);
//        _camTransform.position =
//            enableFirstPerson ?
//            targetCamPosition :
//            Vector3.Lerp(_camTransform.position, targetCamPosition, cameraLerpSpeed * Time.deltaTime);
//​
//        // Camera Rotation
//        _camTransform.LookAt(_camLookTransform);
//    }
//​
//    private void LookRotationInput()
//    {
//        _currentCameraAngle -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
//        _currentCameraAngle = Mathf.Clamp(_currentCameraAngle, MIN_LOOK_UP_ANGLE, MAX_LOOK_DOWN_ANGLE);
//    }
//​
//    private void PlayerRotationInput() =>
//        _characterTransform.Rotate(0, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime, 0);
//​
//    private void ZoomInput()
//    {
//        _currentZoomLevel -= Input.mouseScrollDelta.y * ZOOM_SPEED * Time.deltaTime;
//        _currentZoomLevel = Mathf.Clamp(_currentZoomLevel, FIRST_PERSON_SNAP_DISTANCE - 0.01f, MAX_ZOOM_DISTANCE);
//    }
//​
//    private void MovementInput()
//    {
//        float deltaTime = Time.deltaTime;
//​
//        // Run
//        float movementSpeedMultiplier = GetRunMultiplier() * deltaTime;
//        // Forward
//        float yMove = Input.GetAxis("Vertical");
//        // Strafing
//        float xMove = Input.GetAxis("Horizontal");
//        // Set movement
//        Vector3 velocity = new Vector3(xMove * movementSpeedMultiplier, currentVerticalSpeed * deltaTime, yMove * movementSpeedMultiplier);
//        _characterController.Move(_characterTransform.TransformDirection(velocity));
//    }
//​
//    private void CalculateGravity() =>
//        currentVerticalSpeed -= gravity * Time.deltaTime;
//​
//    private bool IsGrounded()
//    {
//        bool grounded = _characterController.isGrounded;
//        if (grounded && currentVerticalSpeed < 0)
//            currentVerticalSpeed = 0;
//        return grounded;
//    }
//​
//    private void JumpInput()
//    {
//        if (Input.GetKeyDown(KeyCode.Space))
//            currentVerticalSpeed = jumpPower;
//    }
//​
//    private float GetRunMultiplier() => moveSpeed *
//            (Input.GetKey(KeyCode.LeftShift) ?
//            sprintMoveSpeedMultiplier : 1);
//​
//    private bool TryGetCamera(out Camera camera)
//    {
//        camera = Camera.main;
//​
//        if (camera == null)
//            camera = FindObjectOfType<Camera>();
//​
//        return camera != null;
//    }
//}

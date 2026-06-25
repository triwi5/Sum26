using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAiming : MonoBehaviour, IAimProvider
{
    [Header("Aiming")] 
    [SerializeField] private LayerMask aimPlaneLayer;

    private PlayerInputActions inputActions;
    private Vector2 lookInput;
    private Camera mainCamera;
    private Vector3 cachedAimDirection;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        mainCamera = Camera.main;
        cachedAimDirection = transform.forward;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Look.performed += OnLook;
        inputActions.Player.Look.canceled += OnLook;
    }
    
    private void OnDisable()
    {
       inputActions.Player.Look.performed -= OnLook;
        inputActions.Player.Look.canceled -= OnLook;
        inputActions.Player.Disable();
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        cachedAimDirection = ComputeAimDirection();
    }

    public Vector3 GetAimDirection()
    {
        return cachedAimDirection;
    }

    private Vector3 ComputeAimDirection()
    {
        Vector3 aimPoint = GetWorldAimPoint();
        Vector3 direction = aimPoint - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
        {
            return cachedAimDirection;
        }

        return direction.normalized;
    }

    private Vector3 GetWorldAimPoint()
    {
        Ray ray = mainCamera.ScreenPointToRay(lookInput);

        if (Physics.Raycast(ray, out RaycastHit hit, 200f, aimPlaneLayer))
        {
            return hit.point;
        }

        Plane horizontalPlane = new Plane(Vector3.up, new Vector3(0f, transform.position.y, 0f));
        if (horizontalPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }

        return transform.position + transform.forward;
    }
}

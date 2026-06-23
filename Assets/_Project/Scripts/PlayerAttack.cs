using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AbilityRunner basicAttack;
    
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions=new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Attack.performed += OnAttack;

    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Disable();
    }
    
    private void OnAttack(InputAction.CallbackContext context)
    {
        if (basicAttack != null)
        {
            basicAttack.TryCast();
        }
    }
}

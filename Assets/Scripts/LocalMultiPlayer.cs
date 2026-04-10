using UnityEngine;
using UnityEngine.InputSystem;

public class LocalMultiPlayer : MonoBehaviour
{
    public Vector2 moveDirection;
    public float moveSpeed;
    public LocalMultiplayerManager manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;    
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PlayerInput playerInput = GetComponent<PlayerInput>();
            manager.TryAttack(playerInput);
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ClickToMove : MonoBehaviour
{
    public Coroutine MovePlayerCoroutine;

    private float distanceToPlayer;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        distanceToPlayer = Vector2.Distance(currentMousePosition, transform.position);

        MovePlayerCoroutine = StartCoroutine(PlayerMovementUpdate());
    }

    public IEnumerator PlayerMovementUpdate()
    {
        while (distanceToPlayer > 0)
        { 
            transform.position += (Time.deltaTime + distanceToPlayer) * Vector3.one;        
        }

        yield return null;
    }
}

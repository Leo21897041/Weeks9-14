using UnityEngine;
using UnityEngine.InputSystem;


public class Knight : MonoBehaviour
{
    public AudioSource audioSource;
    public float speed;
    public Animator knightAnimator;

    float xMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(xMovement, 0f, 0f) * speed * Time.deltaTime;        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Walking");

        Vector2 moveDirection = context.ReadValue<Vector2>();
        xMovement = moveDirection.x;

        bool isRunning = xMovement != 0f;

        knightAnimator.SetBool("isRunning", isRunning);
    }

    public void OnFootstep()
    {
        audioSource.Play();
    }
}

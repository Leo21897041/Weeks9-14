using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Data;
using System.Collections;

public class Player : MonoBehaviour
{
    public float currentSpeed;
    public float dashSpeed;
    public float normalSpeed;
    public Vector2 directionalInput;

    public AnimationCurve squeezeCurve;
    private Coroutine squeezeCoroutine;

    public float attackProgress;
    public float attackDuration;

    public float dashProgress;
    public float dashDuration;

    private bool isSqueezing;

    public Coroutine dashCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)directionalInput * Time.deltaTime * currentSpeed;

        if (isSqueezing == true)
        {
            squeezeCoroutine = StartCoroutine(SqueezeUpdate());
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        directionalInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        isSqueezing = true;
        Debug.Log("Started");
    }

    public IEnumerator SqueezeUpdate()
    {
        Vector3 currentScale = transform.localScale;
        if (attackProgress < attackDuration)
        {
            attackProgress += Time.deltaTime;

            while (attackProgress < attackDuration)
            {
                transform.localScale = squeezeCurve.Evaluate(attackProgress / attackDuration) * Vector3.one;

                yield return null;
            }
        }
    }

    public void OnInteract()
    {
        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
        }

        dashCoroutine = StartCoroutine(DashUpdate());
    }

    public IEnumerator DashUpdate()
    {
        currentSpeed = dashSpeed;

        yield return new WaitForSeconds(1f);

        currentSpeed = normalSpeed;
    }
}

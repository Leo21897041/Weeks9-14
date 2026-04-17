using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Data;
using System.Collections;
using UnityEngine.UIElements;

public class Player : MonoBehaviour

{
    public float currentSpeed;
    public float dashSpeed;
    public float speed;
    public Vector2 directionalInput;

    public AnimationCurve squeezeCurve;
    private Coroutine squeezeCoroutine;
    private bool isSqueezing;
    public float attackProgress;
    public float attackDuration;
    private bool isAttacking;

    public Coroutine dashCoroutine;
    public float dashProgress;
    public float dashDuration;
    public float isDash;

    public AnimationCurve rotationCurve;
    public Vector3 hitRotation;
    public float hitProgress;
    public float hitDuration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)directionalInput * Time.deltaTime * currentSpeed;        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        directionalInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (isAttacking)    return;
        if (squeezeCoroutine != null)   StopCoroutine(SqueezeUpdate());

        Debug.Log(context);

        squeezeCoroutine = StartCoroutine(SqueezeUpdate());
    }

    public IEnumerator SqueezeUpdate()
    {
        Vector3 currentScale = transform.localScale;        
        attackProgress = 0;
        isAttacking = true;

        while (attackProgress < attackDuration)
        {
            attackProgress += Time.deltaTime;
            
            currentScale = squeezeCurve.Evaluate(attackProgress / attackDuration) * Vector3.one;

            transform.localScale = Vector3.Lerp(transform.localScale, currentScale, attackDuration);

            yield return null; 
        }

        transform.localScale = currentScale;

        isAttacking = false;
    }

    public void OnInteract()
    {
        if (dashCoroutine != null)  StopCoroutine(dashCoroutine);

        dashCoroutine = StartCoroutine(DashUpdate());
    }

    public IEnumerator DashUpdate()
    {
        currentSpeed = dashSpeed;

        yield return new WaitForSeconds(1f);

        currentSpeed = speed;
    }

    public void OnHit()
    {
        transform.eulerAngles = hitRotation;

        hitRotation.z += rotationCurve.Evaluate(hitProgress / hitDuration);

        hitRotation = transform.eulerAngles;
    }
}

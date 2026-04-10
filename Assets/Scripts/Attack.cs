using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public AnimationCurve squeezeCurve;
    private Coroutine squeezeCoroutine;

    public float progress;
    public float duration;

    private bool isSqueezing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSqueezing == true)
        { 
            squeezeCoroutine = StartCoroutine(SqueezeUpdate());
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        isSqueezing = true;
        Debug.Log("Started");
    }

    public IEnumerator SqueezeUpdate()
    {
        if (progress < duration)
        { 
            progress += Time.deltaTime;

            while (progress < duration)
            {
                transform.localScale = squeezeCurve.Evaluate(progress / duration) * Vector3.one;
                
                yield return null;
            }
        }
        
    }
}

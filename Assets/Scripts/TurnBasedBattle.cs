using UnityEngine;
using System.Collections;

public class TurnBasedBattle : MonoBehaviour
{
    public AnimationCurve animationCurve;
    public float duration;
    private float progress;
    public Coroutine moveSpinnerCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpinnerMover()
    {
        float progress = 0f;

        while (progress < duration)
        {
            progress += Time.deltaTime;
            transform.eulerAngles += animationCurve.Evaluate(progress / duration) * Vector3.one;

            yield return null;
        }    
    }

    public void OnMoveSpinner()
    {
        StartCoroutine(SpinnerMover());
    }
}

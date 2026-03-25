using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class StrategyBuilderGrow : MonoBehaviour
{
    public float duration;
    public float speed;

    public AnimationCurve growCurve;
    private Coroutine squareCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ObjectSpawned();
    }

    private IEnumerator SquareGrowUpdate()
    {
        float progress = 0f;

        while (progress < duration)
        {
            progress += Time.deltaTime;
            transform.localScale = growCurve.Evaluate(progress / duration) * Vector3.one;

            yield return null;
        }
    }

    public void ObjectSpawned()
    {
        if (gameObject)
        {
            squareCoroutine = StartCoroutine(SquareGrowUpdate());
        }
    }
}

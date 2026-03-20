using System;
using System.Collections;
using UnityEngine;

public class TreeGrower : MonoBehaviour
{
    public AnimationCurve growCurve;
    public Transform branchesTransform;
    public float maxSpawnDistance;

    public float duration;
    public float appleGrowDuration;
    public GameObject applePrefab;

    private Coroutine treeGrowCoroutine;
    private Coroutine appleCoroutine;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TreeGrowUpdate();
    }

    private IEnumerator TreeGrowUpdate()
    {
        float progress = 0f;

        //The contents of the while loop runs while the condition is true
        while (progress < duration)
        { 
            progress += Time.deltaTime;
            transform.localScale = growCurve.Evaluate(progress / duration) * (Vector3.one);

            //relinquishes control of Unity so everything else can run for the rest of this frame. 
            yield return null;
        }

        //relinquishes control of Unity until apple finished growing. 
        //yield return new WaitForSeconds(appleGrowDuration);

        //relinquishes control of Unity until coroutine for the apple has finished executing.
        appleCoroutine = StartCoroutine(AppleGrowUpdate());
        yield return appleCoroutine;

        appleCoroutine = StartCoroutine(AppleGrowUpdate());
        yield return appleCoroutine;

        StartCoroutine(AppleGrowUpdate());

    }
    private IEnumerator AppleGrowUpdate()    
    {
        Vector3 spawnPosition = branchesTransform.position;
        spawnPosition += (Vector3)UnityEngine.Random.insideUnitCircle * maxSpawnDistance;

        GameObject spawnedApple = Instantiate(applePrefab, spawnPosition, Quaternion.identity);
        spawnedApple.transform.localScale = Vector3.zero;

        float progress = 0f;

        while (progress < appleGrowDuration)
        {
            progress += Time.deltaTime;
            spawnedApple.transform.localScale = growCurve.Evaluate(progress / appleGrowDuration) * (Vector3.one);

            yield return null;
        }
    }
    public void OnGrowPress()
    {
        //IMPORTANT
        treeGrowCoroutine = StartCoroutine(TreeGrowUpdate());
    }

    public void OnStopPress()
    {
        //IMPORTANT
        if (treeGrowCoroutine != null)
        { 
            StopCoroutine(treeGrowCoroutine);
        }

        if (appleCoroutine != null)
        {
            StopCoroutine(appleCoroutine);
        }
    }
}

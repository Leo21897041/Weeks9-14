using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;


public class Enemies : MonoBehaviour

{
    //ALL ENEMIES
    public GameObject startButton;
    public bool isSpawned;
    public bool isInstantiatedEnemyA;
    public bool isInstantiatedEnemyB;
    public bool isPaused;
    public bool isCoroutinePaused;

    //ENEMY A
    public List<GameObject> enemyAList = new List<GameObject>();
    public GameObject enemyAPrefab;
    public AnimationCurve enemyAMovingOntoScreenCurve;
    public EnemyAPrefab enemyAPrefabScript;

    public Coroutine enemyAMoveOntoScreenCoroutine;
    public Coroutine EnemyADiveCoroutine;

    public Vector3 enemyASpawnPosition;
    public Vector3 positionEnemyA;
    public int enemyASpawnDistance;

    public float durationEnemyA;
    public float progressEnemyA = 0f;

    public float durationDiveEnemyA;
    public float progressDiveEnemyA = 0f;

    public float spawnMaxEnemyA;


    //ENEMY B
    public List<GameObject> enemyBList = new List<GameObject>();
    public GameObject enemyBPrefab;
    public AnimationCurve enemyBMovingOntoScreenCurve;
    public EnemyBPrefab enemyBPrefabScript;

    public Coroutine enemyBMoveOntoScreenCoroutine;
    public Coroutine EnemyBDiveCoroutine;

    public Vector3 enemyBSpawnPosition;
    public Vector3 positionEnemyB;
    public int enemyBSpawnDistance;

    public float durationEnemyB;
    public float progressEnemyB = 0f;

    public float durationDiveEnemyB;
    public float progressDiveEnemyB = 0f;

    public float spawnMaxEnemyB;

    //SCORE
    public TextMeshProUGUI scoreText;
    public int score;

    void Start()
    {
        isSpawned = false;
        score = 0;
    }

    void Update()
    {
        if (isSpawned == true)
        {
            //ENEMY A
            while (enemyAList.Count < 6 && isInstantiatedEnemyA == false)
            {
                for (int i = 0; i < 6; i++)
                {
                    GameObject newEnemyA = Instantiate(enemyAPrefab, enemyASpawnPosition += new Vector3(enemyASpawnDistance, 0, 0), Quaternion.identity);
                    enemyAList.Add(newEnemyA);

                    EnemyAPrefab enemyScriptEnemyA = newEnemyA.GetComponent<EnemyAPrefab>();
                    spawnMaxEnemyA = enemyScriptEnemyA.maxMoveLength;

                    Debug.Log(enemyAList[0]);

                    isInstantiatedEnemyA = true;
                }
            }

            //ENEMY B
            while (enemyBList.Count < 6 && isInstantiatedEnemyB == false)
            {
                for (int i = 0; i < 6; i++)
                {
                    GameObject newEnemyB = Instantiate(enemyBPrefab, enemyBSpawnPosition += new Vector3(enemyBSpawnDistance, 0, 0), Quaternion.identity);
                    enemyBList.Add(newEnemyB);

                    EnemyBPrefab enemyScriptEnemyB = newEnemyB.GetComponent<EnemyBPrefab>();
                    spawnMaxEnemyB = enemyScriptEnemyB.maxMoveLength;

                    Debug.Log(enemyBList[0]);

                    isInstantiatedEnemyA = true;
                }
            }

            isSpawned = false;

            enemyAMoveOntoScreenCoroutine = StartCoroutine(EnemyAMoveOntoScreenUpdate());
        }

        //SCORE
        scoreText.text = "Score: " + score;
    }

    public void OnStartButton()
    {
        startButton.SetActive(false);
        isSpawned = true;    
    }

    private IEnumerator EnemyAMoveOntoScreenUpdate()
    {
        while (progressEnemyA < durationEnemyA)
        {
            //Debug.Log("is working" + enemyAList[0]);
            progressEnemyA += Time.deltaTime;          

            yield return null;
        }
        if (progressEnemyA > durationEnemyA)
        { 
            Debug.Log("is working");
            EnemyADiveCoroutine = StartCoroutine(EnemyADiveUpdate());
            yield return EnemyADiveCoroutine;
        }
    }

    private IEnumerator EnemyADiveUpdate()
    {
        for (int i = 0; i < enemyAList.Count; i++)
        {
            progressDiveEnemyA = 0f;

            while (progressDiveEnemyA < durationDiveEnemyA)
            {
                progressDiveEnemyA += Time.deltaTime;

                if (enemyAList[i] != null)
                {
                    //Vector3 startingPosition = enemyAList[0].transform.position;
                    positionEnemyA = Camera.main.ScreenToWorldPoint(enemyAList[i].transform.position);
                    positionEnemyA.x = enemyAList[i].transform.position.x;
                    positionEnemyA.y = 1.5f - enemyAMovingOntoScreenCurve.Evaluate(progressDiveEnemyA / durationDiveEnemyA);
                    positionEnemyA.z = 0f;
                    enemyAList[i].transform.position = positionEnemyA;
                }               

                yield return null;
            }

            if (i == enemyAList.Count - 1)
            {
                EnemyBDiveCoroutine = StartCoroutine(EnemyBDiveUpdate());
                yield return EnemyBDiveCoroutine;
            }
        }                   
    }

    private IEnumerator EnemyBDiveUpdate()
    {
        for (int i = 0; i < enemyBList.Count; i++)
        {
            positionEnemyB = Camera.main.ScreenToWorldPoint(enemyBList[i].transform.position);
            progressDiveEnemyB = 0f;

            while (progressDiveEnemyB < durationDiveEnemyB)
            {
                progressDiveEnemyB += Time.deltaTime;

                if (enemyBList[i] != null)
                {
                    //Vector3 startingPosition = enemyAList[0].transform.position;
                    positionEnemyB.x = enemyBList[i].transform.position.x;
                    positionEnemyB.y = 3.5f - enemyBMovingOntoScreenCurve.Evaluate(progressDiveEnemyB / durationDiveEnemyB);
                    positionEnemyB.z = 0f;
                    enemyBList[i].transform.position = positionEnemyB;
                }

                yield return null;
            }            
        }
    }

    public void OnPauseButton()
    {
        isPaused = !isPaused;

        if (isPaused == true && enemyAMoveOntoScreenCoroutine != null ||
            isPaused == true && EnemyADiveCoroutine != null ||
            isPaused == true && EnemyBDiveCoroutine != null)
        {
            StopCoroutine(enemyAMoveOntoScreenCoroutine);
            StopCoroutine(EnemyADiveCoroutine);
            StopCoroutine(EnemyBDiveCoroutine);

            isCoroutinePaused = true;
        }

        if (isPaused == false && enemyAMoveOntoScreenCoroutine != null ||
            isPaused == false && EnemyADiveCoroutine != null ||
            isPaused == false && EnemyBDiveCoroutine != null)
        {
            isCoroutinePaused = false;
            isSpawned = true;
        }
    }
}
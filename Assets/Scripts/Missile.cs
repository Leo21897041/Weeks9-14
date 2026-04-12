using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Missile : MonoBehaviour

{
    public float missileSpeed;
    public GameObject enemySpawner;
    public SpriteRenderer spriteRenderer;  

    public int enemyAValue = 50;
    public int enemyBValue = 100;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * missileSpeed;

        Enemies enemyScriptList = enemySpawner.GetComponent<Enemies>();

        for (int i = 0; i < enemyScriptList.enemyAList.Count; i++)
        {
            Debug.Log("For loop working" + i);

            if (enemyScriptList.enemyAList[i] != null)
                {                
                GameObject enemyAGameObject = enemyScriptList.enemyAList[i];

                if (Vector3.Distance(transform.position, enemyScriptList.enemyAList[i].transform.position) <= 0.5f)
                {                    
                    enemyScriptList.enemyAList[i].SetActive(false);
                    enemyScriptList.enemyAList.RemoveAt(i);

                    Destroy(gameObject);

                    enemyScriptList.score += enemyAValue;
                    Debug.Log("Score: " + enemyScriptList.score);
                }
            }
        }

        for (int i = 0; i < enemyScriptList.enemyBList.Count; i++)
        {
            if (enemyScriptList.enemyBList[i] != null)
            {
                SpriteRenderer enemyBSprite = enemyScriptList.enemyBList[i].GetComponent<SpriteRenderer>();
                GameObject enemyBGameObject = enemyScriptList.enemyBList[i];

                if (Vector3.Distance(transform.position, enemyScriptList.enemyBList[i].transform.position) <= 0.5f)
                {
                    enemyScriptList.enemyBList[i].SetActive(false);
                    enemyScriptList.enemyBList.RemoveAt(i);

                    Destroy(gameObject);
                    
                    enemyScriptList.score += enemyBValue;
                    Debug.Log("Score: " + enemyScriptList.score);
                }
            }
        }
        Vector3 missilePosition = Camera.main.WorldToScreenPoint(transform.position);

        if (missilePosition.y > Screen.height)
        {
            Destroy(gameObject);
        }
    }

    public void ScoreUpdate()
    {
        
    }
}

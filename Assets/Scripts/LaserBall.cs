using Unity.Collections;
using UnityEngine;

public class LaserBall : MonoBehaviour

{
    public AnimationCurve laserCurve;

    public Vector3 laserBallPosition;
    public Vector3 laserBallSpawnPosition;
    
    public float laserBallSpeed;
    public float laserBallPathProgress;
    public float laserBallPathDuration;

    public GameObject enemySpawner;
    public SpriteRenderer spriteRenderer;

    public int enemyAValue = 50;
    public int enemyBValue = 100;

    //public GameObject player;

    //public Player player;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        laserBallPathProgress += Time.deltaTime;

        if (laserBallPathProgress > laserBallPathDuration)
        {
            laserBallPathProgress = 0f;
        }

        laserBallPosition = transform.position;

        laserBallPosition.x += (laserCurve.Evaluate(laserBallPathProgress / laserBallPathDuration))/100;
        laserBallPosition.y += Time.deltaTime * laserBallSpeed;
        laserBallPosition.z = 0f;

        transform.position = laserBallPosition;

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPosition.y > Screen.height + 20)
        {
            Destroy(gameObject);
        }

        Enemies enemyScriptList = enemySpawner.GetComponent<Enemies>();

        for (int i = 0; i < enemyScriptList.enemyAList.Count; i++)
        {
            Debug.Log("For loop working" + i);

            if (enemyScriptList.enemyAList[i] != null)
            {
                GameObject enemyAGameObject = enemyScriptList.enemyAList[i];

                if (Vector3.Distance(transform.position, enemyScriptList.enemyAList[i].transform.position) <= 1f)
                {
                    enemyScriptList.enemyAList[i].SetActive(false);
                    enemyScriptList.enemyAList.RemoveAt(i);

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

                if (Vector3.Distance(transform.position, enemyScriptList.enemyBList[i].transform.position) <= 1f)
                {
                    enemyScriptList.enemyBList[i].SetActive(false);
                    enemyScriptList.enemyBList.RemoveAt(i);

                    enemyScriptList.score += enemyBValue;
                    Debug.Log("Score: " + enemyScriptList.score);
                }
            }
        }
    }
}

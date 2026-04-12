using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    //MOVEMENT
    public Vector3 playerPosition;
    public float playerMoveSpeed;
    public float xDirection;

    //MISSILE
    public float missileSpawnPosition;

    public GameObject missilePrefab;
    public GameObject spawner;

    public float durationMissile;
    public float progressMissile;

    public bool canShootMissile;

    //LASER BALL
    public GameObject laserPrefab;


    public float durationLaser;
    public float progressLaser;

    public bool canShootLaser;

    //COLLISIONS
    public Enemies enemySpawner;
    public SpriteRenderer playerSpriteRenderer;

    void Start()
    {
        progressMissile = 0f;
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //PLAYER
        playerPosition = transform.position;
        transform.position += new Vector3(xDirection, 0f, 0f) * Time.deltaTime * playerMoveSpeed;

        //MISSILE
        if (progressMissile < durationMissile)
        {
            progressMissile += Time.deltaTime;
        }

        //LASER BALL
        if (progressLaser < durationLaser)
        {
            progressLaser += Time.deltaTime;
        }

        //COLLISIONS
        Enemies enemyScriptList = enemySpawner.GetComponent<Enemies>();

        for (int i = 0; i < enemyScriptList.enemyAList.Count; i++)
        {
            GameObject enemyAGameObject = enemyScriptList.enemyAList[i];

            if (Vector3.Distance(transform.position, enemyScriptList.enemyAList[i].transform.position) <= 1)
            {
                Destroy(gameObject);
            }
        }
        for (int i = 0; i < enemyScriptList.enemyBList.Count; i++)
        {
            GameObject enemyBGameObject = enemyScriptList.enemyBList[i];

            if (Vector3.Distance(transform.position, enemyScriptList.enemyBList[i].transform.position) <= 1)
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        xDirection = movement.x;
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (progressMissile >= durationMissile)
        {
            canShootMissile = true;
        }

        if (canShootMissile && Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameObject missileSpawnLeft = Instantiate(missilePrefab, transform.position + new Vector3(-missileSpawnPosition, 0, 0), Quaternion.identity);
            GameObject missileSpawnRight = Instantiate(missilePrefab, transform.position + new Vector3(missileSpawnPosition, 0, 0), Quaternion.identity);

            Missile newMissleL =  missileSpawnLeft.GetComponent<Missile>();
            newMissleL.enemySpawner = spawner;
            Missile newMissleR = missileSpawnRight.GetComponent<Missile>();
            newMissleR.enemySpawner = spawner;

            canShootMissile = false;
            progressMissile = 0f;
        }
    }
    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (progressLaser >= durationLaser)
        {
            canShootLaser = true;
        }

        if (canShootLaser && Mouse.current.rightButton.wasPressedThisFrame)
        { 
            GameObject laserSpawn = Instantiate(laserPrefab, transform.position, Quaternion.identity);

            LaserBall newLaserball = laserSpawn.GetComponent<LaserBall>();
            newLaserball.enemySpawner = spawner;

            canShootLaser = false;
            progressLaser = 0f;
        }
    }
}

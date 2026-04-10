using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class LocalMultiplayerManager : MonoBehaviour
{
    public List<Sprite> possiblePlayerVisuals;
    public List<PlayerInput> existingPlayers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerJoin(PlayerInput newPlayer)
    {
        //ASSIGN VISUALS TO NEW PLAYER
        SpriteRenderer newPlayerRenderer = newPlayer.GetComponent<SpriteRenderer>();
        newPlayerRenderer.sprite = possiblePlayerVisuals[existingPlayers.Count];

        existingPlayers.Add(newPlayer);

        LocalMultiPlayer playerScript = newPlayer.GetComponent<LocalMultiPlayer>();
        playerScript.manager = this;
            
    }

    public void TryAttack(PlayerInput attackingPlayer)
    {
        for (int i = 0; i < existingPlayers.Count; i++)
        {
            if (attackingPlayer == existingPlayers[i])
            {
                //Go to next iteration of the loop
                continue;
            }
            Vector3 attackingPlayerPosition = attackingPlayer.transform.position;
            Vector3 existingPlayerPosition = existingPlayers[i].transform.position;
            float distanceToPlayer = Vector3.Distance(attackingPlayerPosition, existingPlayerPosition);

            if (distanceToPlayer < 1.5f)
            {
                Debug.Log("ATTACK!");
            }
        }
    }
}

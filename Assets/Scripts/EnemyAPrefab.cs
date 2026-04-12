using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

public class EnemyAPrefab : MonoBehaviour
{
    public Vector3 position;

    public float speed;

    public float maxMoveLength;

    void Start()
    {

    }

    void Update()
    {
        Vector3 worldPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (worldPosition.y > Screen.height - maxMoveLength)
        {
            transform.position -= Time.deltaTime * speed * new Vector3(0, 1, 0);
        }
    }
}

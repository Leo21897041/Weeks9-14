using UnityEngine;
using UnityEngine.InputSystem;

public class StrategyBuilder : MonoBehaviour
{
    public GameObject gameObjectPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (Mouse.current.leftButton.wasPressedThisFrame)
        { 
            Instantiate(gameObjectPrefab, currentMousePosition, Quaternion.identity);
        }
    }
}

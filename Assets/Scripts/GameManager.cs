using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int traveled_doors = 0;
    
    /*
        These values change whenever the player opens their inventory,
        journal, or opens the pause menu.
    */
    private CursorLockMode lockMode = CursorLockMode.Locked;
    private bool cursorVisibility = false;

    void Start()
    {
        Cursor.lockState = lockMode;
        Cursor.visible = cursorVisibility;
    }

    void Update()
    {

    }
}

using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class MouseDirection : MonoBehaviour
{
    [SerializeField] Vector2 startingPosition;
    Vector2 lastFrame;
    Vector2 CurrentFrame;
    public string stateX, stateY;
    public float strengthX, strengthY;
    public float strengthCap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastFrame = new Vector2(Screen.width / 2, Screen.height / 2);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    private void FixedUpdate()
    {
        CurrentFrame = Input.mousePosition;

        if (lastFrame.x  - CurrentFrame.x >= 0)
            stateX = "Left";

        else stateX = "Right";

        if (lastFrame.y - CurrentFrame.y >= 0)
            stateY = "Down";

        else stateY = "Up";

        if (CurrentFrame.x - lastFrame.x != 0 && CurrentFrame.y - lastFrame.y != 0)
            Debug.Log("X: " + stateX + "  Y: " + stateY);
        strengthX = Mathf.Clamp(Mathf.Abs(lastFrame.x - CurrentFrame.x), 0, strengthCap);
        strengthY = Mathf.Clamp(Mathf.Abs(lastFrame.y - CurrentFrame.y), 0, strengthCap);
        if(strengthX < 50 || strengthY < 50)
        {
            strengthX = 0.1f;
            strengthY = 0.1f;
        }
        Debug.Log(strengthX + "  " + strengthY);
        Mouse.current.WarpCursorPosition(lastFrame);
    }
}

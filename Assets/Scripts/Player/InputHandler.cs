using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 _deltaPosition;
    private Vector3 _previousPosition;
    public Vector2 DeltaPosition => _deltaPosition;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _previousPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            _deltaPosition = (_previousPosition - Input.mousePosition)*-speed;
            _deltaPosition.x /= Screen.width;
            _deltaPosition.y /= Screen.height;
            _previousPosition = Input.mousePosition;
        }
        else
        {
            _deltaPosition = Vector3.zero;
        }
    }
}

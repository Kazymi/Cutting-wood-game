using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Vector2 _deltaPosition;
    private Vector2 _previousPosition;
    private Camera _camera;
    
    public Vector2 DeltaPosition => _deltaPosition;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 currentPosition  = Input.mousePosition;
            _deltaPosition =( currentPosition - _previousPosition);
            _previousPosition = currentPosition;
        }
    }
}

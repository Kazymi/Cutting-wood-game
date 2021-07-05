using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private int speed;

    private void Update()
    {
        transform.Rotate(speed * Time.deltaTime,0, 0);
    }
}

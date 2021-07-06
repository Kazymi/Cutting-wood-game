using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private InstrumentControl instrumentControl;

    private void Start()
    {
        instrumentControl.Initialize(inputHandler);
    }
}

using UnityEngine;

public class Container : MonoBehaviour
{
    [field: SerializeField] public InputHandler inputHandler { get; private set; }
    [field: SerializeField] public MaterialManager materialManager { get; private set; }
}
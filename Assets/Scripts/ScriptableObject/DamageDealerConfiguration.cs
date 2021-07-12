using UnityEngine;

[CreateAssetMenu(fileName = "New damage dealer", menuName = "Damage dealer")]
public class DamageDealerConfiguration : ScriptableObject
{
    [SerializeField] private float radius;
    [SerializeField] private float damage;

    public float Radius => radius;
    public float Damage => damage;
}

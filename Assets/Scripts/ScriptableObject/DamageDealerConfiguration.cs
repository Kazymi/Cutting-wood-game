using UnityEngine;

[CreateAssetMenu(fileName = "New damage dealer", menuName = "Damage dealer")]
public class DamageDealerConfiguration : ScriptableObject
{
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float damage;
    [SerializeField] private float damagePolishing;

    public AnimationCurve AnimationCurve => _animationCurve;
    public float Damage => damage;
    public float DamagePolishing => damagePolishing;
}

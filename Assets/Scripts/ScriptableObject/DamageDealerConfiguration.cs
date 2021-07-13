using UnityEngine;

[CreateAssetMenu(fileName = "New damage dealer", menuName = "Damage dealer")]
public class DamageDealerConfiguration : ScriptableObject
{
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float damage;
    [SerializeField] private float damagePolishing;
    [SerializeField] private float reductionPolishing;
    [SerializeField] private float maxPolishing;

    public float ReductionPolishing => reductionPolishing;
    public float MaxPolishing => maxPolishing;
    public AnimationCurve AnimationCurve => _animationCurve;
    public float Damage => damage;
    public float DamagePolishing => damagePolishing;
}

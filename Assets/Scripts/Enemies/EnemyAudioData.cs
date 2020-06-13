using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Audio Data", menuName = "Treehouse Trouble/Enemy Audio Data", order = 1)]
public class EnemyAudioData : ScriptableObject {

    public AK.Wwise.Event DeathEffect;
    public AK.Wwise.Event AttackHouseEffect;
    public AK.Wwise.Event AttackPlayerEffect;
    public AK.Wwise.Event RangedAttackEffect;
}
using Game.Spells.Utils;

namespace Game.Spells
{
    public sealed class AoEBurstDamageSpell : AoESpell
    {
        public int Damage { get; private set; }

        public AoEBurstDamageSpell(AreaTargetingService areaTargeting) : base(areaTargeting) { }
        public AoEBurstDamageSpell(AreaTargetingService areaTargeting, int damage) : base(areaTargeting)
        {
            SetDamage(damage);
        }

        public void SetDamage(int damage)
        {
            Damage = damage;
        }

        public override void Cast()
        {
            base.Cast();

            foreach (var target in _targets)
            {
                target.Health.Reduce(Damage);
            }
        }
    }
}

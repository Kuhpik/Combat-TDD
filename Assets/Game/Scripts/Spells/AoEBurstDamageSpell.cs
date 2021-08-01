using Game.Characters;

namespace Game.Spells
{
    public sealed class AoEBurstDamageSpell : AoESpell
    {
        public int Damage => _user.Stats.SpellDamage * Factor;
        public int Factor => 2;
        public override float Area => 3f;
        readonly Character _user;

        public AoEBurstDamageSpell(Character user)
        {
            _user = user;
        }

        public override void Cast()
        {
            base.Cast();

            foreach (var target in _targets)
            {
                target.Stats.Health -= Damage;
            }
        }
    }
}

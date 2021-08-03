using Game.Characters;

namespace Game.Spells
{
    public class HealReboundSpell : ReboundSpell
    {
        protected override int Rebounds => 3;
        public int HealAmount => 50;

        public HealReboundSpell(Character caster) : base(caster) { }

        public override void Cast()
        {
            if (_caster.Team.IsEnemy(_target)) return;
            base.Cast();
        }

        public override void Affect(Character target)
        {
            target.Stats.Health += HealAmount;
        }
    }
}

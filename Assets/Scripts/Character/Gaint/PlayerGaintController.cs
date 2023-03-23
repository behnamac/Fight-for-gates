using Controllers;

namespace Character.Gaint
{
    public class PlayerGaintController : GaintController
    {
        protected override void Dead()
        {
            base.Dead();
            LevelManager.Instance.LevelFail();
        }

        public void ActiveAttack()
        {
            Attack();
        }
    }
}

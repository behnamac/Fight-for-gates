using Controllers;

namespace Character.Gaint
{
    public class EnemyGaintController : GaintController
    {
        protected override void Dead()
        {
            base.Dead();
            LevelManager.Instance.LevelComplete();
        }
        public void ActiveAttack()
        {
            Attack();
        }
    }
}

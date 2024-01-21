using Controllers;

namespace Character.Gaint
{
    public class EnemyGaintController : GaintController
    {
        protected override void Dead()
        {
            // Call the base Dead method
            base.Dead();

            // Trigger LevelComplete through LevelManager
            LevelManager.Instance.LevelComplete();
        }

        public void ActiveAttack()
        {
            // Call the Attack method
            Attack();
        }
    }
}

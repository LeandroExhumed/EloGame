using UnityEngine;

namespace DefaultCompany.Enemy
{
    public class MovementModel : IMovement
    {
        private readonly EnemyData data;

        private bool canMove = false;
        private float cooldownCounter = 0f;

        private readonly Transform transform;

        private readonly Transform target;
        private Vector3 targetPosition;

        public MovementModel(EnemyData data, Transform transform, Transform target)
        {
            this.data= data;
            this.transform = transform;
            this.target = target;
        }

        public void Initialize()
        {
            targetPosition = target.position;
            targetPosition.y += Random.Range(-0.03f, 0.03f);

            canMove = true;
        }

        public void Tick()
        {
            if (!canMove)
            {
                return;
            }

            if (Vector3.Distance(transform.position, targetPosition) >= data.DistanceToAttack)
            {
                Vector3 direction = (targetPosition - transform.position).normalized;
                Vector3 destination = transform.position + direction;
                transform.SetPositionAndRotation(
                    Vector3.MoveTowards(transform.position, destination, data.Speed * Time.deltaTime),
                    Quaternion.LookRotation(direction, Vector3.up)
                );
            }
            else
            {
                CheckAttackCondition();
            }
        }

        private void CheckAttackCondition()
        {
            if (cooldownCounter >= data.AttackRate)
            {
                if (target.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(data.AttackDamage);
                }
                cooldownCounter = 0f;
            }
            cooldownCounter += Time.deltaTime;
        }
    }
}
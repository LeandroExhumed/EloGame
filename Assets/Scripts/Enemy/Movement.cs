using UnityEngine;

namespace DefaultCompany.Enemy
{
    public class Movement : ITickable
    {
        private readonly float speed;

        private readonly int power;
        private readonly float attackCooldown;
        private float cooldownCounter = 0f;

        private readonly Transform transform;

        private const int TARGET_LAYERMASK = 1 << 6;
        private readonly Vector3 targetPosition;

        public Movement(float speed, int power, float attackCooldown, Transform transform, Vector3 targetPosition)
        {
            this.speed = speed;
            this.power = power;
            this.attackCooldown = attackCooldown;
            this.transform = transform;
            this.targetPosition = targetPosition;
        }

        public void Tick()
        {
            if (Vector3.Distance(transform.position, targetPosition) >= 0.02f)
            {
                Vector3 direction = (targetPosition - transform.position).normalized;
                Vector3 destination = transform.position + direction;
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }
            else
            {
                CheckAttackCondition();
            }
        }

        private void CheckAttackCondition()
        {
            if (cooldownCounter >= attackCooldown)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, TARGET_LAYERMASK))
                {
                    if (hit.transform.TryGetComponent(out IDamageable damageable))
                    {
                        damageable.TakeDamage(power);
                    }
                }
                cooldownCounter = 0f;
            }
            cooldownCounter += Time.deltaTime;
        }
    }
}

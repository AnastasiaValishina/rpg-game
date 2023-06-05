using UnityEngine;
using RPG.Attributes;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] bool followsTarget;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterImpact = 2;
        [SerializeField] UnityEvent onHit;

        Health target = null;
        GameObject instigator;
        float damage = 0;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }
        void Update()
        {
            if (target == null) return;

            if (followsTarget && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCollider = target.GetComponent<CapsuleCollider>();
            if (targetCollider == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCollider.height / 2;
        }

        public void SetTarget(Health target, GameObject instigator,float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;

            Destroy(gameObject, maxLifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            Health targetHealth = other.GetComponent<Health>();
            if (targetHealth != target) return;
            if (target.IsDead()) return;
            targetHealth.TakeDamage(instigator,damage);

            speed = 0;

            onHit.Invoke();

            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy.gameObject);
            }

            Destroy(gameObject, lifeAfterImpact);
        }
    }
}

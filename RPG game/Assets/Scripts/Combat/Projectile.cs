using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;

    Health target = null;
    float damage = 0;

    void Update()
    {
        if (target == null) return;
        transform.LookAt(GetAimLocation());
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

    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        Health targetHealth = other.GetComponent<Health>();
        if (targetHealth != target) return;
        targetHealth.TakeDamage(damage);
        Destroy(gameObject);
    }
}

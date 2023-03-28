using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Attributes;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Fighter fighter;
        Health health;
        void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (health.IsDead()) return;

            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetRay());
            foreach (RaycastHit hit in hits)
            {
                var target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!fighter.CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton(0))
                {
                    fighter.Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    mover.StartMoveAction(hit.point, 1f);
                }
                return true;
            }
            return false;
        }

        private static Ray GetRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}


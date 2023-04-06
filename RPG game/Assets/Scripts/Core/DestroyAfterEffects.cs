using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffects : MonoBehaviour
    {
        [SerializeField] GameObject targetToDestroy = null;

        private void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                if (targetToDestroy != null)
                {
                    Destroy(targetToDestroy);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}

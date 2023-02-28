using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad;
        bool isTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && !isTriggered)
            {
                SceneManager.LoadScene(sceneToLoad);

                isTriggered = true;
            }
        }
    }
}

using UnityEngine;

namespace RPG.Core
{    public class ActionScheduler : MonoBehaviour
    {
        MonoBehaviour currentAction;
        public void StartAction(MonoBehaviour action)
        {
            print("cancelling" + currentAction);
            currentAction = action;
        }
    }
}

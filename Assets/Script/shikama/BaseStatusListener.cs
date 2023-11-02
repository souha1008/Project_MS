using UnityEngine;
using UnityEngine.SceneManagement;


class BaseStatusListener : MonoBehaviour
{
    [SerializeField] BaseStatus[] baseStatuses;

    private void OnDisable()
    {
        foreach(BaseStatus baseStatus in baseStatuses)
        {
            //baseStatus.ResetCost();
        }
    }
}


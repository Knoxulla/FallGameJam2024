using UnityEngine;

public class ViewTarget : MonoBehaviour
{
    private void Start()
    {
        KeepObjectsInView.trackingCamera.GetComponent<KeepObjectsInView>().RegisterObjectForTracking(gameObject);
    }

    private void OnDisable()
    {
        KeepObjectsInView.trackingCamera.GetComponent<KeepObjectsInView>().UnregisterObject(gameObject);
    }
}

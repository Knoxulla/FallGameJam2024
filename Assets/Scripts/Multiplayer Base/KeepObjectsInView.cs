using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class KeepObjectsInView : MonoBehaviour
{
    public float minSize = 5;
    public float padding = 1f;

    public float timeToFocusInSeconds;
    private float currentTimeToFocus;
    [SerializeField] AnimationCurve lerpCurve;

    [HideInInspector] public static Camera trackingCamera;
    private Dictionary<int, GameObject> trackedObjectsInView = new();

    private Vector3 initialPos;
    private Vector3 desiredPos;

    private float initialOrthoSize;
    private float desiredOthoSize;

    private Bounds bounds;

    private Vector3 currentMin;
    private Vector3 currentMax;

    public void RegisterObjectForTracking(GameObject obj)
    {
        if (trackedObjectsInView.ContainsKey(obj.GetHashCode())) return;

        trackedObjectsInView.Add(obj.GetHashCode(), obj);
    }

    public void UnregisterObject(GameObject obj)
    {
        trackedObjectsInView.Remove(obj.GetHashCode());
    }

    private void Awake()
    {
        if (trackingCamera == null)
        {
            trackingCamera = GetComponent<Camera>();
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        initialPos = transform.position;
        initialOrthoSize = trackingCamera.orthographicSize;
        desiredOthoSize = trackingCamera.orthographicSize;
    }

    private void FixedUpdate()
    {
 

            desiredPos = new Vector3(bounds.center.x, bounds.center.y, trackingCamera.transform.position.z);

            if (trackedObjectsInView.Count == 0 || trackingCamera.transform.Equals(desiredPos)) return;

            CalculateBoundsExtents();

            desiredOthoSize = CalculateOrthographicSize(bounds.size);

            currentTimeToFocus += Time.deltaTime;

            if (currentTimeToFocus >= timeToFocusInSeconds)
            {
                ResetTracker();
            }
            else
            {
                float percent = currentTimeToFocus / timeToFocusInSeconds;
                float easing = lerpCurve.Evaluate(percent);

                trackingCamera.orthographicSize = Mathf.Lerp(initialOrthoSize, desiredOthoSize, easing);

                CalculateCameraPosition(easing);
            }
        
    }

    private void ResetTracker()
    {
        currentTimeToFocus = 0;

        initialPos = desiredPos;
        trackingCamera.transform.position = desiredPos;

        initialOrthoSize = desiredOthoSize;
        trackingCamera.orthographicSize = desiredOthoSize;
    }

    private void CalculateBoundsExtents()
    {
        Vector3 min = trackedObjectsInView.Aggregate(
            trackedObjectsInView.First().Value.GetComponent<Collider2D>().bounds.min,
            (current, next) => Vector3.Min(current, next.Value.GetComponent<Collider2D>().bounds.min));

        Vector3 max = trackedObjectsInView.Aggregate(
            trackedObjectsInView.First().Value.GetComponent<Collider2D>().bounds.max,
            (current, next) => Vector3.Max(current, next.Value.GetComponent<Collider2D>().bounds.max));

        min += (Vector3.left + Vector3.down) * padding;
        max += (Vector3.right + Vector3.up) * padding;

        if (currentMin.Equals(min) && currentMax.Equals(max)) return;

        bounds.SetMinMax(min, max);
    }

    private void CalculateCameraPosition(float t)
    {
        transform.position = Vector3.Lerp(initialPos, new Vector3(bounds.center.x, bounds.center.y, trackingCamera.transform.position.z), t);
    }

    private float CalculateOrthographicSize(Vector2 size)
    {
        float aspect = Screen.width / (float)Screen.height;
        float height = size.x / aspect;
        if (height < size.y) height = size.y;

        return Mathf.Max(minSize, height * 0.5f);
    }
}

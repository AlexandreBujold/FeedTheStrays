using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    [Header("Camera Tracking Properties")]
    public static MultipleTargetCamera instance;
    public List<Transform> targets;
    public bool ZoomIntoTargets = true;

    [Space]
    public Vector3 offset;
    public float movementSmoothTime = 0.5f;
    public float zoomSmoothTime = 0.5f;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    [Space]
    [Header("Shake")]
    public bool shake = true;
    public float duration = 0.25f;
    public float strength = 3f;

    private Vector3 velocity;
    private Camera gameCamera;
    private Bounds bounds;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (gameCamera == null)
        {
            gameCamera = GetComponentInChildren<Camera>();
        }
        bounds = new Bounds();
        bounds = SetBounds();
        transform.position = GetCenterPoint() + offset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (targets.Count == 0)
        {
            return;
        }

        bounds = SetBounds();

        Move();
        if (ZoomIntoTargets)
        {
            Zoom();
        }
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPos = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, movementSmoothTime);
    }

    private void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        if (gameCamera != null)
        {
            gameCamera.fieldOfView = Mathf.Lerp(gameCamera.fieldOfView, newZoom, Time.deltaTime);
        }
    }

    #region Bounds Calculation

    private Vector3 GetCenterPoint()
    {
        return bounds.center;
    }

    private float GetGreatestDistance()
    {
        return bounds.size.x;
    }

    private Bounds SetBounds()
    {
        //Create the initial bounds
        var newBounds = new Bounds(targets[0].position, Vector3.zero);
        foreach (Transform target in targets)
        {
            newBounds.Encapsulate(target.position);
        }
        return newBounds;
    }

    #endregion
}

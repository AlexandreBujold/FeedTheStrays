using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterFlow : MonoBehaviour
{
    [Header("Flow Properties")]
    public bool applyForce = true;
    public float pushForce = 5;
    public BoxCollider m_boxCollider;
    [Space]
    public List<Rigidbody> targetsBeingPushed;
    private Vector3 direction;

    private Vector3 pushDir;

    // Start is called before the first frame update
    void Start()
    {
        targetsBeingPushed = new List<Rigidbody>();
    }

    #region Item Pushing & Tracking

    /// <summary> Push the target rigidbody by force in the direction of the water flow </summary>
    public void PushTarget(Rigidbody target, float force)
    {
        if (null != target)
        {
            pushDir = GetForceAlongStreamDirection(force);
            target.AddForce(pushDir, ForceMode.Acceleration);
        }
    }

    public void AddTargetToList(Rigidbody target)
    {
        if (null != targetsBeingPushed && null != target)
        {
            targetsBeingPushed.Add(target);
        }
    }

    public bool RemoveTargetFromList(Rigidbody target)
    {
        if (null != targetsBeingPushed && null != target)
        {
            return targetsBeingPushed.Remove(target);
        }
        return false;
    }

    #endregion

    #region Calculation & GUI

    /// <summary> Return a given force strength aligned with the stream's direction </summary>
    public Vector3 GetForceAlongStreamDirection(float force)
    {
        UpdateDirection();
        Vector3 newDir = direction * force;
        return newDir;
    }

    private void UpdateDirection()
    {
        direction = transform.forward;
    }
    #endregion

    #region OnTrigger

    private void OnTriggerEnter(Collider other)
    {
        AddTargetToList(other.gameObject.GetComponent<Rigidbody>());
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody target = other.gameObject.GetComponent<Rigidbody>();
        if (target != null)
        {
            PushTarget(target, pushForce);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveTargetFromList(other.gameObject.GetComponent<Rigidbody>());
    }

    #endregion

    #region OnDraw

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, GetForceAlongStreamDirection(pushForce), Color.blue);
        DrawTrackedTargets();
    }

    private void DrawTrackedTargets()
    {
        Gizmos.color = Color.magenta;
        if (null != targetsBeingPushed)
        {
            foreach (Rigidbody rb in targetsBeingPushed)
            {
                if (null != rb)
                {
                    Gizmos.DrawWireCube(rb.transform.position, Vector3.one);
                }
            }
        }
    }

    #endregion
}

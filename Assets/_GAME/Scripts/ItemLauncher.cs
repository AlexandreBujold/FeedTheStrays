using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemLauncher : MonoBehaviour
{
    public List<Rigidbody> itemsInRange;
    public float travelTime = 2f;
    public float height = 10f;

    [Space]
    [Header("Testing")]
    public Transform leftDestination;
    public Transform rightDestination;

    // Start is called before the first frame update
    void Start()
    {
        itemsInRange = new List<Rigidbody>();
    }

    private void Update()
    {
        //! Testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int random = UnityEngine.Random.Range(1, 3);
            if (random == 1)
            {
                Debug.Log(LaunchItemAtPlayer1());
            }
            else
            {
                Debug.Log(LaunchItemAtPlayer2());
            }
        }
    }

    #region Launching

    public bool LaunchItemAtPlayer1()
    {
        return LaunchItem(leftDestination.position);
    }

    public bool LaunchItemAtPlayer2()
    {
        return LaunchItem(rightDestination.position);
    }

    public bool LaunchItem(Vector3 destination)
    {
        if (itemsInRange != null && itemsInRange.Count > 0)
        {
            itemsInRange[0].velocity = Vector3.zero;
            itemsInRange[0].isKinematic = true;
            StartCoroutine(LerpObject(itemsInRange[0], destination));
            return true;
        }
        return false;
    }

    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    public IEnumerator LerpObject(Rigidbody target, Vector3 destination)
    {
        Vector3 startPos = target.position;
        for (float t = 0; t <= 1; t += 1 / (travelTime / Time.deltaTime))
        {
            if (target != null)
            {
                target.position = Parabola(startPos, destination, startPos.y + height, t);
            }
            else
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    // public static bool CalculateTrajectory(float TargetDistance, float ProjectileVelocity, out float CalculatedAngle)
    // {
    //     CalculatedAngle = 0.5f * (Mathf.Asin((-Physics.gravity.y * TargetDistance) / (ProjectileVelocity * ProjectileVelocity)) * Mathf.Rad2Deg);
    //     if (float.IsNaN(CalculatedAngle))
    //     {
    //         CalculatedAngle = 0;
    //         return false;
    //     }
    //     return true;
    // }

    #endregion

    #region OnTrigger

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb != null && itemsInRange != null)
        {
            itemsInRange.Add(rb);
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb != null && itemsInRange != null)
        {
            itemsInRange.Remove(rb);
        }
    }

    #endregion

    #region OnDraw

    private void OnDrawGizmos()
    {
        DrawTrackedTargets();
    }

    private void DrawTrackedTargets()
    {
        Gizmos.color = Color.green;
        if (null != itemsInRange)
        {
            foreach (Rigidbody rb in itemsInRange)
            {
                if (null != rb)
                {
                    Gizmos.DrawWireCube(rb.transform.position, Vector3.one * 1.5f);
                }
            }
        }
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float followUpDistance = 10f;
    public float camDistance = -5;

    Rigidbody2D targetRb;

    private void Start()
    {
        targetRb = target.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, camDistance - (targetRb.velocity.sqrMagnitude / 10)), .75f);
    }

    bool IsNeedToFollow
    {
        get
        {
            return (transform.position - target.position).sqrMagnitude > followUpDistance * followUpDistance;
        }
    }
}

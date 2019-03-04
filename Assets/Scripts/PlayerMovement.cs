using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static bool wrongWay;

    public float maxSpeed = 60;
    public float driftSpeed = 1.5f;
    public float forwardSpeed = 8;
    public float backwardSpeed = 0.75f;

    Rigidbody2D playerRB;
    float lastTime = float.PositiveInfinity;
    float currentTime;
    bool brake;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        UIManager.Instance.UpdateLastTime(lastTime);
        UIManager.Instance.UpdateCurrentTime(currentTime);
    }

    void Update()
    {
        EngineCheck();
    }

    void FixedUpdate()
    {
        Drift();
        Brake();
        Accelerate();
        UIManager.Instance.UpdateVelocity(playerRB.velocity.sqrMagnitude);
        UIManager.Instance.UpdateCurrentTime(currentTime += Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag(Constants.FINISH_TAG))
            return;
        var renderer = collision.collider.GetComponent<SpriteRenderer>();
        if (wrongWay)
        {
            lastTime = 60;
            renderer.color = Color.red;
            StartCoroutine(RevertColorBack(renderer));
            UIManager.Instance.UpdateLastTime(lastTime);
            return;
        }
        StartCoroutine(RevertColorBack(renderer));
        renderer.color = Color.white;
        if (float.IsPositiveInfinity(lastTime))
        {
            lastTime = 60;
        }
        else if (currentTime < lastTime)
        {
            lastTime = currentTime;
            if (lastTime < 5) lastTime = 60;
        }
        currentTime = 0;
        UIManager.Instance.UpdateLastTime(lastTime);
    }

    IEnumerator RevertColorBack(SpriteRenderer renderer)
    {
        yield return new WaitForSeconds(1);
        renderer.color = Constants.LAP_COLOR;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        playerRB.AddForce(transform.right * (-DriftSpeed * 2 + 2));
    }

    void EngineCheck()
    {
#if UNITY_EDITOR
        brake = Input.GetKey(KeyCode.Space);
#else
        brake = (Input.touchCount == 2/* && Input.GetTouch(0).position.x < Screen.width / 2 && Input.GetTouch(1).position.x > Screen.width / 2*/);
#endif
    }

    void Accelerate()
    {
        if (!brake && playerRB.velocity.sqrMagnitude < maxSpeed * maxSpeed) playerRB.AddForce(transform.up * forwardSpeed);
    }

    void Accelerate(float speed)
    {
        if (!brake && playerRB.velocity.sqrMagnitude < maxSpeed * maxSpeed) playerRB.AddForce(transform.up * speed);
    }

    void Drift()
    {
        playerRB.AddTorque(DriftSpeed);
    }

    void Brake()
    {
        if (brake)
            if (playerRB.velocity.sqrMagnitude > 0.1f) playerRB.AddForce(-playerRB.velocity * backwardSpeed);
            else playerRB.velocity = Vector2.zero;
    }

    float DriftSpeed
    {
        get
        {
#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.LeftArrow))
                return 1 * driftSpeed;
            else if (Input.GetKey(KeyCode.RightArrow))
                return -1 * driftSpeed;
            else return 0;
#else
            if (Input.touchCount == 1)
                return (Input.GetTouch(0).position.x < Screen.width / 2 ? 1 : -1) * driftSpeed;
            else return 0;
#endif
        }
    }
}
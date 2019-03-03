using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text velocityText;
    public Text lastLapTimeText;
    public Text currentTimeText;

    static float lastTapTime;

    void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.realtimeSinceStartup - lastTapTime < .5f && lastTapTime > 0)
                Application.Quit();
            else
                SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % 2);
            lastTapTime = Time.realtimeSinceStartup;
            PlayerMovement.wrongWay = false;
        }
    }

    public void UpdateLastTime(float lastTime)
    {
        if (IsGameplayScene)
            lastLapTimeText.text = string.Format("Last Lap: {0}", lastTime.ToString("0.00"));
    }

    public void UpdateCurrentTime(float currentTime)
    {
        if (IsGameplayScene)
            currentTimeText.text = string.Format("Current: {0}", currentTime.ToString("0.00"));
    }

    public void UpdateVelocity(float velocity)
    {
        if (IsGameplayScene)
            velocityText.text = string.Format("Velocity: {0}", velocity.ToString("0.00"));
    }

    public static bool IsGameplayScene
    {
        get
        {
            return SceneManager.GetActiveScene().buildIndex == 0;
        }
    }
}

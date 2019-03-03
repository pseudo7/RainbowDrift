using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerMovement.wrongWay = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement.wrongWay = false;
    }
}

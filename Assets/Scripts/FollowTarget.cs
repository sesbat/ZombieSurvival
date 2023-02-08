using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = GameManager.instance.player.transform.position;
    }
}

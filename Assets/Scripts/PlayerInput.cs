using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private string moveVName = "Vertical";
    private string moveHName = "Horizontal";
    public float moveV { get; set; }
    public float moveH { get; set; }

    private void Update()
    {
        moveV = Input.GetAxisRaw(moveVName);
        moveH = Input.GetAxisRaw(moveHName);
    }
}

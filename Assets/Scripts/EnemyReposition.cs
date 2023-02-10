using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReposition : MonoBehaviour
{
    private void LateUpdate()
    {
        var pInputVec = GameManager.instance.player.inputVec;
        var playerPos = GameManager.instance.player.transform.position;
        var distance = (transform.position - playerPos).magnitude;

        if(distance > 30f)
        {
            transform.Translate(pInputVec * 50 +
                new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f)));
        }
    }
}

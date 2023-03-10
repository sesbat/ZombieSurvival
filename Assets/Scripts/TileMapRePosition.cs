using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapRePosition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;
        
        Vector3 playerPos = GameManager.instance.player.transform.position; //플레이어 포지션
        Vector3 myPos = transform.position;
        float disX = Mathf.Abs(playerPos.x - myPos.x);
        float disY = Mathf.Abs(playerPos.y - myPos.y); 

        Vector3 playerDir = GameManager.instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch(transform.tag)
        {
            case "Ground":
                {
                    if(disX>disY)
                    {
                        transform.Translate(Vector3.right * dirX * 40);
                    }
                    else if(disX<disY)
                    {
                        transform.Translate(Vector3.up * dirY * 40); //40 = 맵 크기
                    }
                }
                break;
        }
    }
}

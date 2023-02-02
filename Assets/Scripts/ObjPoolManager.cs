using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjPoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public List<GameObject>[] objectPools;

    private void Awake()
    {
        objectPools = new List<GameObject>[prefabs.Length];

        for(int index = 0; index<prefabs.Length; index++)
        {
            objectPools[index] = new List<GameObject>();
        }
    }
    public GameObject Get(int index)
    {
        GameObject choice = null;
        foreach(GameObject obj in objectPools[index])
        {
            if(!obj.activeSelf)
            {
                choice = obj;
                choice.SetActive(true);
                break;
            }
        }
        if(choice ==null)
        {
            choice = Instantiate(prefabs[index],transform);
            objectPools[index].Add(choice);
        }

        return choice;
    }
}

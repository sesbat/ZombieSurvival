using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    public Bullet[] prefabs;
    private IObjectPool<Bullet>[] bulletPool;

    private void Awake()
    {
        bulletPool = new IObjectPool<Bullet>[prefabs.Length];
        for (int i = 0; i < prefabs.Length; ++i)
        {
            var index = i;
            bulletPool[index] = new ObjectPool<Bullet>(
                () =>
                {
                    var bullet = Instantiate(prefabs[index]);
                    bullet.SetManagePool(bulletPool[index]);
                    return bullet;
                },
                Get, Release, Destroy, maxSize: 30);
        }

    }
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        switch (id)
        {
            case 0:
                {
                    transform.Rotate(Vector3.forward*speed*Time.deltaTime);
                }
                break;
            default:
                break;
        }
        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }
    }
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Deployment();
    }
    public void Init()
    {
        switch(id)
        {
            case 0:
                {
                    speed = -150;
                    Deployment();
                }
                break;
            case 1:
                {

                }
                break;
            case 2:
                {

                }
                break;
            case 3:
                {

                }
                break;
            default:
                break;
        }
    }
    void Deployment()
    {
        for(int index =0; index<count; index++)
        {
            Transform bullet;
            if (index<transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = bulletPool[prefabId].Get().transform;
                bullet.parent = transform;
            }
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotateVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotateVec);
            bullet.Translate(bullet.up*1.5f,Space.World);
            bullet.GetComponent<Bullet>().Init(damage,-1); // -1 = infinity per
        }
    }
    public Bullet Get(int index)
    {
        return bulletPool[index].Get();
    }

    public void Get(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }
    public void Release(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
    public void Destroy(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    private float timer;
    private Player player;

    public Bullet[] prefabs;
    private IObjectPool<Bullet>[] bulletPool;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
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
        foreach (var bullet in prefabs)
        {
            switch (bullet.id)
            {
                case 0:
                    {
                        transform.Rotate(Vector3.forward * bullet.speed * Time.deltaTime);
                    }
                    break;
                case 1:
                    {
                        timer += Time.deltaTime;

                        if (timer > bullet.speed)
                        {
                            timer = 0f;
                            Fire();
                        }
                    }
                    break;
                default:

                    break;
                   
            }
            if (Input.GetButtonDown("Jump"))
            {
                LevelUp(20, 5);
            }
        }
    }
    public void LevelUp(float damage, int count)
    {
        foreach (var bullet in prefabs)
        {
            bullet.damage = damage;
            bullet.count += count;

            if (bullet.id == 0)
                Deployment();
        }
    }
    public void Init()
    {
        foreach (var bullet in prefabs)
        {
            switch (bullet.id)
            {
                case 0:
                    {
                        bullet.speed = 150;
                        Deployment();
                    }
                    break;
                case 1:
                    {
                        bullet.speed = 0.3f;
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
    }
    public void Fire()
    {
        if (!player.scanner.nearTarget)
            return;

        Vector3 position = player.scanner.nearTarget.position;
        Vector3 dir = (position - transform.position).normalized;

        Transform bullet = bulletPool[prefabs[1].prefabId].Get().transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(prefabs[1].damage, prefabs[1].count, dir);
    }
    public void Deployment()
    {
        for(int index =0; index<prefabs[0].count; index++)
        {
            Transform bullet;
            if (index<transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = bulletPool[prefabs[0].prefabId].Get().transform;
                bullet.parent = transform;
            }
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotateVec = Vector3.forward * 360 * index / prefabs[0].count;
            bullet.Rotate(rotateVec);
            bullet.Translate(bullet.up*1.5f,Space.World);
            bullet.GetComponent<Bullet>().Init(prefabs[0].damage, -1,Vector3.zero); // -1 = infinity per
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

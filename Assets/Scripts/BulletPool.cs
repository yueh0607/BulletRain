using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool
{
    private ObjectPool<BulletBehaviour> pool;
    public BulletObject bullet;


    public BulletBehaviour GetItem() => pool.Get();

    public void ReleaseItem(BulletBehaviour bh) => pool.Release(bh);
   
    public BulletPool()
    {
        pool = new ObjectPool<BulletBehaviour>(OnCreateItem, OnGetItem, OnRelesaeItem, OnDestroyItem);

       
    }

    //��������ڴ���ʱ����
    private BulletBehaviour OnCreateItem()
    {
        var bh = GameObject.Instantiate(bullet.prefabs).AddComponent<BulletBehaviour>();
        InitBullet(bh);
        bh.pool = this;
        return bh;
    }
    private void InitBullet(BulletBehaviour bh)
    {
        bh.LinearVelocity = bullet.LinearVelocity;
        bh.Acceleration = bullet.Acceleration;
        bh.AngularAcceleration = bullet.AngularAcceleration;
        bh.AngularVelocity = bullet.AngularVelocity;
        bh.LifeTime = bullet.LifeCycle;
        bh.MaxVelocity = bullet.MaxVelocity;
    }

    private void OnDestroyItem(BulletBehaviour bh)
    {
        //��������
        GameObject.Destroy(bh.gameObject);
    }
    private void OnRelesaeItem(BulletBehaviour bh)
    {
        //��������
        bh.gameObject.SetActive(false);
    }
    private void OnGetItem(BulletBehaviour bh)
    {
        //��ʼ��
        InitBullet(bh);
        //��ʾ����
        bh.gameObject.SetActive(true);
        
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    //���ֵ�
    public Dictionary<int , BulletPool> pools = new Dictionary<int, BulletPool>();

    /// <summary>
    /// ���ض�Ӧ�Ķ����
    /// </summary>
    /// <param name="bullet"></param>
    /// <returns></returns>
    public BulletPool GetPool(BulletObject bullet)
    {
        if(!pools.ContainsKey(bullet.ID))
        {
            var pool = new BulletPool();
            pool.bullet = bullet;
            pools.Add(bullet.ID, pool);
        }
        return pools[bullet.ID];
    }

}

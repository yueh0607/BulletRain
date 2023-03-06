using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    //池字典
    public Dictionary<int , BulletPool> pools = new Dictionary<int, BulletPool>();

    /// <summary>
    /// 返回对应的对象池
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

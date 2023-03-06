using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sender : MonoBehaviour
{
    
    public BulletObject bullet;

    public float currentAngle = 0; //当前发射角度
    public float currentAngularVelocity = 0;//当前角速度
    public float currentTime = 0; //剩余生命时间
    private void Awake()
    {
        //创建对象池，绑定配置文件
        pool = PoolManager.Instance.GetPool(bullet);
       
        currentAngle = bullet.InitRotation;//初始旋转
        currentAngularVelocity = bullet.SenderAngularVelocity; //当前初始角速度
    }

    private void FixedUpdate()
    {
        // v = a * t
        currentAngularVelocity =Mathf.Clamp(currentAngularVelocity+ bullet.SenderAcceleration * Time.fixedDeltaTime,
            -bullet.SenderMaxAngularVelocity,bullet.SenderMaxAngularVelocity);
        //更新角度
        currentAngle += currentAngularVelocity * Time.fixedDeltaTime;
        //限制角度
        if(Mathf.Abs(currentAngle )>720f)
        {
            currentAngle -= Mathf.Sign(currentAngle)* 360f;
        }

        //更新时间
        currentTime += Time.fixedDeltaTime;
        if(currentTime>bullet.SendInterval)
        {
            currentTime-=bullet.SendInterval;
            SendByCount(bullet.Count, currentAngle);
        }

    }
    private void SendByCount(int count,float angle)
    {
        float temp = count%2==0 ? angle+bullet.LineAngle/2: angle;
        //遍历每一条线
        for(int i=0;i<count;++i)
        {
            temp += Mathf.Pow(-1, i) * i * bullet.LineAngle;
            Send(temp);
        }  
    }


    public BulletPool pool;

    private void Send(float angle)
    {
        
        var bh = pool.GetItem();
        bh.gameObject.transform.position = transform.position;
        //设置子弹初始旋转
        bh.gameObject.transform.rotation = Quaternion.Euler(0,0,angle);     
    }


}

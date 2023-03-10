using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    //线速度
    public float LinearVelocity = 0;
    //线加速度
    public float Acceleration = 0;
    //角速度
    public float AngularVelocity = 0;
    //角加速度
    public float AngularAcceleration = 0;
    //最大速度
    public float MaxVelocity = int.MaxValue;

    //生命周期
    public float LifeTime = 5f;
    public BulletPool pool;

    //0.02s执行一次，一秒50次
    private void FixedUpdate()
    { 
        //更新当前的线速度和角速度
        LinearVelocity = Mathf.Clamp(LinearVelocity+Acceleration * Time.fixedDeltaTime,-MaxVelocity,MaxVelocity);
        AngularVelocity += AngularAcceleration * Time.fixedDeltaTime;
        //更新子弹位置
        transform.Translate(LinearVelocity * Vector2.right * Time.fixedDeltaTime,Space.Self);
        transform.rotation *= Quaternion.Euler(new Vector3(0,0,1)*AngularVelocity*Time.fixedDeltaTime);


        //生命结束销毁物体
        LifeTime -= Time.fixedDeltaTime;

        if(LifeTime <=0)
        {
            pool.ReleaseItem(this);
        }

    }

}

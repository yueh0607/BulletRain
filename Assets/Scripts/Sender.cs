using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sender : MonoBehaviour
{
    
    public BulletObject bullet;

    public float currentAngle = 0; //��ǰ����Ƕ�
    public float currentAngularVelocity = 0;//��ǰ���ٶ�
    public float currentTime = 0; //ʣ������ʱ��
    private void Awake()
    {
        //��������أ��������ļ�
        pool = PoolManager.Instance.GetPool(bullet);
       
        currentAngle = bullet.InitRotation;//��ʼ��ת
        currentAngularVelocity = bullet.SenderAngularVelocity; //��ǰ��ʼ���ٶ�
    }

    private void FixedUpdate()
    {
        // v = a * t
        currentAngularVelocity =Mathf.Clamp(currentAngularVelocity+ bullet.SenderAcceleration * Time.fixedDeltaTime,
            -bullet.SenderMaxAngularVelocity,bullet.SenderMaxAngularVelocity);
        //���½Ƕ�
        currentAngle += currentAngularVelocity * Time.fixedDeltaTime;
        //���ƽǶ�
        if(Mathf.Abs(currentAngle )>720f)
        {
            currentAngle -= Mathf.Sign(currentAngle)* 360f;
        }

        //����ʱ��
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
        //����ÿһ����
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
        //�����ӵ���ʼ��ת
        bh.gameObject.transform.rotation = Quaternion.Euler(0,0,angle);     
    }


}

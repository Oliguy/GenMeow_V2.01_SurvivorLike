using DG.Tweening;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DilucSkill : MeowSkillBase
{
    /*
    [Header("迪卢克技能专属")]
    public float acceleration;*/

    //自定义数据结构
    public struct PointWithRotation
    {
        public Vector3 Position;
        public Vector3 Rotation;

        public PointWithRotation(Vector3 pos, Vector3 rot)
        {
            Position = pos;
            Rotation = rot;
        }
    }

    public override void ActiveSkill(float damageBonus = 0, float projectileNumBonus = 0, float releaseTimeBonus = 0,float rangeBonus = 0f)
    {
        base.ActiveSkill(damageBonus, projectileNumBonus, releaseTimeBonus);
        _range = baseRange + rangeBonus;


        Vector3 _generatePos = GameCopilot.Instance.MeowStatus.transform.position;
        PointWithRotation[] myPoints = CalculatePoints(_generatePos, baseProjectileNum + (int)projectileNumBonus, _range);
        for (int i = 0; i < myPoints.Length; i++)
        {
            DamageOnTouch instance =  Instantiate(damagePart, myPoints[i].Position, Quaternion.Euler(myPoints[i].Rotation));
            DilucSkill_Liming Liming = instance.GetComponent<DilucSkill_Liming>();
            Vector2 accelerationDirection = new Vector2(-Mathf.Sin((-myPoints[i].Rotation.z + 90) * Mathf.Deg2Rad), -Mathf.Cos((-myPoints[i].Rotation.z + 90) * Mathf.Deg2Rad));
            Liming.SetInit(accelerationDirection);
        }
        Debug.Log("调用Diluc技能！");
    }




    public PointWithRotation[] CalculatePoints(Vector2 Pos, int num, float distance)
    {
        PointWithRotation[] points = new PointWithRotation[num];

        // 每份的角度（弧度）
        float angleStep = 2 * Mathf.PI / num;

        for (int i = 0; i < num; i++)
        {
            // 计算当前点的角度
            float angle = i * angleStep;

            // 计算当前点的坐标
            float x = Pos.x + distance * Mathf.Sin(angle);
            float y = Pos.y + distance * Mathf.Cos(angle);

            // 计算当前点的旋转角度，这里假设z轴方向旋转
            float rotationZ = -90 - angle * Mathf.Rad2Deg; // 转换为角度并调整旋转方向

            // 把计算出的点和旋转保存到数组中
            points[i] = new PointWithRotation(new Vector3(x, y, 0), new Vector3(0, 0, rotationZ));
        }

        return points;
    }


}

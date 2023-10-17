using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeQingSkill : MeowSkillBase
{
    [Header("刻晴技能专属")]
    public GameObject KeQingSprite;
    protected List<GameObject> KeQingSpriteList;


    public override void ActiveSkill(float damageBonus = 0, float projectileNumBonus = 0, float releaseTimeBonus = 0,float rangeBonus = 0f)
    {
        base.ActiveSkill(damageBonus, projectileNumBonus, releaseTimeBonus);
        _range = baseRange + rangeBonus;
        Stage1();

    }

    private void Stage1()
    {
        KeQingSpriteList = new();

        Vector3 _generatePos = GameCopilot.Instance.MeowStatus.transform.position;

        KeQingSpriteList.Add(Instantiate(KeQingSprite, new Vector3(_generatePos.x + 0.5f * _range, _generatePos.y - 0.6f * _range, transform.position.z), Quaternion.Euler(0, 120, 0)));
        KeQingSpriteList.Add(Instantiate(KeQingSprite, new Vector3(_generatePos.x - 0.8f * _range, _generatePos.y + 0.4f * _range, transform.position.z), Quaternion.Euler(0, 0, 0)));
        KeQingSpriteList.Add(Instantiate(KeQingSprite, new Vector3(_generatePos.x + 0.8f * _range, _generatePos.y + 0.4f * _range, transform.position.z), Quaternion.Euler(0, 180, 0)));
        KeQingSpriteList.Add(Instantiate(KeQingSprite, new Vector3(_generatePos.x - 0.5f * _range, _generatePos.y - 0.6f * _range, transform.position.z), Quaternion.Euler(0, 60, 0)));
        KeQingSpriteList.Add(Instantiate(KeQingSprite, new Vector3(_generatePos.x, _generatePos.y + _range,transform.position.z),Quaternion.Euler(0, 50, 0)));
        foreach(var KQ in KeQingSpriteList)
        {
            KQ.GetComponent<SpriteRenderer>().enabled = false;
        }
        var _thunder = ThunderInstantiate(transform.position);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_thunder.transform.DOMove(KeQingSpriteList[0].transform.position,0.2f).OnComplete(() =>
        {
            KeQingSpriteList[0].GetComponent<SpriteRenderer>().enabled = true;
        }));
        sequence.Append(_thunder.transform.DOMove(KeQingSpriteList[1].transform.position, 0.2f).OnComplete(() =>
        {
            KeQingSpriteList[1].GetComponent<SpriteRenderer>().enabled = true;
        }));
        sequence.Append(_thunder.transform.DOMove(KeQingSpriteList[2].transform.position, 0.2f).OnComplete(() =>
        {
            KeQingSpriteList[2].GetComponent<SpriteRenderer>().enabled = true;
        }));
        sequence.Append(_thunder.transform.DOMove(KeQingSpriteList[3].transform.position, 0.2f).OnComplete(() =>
        {
            KeQingSpriteList[3].GetComponent<SpriteRenderer>().enabled = true;
        }));
        sequence.Append(_thunder.transform.DOMove(KeQingSpriteList[4].transform.position, 0.2f).OnComplete(() =>
        {
            KeQingSpriteList[4].GetComponent<SpriteRenderer>().enabled = true;
        }));
        sequence.Append(_thunder.transform.DOMove(KeQingSpriteList[0].transform.position, 0.2f).OnComplete(() =>
        {
            _thunder.GetComponent<KeQingSkillThunder>().DelayDestroy(0.1f,false);
            this.Stage2(5);
        }
            ));


        sequence.Play();


    }

    public void Stage2(int _times)
    {
        for (int i = 0; i < _times; i++)
        {
            var _t = ThunderInstantiate(KeQingSpriteList[i % KeQingSpriteList.Count].transform.position);
            _t.GetComponent<KeQingSkillThunder>().RandomEject(5, KeQingSpriteList, i % KeQingSpriteList.Count);
        }
        foreach(var _sprite in KeQingSpriteList)
        {
            Destroy(_sprite.gameObject, 0.65f);
        }
    }

    private GameObject ThunderInstantiate(Vector3 _pos)
    {
        GameObject _thunder = Instantiate(damagePart.gameObject, _pos, Quaternion.identity);
        //TODO:更改DamageOnTouch
        return _thunder;
    }


}

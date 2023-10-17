using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationManager : MMSingleton<ConstellationManager>
{
    public List<ConstellationEffect> _inherentsEffects;
    public List<ConstellationRequirement> requires;
    public List<ConstellationEffect> effects;


    protected override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // 如果已经有一个实例存在并且不是当前实例，那么销毁当前实例
            Destroy(gameObject);
        }
        else
        {
            // 否则，这个实例就是我们的单例，不要在加载新场景时销毁它。
            base.Awake();
            DontDestroyOnLoad(this);
        }
    }

    protected virtual void OnEnable()
    {
        GenMeowEvent.GameStart += ApplyInherents;
    }
    private void OnDisable()
    {
        GenMeowEvent.GameStart -= ApplyInherents;
    }


    public void SetConstellations(ConstellationSet _set)
    {
        _inherentsEffects.Clear();
        requires.Clear();
        effects.Clear();
        //清除原先列表
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        //处理固有命座
        foreach (InherentConstellationData data in _set.inherents)
        {
            _inherentsEffects.Add(Instantiate(data.effect, this.transform));
        }


        for(int i = 0;i< _set.constellations.Count; i++)
        {
            ConstellationRequirement require = Instantiate(_set.constellations[i].requirement, this.transform);
            requires.Add(require);
            require.isActived = false;
            require.RequireIdInSet = (i+1);

            ConstellationEffect effect = Instantiate(_set.constellations[i].effect, this.transform);
            effects.Add(effect);
            effect.EffectIdInSet = (i + 1);
        }
    }

    public void ApplyInherents()
    {
        var _inherents = GetComponentsInChildren<InherentConstellationData>();
        foreach(var _inherent in _inherents)
        {
            _inherent.effect.ApplyEffect();
        }
    }

    public void ActiveConstellation(int _id)
    {
        foreach(var effect in effects)
        {
            if (effect.EffectIdInSet == _id)
            {
                effect.ApplyEffect();
                MessageUIManager.Instance?.ConstellationActiveMessage(effect);
            }
        }
    }
    public bool IsConstellationActived(int _id)
    {
        return requires[_id-1].isActived;
    }
}

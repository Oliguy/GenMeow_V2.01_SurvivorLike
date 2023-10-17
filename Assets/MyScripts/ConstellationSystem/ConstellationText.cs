using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstellationText : MonoBehaviour
{
    public Text TextTemplate;

    private void OnEnable()
    {
        ModifyDetails();
    }

    public void ModifyDetails()
    {
        foreach (Transform _i in transform)
        {
            Destroy(_i.gameObject);
        }

        foreach (var _d in ConstellationManager.Instance._inherentsEffects)
        {
            Text _t = Instantiate(TextTemplate, transform);
            _t.text = "<color=#C7B287>" + _d.effectName + "</color>：";
            _t.text += _d.effectDescription;
            Debug.Log(_t.text);
        }

        foreach(var _d in ConstellationManager.Instance.effects)
        {
            Text _t = Instantiate(TextTemplate, transform);
            _t.text = "<color=#C7B287>" + _d.effectName + "</color>：";
            _t.text += _d.effectDescription;
            Debug.Log(_t.text);
        }
    }

}

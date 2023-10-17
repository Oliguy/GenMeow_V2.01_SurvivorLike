using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MMSingleton<CharacterSelectManager>
{
    protected CharacterTemplate character;
    [Header("显示人物")]
    public Image characterIcon; //头像
    public Image characterBG;//人物背景
    public Text characterName_txt;//人物名称

    [Header("人物描述")]
    public GameObject CharacterDetailsPanel;//板子，控制是否激活

    [Header("按钮")]
    public Button gamestartButton;



    private void Start()
    {
        CharacterDetailsPanel.SetActive(false);
        characterIcon.enabled = false;
        characterBG.enabled = false;
        characterBG.sprite = null;
        characterName_txt.enabled = false;
        gamestartButton.interactable = false;

    }

    public void SelectCharacter(CharacterTemplate _character)
    {
        foreach (Transform _i in transform)
        {
            Destroy(_i.gameObject);
        }
        character = _character;
        CharacterInfoManager.Instance.Character = _character;
        ConstellationManager.Instance.SetConstellations(_character.constellationSet);
        ModifyImg(character);
        //ModifyDetails(SetDescription());
        gamestartButton.interactable = true;
        
    }

    public void ModifyImg(CharacterTemplate _character)
    {
        characterBG.enabled=true;
        characterName_txt.enabled = true;
        CharacterDetailsPanel.SetActive(true);
        characterIcon.enabled = true;
        characterBG.sprite = _character.characterSkillUseBGSprite;
        characterIcon.sprite = _character.characterIconSprite;
        characterName_txt.text = _character.characterNameText.text;
        CharacterDetailsPanel.GetComponentInChildren<ConstellationText>().ModifyDetails();
    }

}

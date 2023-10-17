using UnityEngine;
using System;

[System.Serializable]
public class EnemyData
{
    public int ID;
    public EnemyStatus status;
}

[System.Serializable]
public class EnemyGenerateInfo
{
    public int enemyID;
    public int everyGenerateNum;
    public int generateInterval;
}

[System.Serializable]
public class ItemData
{
    public int itemID { 
        get { return this.itemGameObject.itemID; } 
        set { this.itemGameObject.itemID = value; }
    }
    public Sprite itemIcon
    {
        get { return this.itemGameObject.itemIcon; }
        set { this.itemGameObject.itemIcon = value; }
    }
    public string itemName
    {
        get { return this.itemGameObject.itemName; }
        set { this.itemGameObject.itemName = value; }
    }
    public ItemRarity itemRarity
    {
        get { return this.itemGameObject.itemRarity; }
        set { this.itemGameObject.itemRarity = value; }
    }
    public ItemType itemType
    {
        get { return this.itemGameObject.itemType; }
        set { this.itemGameObject.itemType = value; }
    }
    public int itemBasePrice
    {
        get { return this.itemGameObject.itemBasePrice; }
        set { this.itemGameObject.itemBasePrice = value; }
    }
    public ItemTypeDataDetails itemGameObject;

    public string propertyDescription
    {
        get { return this.itemGameObject.propertyDescription; }
        set { this.itemGameObject.propertyDescription = value; }
    }
    public string effectDescription
    {
        get { return this.itemGameObject.effectDescription; }
        set { this. itemGameObject.effectDescription = value; }
    }
    public string attachDescription
    {
        get { return this.itemGameObject.attachDescription; }
        set { this.itemGameObject.attachDescription = value; }
    }
}

[System.Serializable]
public struct ItemMainProperty
{
    public MainProperty mainProperty;
    public int propertyValue;
}

[System.Serializable]
public class MeowBallDamageBonus
{
    public DamageBonus damageBonus;
    public int propertyPercentValue;
}

[System.Serializable]
public class MeowSkillDetails
{
    public int skillID;
    public MeowSkillBase meowSkill;
}

[System.Serializable]
public class CharacterPropertyDescriptionData
{
    public string propertyName;
    public string propertyEffect;
}


[System.Serializable]
public class CharacterData
{
    public int characterID;
    public CharacterTemplate characterT;

}

[System.Serializable]
public class ItemEffectData
{
    public CombatEventTriggerTimeEnum triggerTime;
    public BuffEnum buffEffect;
    public MainProperty mainProperty;
    public int num;
    public int probability;
}


[System.Serializable]
public class LevelExpInfo
{
    public int level;
    public int exp;
}
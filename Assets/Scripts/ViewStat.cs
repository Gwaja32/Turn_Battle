using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewStat : MonoBehaviour
{
    public Text text;
    void Start()
    {
        
    }
    
    void Update()
    {
        int atk = PlayerStat.atk;
        int def = PlayerStat.def;
        int heal = PlayerStat.maxHP;
        float crit = PlayerStat.crit;
        float critDam = PlayerStat.critDamage;
        text.text = $"Attack : {atk}\nDefense : {def}\nHealth : {heal}\nCritical : {crit * 100f}%\nCirtical Damage : {critDam * 100f}%\nGold : {Gold.gold}G";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Artifact", menuName = "New Artifact/artifact")]
public class Artifact : ScriptableObject
{
    public int itemLevel = 1; // 아티팩트 레벨
    public int price = 0; // 가격

    public int attack = 0; // 공격력
    public int defense = 0; // 방어력
    public int health = 0; // 체력
    public float crit = 0; // 치명타 확률
    public float critDamage = 0; // 치명타 피해량

    public Sprite itemImage; // 아티팩트 이미지
}

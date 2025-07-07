using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Artifact", menuName = "New Artifact/artifact")]
public class Artifact : ScriptableObject
{
    public int itemLevel = 1; // ��Ƽ��Ʈ ����
    public int price = 0; // ����

    public int attack = 0; // ���ݷ�
    public int defense = 0; // ����
    public int health = 0; // ü��
    public float crit = 0; // ġ��Ÿ Ȯ��
    public float critDamage = 0; // ġ��Ÿ ���ط�

    public Sprite itemImage; // ��Ƽ��Ʈ �̹���
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static int level = 1;

    public static int atk = 5;
    public static int def = 5;
    public static int maxHP = 20;
    public static int heal = 4;

    public static float critDamage = 1.25f;
    public static float crit = 0.03f;

    public static float atkModifier = 1f;
    public static float defModifier = 1f;
    public static float healModifier = 1f;
    public static float critModifier = 1f;
}

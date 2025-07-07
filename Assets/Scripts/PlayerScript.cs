using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private EnemyScript enemyScript;

    public int PlayerHP { get; set; } = 0;

    public void Start()
    {
        enemyScript = GetComponent<EnemyScript>();
    }

    public void Update()
    {

    }

    public void InitiateHP()
    {
        PlayerHP = PlayerStat.maxHP;
    }

    public int HealPlayer(int heal)
    {
        int res = (int)(heal * PlayerStat.healModifier);
        PlayerHP += res;

        return res;
    }

    public int AttackEnemy(int atk)
    {
        float finalDamage = atk * PlayerStat.atkModifier;
        float critFactor = Random.Range(0f, 1f);
        if (critFactor < PlayerStat.crit)
        {
            finalDamage *= 2f;
        }

        enemyScript.EnemyHP -= (int)finalDamage;
        return (int)finalDamage;
    }
}

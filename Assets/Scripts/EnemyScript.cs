using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerScript playerScript;

    public int EnemyHP { get; set; }
    public int EnemyMaxHP { get; set; }
    public int EnemyAtk { get; set; }

    public void Start()
    {
        gameManager = GetComponent<GameManager>();
        playerScript = GetComponent<PlayerScript>();
    }

    public void Update()
    {

    }

    public int AttackPlayer(int atk)
    {
        int res = (int)(atk / PlayerStat.defModifier);
        playerScript.PlayerHP -= res;

        return res;
    }

    public void GenerateEnemyDemo(int stage)
    {
        EnemyMaxHP = 10 + stage * 2 + Random.Range(0, 2);
        EnemyHP = EnemyMaxHP;
        EnemyAtk = 2 + (stage - 1);
    }
}

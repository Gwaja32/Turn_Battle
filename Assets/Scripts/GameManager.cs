using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{
    public int stage = 1;
    State state = State.NoBattle;

    private PlayerScript playerScript;
    private EnemyScript enemyScript;
    private DelayScript delayScript;


    public TMPro.TMP_Text textDisplay;
    public TMPro.TMP_Text stageDisplay;

    public TMPro.TMP_Text playerHPDisplay;
    public TMPro.TMP_Text enemyHPDisplay;

    public GameObject btnToggler;
    public Button[] btns;
    
    public Artifact[] artifacts;

    public GameObject ob_Enemy;
    public GameObject ob_Player;
    public GameObject ob_Shop;
    public GameObject ob_Inventory;
    public GameObject ob_ShopParent;
    public GameObject ob_Chest;
    public GameObject ob_GameOver;
    private ShopSlot[] ob_ShopChildren;

    private Vector3 enemyShowPos = new Vector3(2.76f, -0.76f, 0f);
    private Vector3 enemyHidePos = new Vector3(13f, 2.5f, 0f);
    private Vector3 playerShowPos = new Vector3(-3.17f, 0.25f, 0f);
    private Vector3 playerHidePos = new Vector3(-13f, 0f, 0f);
    
    private bool hideEnemy = false;
    private bool hidePlayer = false;
    private bool attacked = false;
    private int output = 0;
    private int n = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = gameObject.AddComponent<PlayerScript>();
        enemyScript = gameObject.AddComponent<EnemyScript>();
        delayScript = gameObject.AddComponent<DelayScript>();


        btns[0].onClick.AddListener(ChangeStatSlot1);
        btns[1].onClick.AddListener(ChangeStatSlot2);
        btns[2].onClick.AddListener(ChangeStatSlot3);

        playerScript.InitiateHP();
        enemyScript.GenerateEnemyDemo(stage);

        SetPlayerTurnState();

        ob_ShopChildren = ob_ShopParent.GetComponentsInChildren<ShopSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        stageDisplay.SetText($"Stage {stage}");
        playerHPDisplay.SetText($"HP: {playerScript.PlayerHP}/{PlayerStat.maxHP}");
        enemyHPDisplay.SetText($"HP: {enemyScript.EnemyHP}/{enemyScript.EnemyMaxHP}");

        //Enemy Hiding
        if (hideEnemy)
        {
            if (ob_Enemy.transform.position != enemyHidePos)
                ob_Enemy.transform.position = Vector3.MoveTowards(ob_Enemy.transform.position, enemyHidePos, 0.025f);
            else
                if (ob_Enemy.activeSelf == true)
                ob_Enemy.SetActive(false);
        }
        else
        {
            if (ob_Enemy.activeSelf == false)
                ob_Enemy.SetActive(true);
            if (ob_Enemy.transform.position != enemyShowPos)
                ob_Enemy.transform.position = Vector3.MoveTowards(ob_Enemy.transform.position, enemyShowPos, 0.025f);
        }

        //Player Hiding
        if (hidePlayer)
        {
            if (ob_Player.transform.position != playerHidePos)
                ob_Player.transform.position = Vector3.MoveTowards(ob_Player.transform.position, playerHidePos, 0.025f);
            else
                if (ob_Player.activeSelf == true)
                ob_Player.SetActive(false);
        }
        else
        {
            if (ob_Player.activeSelf == false)
                ob_Player.SetActive(true);
            if (ob_Player.transform.position != playerShowPos)
                ob_Player.transform.position = Vector3.MoveTowards(ob_Player.transform.position, playerShowPos, 0.025f);
        }

        //Turn Start
        if (state == State.PlayerTurn)
        {
            hideEnemy = false;
            hidePlayer = false;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (!attacked)
                {
                    attacked = true;
                    ob_Player.GetComponent<Animator>().SetTrigger("attack");
                    int damage = AttackEnemy(PlayerStat.atk);
                    if (damage > PlayerStat.atk * PlayerStat.atkModifier)
                    {
                        textDisplay.SetText($"Player used critical attack! Given {damage} damage!");
                    }

                    else
                    {
                        textDisplay.SetText($"Player used attack! Given {damage} damage");
                    }
;
                    if (enemyScript.EnemyHP <= 0)
                    {
                        Invoke(nameof(SetBattleEndState), 1f);
                    }

                    else
                    {
                        SetEnemyTurnState();
                    }
                }
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                int heal = playerScript.HealPlayer(PlayerStat.heal);
                textDisplay.SetText($"Player used heal! Gained {heal} HP");

                SetEnemyTurnState();
            }
        }

        else if (state == State.EnemyTurn)
        {
            if (delayScript.Timer > 1f)
            {
                
                ob_Player.GetComponent<Animator>().SetTrigger("hit");
                int damage = AttackPlayer(enemyScript.EnemyAtk);
                textDisplay.SetText($"Enemy's attack! Lost {damage} HP");

                if (playerScript.PlayerHP <= 0)
                {
                    Invoke(nameof(SetGameOverState), 0.99f);
                    Invoke(nameof(InitAttack), 0.99f);
                    delayScript.Timer = 0f;
                }
                else
                {
                    Invoke(nameof(SetPlayerTurnState), 0.99f);
                    Invoke(nameof(InitAttack), 0.99f);
                    delayScript.Timer = 0f;
                }
                
            }
        }

        else if (state == State.BattleEnd)
        {
            InitAttack();
        }

        else if (state == State.NoBattle)
        {
            InitAttack();
            hideEnemy = true;
            hidePlayer = true;
        }
        
        else if (state == State.Event)
        {
            if (n == 1)
            {
                if (output == 0)
                {
                    int heal = Convert.ToInt32(PlayerStat.maxHP * 0.3);
                    playerScript.PlayerHP += heal;
                    if (playerScript.PlayerHP > PlayerStat.maxHP) playerScript.PlayerHP = PlayerStat.maxHP;
                    textDisplay.SetText($"당신은 우연히 신성한 곳을 방문했습니다.\n체력이 {heal} 만큼 회복되었습니다.");
                    Invoke(nameof(SetPlayerTurnState), 3.5f);
                    output = 1;
                }

                if (delayScript.Timer > 3.35f)
                {
                    enemyScript.GenerateEnemyDemo(++stage);
                    delayScript.Timer = 0f;
                }
            }
            else if (n == 2)
            {
                if (output == 0)
                {
                    textDisplay.SetText($"보물 상자를 발견했습니다.");
                    if (delayScript.Timer > 2.0f)
                    {
                        output = 1;
                    }
                }
                else if (output == 1)
                {
                    textDisplay.SetText($"보상을 획득합니다.");
                    EnableChest();
                    output = 2;
                    Invoke(nameof(DisableChest), 3f);
                    Invoke(nameof(SetPlayerTurnState), 3f);
                }
            }
            else if (n == 3)
            {
                if (output == 0)
                {
                    textDisplay.SetText($"보물 상자를 발견했습니다.");
                    if (delayScript.Timer > 2.0f)
                    {
                        output = 1;
                        delayScript.Timer = 0f;
                    }
                }
                else if (output == 1)
                {
                    textDisplay.SetText($"보물 상자는 사실 미믹이었습니다.\n전투를 시작합니다.");
                    output = 2;
                    Invoke(nameof(SetPlayerTurnState), 3f);
                }
                else if (output == 2)
                {
                    if (delayScript.Timer > 2.5f)
                    {
                        enemyScript.GenerateEnemyDemo(stage);
                        delayScript.Timer = 0f;
                    }
                }
            }
            else if (n == 4)
            {
                textDisplay.SetText($"축복의 제단을 발견하였습니다.\n기도를 하여 축복을 받습니다. (버프 중복 적용 X)");
            }
        }
        else if (state == State.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                InitGame();
            }
        }
    }

    void InitGame()
    {
        Gold.gold = 0;
        SetPlayerTurnState();
        InitAttack();
        ob_Inventory.GetComponent<Inventory>().InitInventory();
        stage = 1;
        enemyScript.GenerateEnemyDemo(stage);
        ob_GameOver.SetActive(false);
        playerScript.InitiateHP();
    }

    void SettingDropSet()
    {
        System.Random random = new System.Random();
        int n = artifacts.Length;
        List<int> list = new List<int>();
        for (int i = 0; i < n; ++i) list.Add(i);
        for (int i = 0; i < 3; ++i)
        {
            int a = random.Next(0, n - i);
            btns[i].GetComponent<DropItem>().item = artifacts[list[a]];
            btns[i].GetComponent<DropItem>().ShowArtifact();
            list.Remove(a);
        }
    }

    void InitAttack()
    {
        attacked = false;
        ob_Player.GetComponent<Animator>().SetTrigger("stay");
    }

    void EnableImages()
    {
        btnToggler.SetActive(true);
        SettingDropSet();
    }

    void DisableImages()
    {
        btnToggler.SetActive(false);
    }

    void EnableShop()
    {
        ob_Shop.SetActive(true);
        System.Random random = new System.Random();
        int n = artifacts.Length;
        for (int i = 0; i < 4; ++i)
        {
            int a = random.Next(0, n - i);
            ob_ShopChildren[i].GetComponent<ShopSlot>().SetItem(artifacts[a]);
        }
        hideEnemy = true;
        hidePlayer = true;
    }

    void DisableShop()
    {
        ob_Shop.SetActive(false);
        hideEnemy = false;
        hidePlayer = false;
    }

    void EnableChest()
    {
        ob_Chest.SetActive(true);
        Artifact _art = artifacts[UnityEngine.Random.Range(0, artifacts.Length)];
        ob_Chest.GetComponentInChildren<Slot>().GetComponent<Slot>().AddArtifact(_art);
        ob_Inventory.GetComponent<Inventory>().AcquireItem(_art);
        hideEnemy = true;
        hidePlayer = true;
    }

    void DisableChest()
    {
        ob_Chest.SetActive(false);
        hideEnemy = false;
        hidePlayer = false;
    }

    int AttackEnemy(int value)
    {
        int res = playerScript.AttackEnemy(value);

        return res;
    }

    int AttackPlayer(int value)
    {
        int res = enemyScript.AttackPlayer(value);

        return res;
    }

    void ChangeStatSlot1()
    {
        ob_Inventory.GetComponent<Inventory>().AcquireItem(btns[0].GetComponent<DropItem>().item);
        //PlayerStat.atkModifier += 0.2f;
        enemyScript.GenerateEnemyDemo(++stage);
        IsShopStage();
    }

    void ChangeStatSlot2()
    {
        ob_Inventory.GetComponent<Inventory>().AcquireItem(btns[1].GetComponent<DropItem>().item);
        //PlayerStat.defModifier += 0.2f;
        enemyScript.GenerateEnemyDemo(++stage);
        IsShopStage();
    }

    void ChangeStatSlot3()
    {
        ob_Inventory.GetComponent<Inventory>().AcquireItem(btns[2].GetComponent<DropItem>().item);
        //PlayerStat.healModifier += 0.25f;
        enemyScript.GenerateEnemyDemo(++stage);
        IsShopStage();
    }

    void IsShopStage()
    {
        if (stage % 10 == 0)
        {
            SetNoBattleState();
        }
        else
        {
            if (Probability(0.4))
            {
                SetEventState();
            }
            else
            {
                SetPlayerTurnState();
            }
        }
    }

    public void ShopQuit()
    {
        ob_Shop.SetActive(false);
        enemyScript.GenerateEnemyDemo(++stage);
        SetPlayerTurnState();
    }

    // State Change Method
    public void SetPlayerTurnState()
    {
        state = State.PlayerTurn;
        textDisplay.SetText("Player Turn\nPress 1 for attack / 2 for heal");
        delayScript.Timer = 0f;
        DisableImages();
        DisableShop();
    }

    public void SetEnemyTurnState()
    {
        state = State.EnemyTurn;
        delayScript.Timer = 0f;
        DisableImages();
        DisableShop();
    }

    public void SetBattleEndState()
    {
        state = State.BattleEnd;
        //textDisplay.SetText("Player wins!\nChoose an option:\n1: ATK +20% / 2: HEAL +25% / 3: DEF +20%");
        Gold.gold += stage * 10;
        textDisplay.SetText("\nPlayer wins!\nChoose a artifact.");
        delayScript.Timer = 0f;
        EnableImages();
        DisableShop();
        hideEnemy = true;
    }

    public void SetNoBattleState()
    {
        state = State.NoBattle;
        delayScript.Timer = 0f;
        DisableImages();
        EnableShop();
    }

    public void SetEventState()
    {
        n = UnityEngine.Random.Range(1, 4);
        output = 0;
        state = State.Event;
        delayScript.Timer = 0f;
        DisableImages();
        DisableShop();
        hideEnemy = true;
    }

    public void SetGameOverState()
    {
        state = State.GameOver;
        delayScript.Timer = 0f;
        DisableImages();
        DisableShop();
        hideEnemy = true;
        hidePlayer = true;
        ob_GameOver.SetActive(true);
    }

    // 확률 함수
    public static bool Probability(double chance)
    {
        return (UnityEngine.Random.value <= chance);
    }
    public static bool Probability(float chance)
    {
        return (UnityEngine.Random.value <= chance);
    }
}

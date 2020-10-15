using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LVLcamp : SquareScript
{
    public int level;
    public string campName;
    public int health;
    public int currentHealth;
    public int damage;
    public int gold;
    public int expYield;
    
    Animator mainCam;
    public Sprite sprite;
    public override void StartFunction()
    {
        base.StartFunction();
        mainCam = CameraScript.instance.gameObject.GetComponent<Animator>();
        actionName = "Fight";
        passThrough = false;
        panel = GameCanvasScript.instance.beforeBattleInstancePrefab;
    }
    public override void Action()
    {
        base.Action();
        battleInterface.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -80, 0);
        battleInterface.transform.Find("EnemyStatus").gameObject.GetComponent<TextMeshProUGUI>().text = campName + "\nDamage: " + damage + "\nHealth: " + health;
        battleInterface.transform.Find("EnemyAbilities").gameObject.GetComponent<TextMeshProUGUI>().text = "Abilities: none";
        battleInterface.transform.Find("EnemyRewards").gameObject.GetComponent<TextMeshProUGUI>().text = "Reward: " + gold + " gold";
        battleInterface.transform.Find("PlayerStatus").gameObject.GetComponent<TextMeshProUGUI>().text = "Player\nDamage: " + currentPlayer.damage.Value() + "\nHealth: " + currentPlayer.currentHealth;
        battleInterface.transform.Find("StartButton").gameObject.GetComponent<Button>().onClick.AddListener(close);
        battleInterface.transform.Find("StartButton").gameObject.GetComponent<Button>().onClick.AddListener(startBattle);
    }
    public override void close()
    {
        base.close();
    }
    public void startBattle()
    {
        respectiveButton.interactable = false;
        boardUI.gameObject.SetActive(false);
        mainCam.SetTrigger("startBattle");
        currentHealth = health;
        BattleScreenScript.instance.setupBattleVsAI(battlesys.players[battlesys.currentTurn], this);
    }
    public void ChangeHealth(int value)
    {
        currentHealth = currentHealth + value;
        if (currentHealth > health) currentHealth = health;
        if (currentHealth <= 0) currentHealth = 0;
    }
    public virtual void CampAction()
    {
        BattleScreenScript.instance.EndTurn();
    }
}

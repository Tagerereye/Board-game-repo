using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum FightState { leftTurn, rightTurn, beforeBattle, afterBattle }
public class BattleScreenScript : MonoBehaviour
{
    public bool isAgainstAI;                                    //
    public static BattleScreenScript instance;                  //this
    Transform actions;                                          //button parent pt createbuttons()
    public Canvas battleCanvas;                                 //this UI

    public Transform leftFighter, rightFighter;                 //cei 2 luptatori, referinte
    TextMesh leftFighterHealthText, rightFighterHealthText;     //text

    public Transform leftSlider, rightSlider;                   //slider pt hp
    float sliderScale;                                          //marimea originala a sliderului

    public FightState fightstate;                               //fightstate: statiul bataliei
    List<Button> buttons = new List<Button>();                  //referinta pt butoane
    public Button attackButton;                                 //referinta pt butonul de atac
    public Button leaveButton;
    public PlayerScript leftPlayer, rightPlayer, currentPlayer, enemyPlayer; //cei doi playeri, si playerul curent
    public LVLcamp enemyCamp;                                   //inamicul AI, luptator care mereu este in dreapta
    public static TextMesh combatLog;                           //Combat log-ul din mijlocul ecranului
    void Awake()
    {
        combatLog = transform.GetChild(2).GetComponent<TextMesh>();
        instance = this;
        leftFighter = transform.GetChild(0);
        rightFighter = transform.GetChild(1);
        actions = battleCanvas.transform.GetChild(1);
        actions.gameObject.SetActive(false);
        leftFighterHealthText = leftFighter.GetChild(3).GetComponent<TextMesh>();
        rightFighterHealthText = rightFighter.GetChild(3).GetComponent<TextMesh>();
        sliderScale = leftSlider.localScale.x;
    }

    public void setupBattleVsAI(PlayerScript player, LVLcamp camp)
    {
        //schimbi infatisarea board-ului
        leftFighter.GetChild(0).GetComponent<SpriteRenderer>().sprite = player.texture;
        leftFighter.GetChild(2).GetComponent<TextMesh>().text = "Jucator";
        leftFighter.GetChild(3).GetComponent<TextMesh>().text = player.currentHealth + "/" + player.maxHealth.Value();

        rightFighter.GetChild(0).GetComponent<SpriteRenderer>().sprite = camp.sprite;
        rightFighter.GetChild(2).GetComponent<TextMesh>().text = camp.campName;
        rightFighter.GetChild(3).GetComponent<TextMesh>().text = camp.currentHealth + "/" + camp.health;
        //apoi... schimbi UI-ul astfel incat sa poti sa controlezi batalia
        fightstate = FightState.beforeBattle;
        //editarea scriptului si a playerilor
        isAgainstAI = true;
        leftPlayer = player;
        enemyCamp = camp;
        StartCoroutine(setupUIAI());
    }

    IEnumerator setupUIAI()
    {
        updateUI();
        BattleScreenScript.combatLog.text = "Let the battle begin!";
        yield return new WaitForSeconds(2);
        battleCanvas.gameObject.SetActive(true);
        //fightstate = FightState.leftTurn;
        //currentPlayer = leftPlayer;
        //enemyCamp.CampAction();
        //CreateActionButtons();
        StartCoroutine(NextState());
    }
    IEnumerator NextState()
    {
        if(fightstate==FightState.beforeBattle || (isAgainstAI && fightstate==FightState.leftTurn)) yield return new WaitForSeconds(0);
        else yield return new WaitForSeconds(1);

        if (fightstate == FightState.beforeBattle && isAgainstAI)
        {
            fightstate = FightState.rightTurn;
        }
        else if (fightstate == FightState.beforeBattle && !isAgainstAI)
        {
            //alegi cine incepe primul bazat pe viteza fiecaruia
        }
        if (fightstate == FightState.leftTurn)
        {
            currentPlayer = leftPlayer;
            enemyPlayer = rightPlayer;
            CreateActionButtons();
        }
        else if (fightstate == FightState.rightTurn)
        {
            if (isAgainstAI)
            {
                enemyCamp.CampAction();
            }
            else
            {
                //actiune pt playerul din dreapta
                currentPlayer = rightPlayer;
                enemyPlayer = leftPlayer;
                CreateActionButtons();
            }

        }
        if(fightstate==FightState.afterBattle)
        {
            BattleScreenScript.combatLog.text = "Battle over";
            yield return new WaitForSeconds(1);
            if(isAgainstAI && leftPlayer.currentHealth == 0)
            {
                //player lost in pve
            }
            else if(isAgainstAI && enemyCamp.currentHealth == 0)
            {
                //player won in pve
                BattleScreenScript.combatLog.text = leftPlayer.gamename + " won "+ enemyCamp.gold + " gold.";
                leftPlayer.gold += enemyCamp.gold;
                leftPlayer.GainExp(enemyCamp.expYield);
            }
            else if (leftPlayer.currentHealth == 0 && rightPlayer.currentHealth == 0)
            {
                //both died in pvp
            }
            else if(leftPlayer.currentHealth == 0)
            {
                //left died and right won in pvp
            }
            else if(rightPlayer.currentHealth == 0)
            {
                //right died and left won in pvp
            }
            actions.gameObject.SetActive(false);
            yield return new WaitForSeconds(2);
            buttons.Add(Instantiate(leaveButton, actions));
            buttons[0].onClick.AddListener(ExitBattle);
            actions.gameObject.SetActive(true);
        }
    }
    void updateUI()
    {
        if (actions.childCount == 0) actions.gameObject.SetActive(false); else actions.gameObject.SetActive(true);
        leftFighterHealthText.text = leftPlayer.currentHealth + "/" + leftPlayer.maxHealth.Value();
        if (isAgainstAI) rightFighterHealthText.text = enemyCamp.currentHealth + "/" + enemyCamp.health;

        leftSlider.localScale = new Vector3(((float)leftPlayer.currentHealth / leftPlayer.maxHealth.Value()) * sliderScale, 1, 1);
        if (isAgainstAI) rightSlider.localScale = new Vector3(((float)enemyCamp.currentHealth / enemyCamp.health) * sliderScale, 1, 1);
    }
    void CreateActionButtons()
    {
        buttons.Add(Instantiate(attackButton, actions));
        buttons[0].onClick.AddListener(currentPlayer.Attack);
        buttons[0].onClick.AddListener(EndTurn);

        //abilitati
        int n = currentPlayer.learnedSpells.Count, bc = 1;
        for(int i=0;i<n;i++)
        {
            Spell currentSpell = currentPlayer.learnedSpells[i];
            buttons.Add(Instantiate(attackButton, actions));
            buttons[bc].GetComponentInChildren<TextMeshProUGUI>().text = currentSpell.name;
            buttons[bc].onClick.AddListener(currentSpell.Effect);
            buttons[bc].onClick.AddListener(EndTurn);
        }
        //iteme
        updateUI();
    }
    public void EndTurn()
    {
        for (int i = 0; i < buttons.Count; i++)
            Destroy(buttons[i].gameObject);
        buttons.Clear();
        Debug.Log(enemyCamp.currentHealth);
        if (leftPlayer.currentHealth == 0 || (isAgainstAI && enemyCamp.currentHealth == 0) || (!isAgainstAI && rightPlayer.currentHealth == 0)) fightstate = FightState.afterBattle;
        else if (fightstate == FightState.leftTurn)
        {
            leftPlayer.BuffUpdateFight();
            fightstate = FightState.rightTurn;
        }
        else if (fightstate == FightState.rightTurn)
        {
            if(isAgainstAI)
            {

            } else
            {
                rightPlayer.BuffUpdateFight();
            }
            fightstate = FightState.leftTurn;
        }
        updateUI();
        StartCoroutine(NextState());

    }
    void ExitBattle()
    {
        leftPlayer.BuffUpdateBattle();
        if(rightPlayer!=null) rightPlayer.BuffUpdateBattle();
        leftPlayer = null;
        rightPlayer = null;
        enemyCamp = null;
        enemyPlayer = null;
        currentPlayer = null;
        CameraScript.instance.GetComponent<Animator>().SetTrigger("endBattle");
        battleCanvas.gameObject.SetActive(false);
        BattleSys.instance.onUpdatingUICallBack.Invoke();
        StartCoroutine(ExitBattleCoroutine());
    }
    IEnumerator ExitBattleCoroutine()
    {
        yield return new WaitForSeconds(4);
        GameCanvasScript.instance.gameObject.SetActive(true);
    }
}

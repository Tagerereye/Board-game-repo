using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class FountainSquare : SquareScript
{
    public TextMeshProUGUI selectedReward;
    bool hasRolled = false;
    //FountainBonusAttack atkbuff;
    //FountainBonusHealth hpbuff;
    public override void StartFunction()
    {
        base.StartFunction();
        actionName = "Fountain";
        panel = GameCanvasScript.instance.FountainUIPrefab;
    }

    public override void Action()
    {
        base.Action();
        battleInterface.transform.Find("Button").GetComponent<Button>().onClick.AddListener(RollFountainCoroutineStarter);
    }
    public void RollFountainCoroutineStarter()
    {
        StartCoroutine(RollFountain());
    }

    public IEnumerator RollFountain()
    {
        hasRolled = true;
        battlesys.eventSystem.SetActive(false);
        battleInterface.GetComponent<Animator>().SetTrigger("roll");
        int dice = Random.Range(1, 7);;
        selectedReward = battleInterface.transform.GetChild(dice).GetComponent<TextMeshProUGUI>();
        yield return new WaitForSeconds(5f);
        battleInterface.GetComponent<Animator>().enabled = false;
        selectedReward.color = new Color(255, 0, 0);
        if(dice==1)
        {
            
            battlesys.players[battlesys.currentTurn].AddBuff(new FountainBonusAttack());
        }
        else if (dice==2)
        {
            battlesys.players[battlesys.currentTurn].AddBuff(new FountainBonusHealth());
        }
        else if (dice == 3)
        {
            battlesys.players[battlesys.currentTurn].gold += 10;
        }
        else if (dice == 4)
        {
            battlesys.players[battlesys.currentTurn].GainExp(2);
        }
        else if (dice == 5)
        {
            battlesys.players[battlesys.currentTurn].ChangeHealth(2);
        }
        else if (dice == 6)
        {
            battlesys.MoveToSquare((battlesys.players[battlesys.currentTurn].boardPosition + Random.Range(1,7)) % 48, ref battlesys.players[battlesys.currentTurn]);
        }
        
        yield return new WaitForSeconds(1f);
        battlesys.eventSystem.SetActive(true);

    }
    public override void close()
    {
        base.close();
        if(hasRolled == true)
        {
            hasRolled = false;
            respectiveButton.interactable = false;
        }
    }
}

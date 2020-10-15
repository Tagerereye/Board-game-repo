using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : SquareScript
{
    public static bool firstInstance = true;
    public override void StartFunction()
    {
        base.StartFunction();
        actionName = "Shop";
        beforeTurn = true;
        //passThrough = false;
        panel = GameCanvasScript.instance.shopPrefab;

    }
    public override void Action()
    {
        /*
        base.Action();
        respectiveButton.onClick.RemoveAllListeners();
        respectiveButton.onClick.AddListener(Action);
        respectiveButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Shop";
        */
        if(firstInstance)
        {
        battleInterface = Instantiate(panel, parent.transform);
        firstInstance = false;
        battleInterface.name = "ShopPanel";
        }
        else
        {
            ShopScript.instance.instanceGO.SetActive(true);
        }
    }
    public override void close()
    {
        base.close();
        //BattleSys.instance.statsPanel.gameObject.SetActive(true);
    }
}

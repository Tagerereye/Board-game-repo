using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopScript : MonoBehaviour
{
    public static ShopScript instance;
    public GameObject instanceGO;
    public Item spotlightItem;
    public Transform spotLightPanel;
    private void Start()
    {
        instance = this;
        instanceGO = gameObject;
    }
    public void CloseButton()
    {
        //Destroy(instanceGO);
        instanceGO.SetActive(false);
    }
    public void SpotLightItem()
    {
        spotLightPanel.GetChild(0).GetComponent<Image>().sprite = spotlightItem.icon;
        spotLightPanel.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 255);
        spotLightPanel.GetChild(1).GetComponent<TextMeshProUGUI>().text = spotlightItem.description;
        spotLightPanel.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Cost: " + spotlightItem.goldCost;
        spotLightPanel.GetChild(3).GetComponent<Button>().interactable = true;
    }
    private void OnDestroy()
    {
        Shop.firstInstance = true;
    }
    public void BuyItem()
    {
        PlayerScript player = BattleSys.instance.players[BattleSys.instance.currentTurn];
        if (player.gold >= spotlightItem.goldCost && player.inventory.Count < PlayerScript.inventorySpace)
        {
            player.gold -= spotlightItem.goldCost;
            player.AddItem(spotlightItem);
        }
        else Debug.Log("Unable to buy");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Timeline;
using TMPro;

public class ItemShopScript : MonoBehaviour
{
    public string itemName;
    public Type tip;
    public object itemobj;
    public Item item;
    private void Start()
    {
        tip = Type.GetType(itemName);
        itemobj = Activator.CreateInstance(tip);
        item = itemobj as Item;
        transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.name;
        transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.descriptionShort;
    }
    public void SpotLightItem()
    {
        ShopScript.instance.spotlightItem = this.item;
        ShopScript.instance.SpotLightItem();
    }
}

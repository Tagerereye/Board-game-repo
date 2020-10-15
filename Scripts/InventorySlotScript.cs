using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InventorySlotScript : MonoBehaviour
{
    Sprite currentIcon = null;
    PlayerScript currentPlayer;
    Image imageComp;
    public Button button;
    private void Awake()
    {
        imageComp = gameObject.GetComponent<Image>();
        button = this.GetComponent<Button>();
    }
    public void UpdateIcon(Sprite sprite)
    {
        //Debug.Log(imageComp.color.a);
        currentIcon = sprite;
        if(sprite == null)
        {
            imageComp.sprite = null;
            Color tempcol = new Color(255, 255, 255);
            tempcol.a = 0.4f;
            imageComp.color = tempcol;
            return;
        }
        imageComp.sprite = sprite;
        imageComp.color = new Color(255, 255, 255, 255);
    }
    public void UpdateButton(Action func)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(func.Invoke);
    }
}

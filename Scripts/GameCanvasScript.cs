using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasScript : MonoBehaviour
{
    public GameObject restOfUIObjects;
    public GameObject blackPanel;
    public static GameCanvasScript instance;

    public GameObject beforeBattleInstancePrefab;
    public GameObject FountainUIPrefab;
    public GameObject shopPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        restOfUIObjects.SetActive(false);
        StartCoroutine(GameIntro());
    }

    IEnumerator GameIntro()
    {
        yield return new WaitForSeconds(4.5f);
        restOfUIObjects.SetActive(true);
        blackPanel.SetActive(false);
        BattleSys.instance.AtStartOpenShop();
    }
}

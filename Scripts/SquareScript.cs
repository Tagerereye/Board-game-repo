using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SquareScript : MonoBehaviour
{
    public bool passThrough = false;
    public bool beforeTurn = false;
    public Button respectiveButton;
    public string actionName;
    public PlayerScript currentPlayer;
    public GameObject panel;
    public GameObject parent;
    public Transform boardUI;
    public BattleSys battlesys;
    public GameObject battleInterface;
    void Start()
    {
        StartFunction();
    }
    public virtual void StartFunction()
    {
        parent = GameCanvasScript.instance.gameObject;
        boardUI = GameCanvasScript.instance.transform;
        battlesys = BattleSys.instance;
        
    }
    public virtual void Action()
    {
        battleInterface = Instantiate(panel, parent.transform);
        battleInterface.name = "SquarePanel";
        respectiveButton.onClick.RemoveAllListeners();
        respectiveButton.onClick.AddListener(close);
        respectiveButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "back";
        currentPlayer = battlesys.players[battlesys.currentTurn];
    }
    public virtual void close()
    {
        Destroy(parent.transform.Find("SquarePanel").gameObject);
        respectiveButton.onClick.RemoveAllListeners();
        respectiveButton.onClick.AddListener(Action);
        respectiveButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = actionName;
        battlesys.onUpdatingUICallBack.Invoke();
    }
}

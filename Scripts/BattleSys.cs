using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public enum battleState {}
public class BattleSys : MonoBehaviour
{
    public static BattleSys instance;
    public int maxPlayers;
    public int currentTurn = 0;
    public TextMeshProUGUI currentTurnIndicator;
    //PlayerScript currentPlayer;
    public PlayerScript[] players = new PlayerScript[6];
    int dice1, dice2;
    GameObject pos;
    public GameObject button;
    public Canvas canvas;
    public Button rollButton;
    public Transform statsPanel;
    InventorySlotScript[] inventorySlots;
    int previous, current;
    int createdButtonPosition = -137;
    int inventorySpace;
    List<GameObject> ActionButtons = new List<GameObject>();
    Button endTurnButton;
    public GameObject eventSystem;

    public delegate void OnUpdatingUI();
    public OnUpdatingUI onUpdatingUICallBack;

    public delegate void OnInventoryChange(Item item);
    public OnInventoryChange onInventoryChangeCallback;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        pos = GetComponent<CreatePlayers>().pos;
        inventorySlots = statsPanel.GetChild(5).GetComponentsInChildren<InventorySlotScript>();
        onUpdatingUICallBack += UpdateTurnUI;
        onUpdatingUICallBack += InventorySlotsUpdateUI;
        onUpdatingUICallBack.Invoke();
        inventorySpace = PlayerScript.inventorySpace;
    }

    public void SOMESHITTYMETHOD()
    {
        /*
        Item vest = new StarterVest();
        Debug.Log(vest.description);
        players[currentTurn].AddItem(vest);
        */
        players[currentTurn].LearnSpell(new HeroicStrike());
    }
    public void AtStartOpenShop()
    {
        SquareScript c = pos.transform.GetChild(0).GetComponent<SquareScript>();
        ActionButtons.Add(createButton(c.actionName));
        ActionButtons[ActionButtons.Count - 1].GetComponent<Button>().onClick.AddListener(c.Action);
        c.respectiveButton = ActionButtons[ActionButtons.Count - 1].GetComponent<Button>();
    }
    public void UpdateTurnUI()
    {
        if (currentTurn == 0) { currentTurnIndicator.text = "Red"; currentTurnIndicator.color = new Color(255, 0, 0); }
        else if (currentTurn == 1) { currentTurnIndicator.text = "Blue"; currentTurnIndicator.color = new Color(0, 0, 255); }
        else if (currentTurn == 2) { currentTurnIndicator.text = "Yellow"; currentTurnIndicator.color = new Color(255, 255, 0); }
        else if (currentTurn == 3) { currentTurnIndicator.text = "Green"; currentTurnIndicator.color = new Color(0, 255, 0); }
        else if (currentTurn == 4) { currentTurnIndicator.text = "White"; currentTurnIndicator.color = new Color(255, 255, 255); }
        else if (currentTurn == 5) { currentTurnIndicator.text = "Purple"; currentTurnIndicator.color = new Color(255, 0, 255); }

        statsPanel.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Health: " + players[currentTurn].currentHealth + "/" + players[currentTurn].maxHealth.Value();
        statsPanel.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Damage: " + players[currentTurn].damage.Value();
        statsPanel.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Gold: " + players[currentTurn].gold;
        statsPanel.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Level: " + players[currentTurn].level + "-" + players[currentTurn].experience + "/" + players[currentTurn].lvlupReq[players[currentTurn].level];
    }
    GameObject createButton(string txt)
    {
        GameObject b = Instantiate(button, canvas.transform);
        b.name = txt;
        b.GetComponent<RectTransform>().anchoredPosition = new Vector3(-182, createdButtonPosition, 0);
        createdButtonPosition -= 75;
        b.GetComponentInChildren<TextMeshProUGUI>().text = txt;
        return b;
    }
    public void Roll()
    {
        RemoveButtons();
        rollButton.interactable = false;
        previous = players[currentTurn].boardPosition;  //salvezi pozitia precedenta
        dice1 = Random.Range(1, 7);
        dice2 = Random.Range(1, 7);
        //dice1 = 3;
        //dice2 = 4;
        MoveToSquare((previous + dice1 + dice2) % 48, ref players[currentTurn]);
        current = players[currentTurn].boardPosition;   //salvezi pozitia curenta
        
        SquareActions(previous, current);  //actiunile care trebuie facute odata ce a ajuns la patratelul nou
        endTurnButton = createButton("End Turn").GetComponent<Button>();
        endTurnButton.onClick.AddListener(EndPlayerTurn);

    }

    public void MoveToSquare(int _position, ref PlayerScript playerToMove)
    {
        players[currentTurn].boardPosition = _position;                                 //actualizezi pozitia playerului respectiv
        players[currentTurn].transform.parent = pos.transform.GetChild(_position);      //muti playerul pe pozitia patratului respectiv pe board
        players[currentTurn].transform.localPosition = new Vector3(0.0f, 0.1f, 0.0f);   //Il pui un pic deasupra board-ului pentru ca textura sa nu se glitch-uiasca
    }

    void SquareActions(int previous, int current)      //Functia care creeaza butoanele care reprezinta actiunile pe care le poate face jucatorul pe un patrat anume
    {
        SquareScript c;
        Debug.Log("from " + previous + " to " + current);
        for (int i = previous; i != current; i= (i+1)%48)
        {
            Debug.Log(i);
            c = pos.transform.GetChild(i).GetComponent<SquareScript>();    //obiectul pozitiei pe board, impreuna cu toate componentele si scripturile
            if (c != null && c.passThrough==true)
            {
            ActionButtons.Add(createButton(c.actionName));
            ActionButtons[ActionButtons.Count - 1].GetComponent<Button>().onClick.AddListener(c.Action);
            c.respectiveButton = ActionButtons[ActionButtons.Count - 1].GetComponent<Button>();
            }
        }
        c = pos.transform.GetChild(current).GetComponent<SquareScript>();    //obiectul pozitiei pe board, impreuna cu toate componentele si scripturile
        if (c != null)
        {
            ActionButtons.Add(createButton(c.actionName));
            ActionButtons[ActionButtons.Count - 1].GetComponent<Button>().onClick.AddListener(c.Action);
            c.respectiveButton = ActionButtons[ActionButtons.Count - 1].GetComponent<Button>();
        }
    }
    void EndPlayerTurn()
    {
        //players[currentTurn].BuffUpdateTurn();
        RemoveButtons();
        rollButton.interactable = true;
        players[currentTurn].BuffUpdateTurn();
        currentTurn= (currentTurn+1) %(maxPlayers);
        SquareScript c = players[currentTurn].GetComponentInParent<SquareScript>();
        if (c.beforeTurn == true)
        {
            ActionButtons.Add(createButton(c.actionName));
            ActionButtons[ActionButtons.Count - 1].GetComponent<Button>().onClick.AddListener(c.Action);
            c.respectiveButton = ActionButtons[ActionButtons.Count - 1].GetComponent<Button>();
        }
        if (currentTurn >= maxPlayers) currentTurn = 0;
        onUpdatingUICallBack.Invoke();
    }
    void InventorySlotsUpdateUI()
    {
        PlayerScript currentPlayer = players[currentTurn];
        //Debug.Log(currentPlayer.inventory);
        //Debug.Log(currentPlayer.inventory.Count);
        for (int i = 0; i < inventorySpace; i++)
        {
            inventorySlots[i].button.onClick.RemoveAllListeners();
            if (i >= currentPlayer.inventory.Count || currentPlayer.inventory[i] == null)
            {
                inventorySlots[i].UpdateIcon(null);
            }
            else
            {
                inventorySlots[i].UpdateIcon(currentPlayer.inventory[i].icon);
                inventorySlots[i].button.onClick.AddListener(currentPlayer.inventory[i].OnUse);
            }
        }
    }
    void RemoveButtons()
    {
        Transform n = GameCanvasScript.instance.transform;
        for (int i = 2; i < n.childCount; i++) Destroy(n.GetChild(i).gameObject);
        createdButtonPosition = -137;
    }
}
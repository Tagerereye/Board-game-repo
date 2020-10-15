using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum playerClass { warrior, rogue, mage }
public class CreatePlayers : MonoBehaviour
{
    public bool debugGame = false;
    public PlayerScript playerType;
    int numberOfPlayers = SingletonScript.nrOfPlayers;
    public playerClass[] playerclass = new playerClass[6];
    string col;
    public GameObject pos;
    BattleSys battlesys;
    
   
    void Start()
    {
        if(debugGame)
        {
            numberOfPlayers = 6;
            for (int i = 0; i < numberOfPlayers; i++) { playerclass[i] = playerClass.warrior; }

        }
        
        if(!debugGame) for(int i=0;i<numberOfPlayers;i++)
        {
            if (SingletonScript.playerClasses[i] == 0) playerclass[i] = playerClass.warrior;
            if (SingletonScript.playerClasses[i] == 1) playerclass[i] = playerClass.mage;
            if (SingletonScript.playerClasses[i] == 2) playerclass[i] = playerClass.rogue;
        }
        //battlesys = GetComponent<BattleSys>();
        battlesys = BattleSys.instance;
        battlesys.maxPlayers = numberOfPlayers;
        for(int i=0;i<numberOfPlayers;i++)
        {
            if (i == 0) col = "red";
            if (i == 1) col = "blue";
            if (i == 2) col = "yellow";
            if (i == 3) col = "green";
            if (i == 4) col = "white";
            if (i == 5) col = "purple";
            PlayerScript player = Instantiate(playerType, new Vector3(-4.5f - (float)i,0.1f,4.5f), Quaternion.Euler(90.0f, 0.0f, 180.0f));
            player.boardPosition = 0;
            battlesys.players[i] = player;
            switch(playerclass[i])
            {
                case playerClass.warrior: createWarrior(ref player); break;
                case playerClass.rogue: createRogue(ref player); break;
                case playerClass.mage: createMage(ref player);break;
            }
            player.currentHealth = player.maxHealth.Value();
            player.gold = 10;
            player.gamename = col;
            player.transform.SetParent(pos.transform.GetChild(0));
            
        }
    }
    void createWarrior(ref PlayerScript obj)
    {
        obj.maxHealth.baseValue = 10;
        obj.damage.baseValue = 2;
        obj.mana.baseValue = 3;
        obj.spellPower.baseValue = 0;
        obj.playerClass = "WARRIOR";
        obj.texture = Resources.Load<Sprite>("Sprites/"+col+"warrior");
    }
    void createRogue(ref PlayerScript obj)
    {
        obj.maxHealth.baseValue = 8;
        obj.damage.baseValue = 3;
        obj.mana.baseValue = 2;
        obj.spellPower.baseValue = 0;
        obj.playerClass = "ROGUE";
        obj.texture = Resources.Load<Sprite>("Sprites/" + col + "rogue");
    }
    void createMage(ref PlayerScript obj)
    {
        obj.maxHealth.baseValue = 8;
        obj.damage.baseValue = 2;
        obj.mana.baseValue = 8;
        obj.spellPower.baseValue = 2;
        obj.playerClass = "MAGE";
        obj.texture = Resources.Load<Sprite>("Sprites/" + col + "mage");
    }
}

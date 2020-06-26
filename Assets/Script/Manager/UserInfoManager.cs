using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoManager : MonoBehaviour
{
    public float point = 100;
    public int research_level = 1;

    public bool hp_research;
    public bool atk_research;
    public bool atkspeed_research;
    public bool speed_research;
    public bool item_research;
    public bool shield_research;
    public bool recovery_research;
    public bool skilldamage_research;
    public bool exp_research;
    public bool point_research;

    [SerializeField]
    public struct PlayerStruct
    {
        public string name;
        public int max_hp;
        public int attack;
        public int def;
        public int attack_speed;
        public int move_speed;
        public int attack_range;
        public int getitem_range;
        public int max_skilllmarble_count;
        public int attack_count;
        public string explanation;
    }

    public PlayerStruct[] PlayerList;

    public PlayerStruct GetPlayer(string name)
    {
        for(int i = 0; i<PlayerList.Length; i++)
        {
            if (PlayerList[i].name.Contains(name))
                return PlayerList[i];
        }
        PlayerStruct Null = new PlayerStruct();
        Null.name = "";
        return Null;
    }

    private void Start()
    {
        int player_num = GetComponent<DatabaseManager>().monster_DB.GetLineSize();
        PlayerList = new PlayerStruct[player_num];
        for (int i = 0; i < player_num; i++)
        {
            List<string> playerInfo = GetComponent<DatabaseManager>().player_DB.GetRowData(i);
            PlayerList[i].name = playerInfo[1];
            PlayerList[i].max_hp = int.Parse(playerInfo[7]);
            PlayerList[i].def = int.Parse(playerInfo[8]);
            PlayerList[i].attack_speed = int.Parse(playerInfo[9]);
            PlayerList[i].move_speed = int.Parse(playerInfo[10]);
            PlayerList[i].attack_range = int.Parse(playerInfo[11]);
            PlayerList[i].getitem_range = int.Parse(playerInfo[12]);
            PlayerList[i].max_skilllmarble_count = int.Parse(playerInfo[13]);
            PlayerList[i].max_skilllmarble_count = int.Parse(playerInfo[14]);
            PlayerList[i].explanation = playerInfo[15];
        }
    }

    public int GetMasicMaxHP()
    {
        if (hp_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }
    public int GetMasicAtk()
    {
        if (atk_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }

    public int GetMasicAtkspeed()
    {
        if (atkspeed_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }

    public int GetMasicSpeed()
    {
        if (speed_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }

    public int GetMasicItemDistance()
    {
        if (item_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }

    public int GetMasicShield()
    {
        if (shield_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }

    public int GetMasicRecovery()
    {
        if (recovery_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }

    public int GetMasicSkillDamage()
    {
        if (skilldamage_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }

    public int GetMasicExp()
    {
        if (exp_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }

    public int GetMasicPoint()
    {
        if (point_research)
        {
            return research_level;
        }
        else
        {
            return research_level - 1;
        }
    }
}

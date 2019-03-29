/// Quest management system
/// Author: Cucu Stefan
/// ----------------------

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public List<Quest> questList = new List<Quest>();
    public TextMeshProUGUI TitleText, DescText, objText1, objText2, objText3, objText4;
    int questId = 0;


    void Start()
    {
        int questCount = questList.Count;
        TitleText.text = questList[questId].Title;
        DescText.text = questList[questId].Description;
        for (int i = 0; i < 4; i++)
        {
            if (questList[questId].ObjectiveDesc[i] != null)
            {
                if (i == 0) objText1.text = questList[questId].ObjectiveDesc[i];
                else if (i == 1) objText2.text = questList[questId].ObjectiveDesc[i];
                else if (i == 2) objText3.text = questList[questId].ObjectiveDesc[i];
                else if (i == 3) objText4.text = questList[questId].ObjectiveDesc[i];
            }
            else
            {
                if (i == 0) objText1.text = " ";
                else if (i == 1) objText2.text = " ";
                else if (i == 2) objText3.text = " ";
                else if (i == 3) objText4.text = " ";
            }
        }
    }
    void Update()
    {
        if (Validate(questId))
        {
            int questCount = questList.Count;
            if(questId < questCount) questId++;
            TitleText.text = questList[questId].Title;
            DescText.text = questList[questId].Description;
            for (int i = 0; i < 4; i++)
            {
                if (questList[questId].ObjectiveDesc[i] != null)
                {
                    if (i == 0) objText1.text = questList[questId].ObjectiveDesc[i];
                    else if (i == 1) objText2.text = questList[questId].ObjectiveDesc[i];
                    else if (i == 2) objText3.text = questList[questId].ObjectiveDesc[i];
                    else if (i == 3) objText4.text = questList[questId].ObjectiveDesc[i];
                }
                else
                {
                    if (i == 0) objText1.text = "-";
                    else if (i == 1) objText2.text = "-";
                    else if (i == 2) objText3.text = "-";
                    else if (i == 3) objText4.text = "-";
                }
            }
        }

    }

    #region Quest Requirements
    bool Validate(int q)
    {
        if (q == 0)
        {
            if (Global.wood >= 40) return true;
        }
        else if (q == 1)
        {
            if (Global.buildings[4] >= 4) return true;
        }
        else if (q == 2)
        {
            if (Global.buildings[16] >= 1) return true;
        }
        else if (q == 3)
        {
            if (Global.isBuildingResearched[10] == true) return true;
        }
        else if (q == 4)
        {
            if (Global.buildings[7] >= 1) return true;
        }
        else if (q == 5)
        {
            if (Global.buildings[8] >= 1 && Global.buildings[12] >= 1) return true;
        }
        else if (q == 6)
        {
            if (Global.buildings[21] >= 1 && Global.isBuildingResearched[19] == true) return true;
        }
        else if (q == 7)
        {
            if (Global.isBuildingResearched[3] && Global.buildings[5] >= 1 && Global.buildings[14] >= 1) return true;
        }
        else if (q == 8)
        {
            if (Global.isBuildingResearched[15] && Global.buildings[17] >= 1 && Global.food >= 100) return true;
        }
        else if (q == 9)
        {
            if (Global.isBuildingResearched[16] && Global.buildings[18] >= 1) return true;
        }
        else if (q == 10)
        {
            if (Global.isBuildingResearched[8] && Global.buildings[10] >= 1 && Global.buildings[13] >= 1) return true;
        }
        else if (q == 11)
        {
            if (Global.isBuildingResearched[7] && Global.buildings[9] >= 1 && Global.isBuildingResearched[20] && Global.buildings[22] >= 1) return true;
        }
        else if (q == 12)
        {
            if (Global.isBuildingResearched[13] && Global.buildings[15] >= 1 && Global.buildings[19] >= 1 && Global.buildings[23] >= 1) return true;
        }
        else if (q == 13)
        {
            if (Global.isBuildingResearched[18] && Global.buildings[20] >= 1) return true;
        }
        else if (q == 14)
        {
            if (Global.isBuildingResearched[22] && Global.buildings[24] >= 1) return true;
        }
        else if (q == 15)
        {
            if (Global.isBuildingResearched[9] && Global.buildings[11] >= 1 && Global.buildings[25] >= 1) return true;
        }
        else if (q == 16)
        {
            if (Global.population >= 10000) return true;
        }
        return false;
    }
    #endregion

    [System.Serializable]
    public struct Quest
    {
        public string Title, Description;
        public string[] ObjectiveDesc;
    }
}

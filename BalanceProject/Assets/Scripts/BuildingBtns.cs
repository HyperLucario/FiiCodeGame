using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBtns : MonoBehaviour
{
    public TileHover mouseClass;
    public GameObject infoPanel;
    public List<GameObject> buttons;
    public List<Info> btnInfo;

    private Color notAvailable = Color.black;
    private Color none = Color.cyan;

    #region Button Functions
    public void button1()
    {
        mouseClass.type = 0;
    }

    public void button2()
    {
        mouseClass.type = 1;
    }
    public void button3()
    {
        mouseClass.type = 2;
    }
    public void button4()
    {
        mouseClass.type = 3;
    }
    public void button5()
    {
        mouseClass.type = 4;
    }
    public void button6()
    {
        mouseClass.type = 5;
    }
    public void button7()
    {
        mouseClass.type = 6;
    }
    public void button8()
    {
        mouseClass.type = 7;
    }
    public void button9()
    {
        mouseClass.type = 8;
    }
    public void button10()
    {
        mouseClass.type = 9;
    }
    public void button11()
    {
        mouseClass.type = 10;
    }
    public void button12()
    {
        mouseClass.type = 11;
    }
    public void button13()
    {
        mouseClass.type = 12;
    }
    public void button14()
    {
        mouseClass.type = 13;
    }
    public void button15()
    {
        mouseClass.type = 14;
    }
    public void button16()
    {
        mouseClass.type = 15;
    }
    public void button17()
    {
        mouseClass.type = 16;
    }
    public void button18()
    {
        mouseClass.type = 17;
    }
    public void button19()
    {
        mouseClass.type = 18;
    }
    public void button20()
    {
        mouseClass.type = 19;
    }
    public void button21()
    {
        mouseClass.type = 20;
    }
    public void button22()
    {
        mouseClass.type = 21;
    }
    public void button23()
    {
        mouseClass.type = 22;
    }
    public void button24()
    {
        mouseClass.type = 23;
    }

    #endregion

    #region Mouseover functions
    public void button1enter()
    {
        infoPanel.SetActive(true);
        Debug.Log(infoPanel.activeSelf);
        writeInfoPanel(0);
    }

    public void button2enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(1);
    }

    public void button3enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(2);
    }

    public void button4enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(3);
    }

    public void button5enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(4);
    }

    public void button6enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(5);
    }

    public void button7enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(6);
    }

    public void button8enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(7);
    }
    public void button9enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(8);
    }
    public void button10enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(9);
    }
    public void button11enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(10);
    }
    public void button12enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(11);
    }
    public void button13enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(12);
    }
    public void button14enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(13);
    }
    public void button15enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(14);
    }
    public void button16enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(15);
    }
    public void button17enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(16);
    }
    public void button18enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(17);
    }
    public void button19enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(18);
    }
    public void button20enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(19);
    }
    public void button21enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(20);
    }
    public void button22enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(21);
    }
    public void button23enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(22);
    }
    public void button24enter()
    {
        infoPanel.SetActive(true);
        writeInfoPanel(23);
    }

    public void buttonexit()
    {
        infoPanel.SetActive(false);
    }
    #endregion

    void Update()
    {
        buttons[0].GetComponent<Image>().color = none;
        for (int i = 1; i < buttons.Count; i++)
        {
            if (Global.isBuildingResearched[i])
                buttons[i].GetComponent<Image>().color = none;
            else buttons[i].GetComponent<Image>().color = notAvailable;
        }

    }
    private void writeInfoPanel(int id)
    {
        infoPanel.GetComponentInChildren<TextMeshProUGUI>().text = btnInfo[id].title;
        foreach (income i in btnInfo[id].cost)
            infoPanel.GetComponentInChildren<TextMeshProUGUI>().text += "\n " + i.type + ": " + i.quantity;
        foreach (income i in btnInfo[id].product)
            infoPanel.GetComponentInChildren<TextMeshProUGUI>().text += "\n " + i.type + ": " + i.quantity;
    }

    #region Structs
    [System.Serializable]
    public struct Info
    {
        public string title;
        public List<income> cost, product;
    }

    [System.Serializable]
    public struct income
    {
        public string type;
        public int quantity;
    }
    #endregion
}

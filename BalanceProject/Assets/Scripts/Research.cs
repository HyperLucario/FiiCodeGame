using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Research : MonoBehaviour
{
    public GameObject canvas1, canvas2;
    public GameObject btnMining, btnFarm, btnFish, btnMat1, btnMat2, btnEnergy, btnOilP, btnOilE, btnNuclear, btnEco1, btnEco2, btnEco3;
    public TextMeshProUGUI researchText;
    private Color researched = Color.green;
    private Color inResearch = Color.cyan;
    private Color notAvailable = Color.black;
    private Color none = Color.white;
    private bool isResearching = false;

    void Update()
    {
        if (!Global.isBuildingResearched[10])
        {
            btnFarm.GetComponent<Image>().color = notAvailable; btnMat1.GetComponent<Image>().color = notAvailable; btnEnergy.GetComponent<Image>().color = notAvailable;
        }
        else
        {
            if(btnFarm.GetComponent<Image>().color != researched)
                btnFarm.GetComponent<Image>().color = none;
            if(btnMat1.GetComponent<Image>().color != researched)
                btnMat1.GetComponent<Image>().color = none;
            if(btnEnergy.GetComponent<Image>().color != researched)
                btnEnergy.GetComponent<Image>().color = none;
        }
        if(!Global.isBuildingResearched[12])
        {
            btnMat2.GetComponent<Image>().color = notAvailable;
        }
        else if(btnMat2.GetComponent<Image>().color != researched)
        {
            btnMat2.GetComponent<Image>().color = none; 
        }
        if(!Global.isBuildingResearched[15])
        {
            btnFish.GetComponent<Image>().color = notAvailable;
        }
        else if(btnFish.GetComponent<Image>().color != researched)
        {
            btnFish.GetComponent<Image>().color = none;
        }
        if(!Global.isBuildingResearched[16])
        {
            btnOilP.GetComponent<Image>().color = notAvailable;
        }
        else if(btnOilP.GetComponent<Image>().color != researched)
        {
            btnOilP.GetComponent<Image>().color = none;
        }
        if (!Global.isBuildingResearched[7])
        {
            btnOilE.GetComponent<Image>().color = notAvailable;
        }
        else if(btnOilE.GetComponent<Image>().color != researched)
        {
            btnOilE.GetComponent<Image>().color = none;
        }
        if(!Global.isBuildingResearched[19])
        {
            btnEco1.GetComponent<Image>().color = notAvailable;
        }
        else if(btnEco1.GetComponent<Image>().color != researched)
        {
            btnEco1.GetComponent<Image>().color = none;
        }
        if(!Global.isBuildingResearched[17])
        {
            btnEco2.GetComponent<Image>().color = notAvailable;
        }
        else if(btnEco2.GetComponent<Image>().color != researched)
        {
            btnEco2.GetComponent<Image>().color = none;
        }
        if(!Global.isBuildingResearched[18])
        {
            btnEco3.GetComponent<Image>().color = notAvailable;
        }
        else if(btnEco3.GetComponent<Image>().color != researched)
        {
            btnEco3.GetComponent<Image>().color = none;
        }
        if(!Global.isBuildingResearched[19] && !Global.isBuildingResearched[11])
        {
            btnNuclear.GetComponent<Image>().color = notAvailable;
        }
        else if(btnNuclear.GetComponent<Image>().color != researched)
        {
            btnNuclear.GetComponent<Image>().color = none;
        }
    }
    public void showResearch()
    {
        canvas1.SetActive(false);
        canvas2.SetActive(true);
    }
    
    public void hideResearch()
    {
        canvas1.SetActive(true);
        canvas2.SetActive(false);
    }

    #region Button functions
    public void ResearchMining()
    {
        if (Global.wood >= 30)
        {
            btnMining.GetComponent<Image>().color = Color.cyan;
            Global.wood -= 30;
            StartCoroutine(Wait1());
        }
    }

    public void ResearchElectricity()
    {
        if (!Global.isBuildingResearched[10]) return;
        if (Global.stone >= 20)
        {
            Global.stone -= 20;
            btnEnergy.GetComponent<Image>().color = inResearch;
            StartCoroutine(Wait2());
        }
    }

    public void ResearchMaterials1()
    {
        if (!Global.isBuildingResearched[10]) return;
        if (Global.stone >= 30 && Global.wood >= 20)
        {
            Global.stone -= 30;
            Global.wood -= 20;
            btnMat1.GetComponent<Image>().color = inResearch;
            StartCoroutine(Wait3());
        }
    }

    public void ResearchMaterials2()
    {
        if (!Global.isBuildingResearched[12]) return;
        if (Global.stone >= 20 && Global.glass >= 20)
        {
            Global.stone -= 20;
            Global.glass -= 20;
            btnMat2.GetComponent<Image>().color = inResearch;
            StartCoroutine(Wait4());
        }
    }

    public void ResearchFarm()
    {
        if (!Global.isBuildingResearched[10]) return;

        if (Global.wood >= 30)
        {
            Global.wood -= 30;
            btnFarm.GetComponent<Image>().color = inResearch;
            StartCoroutine(Wait5());
        }
    }

    public void ResearchFish()
    {
        if (!Global.isBuildingResearched[15]) return;
        if (Global.wood >= 20 && Global.stone >= 20)
        {
            Global.wood -= 20;
            Global.stone -= 20;
            btnFish.GetComponent<Image>().color = inResearch;
            StartCoroutine(Wait6());
        }
    }

    public void ResearchOil()
    {
        if (!Global.isBuildingResearched[16]) return;
        if (Global.stone >= 50 && Global.bricks >= 20)
        {
            Global.stone -= 50;
            Global.bricks -= 20;
            btnOilP.GetComponent<Image>().color = inResearch;
            StartCoroutine(Wait7());
        }
    }

    public void ResearchOilEnergy()
    {
        if (!Global.isBuildingResearched[7]) return;
        if (Global.stone >= 30 && Global.bricks >= 50)
        {
            Global.stone -= 30;
            Global.bricks -= 50;
            btnOilE.GetComponent<Image>().color = inResearch;
            StartCoroutine(Wait8());
        }
    }

    public void ResearchNuclear()
    {
        if (!Global.isBuildingResearched[8]) return;
        if (Global.metal >= 80)
        {
            Global.metal -= 80;
            btnNuclear.GetComponent<Image>().color = inResearch;
            StartCoroutine(Wait9());
        }
    }

    public void ResearchEco1()
    {
        if (!Global.isBuildingResearched[20]) return;
        if (Global.glass >= 50 && Global.metal >= 20)
        {
            Global.glass -= 50;
            Global.metal -= 80;
            btnEco1.GetComponent<Image>().color = inResearch;
            StartCoroutine(Wait10());
        }
    }

    public void ResearchEco2()
    {
        if (!Global.isBuildingResearched[17]) return;
        if (Global.glass >= 60 && Global.metal >= 40)
        {
            Global.glass -= 60;
            Global.metal -= 40;
            btnEco2.GetComponent<Image>().color = inResearch;
            StartCoroutine(Wait11());
        }
    }

    public void ResearchEco3()
    {
        if (!Global.isBuildingResearched[18]) return;
        if (Global.metal >= 100)
        {
            Global.metal -= 100;
            btnEco3.GetComponent<Image>().color = inResearch;
            StartCoroutine(Wait12());
        }
    }

    IEnumerator Wait1()
    {
        yield return new WaitForSeconds(10);
        Global.isBuildingResearched[5] = true;
        Global.isBuildingResearched[6] = true;
        Global.isBuildingResearched[9] = true;
        Global.isBuildingResearched[10] = true;
        btnMining.GetComponent<Image>().color = researched;
    }

    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(10);
        Global.isBuildingResearched[19] = true;
        btnEnergy.GetComponent<Image>().color = researched;
    }

    IEnumerator Wait3()
    {
        yield return new WaitForSeconds(10);
        Global.isBuildingResearched[3] = true;
        Global.isBuildingResearched[12] = true;
        btnMat1.GetComponent<Image>().color = researched;
    }
    IEnumerator Wait4()
    {
        yield return new WaitForSeconds(10);
        Global.isBuildingResearched[11] = true;
        Global.isBuildingResearched[8] = true;
        btnMat2.GetComponent<Image>().color = researched;
    }
    //farm fish oil oilen nucle eco123
    IEnumerator Wait5()
    {
        yield return new WaitForSeconds(10);
        Global.isBuildingResearched[15] = true;
        btnFarm.GetComponent<Image>().color = researched;
    }
    IEnumerator Wait6()
    {
        yield return new WaitForSeconds(10);
        Global.isBuildingResearched[16] = true;
        btnFish.GetComponent<Image>().color = researched;
    }
    IEnumerator Wait7()
    {
        yield return new WaitForSeconds(10);
        Global.isBuildingResearched[7] = true;
        btnOilP.GetComponent<Image>().color = researched;
    }

    IEnumerator Wait8()
    {
        yield return new WaitForSeconds(10);
        Global.isBuildingResearched[20] = true;
        btnOilE.GetComponent<Image>().color = researched;
    }

    IEnumerator Wait9()
    {
        yield return new WaitForSeconds(10);
        Global.isBuildingResearched[23] = true;
        btnNuclear.GetComponent<Image>().color = researched;
    }
    IEnumerator Wait10()
    {
        yield return new WaitForSeconds(10);
        Global.isBuildingResearched[13] = true;
        Global.isBuildingResearched[17] = true;
        Global.isBuildingResearched[21] = true;
        btnEco1.GetComponent<Image>().color = researched;
    }

    IEnumerator Wait11()
    {
        yield return new WaitForSeconds(10);
        Global.isBuildingResearched[18] = true;
        btnEco2.GetComponent<Image>().color = researched;
    }

    IEnumerator Wait12()
    {
        yield return new WaitForSeconds(10);
        Global.isBuildingResearched[22] = true;
        btnEco3.GetComponent<Image>().color = researched;
    }

    #endregion

    #region Mouseover Functions
    public void mouseoverMine()
    {
        researchText.text = "10 seconds to research \n 10 Wood";
    }

    public void mouseoverEnergy()
    {
        researchText.text = "10 seconds to research \n 20 Stone";
    }

    public void mouseoverMat1()
    {
        researchText.text = "10 seconds to research \n 20 Wood \n 30 Stone";
    }

    public void mouseoverMat2()
    {
        researchText.text = "10 seconds to research \n 20 Stone \n 20 Glass";
    }
    public void mouseoverFarm()
    {
        researchText.text = "10 seconds to research \n 30 Wood";
    }

    public void mouseoverFish()
    {
        researchText.text = "10 seconds to research \n 20 Wood \n 20 Stone";
    }

    public void mouseoverOilP()
    {
        researchText.text = "10 seconds to research \n 50 Stone \n 20 Bricks";
    }

    public void mouseoverOilE()
    {
        researchText.text = "10 seconds to research \n 30 Stone \n 50 Bricks";
    }

    public void mouseoverEco1()
    {
        researchText.text = "10 seconds to research \n 50 Glass \n 80 Metal";
    }
    public void mouseoverEco2()
    {
        researchText.text = "10 seconds to research \n 60 Glass \n 40 Metal";
    }
    public void mouseoverEco3()
    {
        researchText.text = "10 seconds to research \n 100 Metal";
    }
    public void mouseoverNuclear()
    {
        researchText.text = "10 seconds to research \n 80 Metal";
    }
    #endregion
}

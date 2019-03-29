using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameButtonsScript : MonoBehaviour
{
    public GameObject panelB, panelM, panelQ, panelP, panelR, panelBlock;
    public TileHover mouseClass;
    public TimeManager tm;

    private bool questBoxIsOpen = false, researchBoxIsOpen = false;

    public void ChangeStateBuildingsPanel()
    {
        mouseClass.type = -1;
        panelM.SetActive(false);
        panelB.SetActive(!panelB.activeSelf);
    }

    public void ChangeStateMaterialsPanel()
    {
        panelB.SetActive(false);
        panelM.SetActive(!panelM.activeSelf);
    }

    public void ChangeStateQuestPanel()
    {
        panelQ.SetActive(!panelQ.activeSelf);
        questBoxIsOpen = !questBoxIsOpen;
    }

    public void ChangeStateResearchPanel()
    {
        if (questBoxIsOpen) ChangeStateQuestPanel();

        ChangeStateBlockingPanel();

        panelR.SetActive(!panelR.activeSelf);
        researchBoxIsOpen = !researchBoxIsOpen;
    }

    public void ChangeStatePausePanel()
    {
        panelP.SetActive(!panelP.activeSelf);
    }

    public void ChangeStateBlockingPanel()
    {
        panelBlock.SetActive(!panelBlock.activeSelf);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void Save()
    {
        MapSave.Save();
    }

    public void ChangeScene(string sceneToChangeTo)
    {
        MapSave.Save();
        SceneManager.LoadScene(sceneToChangeTo);
    }

    private void Update()
    {
        if (panelP.activeSelf == true)
        {
            tm.ok = false;
        }
        else tm.ok = true;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (researchBoxIsOpen || questBoxIsOpen)
            {
                if (researchBoxIsOpen)
                {
                    ChangeStateResearchPanel();
                }
                if (questBoxIsOpen)
                {
                    ChangeStateQuestPanel();
                }
            }
            else
            {
                ChangeStatePausePanel();
            }
        }
    }
}

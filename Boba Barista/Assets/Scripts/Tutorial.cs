using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private UIMainMenuScene m_uiMainMenuScene;
    [SerializeField] private GameObject[] tutorialPanels;
    private int currentTutorial = 0;
    [SerializeField] TextMeshProUGUI helpHeaderTxt;

    void Awake()
    {
        m_uiMainMenuScene = FindFirstObjectByType<UIMainMenuScene>();
    }
    private void Start()
    {
        UpdatePanels();
        UpdateButtons();
        PerformPanelSpecificActions(); // Call this to handle initial state
    }

    public void Next()
    {
        if (currentTutorial < tutorialPanels.Length - 1)
        {
            currentTutorial++;
            UpdatePanels();
            UpdateButtons();
            PerformPanelSpecificActions(); // Call after panel change
        }
    }

    public void Back()
    {
        if (currentTutorial > 0)
        {
            currentTutorial--;
        }
        else
        {
            m_uiMainMenuScene.helpPanel.SetActive(false);
        }

        UpdatePanels();
        UpdateButtons();
        PerformPanelSpecificActions(); // Call after panel change
    }

    private void UpdatePanels()
    {
        for (int i = 0; i < tutorialPanels.Length; i++)
        {
            tutorialPanels[i].SetActive(i == currentTutorial);
        }
    }

    private void UpdateButtons()
    {
        // Next is only interactable if we're not on the last panel
        m_uiMainMenuScene.nextButton.interactable = currentTutorial < tutorialPanels.Length - 1;

        // Back is always interactable now, as per your requirement
        m_uiMainMenuScene.backButton.interactable = true;
    }

    private void PerformPanelSpecificActions()
    {
        switch (currentTutorial)
        {
            case 0:
                helpHeaderTxt.text = "OBJECTIVE";
                break;
            case 1:
                helpHeaderTxt.text = "KEYBOARD CONTROLS";
                break;
            case 2:
                helpHeaderTxt.text = "KEYBOARD CONTROLS";
                break;
            case 3:
                helpHeaderTxt.text = "MOUSE CONTROLS";
                break;
            case 4:
                helpHeaderTxt.text = "MOUSE CONTROLS";
                break;
            case 5:
                helpHeaderTxt.text = "HEALTH";
                break;
        }
    }
}
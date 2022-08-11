using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum mainMenuStates { pressToStart, optionsList };

public class MainMenuBehavior : MonoBehaviour
{
  public mainMenuStates menuStates;

  [SerializeField] GameObject startInst, titleLabel, titleLabel2, optionsMainMenu, labelTrigger;

  void Start()
  {
    startInst = GameObject.Find("StartInstruction");
    titleLabel = GameObject.Find("Title");
    titleLabel2 = GameObject.Find("Title1");
    optionsMainMenu = GameObject.Find("OptionsMainMenu");

    optionsMainMenu.SetActive(false);
    titleLabel2.SetActive(false);

    menuStates = mainMenuStates.pressToStart;

  }

  private void Update()
  {
    ValidateSpace();
    Debug.Log("valor menuStates= " + menuStates);
  }

  public void ShowMainMenuOptions()
  {
    startInst.SetActive(false);
    titleLabel.SetActive(false);
    titleLabel2.SetActive(true);
    optionsMainMenu.SetActive(true);
  }

  public void ShowPrimevalScreen()
  {
    startInst.SetActive(true);
    titleLabel.SetActive(true);
    titleLabel2.SetActive(false);
    optionsMainMenu.SetActive(false);
  }

  public void ValidateSpace()
  {
    if (Input.GetKey(KeyCode.Space) && menuStates == mainMenuStates.pressToStart)
    {
      ShowMainMenuOptions();
      menuStates = mainMenuStates.optionsList;
    }
    else
    {
      if (Input.GetKey(KeyCode.Return) && menuStates == mainMenuStates.optionsList)
      {
        ShowPrimevalScreen();
        menuStates = mainMenuStates.pressToStart;
      }
    }
  }

  public void StartGame()
  {
    SceneManager.LoadScene("SampleScene");
  }
}
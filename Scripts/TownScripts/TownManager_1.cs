using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager_1 : MonoBehaviour
{
  public  GameObject[] townButtons;
  public GameObject[] townScrollViews;
  private void Awake()
  {
    townButtons = new GameObject[4];
    townScrollViews = new GameObject[2];
    townButtons = GameObject.FindGameObjectsWithTag("Btn_Town");
    townScrollViews = GameObject.FindGameObjectsWithTag("ScrVw_Town");
  }
  // Start is called before the first frame update
  void Start() {
    for (int i = 0; i < townButtons.Length; i++)
    {
      townScrollViews[i].SetActive(false);
    }
  }

    // Update is called once per frame
    void Update()
    {
        
    }
  #region Menu Trader Behavior On and Off
  public void TraderSetOnandOff()
  {
    switch (townButtons[3].activeSelf)
    {
      case true:
        townButtons[0].SetActive(false);
        townButtons[1].SetActive(false);
        townButtons[2].SetActive(false);
        townButtons[3].SetActive(false);
        townScrollViews[0].SetActive(true);
        break;
      case false:
        townButtons[0].SetActive(true);
        townButtons[1].SetActive(true);
        townButtons[2].SetActive(true);
        townButtons[3].SetActive(true);
        townScrollViews[0].SetActive(true);
        break;
    }
  }
  #endregion
}

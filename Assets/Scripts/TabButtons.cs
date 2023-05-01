using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabButtons : MonoBehaviour
{
    public GameObject[] tabs;
    public GameObject[] tabButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TabButtonsPressed(int tabIndex)
    {
        // Activate the tab with the specified index and deactivate other tabs
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(i == tabIndex);
        }
    }


}

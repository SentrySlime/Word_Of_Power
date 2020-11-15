using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Checking : MonoBehaviour
{

    public Button[] buttons;
    public GameObject buttonTest;

    void Start()
    {
        buttonTest = Resources.Load<GameObject>("Abilities/Button");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(buttons[0].gameObject.activeSelf == true)
            {
                buttons[0].gameObject.SetActive(false);
            }
            else
            {
                buttons[0].gameObject.SetActive(true);
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (buttons[1].gameObject.activeSelf == true)
            {
                buttons[1].gameObject.SetActive(false);
            }
            else
            {
                buttons[1].gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (buttons[2].gameObject.activeSelf == true)
            {
                buttons[2].gameObject.SetActive(false);
            }
            else
            {
                buttons[2].gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (buttons[3].gameObject.activeSelf == true)
            {
                buttons[3].gameObject.SetActive(false);
            }
            else
            {
                buttons[3].gameObject.SetActive(true);
            }
        }

    }
}

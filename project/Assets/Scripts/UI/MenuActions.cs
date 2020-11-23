/*-----------------------------------
    File Name: MenuActions.cs
    Purpose: Control the menu actions
    Author: Ruben Anato
    Modified: 23 November 2020
-------------------------------------
    Copyright 2020 Caffeinated.
-----------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuActions : MonoBehaviour
{
    public GameObject startMenu;
    public bool pause_menu = false;

    public GameObject DefaultSelectedButton;

    private GameObject activeMenu;
    private PlayerControls Controles;

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    private void Awake()
    {
        Controles = new PlayerControls();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active
    /// </summary>
    private void OnEnable()
    {
        Controles.Player.Pause.performed += Pause_performed;
        Controles.Player.Pause.Enable();
        //Controles.Enable();
    }

    /// <summary>
    /// Perform pause
    /// </summary>
    /// <param name="obj"></param>
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (pause_menu)
        {
            Toggle();
        }
    }
    public void m_Pause()
    {
        if (pause_menu)
        {
            Toggle();
        }
    }

    /// <summary>
    /// This function is called when the object becomes disabled or inactive
    /// </summary>
    private void OnDisable()
    {
        Controles.Player.Pause.performed -= Pause_performed;
        Controles.Player.Pause.Disable();
        //Controles.Disable();
    }

    /// <summary>
    /// Start is called just before any of the Update methods is called the first time
    /// </summary>
    void Start()
    {
        activeMenu = startMenu;
        EventSystem.current.SetSelectedGameObject(DefaultSelectedButton);
    }

    // Update is called once per frame
    void Update()
    {
        //if (pause_menu)
        //{
        //    float p_Input = Controles.Default.Pause.ReadValue<float>();
        //    if (p_Input > 0)
        //    {
        //        Toggle();
        //    }
        //}
    }

    /// <summary>
    /// Toggle menu
    /// </summary>
    public void Toggle()
    {
        if (!startMenu.activeInHierarchy)
        {
            activeMenu.SetActive(true);
            Time.timeScale = 0;

            // clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            // set a new selected object
            EventSystem.current.SetSelectedGameObject(DefaultSelectedButton);
        }
        else
        {
            activeMenu.SetActive(false);
            Time.timeScale = 1;
        }
        
    }

    /// <summary>
    /// Load chosen scene
    /// </summary>
    /// <param name="Level">The chosen scene</param>
    public void SetScene(int Level)
    {
        Debug.Log("Load Scene");
        SceneManager.LoadScene(Level);
        Time.timeScale = 1;
    }

    /// <summary>
    /// Set up menu
    /// </summary>
    /// <param name="menu">The chosen menu</param>
    public void SetMenu(GameObject menu)
    {
        // Disable current menu
        activeMenu.SetActive(false);
        // Set new menu
        activeMenu = menu;
        // display new menu
        activeMenu.SetActive(true);
    }

    /// <summary>
    /// Set up button
    /// </summary>
    /// <param name="button">The chosen button</param>
    public void SetButton(GameObject button)
    {
        /* really want some input validation
         * and some insurance to make sure the button doesnt get de-selected when sswitching from keyboard to controler
         */
        // Disable selected button
        EventSystem.current.SetSelectedGameObject(null);
        // Set new button
        DefaultSelectedButton = button;
        EventSystem.current.SetSelectedGameObject(DefaultSelectedButton);
    }

    /// <summary>
    /// Close the app
    /// </summary>
    public void CloseApp()
    {
        Application.Quit();
    }
}

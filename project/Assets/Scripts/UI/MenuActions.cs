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
    private void Awake()
    {
        Controles = new PlayerControls();
    }

    private void OnEnable()
    {
        Controles.Player.Pause.performed += Pause_performed;
        Controles.Player.Pause.Enable();
        //Controles.Enable();
    }

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

    private void OnDisable()
    {
        Controles.Player.Pause.performed -= Pause_performed;
        Controles.Player.Pause.Disable();
        //Controles.Disable();
    }
    // Start is called before the first frame update
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
    public void SetScene(int Level)
    {
        Debug.Log("Load Scene");
        SceneManager.LoadScene(Level);
        Time.timeScale = 1;
    }
    public void SetMenu(GameObject menu)
    {
        // Disable current menu
        activeMenu.SetActive(false);
        // Set new menu
        activeMenu = menu;
        // display new menu
        activeMenu.SetActive(true);
    }
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
    public void CloseApp()
    {
        Application.Quit();
    }
}

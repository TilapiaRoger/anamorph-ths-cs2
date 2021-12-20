using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWindowUI : MonoBehaviour
{
    [SerializeField] GameObject window;
    [SerializeField] GameObject sourceWindow;

    [SerializeField] bool sourceWindowDisappears = false;

    // Start is called before the first frame update
    void Start()
    {
        //window.SetActive(false);
    }

    public void OpenWindow()
    {
        
        if(sourceWindowDisappears == true)
        {
            HideSourceWindow();
        }

        window.SetActive(true);

    }

    void HideSourceWindow()
    {
        sourceWindow.SetActive(false);
    }

    void ShowSourceWindow()
    {
        sourceWindow.SetActive(true);
    }

    public void CloseWindow()
    {
        window.SetActive(false);

        if (sourceWindowDisappears == true)
        {
            ShowSourceWindow(); 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomButtonGUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] Button button;
    [SerializeField] Text buttonText;

    private Text defaultButtonText;

    private int defaultButtonTextSize;
    private Color defaultButtonColor;

    [Header("Custom Properties")]
    [SerializeField] int hoverSize;
    [SerializeField] Color clickButtonColor;

    // Start is called before the first frame update
    void Start()
    {
        defaultButtonText = buttonText;
        defaultButtonColor = defaultButtonText.color;
        defaultButtonTextSize = defaultButtonText.fontSize;

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.fontSize = hoverSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.fontSize = defaultButtonTextSize;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonText.fontSize = defaultButtonTextSize;
        buttonText.color = defaultButtonColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonText.fontSize = hoverSize;
        buttonText.color = clickButtonColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{

    [SerializeField] private PlayerMovement player;

    [SerializeField] private Slider moveSlider;
    [SerializeField] private Slider rotateSlider;

    [SerializeField] private InputField moveValField;
    [SerializeField] private InputField rotateValField;

    [SerializeField] private Text errorMessage;

    private float moveSpeed;
    private float rotateSpeed;

    private Regex numRegex;


    // Start is called before the first frame update
    void Start()
    {
        numRegex = new Regex(@"\d+");

        errorMessage.gameObject.SetActive(false);

        moveSpeed = player.movementSpeed;
        rotateSpeed = player.rotateSpeed;

        moveSlider.value = moveSpeed;
        rotateSlider.value = rotateSpeed;

        player.SetMoveSpeed(moveSpeed);
        player.SetRotateSpeed(rotateSpeed);

        moveValField.text = moveSpeed.ToString();
        rotateValField.text = rotateSpeed.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSliderVal()
    {
        moveSpeed = moveSlider.value;
        rotateSpeed = rotateSlider.value;

        moveValField.text = moveSpeed.ToString();
        rotateValField.text = rotateSpeed.ToString();

        updatePlayerStats();
    }

    public void ChangeInputFieldVal()
    {
        float prevMoveSpeed, prevRotateSpeed;

        prevMoveSpeed = moveSpeed;
        prevRotateSpeed = rotateSpeed;

        if (numRegex.IsMatch(rotateValField.text))
        {
            errorMessage.gameObject.SetActive(false);

            rotateSpeed = float.Parse(rotateValField.text);

            updatePlayerStats();
        }
        if (numRegex.IsMatch(moveValField.text))
        {
            errorMessage.gameObject.SetActive(false);

            moveSpeed = float.Parse(moveValField.text);

            updatePlayerStats();
        }

        else
        {
            errorMessage.gameObject.SetActive(true);

            moveValField.text = prevMoveSpeed.ToString();
            rotateValField.text = prevRotateSpeed.ToString();
        }

        moveSlider.value = moveSpeed;
        rotateSlider.value = rotateSpeed;
    }

    void updatePlayerStats()
    {
        player.SetMoveSpeed(moveSpeed);
        player.SetRotateSpeed(rotateSpeed);
    }

    public void CloseWindow()
    {
        GameObject player = GameObject.Find("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        
        this.gameObject.SetActive(false);
        playerMovement.SetMoveStatus(true);
    }
}

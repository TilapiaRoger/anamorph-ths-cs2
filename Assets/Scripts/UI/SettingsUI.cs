using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{

    [SerializeField] private PlayerMovement player;

    [SerializeField] private Slider moveSpeedSlider;
    [SerializeField] private Slider rotateSpeedSlider;

    [SerializeField] private InputField moveValField;
    [SerializeField] private InputField rotateValField;

    private float moveSpeed;
    private float rotateSpeed;

    private float curPlayerZrotation;

    private Regex numRegex;

    [SerializeField] private Slider zRotateSlider;

    public GameObject modelSpawnPoint;
    public Image rotatorModelImage;
    [SerializeField] public Sprite[] modelImageList;

    private bool startsUpsideDown = true;

    // Start is called before the first frame update
    void Start()
    {
        numRegex = new Regex(@"\d+");

        moveSpeed = player.movementSpeed;
        moveSpeedSlider.value = moveSpeed;

        rotateSpeed = player.rotateSpeed;
        rotateSpeedSlider.value = rotateSpeed;

        Debug.Log("Rotate Speed: " + rotateSpeed);

        moveValField.text = moveSpeed.ToString();
        rotateValField.text = rotateSpeed.ToString();

        player.SetMoveSpeed(moveSpeed);
        player.SetRotateSpeed(rotateSpeed);

        curPlayerZrotation = zRotateSlider.value;

        for (int i = 0; i < modelImageList.Length; i++)
        {
            Debug.Log("Model name: " + modelSpawnPoint.transform.GetChild(0).name);
            if (modelSpawnPoint.transform.GetChild(0).name.StartsWith(modelImageList[i].name))
            {
                rotatorModelImage.sprite = modelImageList[i];
            }
        }
        
        /*if (Vector3.Dot(modelSpawnPoint.transform.GetChild(0).up, Vector3.down) > 0)
        {
            zRotateSlider.value = 180;
            rotatorModelImage.transform.localEulerAngles = new Vector3(0, 0, -zRotateSlider.value);
        }*/
    }
    public void RotateZModel()
    {
        curPlayerZrotation = zRotateSlider.value;
        //rotatorModelImage.transform.Rotate(Vector3.forward * curPlayerZrotation);

        rotatorModelImage.transform.localEulerAngles = new Vector3(0, 0, -curPlayerZrotation);

        player.SetPlayerZRotation(curPlayerZrotation);
    }

    public void RealTimeInputTextChange()
    {
        moveValField.text = moveSpeedSlider.value.ToString();
        rotateValField.text = rotateSpeedSlider.value.ToString();
    }

    public void ChangeSliderVal()
    {
        updatePlayerStats(moveSpeedSlider.value, rotateSpeedSlider.value);
    }

    public void ChangeInputFieldVal()
    {
        float prevMoveSpeed, prevRotateSpeed;

        prevMoveSpeed = moveSpeed;
        prevRotateSpeed = rotateSpeed;

        if (numRegex.IsMatch(rotateValField.text) || !(rotateValField.text == "0." || rotateValField.text == "."))
        {
            rotateSpeed = float.Parse(rotateValField.text);
            rotateSpeedSlider.value = rotateSpeed;
            updatePlayerStats(moveSpeed, rotateSpeed);
        }
        if (numRegex.IsMatch(moveValField.text) || !(moveValField.text == "0." || moveValField.text == "."))
        {
            moveSpeed = float.Parse(moveValField.text);
            moveSpeedSlider.value = moveSpeed;
            updatePlayerStats(moveSpeed, rotateSpeed);
        }
        else
        {
            moveValField.text = prevMoveSpeed.ToString();
            rotateValField.text = prevRotateSpeed.ToString();
        }
    }

    void updatePlayerStats(float newMoveSpeed, float newRotateSpeed)
    {
        player.SetMoveSpeed(newMoveSpeed);
        player.SetRotateSpeed(newRotateSpeed);
    }
}

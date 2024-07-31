using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; set; }

    public string Element1Listner {  get; set; }
    public string Element2Listner { get; set; }
    public string Element3Listner { get; set; }
    public string ConfirmListner { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public bool CloseGame()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    //add input element 1
    public bool Element1()
    {
        return Input.GetKey(KeyCode.Q);
    }
    public bool Element1Down()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }
    public bool Element1Up()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }

    //add input element 2
    public bool Element2()
    {
        return Input.GetKey(KeyCode.E);
    }
    public bool Element2Down()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
    public bool Element2Up()
    {
        return Input.GetKeyUp(KeyCode.E);
    }

    //add input element 3
    public bool Element3()
    {
        return Input.GetMouseButton(1);
    }
    public bool Element3Down()
    {
        return Input.GetMouseButtonDown(1);
    }
    public bool Element3Up()
    {
        return Input.GetMouseButtonUp(1);
    }

    //add input confirm
    public bool Confirm()
    {
        return Input.GetMouseButton(0);
    }
    public bool ConfirmDown()
    {
        return Input.GetMouseButtonDown(0);
    }
    public bool ConfirmUp()
    {
        return Input.GetMouseButtonUp(0);
    }

    //add input move
    public float Horizontal()
    {
        return 0f;
    }
    public float Vertical()
    {
        return 0f;
    }
    public float Horizontal01()
    {
        return 0f;
    }
    public float Vertical01()
    {
        return 0f;
    }

    //add input camera movement
    public float CameraHorzontal()
    {
        return 0f;
    }
    public float CameraVertical()
    {
        return 0f;
    }
    public float CameraHorzontal01()
    {
        return 0f;
    }
    public float CameraVertical01()
    {
        return 0f;
    }


    //add input listner
}

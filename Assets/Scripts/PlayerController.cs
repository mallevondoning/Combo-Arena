using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _playerCamera;

    [SerializeField] private ProjectileController _bullet;
    [SerializeField] private Transform _muzzle;

    private int _listPos = 0;

    private float xRot;
    private float yRot;

    [SerializeField] private float sensitivity = 100f;
    private float senX;
    private float senY;

    [SerializeField] private bool _centerScreen = false;

    private void Awake()
    {
        Array.Fill(DataManager.ComboList, ElementType.NoneID);

        senX = senY = sensitivity; //<--- fix so separate

        if (_centerScreen)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1)) _listPos++;
        
        if (_listPos > 3)
        {
            _listPos = 1;
            Array.Fill(DataManager.ComboList, ElementType.NoneID);
        }

        if (Input.GetKeyDown(KeyCode.Q)) DataManager.ComboList[_listPos - 1] = ElementType.fire;
        if (Input.GetKeyDown(KeyCode.E)) DataManager.ComboList[_listPos - 1] = ElementType.lighting;
        if (Input.GetMouseButtonDown(1)) DataManager.ComboList[_listPos - 1] = ElementType.ice;

        if (Input.GetMouseButtonDown(0))
        {
            if (DataManager.ComboList[0] != ElementType.NoneID)
            {
                //<make> swap to next step
                ProjectileController projectile = Instantiate(_bullet, _muzzle);
               
                if (projectile != null)
                {
                    projectile.Init(_playerCamera.forward);
                    projectile.gameObject.transform.parent.DetachChildren(); //<--- something feels wrong
                }
                else Debug.LogError("no ProjectileController");

                //Debug.Log(DebugCombo(DataManager.ComboList));
                //</make>
            }
            else
            {
                Debug.Log("No element has selected");
            }

            _listPos = 0;
            Array.Fill(DataManager.ComboList, ElementType.NoneID);
        }

        MoveCamera();
    }

    private void MoveCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senY;

        xRot += mouseX;
        yRot -= mouseY;
        yRot = Mathf.Clamp(yRot, -90f, 90f);

        transform.rotation = Quaternion.Euler(0, xRot, 0);
        _playerCamera.rotation = Quaternion.Euler(yRot, transform.rotation.eulerAngles.y, 0);
    }

    private string DebugCombo(ElementType[] ComboList)
    {
        string combo = "";
        for (int i = 0; i < DataManager.ComboList.Length; i++)
        {
            switch (DataManager.ComboList[i])
            {
                case ElementType.fire:
                    combo += "F";
                    break;
                case ElementType.lighting:
                    combo += "L";
                    break;
                case ElementType.ice:
                    combo += "I";
                    break;
            }
            combo += " ";
        }
        return combo;
    }
}

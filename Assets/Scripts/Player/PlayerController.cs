using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private Rigidbody _playerRigidbody;

    [SerializeField] private List<Transform> _muzzleList;
    [SerializeField] private Animation _anim;

    private Locomation _locomation = new Locomation();

    private int _listPos = 0;

    private float speed = 15f;

    //<update> move to option
    [SerializeField] private float sensitivity = 600f;
    private float senX;
    private float senY;
    //</update>

    private bool _isCoActive = false;

    private void Awake()
    {
        Array.Fill(DataManager.ComboList, ElementType.NoneID);

        senX = senY = sensitivity;
        DataManager.Sensitvity = sensitivity;
        DataManager.senX = senX;
        DataManager.senY = senY;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1)) _listPos++;
        
        if (_listPos > 3)
        {
            _listPos = 1;
            Array.Fill(DataManager.ComboList, ElementType.NoneID);
        }

        //<update> make this more general
        if (Input.GetKeyDown(KeyCode.Q)) DataManager.ComboList[_listPos - 1] = ElementType.fire;
        if (Input.GetKeyDown(KeyCode.E)) DataManager.ComboList[_listPos - 1] = ElementType.lighting;
        if (Input.GetMouseButtonDown(1)) DataManager.ComboList[_listPos - 1] = ElementType.ice;
        //</update>

        if (Input.GetMouseButtonDown(0) && !_isCoActive)
        {
            if (DataManager.ComboList[0] != ElementType.NoneID)
            {
                //Finds the Element combo
                ElementComboContens elementCombo = FindElementComboContens();

                if (!_isCoActive && elementCombo != null) StartCoroutine(BulletCoroutine(elementCombo));
                else Debug.LogError(DebugCombo() + " does not exsist in manager");
            }
            else Debug.Log("No element has selected");

            _listPos = 0;
            Array.Fill(DataManager.ComboList, ElementType.NoneID);
        }
    }

    private void FixedUpdate()
    {
        _locomation.MoveCamera(transform, _playerCamera);
        _locomation.MovePlayer(transform, speed);
        _locomation.JumpPlayer(transform);
    }

    private ElementComboContens FindElementComboContens()
    {
        List<ElementComboContens> ECCList = ElementComboManager.Instance.ECContens;
        ElementComboContens ECC = null;

        if (ECCList == null) return null;

        int listConf = 0;
        for (int i = 0; i < ECCList.Count; i++)
        {
            for (int o = 0; o < ECCList[i].ElementCombo.Length; o++)
            {
                if (ECCList[i].ElementCombo[o] == DataManager.ComboList[o]) listConf++;
                else
                {
                    listConf = 0;
                    break;
                }
            }

            if (listConf >= 3)
            {
                ECC = ECCList[i];
                break;
            }
        }

        return ECC;
    }

    private IEnumerator BulletCoroutine(ElementComboContens ECC)
    {
        _isCoActive = true;

        if (ECC.Animation != null && !ECC.Animation.empty)
        {
            _anim.Play(ECC.Animation.name);
            yield return new WaitForSeconds(Mathf.Lerp(0, ECC.Animation.length, ECC.WhenToSpawn));
        }

        for (int i = 0; i < ECC.Contens.Count; i++)
        {
            if (ECC.Contens[i].Projectile == null) break;

            for (int o = 0; o < ECC.Contens[i].Amount; o++)
            {
                ProjectileController projectile = ECC.Contens[i].Projectile.GetComponent<ProjectileController>();

                Instantiate(projectile, _muzzleList[(int)ECC.Contens[i].Hand - 1].position, Quaternion.LookRotation(_playerCamera.forward));

                yield return new WaitForSeconds(ECC.Contens[i].DelayTimerInSec);
            }
        }

        _isCoActive = false;
        yield return null;
    }

    private string DebugCombo()
    {
        string combo = "\"";
        for (int i = 0; i < DataManager.ComboList.Length; i++)
        {
            switch (DataManager.ComboList[i])
            {
                case ElementType.fire:
                    combo += "Fire";
                    break;
                case ElementType.lighting:
                    combo += "Lighting";
                    break;
                case ElementType.ice:
                    combo += "Ice";
                    break;
                case ElementType.NoneID:
                    break;
            }
            if (DataManager.ComboList[i] != ElementType.NoneID) combo += ", ";
        }
        combo = combo.Remove(combo.Length-2, 2); //removes too much
        combo += "\"";
        return combo;
    }
}

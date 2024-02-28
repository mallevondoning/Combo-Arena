using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _playerCamera;

    [SerializeField] private ProjectileController _bullet; //<== remove
    [SerializeField] private List<Transform> _muzzleList;
    [SerializeField] private Animation _anim;

    private int _listPos = 0;

    private float xRot;
    private float yRot;

    //<update> move to option
    [SerializeField] private float sensitivity = 150f;
    private float senX;
    private float senY;
    //</update>

    private bool _isCoActive = false;

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
                ElementComboContens elementComboContens = FindElementComboContens();

                if (!_isCoActive && elementComboContens != null) StartCoroutine(BulletCoroutine(elementComboContens));
                else Debug.LogError(DebugCombo() + " does not exsist");
            }
            else Debug.Log("No element has selected");

            _listPos = 0;
            Array.Fill(DataManager.ComboList, ElementType.NoneID);
        }

        MoveCamera();
    }

    //move to locomotion script
    private void MoveCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senY;

        xRot += mouseX;
        yRot -= mouseY;
        yRot = Mathf.Clamp(yRot, -90f, 90f);

        transform.rotation = Quaternion.Euler(0, xRot, 0); //<-- some wierd unity bug/system problem
        _playerCamera.rotation = Quaternion.Euler(yRot, transform.rotation.eulerAngles.y, 0); //<-- some wierd unity bug/system problem
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
            if (DataManager.ComboList[i] != ElementType.NoneID && i < DataManager.ComboList.Length-1) combo += ", ";
        }
        combo = combo.Remove(combo.Length-2, 2);
        combo += "\"";
        return combo;
    }
}

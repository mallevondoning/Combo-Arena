using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private Rigidbody _playerRigidbody;

    [SerializeField] private List<Transform> _muzzleList;
    [SerializeField] private Animation _anim;
    [SerializeField] private AnimationCurve _jumpCurve;

    private Locomation _locomation = new Locomation();

    private float speed = 10f;

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
        DataManager.SenX = senX;
        DataManager.SenY = senY;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (InputManager.Instance.Element1Down() || InputManager.Instance.Element2Down() || InputManager.Instance.Element3Down()) DataManager.SetCurrentElement++;
        
        if (DataManager.SetCurrentElement > 3)
        {
            DataManager.SetCurrentElement = 1;
            DataManager.HasResetElement = true;
            Array.Fill(DataManager.ComboList, ElementType.NoneID);
        }

        //<update> make this more general
        if (InputManager.Instance.Element1Down()) DataManager.ComboList[DataManager.SetCurrentElement - 1] = ElementType.fire;
        if (InputManager.Instance.Element2Down()) DataManager.ComboList[DataManager.SetCurrentElement - 1] = ElementType.lighting;
        if (InputManager.Instance.Element3Down()) DataManager.ComboList[DataManager.SetCurrentElement - 1] = ElementType.ice;
        //</update>

        if (InputManager.Instance.ConfirmDown() && !_isCoActive)
        {
            if (DataManager.ComboList[0] != ElementType.NoneID)
            {
                //Finds the Element combo
                ElementComboContens elementCombo = FindElementComboContens();

                if (!_isCoActive && elementCombo != null) StartCoroutine(BulletCoroutine(elementCombo));
                else Debug.LogError(DebugCombo() + " does not exsist in manager");
            }
            else Debug.Log("No element has selected");

            DataManager.SetCurrentElement = 0;
            DataManager.AmountFilledAfterReset = DataManager.GetTotalComboFilled();
            Array.Fill(DataManager.ComboList, ElementType.NoneID);
        }

        _locomation.MoveCamera(transform, _playerCamera);
        _locomation.JumpPlayer(this, transform);

        Close(); //<--- testing purpose
    }

    private void FixedUpdate()
    {
        _locomation.MovePlayer(transform, speed);
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

    public AnimationCurve GetJumpCurve()
    {
        return _jumpCurve;
    }

    //Debug what the combo is
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
        combo = combo.Remove(combo.Length-2, 2);
        combo += "\"";
        return combo;
    }

    //Fast exit for builds debug
    private void Close()
    {
        if (InputManager.Instance.CloseGame())
        {
            Debug.Log("Closed game");
            Application.Quit();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{
    [Header("Visual")]
    public VisualElement[] ElementUI = new VisualElement[3];
    [SerializeField] private List<Element> _elementTypes = new List<Element>();
    [Header("Animation")]
    [SerializeField] private Animator _containerAnimator;
    [SerializeField] private Animator[] _elementAnimators = new Animator[3];

    private int _currentPos = -1;

    private void Update()
    {
        if (DataManager.HasResetElement || InputManager.Instance.ConfirmDown())
        {
            if (InputManager.Instance.ConfirmDown() && DataManager.AmountFilledAfterReset > 0) //<== activate when combo list has content in it
            {
                for (int i = 0; i < DataManager.AmountFilledAfterReset; i++) _elementAnimators[i].SetTrigger("Active");
                _containerAnimator.SetTrigger("Active");

                DataManager.AmountFilledAfterReset = 0;
                _currentPos = SetCurrentPos();
            }
            else if (DataManager.HasResetElement)
            {
                for (int i = 0; i < _elementAnimators.Length; i++) _elementAnimators[i].SetTrigger("Discard");
            }

            for (int i = 0; i < ElementUI.Length; i++)
            {
                ElementUI[i].Element = _elementTypes[0];
            }

            DataManager.HasResetElement = false;
        }

        if (InputManager.Instance.Element1Down() || InputManager.Instance.Element2Down() || InputManager.Instance.Element3())
        {
            ElementUI[DataManager.SetCurrentElement - 1].Element = _elementTypes[(int)DataManager.ComboList[DataManager.SetCurrentElement - 1]];

            if (_currentPos != DataManager.SetCurrentElement - 1)
            {
                _elementAnimators[DataManager.SetCurrentElement - 1].SetTrigger("Flip");

                _currentPos = SetCurrentPos();
            }
        }
    }

    private int SetCurrentPos()
    {
        return DataManager.SetCurrentElement - 1;
    }
}

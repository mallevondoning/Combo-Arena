using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExp : MonoBehaviour
{
    [SerializeField] private GameObject _barHolderPrefab;
    [SerializeField] private TextMeshProUGUI _expText;

    private List<Transform> _expBarList = new List<Transform>();

    private void Awake()
    {
        for (int i = 0; i < _barHolderPrefab.transform.childCount; i++) _expBarList.Add(_barHolderPrefab.transform.GetChild(i));
    }

    private void Update()
    {
        for (int i = 0; i < _expBarList.Count; i++)
        {
            float fill;

            if (i == 0) fill = GameManager.Instance.PlayerStatus.NormExp(1f / _expBarList.Count, true);
            else if (i == _expBarList.Count - 1) fill = GameManager.Instance.PlayerStatus.NormExp((_expBarList.Count - 1f) / _expBarList.Count, false);
            else fill = GameManager.Instance.PlayerStatus.NormExp((i + 1f) / _expBarList.Count, (float)i / _expBarList.Count);

            _expBarList[i].GetChild(0).GetComponent<Image>().fillAmount = fill;
        }

        if (GameManager.Instance.PlayerStatus.Exp >= GameManager.Instance.PlayerStatus.MaxExp)
        {
            GameManager.Instance.PlayerStatus.Exp = 0;
            GameManager.Instance.PlayerStatus.ExpCalc();
            GameManager.Instance.PlayerStatus.Level++;
        }

        _expText.text = GameManager.Instance.PlayerStatus.Level.ToString();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    [SerializeField]
    private TextMeshProUGUI _nameText;
    [SerializeField]
    private TextMeshProUGUI _descriptionText;

    [SerializeField]
    private Image _unitImage;

    [SerializeField]
    private TextMeshProUGUI _statText;

    private UnitData _unitData;
    public UnitData CardUnitData => _unitData;

    public event Action<UnitData> ClickEvent;

    public void OnPointerDown(PointerEventData eventData)
    {

        GameManager.Instance.PlayUIClickSound();
        ClickEvent?.Invoke(_unitData);
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _unitImage.sprite = _unitData.UnitMouseOnUISprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _unitImage.sprite = _unitData.UnitUISprite;
    }

    public void SetData(UnitData data)
    {

        _unitData = data;
        _nameText.text          = data.UnitName;
        _descriptionText.text   = data.UnitDescription;

        _unitImage.sprite       = data.UnitUISprite;

        _statText.text = $"공격력 {data.Strength} | 체력 {data.MaxHealth}";

    }

}

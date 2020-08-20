using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button Slot;
    public Image InfoImage;
    public Sprite InfoSprite;

    private void Start()
    {
        InfoImage.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InfoImage.gameObject.SetActive(true);
        InfoImage.sprite = InfoSprite;
        InfoImage.transform.position = new Vector2(Slot.transform.position.x - 50f, Slot.transform.position.y + 200f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InfoImage.gameObject.SetActive(false);
    }
}

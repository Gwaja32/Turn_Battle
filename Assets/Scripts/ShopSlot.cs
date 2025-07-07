using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Artifact item;

    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject ob_Inner;
    [SerializeField]
    private TextMeshProUGUI ob_Level;
    [SerializeField]
    private GameObject ob_Button;

    private GameObject ob_Inventory;
    private void Start()
    {
        ob_Inventory = GameObject.Find("Inventory");
    }
    public void BuyArtifact()
    {
        if (item != null)
        {
            if (Gold.gold >= int.Parse(text.text.Replace("G", "")))
            {
                Gold.gold -= int.Parse(text.text.Replace("G", ""));
                
                ob_Inventory.GetComponent<Inventory>().AcquireItem(item);
                image.sprite = null;
                item = null;
                text.text = "0G";
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
                ob_Inner.SetActive(false);
                ob_Button.SetActive(false);
            }
        }
    }

    public void SetItem(Artifact _item)
    {
        item = _item;
        text.text = _item.price.ToString() + "G";
        image.sprite = _item.itemImage;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        ob_Level.text = _item.itemLevel.ToString();
        ob_Inner.SetActive(true);
        ob_Button.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            GameObject.Find("SlotTooltip").GetComponent<SlotTooltip>().ShowTooltip(item, transform.position);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject.Find("SlotTooltip").GetComponent<SlotTooltip>().HideTootip();
    }
}

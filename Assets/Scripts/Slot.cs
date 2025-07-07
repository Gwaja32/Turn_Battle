using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Artifact item;
    public int itemLevel;
    public Image itemImage;

    [SerializeField]
    private Text text_Level;
    [SerializeField]
    private GameObject go_LevelImage;

    // ��Ƽ��Ʈ ���� ����
    private void SetAlpha(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }

    // �κ��丮�� ��Ƽ��Ʈ �߰�
    public void AddArtifact(Artifact art)
    {
        item = art;
        itemLevel = item.itemLevel;
        itemImage.sprite = item.itemImage;

        go_LevelImage.SetActive(true);
        text_Level.text = itemLevel.ToString();

        SetAlpha(1);
    }

    // ���� ������ ����
    public void ClearSlot()
    {
        item = null;
        itemLevel = 0;
        itemImage.sprite = null;
        SetAlpha(0);

        text_Level.text = "0";
        go_LevelImage.SetActive(false);
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

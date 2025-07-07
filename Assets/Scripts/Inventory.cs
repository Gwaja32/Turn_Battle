using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Artifact test;
    public static bool inventoryActivated = false;  // 인벤토리 활성화 여부

    [SerializeField]
    private GameObject go_InventoryBase; // Inventory_Base 이미지
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting 

    private Slot[] slots;  // 슬롯들 배열

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        CloseInventory();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AcquireItem(test);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = slots.Length - 1; i >= 0; i--)
            {
                if (slots[i].item != null)
                {
                    RemoveItem(i);
                    break;
                }
            }
        }
    }

    public void TryOpenInventory()
    {
        inventoryActivated = !inventoryActivated;

        if (inventoryActivated)
                OpenInventory();
        else
           CloseInventory();  
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
        GameObject.Find("SlotTooltip").GetComponent<SlotTooltip>().HideTootip();
    }

    // 아티팩트 획득
    public void AcquireItem(Artifact _item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                int index = -1;
                for (int j = 0; j < 12; ++j) {
                    if (slots[j].item != null && slots[j].item.itemImage == _item.itemImage && slots[j].item.itemLevel == _item.itemLevel)
                    {
                        index = j;
                        break;
                    }     
                }
                if (index == -1)
                {
                    slots[i].AddArtifact(_item);
                    PlayerStat.maxHP += _item.health;
                    PlayerStat.atk += _item.attack;
                    PlayerStat.def += _item.defense;
                    PlayerStat.crit += _item.crit;
                    PlayerStat.critDamage += _item.critDamage;
                    if (_item.health > 0)
                    {
                        //GameObject.Find("GameManager").GetComponent<PlayerScript>().HealPlayer(_item.health);
                    }
                }
                else
                {
                    Artifact __item = Instantiate(_item);
                    __item.itemLevel += 1;
                    __item.health += slots[index].item.health;
                    __item.attack += slots[index].item.attack;
                    __item.defense += slots[index].item.defense;
                    __item.crit += slots[index].item.crit;
                    __item.critDamage += slots[index].item.critDamage;
                    RemoveItem(index);
                    AcquireItem(__item);
                }
                return;
            }
        }
    }

    // 아티팩트 제거
    public void RemoveItem(int _slot)
    {
        if (slots[_slot].item != null)
        { 
            PlayerStat.maxHP -= slots[_slot].item.health;
            PlayerStat.atk -= slots[_slot].item.attack;
            PlayerStat.def -= slots[_slot].item.defense;
            PlayerStat.crit -= slots[_slot].item.crit;
            PlayerStat.critDamage -= slots[_slot].item.critDamage;
            slots[_slot].ClearSlot();
            return;
        }
    }

    public void InitInventory()
    {
        for(int i = 0; i < 12; ++i)
        {
            RemoveItem(i);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotTooltip : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private Text text;

    public void ShowTooltip(Artifact item, Vector3 pos)
    {
        go_Base.SetActive(true);
        RectTransform r = go_Base.GetComponent<RectTransform>();
        pos += new Vector3(r.rect.width / 2 + 10, r.rect.height / 2 + 10, 0);
        go_Base.transform.position = pos;

        text.text = "";
        if (item.attack != 0) text.text += $"Attack +{item.attack}\n";
        if (item.defense != 0) text.text += $"Defense +{item.defense}\n";
        if (item.health != 0) text.text += $"Health +{item.health}\n";
        if (item.crit != 0) text.text += $"Critical +{item.crit * 100}%\n";
        if (item.critDamage != 0) text.text += $"Critical Damage +{item.critDamage * 100}%";
    }

    public void HideTootip()
    {
        go_Base.SetActive(false);
    }
}

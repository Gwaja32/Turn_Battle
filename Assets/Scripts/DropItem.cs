using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropItem : MonoBehaviour
{
    public Artifact item;

    [SerializeField]
    private Text text;
    [SerializeField]
    private Image image;

    public void ShowArtifact()
    {
        image.sprite = item.itemImage;
        text.text = "";
        if (item.attack != 0) text.text += $"Attack +{item.attack}\n";
        if (item.defense != 0) text.text += $"Defense +{item.defense}\n";
        if (item.health != 0) text.text += $"Health +{item.health}\n";
        if (item.crit != 0) text.text += $"Critical +{item.crit * 100}%\n";
        if (item.critDamage != 0) text.text += $"Critical Damage +{item.critDamage * 100}%";
    }
}

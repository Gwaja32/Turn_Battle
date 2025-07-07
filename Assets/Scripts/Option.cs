using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public GameObject go_Option;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            go_Option.SetActive(true);
        }
    }
}

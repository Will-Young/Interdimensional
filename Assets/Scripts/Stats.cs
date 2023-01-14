using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private Transform StatPanelTransform;

    public void ToggleStatsPanel()
    {
        StatPanelTransform.gameObject.SetActive(!StatPanelTransform.gameObject.activeSelf);
    }
}

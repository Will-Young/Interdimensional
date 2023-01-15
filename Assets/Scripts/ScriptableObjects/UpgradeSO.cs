using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Upgrade")]
public abstract class UpgradeSO : ScriptableObject
{
    [SerializeField] private float DebugCost;

    public string Name;
    public float Number;
    public Image Image;
    public ClickManager.NumberType NumberType;

    public virtual float UpgradeCost()
    {

        // Add in some math to determine the cost
        return DebugCost/100;
    }
}

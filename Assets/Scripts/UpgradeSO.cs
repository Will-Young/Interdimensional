using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade")]
public abstract class UpgradeSO : ScriptableObject
{
    public string Name;
    public string Number;
    public ClickManager.NumberType NumberType;


}

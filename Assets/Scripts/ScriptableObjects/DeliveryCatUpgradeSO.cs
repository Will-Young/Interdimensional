using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="DeliveryCatUpgrade")]
public class DeliveryCatUpgradeSO : ScriptableObject
{
    public string Name;
    public Sprite CatHead; 
    public Sprite CatBody; 
    public DeliveryCatUpgrade.DeliveryCatType Type;
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperAttackCooldownView : MonoBehaviour
{
    [SerializeField] private Player Player;

    [Header("Text with cooldown")]
    [SerializeField] private Text TextWithSuperAttackCooldown;
    [SerializeField] private string InformationBeforeNumber = "Cooldown: ";
    [SerializeField] private string InformationAfterNumber = "";
    [SerializeField, Range(0, int.MaxValue)] private int Precision = 2;
    
    [Header("Objects which should be active at specific values of cooldown")]
    [SerializeField] private List<GameObject> ObjectsWhichShouldBeActiveOnlyWhenCooldownIsGreaterThanZero = new();

    private void LateUpdate()
    {
        if (TextWithSuperAttackCooldown != null)
        {
            TextWithSuperAttackCooldown.text = $"{InformationBeforeNumber}{Player.CurrentSuperAttackCooldown.ToString($"F{Precision}")}{InformationAfterNumber}";
        }
        ObjectsWhichShouldBeActiveOnlyWhenCooldownIsGreaterThanZero.ForEach(gameObject => gameObject.SetActive(Player.CurrentSuperAttackCooldown > 0));
    }
}
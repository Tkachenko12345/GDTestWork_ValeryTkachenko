using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SuperAttackButton : MonoBehaviour
{
    [SerializeField] private Player Player;

    private Button ThisButton;

    private void Awake()
    {
        ThisButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        Player.OnDetectingIfEnemiesCanBeAttacked += SetInteractable;
        ThisButton.onClick.AddListener(Player.PerformSuperAttackIfEnemieIsNear);
    }

    private void OnDisable()
    {
        Player.OnDetectingIfEnemiesCanBeAttacked -= SetInteractable;
        ThisButton.onClick.RemoveListener(Player.PerformSuperAttackIfEnemieIsNear);
    }

    private void SetInteractable(bool interactable)
    {
        ThisButton.interactable = interactable;
    }
}
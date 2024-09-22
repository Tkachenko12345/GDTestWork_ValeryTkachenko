using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovableRigidbody : MonoBehaviour
{
    [SerializeField] private MoveConfig Config;

    private Rigidbody Rigidbody;
    private Vector3 VelocityWhichWasInput;
    private bool IsStopped = false;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        GameCycleEventsProvider.OnDeath += StopMoving;
    }

    private void OnDestroy()
    {
        GameCycleEventsProvider.OnDeath -= StopMoving;
    }

    private void CalculateAndApplyInputVelocity()
    {
        if (IsStopped)
        {
            return;
        }

        VelocityWhichWasInput = Vector3.zero;

        void MoveWithDirectionOnButton(MovingButtonData movingButtonData)
        {
            if (Input.GetButton(movingButtonData.ButtonName))
            {
                VelocityWhichWasInput += movingButtonData.Direction;
            }
        }

        for(int i = 0; i < Config.MovingButtonsAmount; i++)
        {
            MoveWithDirectionOnButton(Config[i]);
        }

        VelocityWhichWasInput = VelocityWhichWasInput.normalized * Config.Speed;

        Rigidbody.velocity = VelocityWhichWasInput;

        if (VelocityWhichWasInput.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(VelocityWhichWasInput);
        }
    }

    private void LateUpdate()
    {
        CalculateAndApplyInputVelocity();
    }

    private void StopMoving()
    {
        VelocityWhichWasInput = Vector3.zero;
        Rigidbody.velocity = VelocityWhichWasInput;
        IsStopped = true;
    }
}
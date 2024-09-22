using UnityEngine;

public class UniformMotionToAnotherPoint : MonoBehaviour
{
    private int TotalStepsAmount;
    private Vector3 Step;
    private float CurrentStepIndex;

    public void StartMovingWithDisplacementAndTime(Vector3 displacement, float time)
    {
        TotalStepsAmount = (int)(time / Time.fixedDeltaTime);
        Step = displacement / TotalStepsAmount;
        CurrentStepIndex = 0;
    }

    private void FixedUpdate()
    {
        if (CurrentStepIndex < TotalStepsAmount)
        {
            transform.position += Step;
            CurrentStepIndex++;
        }
    }
}
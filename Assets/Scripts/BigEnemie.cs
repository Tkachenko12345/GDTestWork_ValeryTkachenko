using UnityEngine;

public class BigEnemie : Enemie
{
    [SerializeField] private BigEnemieConfig Config;

    public override void Die()
    {
        base.Die();
        SceneManager.Instance.RemoveEnemie(this, ShouldCheckForFinishingWave.No);
        AnimatorController.SetTrigger("Die");
        for(int i = 0; i < Config.SpawnedObjectsAmount; i++)
        {
            GameObject NewObject = Instantiate(Config[i].ObjectTemplate);
            NewObject.transform.position = transform.position + Config[i].StartOffset;
            UniformMotionToAnotherPoint NewObjectUniformMotionToAnotherPoint = NewObject.GetComponent<UniformMotionToAnotherPoint>();
            if(NewObjectUniformMotionToAnotherPoint == null)
            {
                NewObjectUniformMotionToAnotherPoint = NewObject.AddComponent<UniformMotionToAnotherPoint>();
            }
            NewObjectUniformMotionToAnotherPoint.StartMovingWithDisplacementAndTime(Config[i].MotionDisplacement, Config[i].DisplacementTime);
        }
    }
}
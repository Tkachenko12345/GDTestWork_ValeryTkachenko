public class SmallEnemie : Enemie
{
    public override void Die()
    {
        base.Die();
        SceneManager.Instance.RemoveEnemie(this, ShouldCheckForFinishingWave.Yes);
        AnimatorController.SetTrigger("Die");
    }
}
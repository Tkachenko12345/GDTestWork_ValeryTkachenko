using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Hp;
    public float Damage;
    public float DamageWithSuperAttack;
    public float AtackSpeed;
    public float AttackRange = 2;
    public float SuperAttackCooldownInSeconds = 2;

    private float lastAttackAttemptTime = 0;
    private float lastSuperAttackAttemptTime = float.NegativeInfinity;
    private bool isDead = false;
    private Enemie ClosestEnemie;

    public Animator AnimatorController;

    public event Action<bool> OnDetectingIfEnemiesCanBeAttacked;

    public float CurrentSuperAttackCooldown { get => Mathf.Clamp(SuperAttackCooldownInSeconds - (Time.time - lastSuperAttackAttemptTime), 0, float.PositiveInfinity); }

    private void Awake()
    {
        GameCycleEventsProvider.OnDeath += MarkAsDead;
    }

    private void OnDestroy()
    {
        GameCycleEventsProvider.OnDeath -= MarkAsDead;
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        if (Hp <= 0)
        {
            Die();
            return;
        }

        var enemies = SceneManager.Instance.Enemies;
        ClosestEnemie = null;

        for (int i = 0; i < enemies.Count; i++)
        {
            var enemie = enemies[i];
            if (enemie == null)
            {
                continue;
            }

            var distance = GetDistanceToEnemie(enemie);
            if (ClosestEnemie == null || distance < GetDistanceToEnemie(ClosestEnemie))
            {
                ClosestEnemie = enemie;
            }
        }
        OnDetectingIfEnemiesCanBeAttacked?.Invoke(ClosestEnemie != null && GetDistanceToEnemie(ClosestEnemie) < AttackRange);
    }

    private float GetDistanceToEnemie(Enemie enemie)
    {
        return Vector3.Distance(transform.position, enemie.transform.position);
    }

    private void AttackIfEnemieIsNear(float damage)
    {
        if (!isDead && ClosestEnemie != null)
        {
            var distance = Vector3.Distance(transform.position, ClosestEnemie.transform.position);
            if (distance <= AttackRange)
            {
                transform.rotation = Quaternion.LookRotation(ClosestEnemie.transform.position - transform.position);

                ClosestEnemie.Hp -= damage;
            }
        }
    }

    public void PerformUsualAttackIfEnemieIsNear()
    {
        if (Time.time - lastAttackAttemptTime <= AtackSpeed || isDead) return;
        lastAttackAttemptTime = Time.time;
        AnimatorController.SetTrigger("Attack");
        AttackIfEnemieIsNear(Damage);
    }

    public void PerformSuperAttackIfEnemieIsNear()
    {
        if (CurrentSuperAttackCooldown > 0 || isDead) return;
        lastSuperAttackAttemptTime = Time.time;
        AnimatorController.SetTrigger("DoubleAttack");
        AttackIfEnemieIsNear(DamageWithSuperAttack);
    }

    private void MarkAsDead()
    {
        isDead = true;
        AnimatorController.SetTrigger("Die");
    }

    private void Die()
    {
        GameCycleEventsProvider.Die();
    }
}

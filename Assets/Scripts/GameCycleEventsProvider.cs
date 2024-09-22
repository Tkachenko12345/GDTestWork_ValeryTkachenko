using System;

public static class GameCycleEventsProvider
{
    public static event Action OnVictory;
    public static event Action OnDeath;

    public static void Win()
    {
        OnVictory.Invoke();
    }

    public static void Die()
    {
        OnDeath?.Invoke();
    }
}
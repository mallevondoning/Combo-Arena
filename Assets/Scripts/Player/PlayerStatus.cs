using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Manager/PlayerHealth")]
public class PlayerStatus : ScriptableObject
{
    [Header("Health")]
    public float Health = 100f;
    public float MaxHealth = 100f;
    [Header("Element status")]
    public float Ablaze;
    public float MaxAblaze = 100f;
    public float Shocked;
    public float MaxShocked = 100f;
    public float Frozen;
    public float MaxFrozen = 100f;

    public float NormHealth()
    {
        return Norm(Health, MaxHealth);
    }
    public bool IsDead()
    {
        return Health <= 0;
    }

    public float NormAblaze()
    {
        return Norm(Ablaze, MaxAblaze);
    }
    public float NormAblaze(float threshold)
    {
        return Norm(Ablaze, MaxAblaze, threshold);
    }
    public float NormAblaze(float maxThreshold, float minThreshold)
    {
        return Norm(Ablaze, MaxAblaze, maxThreshold, minThreshold);
    }


    public float NormShocked()
    {
        return Norm(Shocked, MaxShocked);
    }
    public float NormShocked(float threshold)
    {
        return Norm(Shocked, MaxShocked, threshold);
    }
    public float NormShocked(float maxThreshold, float minThreshold)
    {
        return Norm(Shocked, MaxShocked, maxThreshold, minThreshold);
    }

    public float NormFrozen()
    {
        return Norm(Frozen, MaxFrozen);
    }
    public float NormFrozen(float threshold)
    {
        return Norm(Frozen, MaxFrozen, threshold);
    }
    public float NormFrozen(float maxThreshold, float minThreshold)
    {
        return Norm(Frozen, MaxFrozen, maxThreshold, minThreshold);
    }


    private float Norm(float current, float max)
    {
        return current / max;
    }
    private float Norm(float current, float max, float threshold)
    {
        threshold = Mathf.Clamp01(threshold);

        float newThreshold = max * threshold;
        return Mathf.Clamp01(Norm(current, newThreshold));
    }
    private float Norm(float current, float max, float maxThreshold, float minThreshold)
    {
        maxThreshold = Mathf.Clamp01(maxThreshold);
        minThreshold = Mathf.Clamp01(minThreshold);

        float newMax = max * maxThreshold;
        float newMin = max * minThreshold;

        float newThreshold = newMax - newMin;

        return Mathf.Clamp01(Norm(current, newThreshold));
    }
}

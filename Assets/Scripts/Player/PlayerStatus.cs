using UnityEngine;

[CreateAssetMenu(menuName = "Game/Manager/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    [Header("Health")]
    public float Health = 100f;
    public float MaxHealth = 100f;
    [Header("Exp")]
    public float Exp;
    public float MaxExp = 100;
    public int Level = 1;
    [Header("Element status")]
    public float Ablaze;
    public float MaxAblaze = 100f;
    public float Shocked;
    public float MaxShocked = 100f;
    public float Frozen;
    public float MaxFrozen = 100f;

    private void OnValidate()
    {
        Exp = Mathf.Max(0, Exp);
        Level = Mathf.Clamp(Level, 1, 9999);

        Ablaze = Mathf.Max(0, Ablaze);
        Shocked = Mathf.Max(0, Shocked);
        Frozen = Mathf.Max(0, Frozen);
    }

    public float NormExp()
    {
        return Norm(Exp, MaxExp, 0);
    }
    public float NormExp(float threshold, bool isMax)
    {
        return Norm(Exp, MaxExp, threshold, isMax);
    }
    public float NormExp(float maxThreshold, float minThreshold)
    {
        return Norm(Exp, MaxExp, maxThreshold, minThreshold);
    }
    public void ExpCalc()
    {
        //MaxExp = <--- exp curve
    }

    public float NormHealth()
    {
        return Norm(Health, MaxHealth, 0);
    }
    public float NormHealth(float threshold, bool isMax)
    {
        return Norm(Health, MaxHealth, threshold, isMax);
    }
    public float NormHealth(float maxThreshold, float minThreshold)
    {
        return Norm(Health, MaxHealth, maxThreshold, minThreshold);
    }
    public bool IsDead()
    {
        return Health <= 0;
    }

    public float NormAblaze()
    {
        return Norm(Ablaze, MaxAblaze, 0);
    }
    public float NormAblaze(float threshold, bool isMax)
    {
        return Norm(Ablaze, MaxAblaze, threshold, isMax);
    }
    public float NormAblaze(float maxThreshold, float minThreshold)
    {
        return Norm(Ablaze, MaxAblaze, maxThreshold, minThreshold);
    }

    public float NormShocked()
    {
        return Norm(Shocked, MaxShocked, 0);
    }
    public float NormShocked(float threshold, bool isMax)
    {
        return Norm(Shocked, MaxShocked, threshold, isMax);
    }
    public float NormShocked(float maxThreshold, float minThreshold)
    {
        return Norm(Shocked, MaxShocked, maxThreshold, minThreshold);
    }

    public float NormFrozen()
    {
        return Norm(Frozen, MaxFrozen, 0);
    }
    public float NormFrozen(float threshold, bool isMax)
    {
        return Norm(Frozen, MaxFrozen, threshold, isMax);
    }
    public float NormFrozen(float maxThreshold, float minThreshold)
    {
        return Norm(Frozen, MaxFrozen, maxThreshold, minThreshold);
    }


    private float Norm(float current, float max, float min)
    {
        return Mathf.Clamp01((current - min) / (max - min));
    }
    private float Norm(float current, float max, float threshold, bool isMax)
    {
        float newThreshold = max * threshold;

        if (isMax) return Norm(current, newThreshold, 0);
        else return Norm(current, max, newThreshold);
    }
    private float Norm(float current, float max, float maxThreshold, float minThreshold)
    {
        float newMax = max * maxThreshold;
        float newMin = max * minThreshold;

        return Norm(current, newMax, newMin);
    }
}

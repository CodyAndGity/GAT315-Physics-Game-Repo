using UnityEngine;

[System.Serializable]
public class Polar
{
    public float angle;//radians
    public float length;

    public Polar() { }
    public Polar(float angle, float length)
    {
        this.angle = angle;
        this.length = length;
    }

    public Vector2 ToVector2()
    {
        return PolarToVector2(this);
    }

    public static Vector2 PolarToVector2(Polar polar)
    {
        Vector2 result = Vector2.zero;
        result.x = Mathf.Cos(polar.angle);
        result.y = Mathf.Sin(polar.angle);
        return result * polar.length;
    }
    public static Polar Vector2ToPolar(Vector2 vector)
    {
        Polar result = new Polar();
        result.angle= Mathf.Atan2(vector.y, vector.x);
        result.length = vector.magnitude;
        return result;
    }
}

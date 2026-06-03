using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PolarPaint : MonoBehaviour
{
    [SerializeField, Range(2, 1000)] int numberOfPoints;
    [SerializeField, Range(0, 1080)] float rotation = 360f;
    public float Theta => (rotation / (numberOfPoints - 1)) * Mathf.Deg2Rad;
    LineRenderer lineRenderer;

    public Polar polar = new Polar();

    public enum PolarPattern
    {
        Circle,
        Spiral,
        Cardioid,
        Limacon,
        Rose,
        SpiralRandom
    }
    [SerializeField] PolarPattern pattern;
    [SerializeField] float a = 1.0f;
    [SerializeField] float b = 1.0f;
    [SerializeField] bool angleToPower = false;
    [SerializeField] float c = 1.0f;

    public float AngleToPower => Mathf.Pow(c, polar.angle);
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    void Update()
    {
        lineRenderer.positionCount = numberOfPoints;
        if (angleToPower)
        {
            switch (pattern)
            {
                case PolarPattern.Circle:
                    DrawCirclePower();
                    break;
                case PolarPattern.Spiral:
                    DrawSpiralPower();
                    break;
                case PolarPattern.Cardioid:
                    DrawCardioidPower();
                    break;
                case PolarPattern.Limacon:
                    DrawLimaconPower();
                    break;
                case PolarPattern.Rose:
                    DrawRosePower();
                    break;
                case PolarPattern.SpiralRandom:
                    DrawRandomSpiralPower();
                    break;
                default:
                    break;
            }
            return;
        }
        else
        {
            switch (pattern)
            {
                case PolarPattern.Circle:
                    DrawCircle();
                    break;
                case PolarPattern.Spiral:
                    DrawSpiral();
                    break;
                case PolarPattern.Cardioid:
                    DrawCardioid();
                    break;
                case PolarPattern.Limacon:
                    DrawLimacon();
                    break;
                case PolarPattern.Rose:
                    DrawRose();
                    break;
                case PolarPattern.SpiralRandom:
                    DrawRandomSpiral();
                    break;
                default:
                    break;
            }
        }
    }

    void DrawCircle()
    {

        polar.angle = 0;
        polar.length = a;
        for (int i = 0; i < numberOfPoints; i++)
        {
            lineRenderer.SetPosition(i, polar.ToVector2());
            polar.angle += Theta;
        }
    }
    void DrawCirclePower()
    {

        polar.angle = 0;
        polar.length = a;
        for (int i = 0; i < numberOfPoints; i++)
        {
            lineRenderer.SetPosition(i, polar.ToVector2());
            polar.angle += Theta;
        }
    }
    void DrawSpiral()
    {

        polar.angle = 0;
        polar.length = 1;
        for (int i = 0; i < numberOfPoints; i++)
        {
            polar.length = a + b * polar.angle;
            lineRenderer.SetPosition(i, polar.ToVector2());
            polar.angle += Theta;
        }
    }
    void DrawCardioid()
    {

        polar.angle = 0;
        polar.length = a;
        for (int i = 0; i < numberOfPoints; i++)
        {
            polar.length = a * (1 - Mathf.Cos(polar.angle));
            lineRenderer.SetPosition(i, polar.ToVector2());
            polar.angle += Theta;
        }
    }
    void DrawCardioidPower()
    {

        polar.angle = 0;
        polar.length = a;
        for (int i = 0; i < numberOfPoints; i++)
        {
            polar.length = a * (1 - Mathf.Cos(AngleToPower));
            lineRenderer.SetPosition(i, polar.ToVector2());
            polar.angle += Theta;
        }
    }
    void DrawLimacon()
    {

        polar.angle = 0;
        polar.length = a;
        for (int i = 0; i < numberOfPoints; i++)
        {
            polar.length = a + (b * Mathf.Cos(polar.angle));
            lineRenderer.SetPosition(i, polar.ToVector2());
            polar.angle += Theta;
        }
    }
    void DrawLimaconPower()
    {

        polar.angle = 0;
        polar.length = a;
        for (int i = 0; i < numberOfPoints; i++)
        {
            polar.length = a + (b * Mathf.Cos(AngleToPower));
            lineRenderer.SetPosition(i, polar.ToVector2());
            polar.angle += Theta;
        }
    }

    void DrawRose()
    {

        polar.angle = 0;
        polar.length = a;
        for (int i = 0; i < numberOfPoints; i++)
        {
            polar.length = a * (Mathf.Cos(b * polar.angle));
            lineRenderer.SetPosition(i, polar.ToVector2());
            polar.angle += Theta;
        }
    }
    void DrawRosePower()
    {

        polar.angle = 0;
        polar.length = a;
        for (int i = 0; i < numberOfPoints; i++)
        {
            polar.length = a * (Mathf.Cos(b * AngleToPower));
            lineRenderer.SetPosition(i, polar.ToVector2());
            polar.angle += Theta;
        }
    }

    void DrawRandomSpiral()
    {

        polar.angle = 0;
        polar.length = a;
        for (int i = 0; i < numberOfPoints; i++)
        {
            polar.length = a + b * polar.angle * (Random.value + 1);
            lineRenderer.SetPosition(i, polar.ToVector2());
            polar.angle += Theta;
        }
    }
    void DrawRandomSpiralPower()
    {

        polar.angle = 0;
        polar.length = a;
        for (int i = 0; i < numberOfPoints; i++)
        {
            polar.length = a + b * AngleToPower * (Random.value + 1);
            lineRenderer.SetPosition(i, polar.ToVector2());
            polar.angle += Theta;
        }
    }
    void DrawSpiralPower()
    {

        polar.angle = 0;
        polar.length = a;
        for (int i = 0; i < numberOfPoints; i++)
        {
            polar.length = a + b * AngleToPower;
            lineRenderer.SetPosition(i, polar.ToVector2());
            polar.angle += Theta;
        }
    }
    void DrawSpiralInversePower()
    {

        polar.angle = 0;
        polar.length = a;
        for (int i = 0; i < numberOfPoints; i++)
        {
            polar.length = a + b * Mathf.Pow(c, polar.length);
            lineRenderer.SetPosition(i, polar.ToVector2());
            polar.angle += Theta;
        }
    }

}

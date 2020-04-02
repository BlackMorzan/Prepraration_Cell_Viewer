using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EulerGet : MonoBehaviour
{

    //https://forum.unity.com/threads/solved-how-to-get-rotation-value-that-is-in-the-inspector.460310/

    // ===========================================
    // EULER CALC
    // ===========================================
    public double EulerX = 0;
    public double EulerY = 0;
    public double EulerZ = 0;
    // Hold Euler list
    private List<double[]> Euler_list;
    // Hold last angle to calc angles over 360
    private double last_euler_x;
    private double last_euler_y;
    private double last_euler_z;
    // Hold 360 passes
    private double euler_x_360_pass = 0;
    private double euler_y_360_pass = 0;
    private double euler_z_360_pass = 0;
    // ===========================================

    // Use this for initialization
    void Start()
    {
        // Init euler calc
        init_euler_calc();
    }

    public double GetEulerZ()
    {
        return EulerZ;
    }

    // Update is called once per frame
    void Update()
    {
        // Update euler calc
        update_euler_calc();
    }

    // ===========================================
    // EULER CALC
    // ===========================================
    public void init_euler_calc()
    {
        // Init first Euler as reference
        float x = transform.eulerAngles.x / 360.0f * 2.0f * Mathf.PI;
        float y = transform.eulerAngles.y / 360.0f * 2.0f * Mathf.PI;
        float z = transform.eulerAngles.z / 360.0f * 2.0f * Mathf.PI;

        // Abbreviations for the various angular functions
        double cx = Mathf.Cos(x * 0.5f);
        double sx = Mathf.Sin(x * 0.5f);
        double cy = Mathf.Cos(y * 0.5f);
        double sy = Mathf.Sin(y * 0.5f);
        double cz = Mathf.Cos(z * 0.5f);
        double sz = Mathf.Sin(z * 0.5f);

        Vector4 xSandwich = new Vector4((float)sx, 0f, 0f, (float)cx);
        Vector4 ySandwich = new Vector4(0f, (float)sy, 0f, (float)cy);
        Vector4 zSandwich = new Vector4(0f, 0f, (float)sz, (float)cz);
        Quaternion q5 = CreateVectorSandwich(ySandwich, xSandwich, zSandwich);

        /*
        // Get current euler list
        Euler_list = GetAllEulerAngles();
        // Show list
        foreach (double[] test_value in Euler_list)
        {
            Debug.Log("x:" + Mathf.Rad2Deg*test_value[0] + " y:" + Mathf.Rad2Deg * test_value[1] + " z:" + Mathf.Rad2Deg * test_value[2]);
        }
        */
    }

    public void update_euler_calc()
    {
        // Get only Unity euler
        double[] value = GetUnityEulerAngles();

        double EulerX_org = Mathf.Rad2Deg * value[0];
        double EulerY_org = Mathf.Rad2Deg * value[1];
        double EulerZ_org = Mathf.Rad2Deg * value[2];

        // Add 360 passes
        EulerX = EulerX_org + (360 * euler_x_360_pass);
        EulerY = EulerY_org + (360 * euler_y_360_pass);
        EulerZ = EulerZ_org + (360 * euler_z_360_pass);

        // Check if x passed 360
        float x_change = (float)last_euler_x - (float)EulerX;
        if (Mathf.Abs(x_change) > 180)
        {
            // Passed 360
            // Check direction we passed
            if (x_change > 0)
            {
                // Add 360
                euler_x_360_pass += 1;
            }
            else
            {
                // Sub 360
                euler_x_360_pass -= 1;
            }
            // Recalc euler
            EulerX = EulerX_org + (360 * euler_x_360_pass);
        }

        // Check if y passed 360
        float y_change = (float)last_euler_y - (float)EulerY;
        if (Mathf.Abs(y_change) > 180)
        {
            // Passed 360
            // Check direction we passed
            if (y_change > 0)
            {
                // Add 360
                euler_y_360_pass += 1;
            }
            else
            {
                // Sub 360
                euler_y_360_pass -= 1;
            }
            // Recalc euler
            EulerY = EulerY_org + (360 * euler_y_360_pass);
        }

        // Check if z passed 360
        float z_change = (float)last_euler_z - (float)EulerZ;
        if (Mathf.Abs(z_change) > 180)
        {
            // Passed 360
            // Check direction we passed
            if (z_change > 0)
            {
                // Add 360
                euler_z_360_pass += 1;
            }
            else
            {
                // Sub 360
                euler_z_360_pass -= 1;
            }
            // Recalc euler
            EulerZ = EulerZ_org + (360 * euler_z_360_pass);
        }

        // Save last
        last_euler_x = EulerX;
        last_euler_y = EulerY;
        last_euler_z = EulerZ;
    }

    public static Vector4 SandwichProduct(Vector4 q1, Vector4 q2)
    {
        Vector4 q;
        q.w = -q1.x * q2.x - q1.y * q2.y - q1.z * q2.z + q1.w * q2.w;
        q.x = q1.x * q2.w + q1.y * q2.z - q1.z * q2.y + q1.w * q2.x;
        q.y = -q1.x * q2.z + q1.y * q2.w + q1.z * q2.x + q1.w * q2.y;
        q.z = q1.x * q2.y - q1.y * q2.x + q1.z * q2.w + q1.w * q2.z;

        return q;
    }

    public static Quaternion CreateVectorSandwich(Vector4 a, Vector4 b, Vector4 c)
    {
        Vector4 v = SandwichProduct(SandwichProduct(a, b), c);
        Quaternion q = new Quaternion(v.x, v.y, v.z, v.w);
        return q;
    }

    public double[,] CreateRotationMatrixFromQuaternion(double first, double second, double third, double w)
    {
        double[,] matrix = new double[3, 3];
        double x = first;
        double y = second;
        double z = third;

        double m1_1 = 1.0 - 2.0 * (y * y + z * z);
        double m1_2 = 2.0 * (x * y - z * w);
        double m1_3 = 2.0 * (x * z + y * w);

        double m2_1 = 2.0 * (x * y + z * w);
        double m2_2 = 1.0 - 2.0 * (x * x + z * z);
        double m2_3 = 2.0 * (y * z - x * w);

        double m3_1 = 2.0 * (x * z - y * w);
        double m3_2 = 2.0 * (y * z + x * w);
        double m3_3 = 1.0 - 2.0 * (x * x + y * y);

        matrix[0, 0] = m1_1; matrix[0, 1] = m1_2; matrix[0, 2] = m1_3;
        matrix[1, 0] = m2_1; matrix[1, 1] = m2_2; matrix[1, 2] = m3_3;
        matrix[2, 0] = m3_1; matrix[2, 1] = m3_2; matrix[2, 2] = m3_3;

        return matrix;
    }

    public double[] CreateEulerFromRotationMatrix(double[,] matrix, int idx1, int idx2, int idx3)
    {
        double[] angles = new double[3];
        double ftany = matrix[2, 1];
        double ftanx = matrix[2, 2];
        double first = System.Math.Atan2(ftany, ftanx);
        double stany = matrix[1, 0];
        double stanx = matrix[0, 0];
        double second = System.Math.Atan2(stany, stanx);
        double tsiny = matrix[2, 0];
        double third = System.Math.Asin(-1.0 * tsiny);

        angles[idx1] = first;
        angles[idx2] = second;
        angles[idx3] = third;

        return angles;
    }

    public double[] GetUnityEulerAngles()
    {
        // Get local transform
        Quaternion q;
        q.x = transform.localRotation.x;
        q.y = transform.localRotation.y;
        q.z = transform.localRotation.z;
        q.w = transform.localRotation.w;
        // Hold return values
        double[,] matrices;
        double[] angles;
        // Get needed matrix
        matrices = CreateRotationMatrixFromQuaternion(q.z, q.x, q.y, q.w);
        // Calc Unity Euler
        angles = CreateEulerFromRotationMatrix(matrices, 2, 1, 0);
        // Return Unity euler
        return angles;
    }

    public List<double[]> GetAllEulerAngles()
    {
        // Get local transform
        Quaternion q;
        q.x = transform.localRotation.x;
        q.y = transform.localRotation.y;
        q.z = transform.localRotation.z;
        q.w = transform.localRotation.w;

        List<double[,]> matrices = new List<double[,]>();
        List<double[]> angles = new List<double[]>();

        matrices.Add(CreateRotationMatrixFromQuaternion(q.x, q.y, q.z, q.w));
        matrices.Add(CreateRotationMatrixFromQuaternion(q.x, q.z, q.y, q.w));
        matrices.Add(CreateRotationMatrixFromQuaternion(q.y, q.x, q.y, q.w));
        matrices.Add(CreateRotationMatrixFromQuaternion(q.y, q.y, q.x, q.w));
        matrices.Add(CreateRotationMatrixFromQuaternion(q.z, q.x, q.y, q.w));
        matrices.Add(CreateRotationMatrixFromQuaternion(q.z, q.y, q.x, q.w));

        for (int i1 = 0; i1 < 6; i1++)
        {
            angles.Add(CreateEulerFromRotationMatrix(matrices[i1], 0, 1, 2));
            angles.Add(CreateEulerFromRotationMatrix(matrices[i1], 0, 2, 1));
            angles.Add(CreateEulerFromRotationMatrix(matrices[i1], 1, 0, 2));
            angles.Add(CreateEulerFromRotationMatrix(matrices[i1], 1, 2, 0));
            angles.Add(CreateEulerFromRotationMatrix(matrices[i1], 2, 0, 1));
            angles.Add(CreateEulerFromRotationMatrix(matrices[i1], 2, 1, 0));
        }

        return angles;

    }
}


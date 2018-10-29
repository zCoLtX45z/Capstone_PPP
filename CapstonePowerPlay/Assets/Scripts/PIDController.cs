using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIDController : MonoBehaviour {

    // PID Gains
    [SerializeField]
    private float Kp = 1.0f;
    [SerializeField]
    private float Ki = 0.0f;
    [SerializeField]
    private float Kd = 0.0f;

    // Errors
    private float cumError = 0, prevError = 0, output;

    // Clamp Seetings
    public bool isClamp = false;
    [SerializeField]
    private float min = 0.0f;
    [SerializeField]
    private float max = 1.0f;

    public void setGains(float kp, float ki, float kd)
    {
        Kp = kp;
        Ki = ki;
        Kd = kd;
    }

    public void step(float targetValue, float currentValue)
    {
        float error = targetValue - currentValue;

        cumError += error;
        float slope = error - prevError;

        output = (Kp * error) + (Ki * cumError) + (Kd * slope);
        if (isClamp)
        {
            output = Mathf.Clamp(output, min, max);
        }

        prevError = error;
    }

    public float getOutput()
    {
        return output;
    }

    public void EnableClamp(float minValue, float maxValue)
    {
        isClamp = true;
        min = minValue;
        max = maxValue;
    }

    public void WipeErrors()
    {
        cumError = 0f;
        prevError = 0f;
    }
}

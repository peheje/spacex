using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class PIDController
    {
        private float lastError;
        private float derivative;
        private float integral;

        public float GetControl(float error, float kp, float ki, float kd)
        {
            error *= -1;
            integral += error;
            derivative = error - lastError;
            lastError = error;
            float control = kp * error + ki * integral + kd * derivative;
            return control;
        }
    }
}

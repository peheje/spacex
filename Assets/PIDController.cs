using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class PIDController
    {
        public float kp = 1.51f;
        public float ki = 0.01f;
        public float kd = 4.66f;

        private float lastError;
        private float derivative;
        private float integral;

        public PIDController()
        {

        }

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

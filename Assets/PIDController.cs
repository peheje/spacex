using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class PIDController
    {
        public float kp = 1.0f;
        public float ki = 0.0f;
        public float kd = 0.0f;

        private float lastError;
        private float derivative;
        private float integral;

        public PIDController()
        {

        }

        public float GetControl(float error)
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

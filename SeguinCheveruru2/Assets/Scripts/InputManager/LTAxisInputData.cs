using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LowTeeGames
{
    [System.Serializable]
    public class LTAxisInputData : LTInputData
    {
        public string axisName;
        public AxisType axisType = AxisType.Raw;

        [System.Serializable] public class OnInputEvent : UnityEvent<float> { }
        public OnInputEvent onInput;

        public override void CheckInputs()
        {
            float axis = 0f;
            switch (axisType)
            {
                case AxisType.Smoothed:
                    axis = Input.GetAxis(axisName);
                    break;

                case AxisType.Raw:
                    axis = Input.GetAxisRaw(axisName);
                    break;

                default:
                    break;
            }

            onInput?.Invoke(axis);
        }

        public override void ResetName()
        {
            name = axisName;
        }
    }
}
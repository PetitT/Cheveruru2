using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LowTeeGames
{
    [System.Serializable]
    public class LTButtonInputData : LTInputData
    {
        public string button;
        public InputType inputType;
        public UnityEvent onInput;

        public override void ResetName()
        {
            name = button + " " + inputType.ToString();
        }

        public override void CheckInputs()
        {
            switch (inputType)
            {
                case InputType.Press:
                    if (Input.GetButtonDown(button)) { onInput?.Invoke(); }
                    break;

                case InputType.Hold:
                    if (Input.GetButton(button)) { onInput?.Invoke(); }
                    break;

                case InputType.Release:
                    if (Input.GetButtonUp(button)) { onInput?.Invoke(); }
                    break;

                default:
                    break;
            }
        }
    }
}
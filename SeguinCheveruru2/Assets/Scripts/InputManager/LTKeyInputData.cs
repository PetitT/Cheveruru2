using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LowTeeGames
{
    [System.Serializable]
    public class LTKeyInputData : LTInputData
    {
        public KeyCode key;
        public InputType inputType;
        public UnityEvent onInput;

        public override void ResetName()
        {
            name = key.ToString() + " " + inputType.ToString();
        }

        public override void CheckInputs()
        {
            switch (inputType)
            {
                case InputType.Press:
                    if (Input.GetKeyDown(key)) { onInput?.Invoke(); }
                    break;

                case InputType.Hold:
                    if (Input.GetKey(key)) { onInput?.Invoke(); }
                    break;

                case InputType.Release:
                    if (Input.GetKeyUp(key)) { onInput?.Invoke(); }
                    break;

                default:
                    break;
            }
        }
    }
}
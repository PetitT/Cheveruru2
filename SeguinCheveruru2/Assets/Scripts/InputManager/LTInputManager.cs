using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LowTeeGames
{
    [AddComponentMenu("LowTee/Input Manager")]
    public class LTInputManager : MonoBehaviour
    {
        public List<LTKeyInputData> keyInputs;
        public List<LTButtonInputData> buttonInputs;
        public List<LTAxisInputData> axisInputs;

        private void OnValidate()
        {
            if (keyInputs != null) ResetNames(keyInputs);
            if (buttonInputs != null) ResetNames(buttonInputs);
            if (axisInputs != null) ResetNames(axisInputs);
        }

        private void ResetNames<T>(List<T> inputs) where T : LTInputData
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                inputs[i].ResetName();
            }
        }

        private void Update()
        {
            if (keyInputs != null) ProcessInput(keyInputs);
            if (buttonInputs != null) ProcessInput(buttonInputs);
            if (axisInputs != null) ProcessInput(axisInputs);
        }

        private void ProcessInput<T>(List<T> inputData) where T : LTInputData
        {
            for (int i = 0; i < inputData.Count; i++)
            {
                inputData[i].CheckInputs();
            }
        }
    }
}
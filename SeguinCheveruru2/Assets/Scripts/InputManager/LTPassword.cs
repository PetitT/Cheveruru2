using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LowTeeGames
{
	[AddComponentMenu("LowTee/Password")]
	public class LTPassword : MonoBehaviour
	{
		public string password;
		public bool reactivable = false;

		private List<string> passwordLetters = new List<string>();
		private int passIndex = 0;
		private bool hasBeenActivated = false;

		public UnityEvent onPasswordActivated;

        private void Awake()
        {
            char[] characters = password.ToCharArray();
            for (int i = 0; i < characters.Length; i++)
            {
                passwordLetters.Add(characters[i].ToString());
            }
        }

        private void Update()
        {
            if(hasBeenActivated && !reactivable) { return; }
			CheckInputs();
        }

        private void CheckInputs()
        {
            if (!Input.anyKeyDown) { return; }

            if (Input.inputString == passwordLetters[passIndex])
            {
                passIndex++;
                if (passIndex < passwordLetters.Count) { return; }
                ExecutePassword();
            }
            else
            {
                passIndex = 0;
            }
        }

        private void ExecutePassword()
        {
            onPasswordActivated?.Invoke();
            hasBeenActivated = true;
            passIndex = 0;
        }
    }
}
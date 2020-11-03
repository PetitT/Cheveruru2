using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LowTeeGames
{
	public abstract class LTInputData
	{
		[HideInInspector] public string name;

		public abstract void ResetName();
		public abstract void CheckInputs();
    }
}
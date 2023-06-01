using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LogicLevels
{
    public class StepsController : MonoBehaviour
    {
        [SerializeField] private FoxMover _foxMover;
        [SerializeField] private float _timeOfStepChanging;

        private IStepChangable[] _objToChangeStep;

		private void Start()
		{
            _objToChangeStep = FindObjectsOfType<MonoBehaviour>().OfType<IStepChangable>().ToArray();
		}

		public void ChangeStepToBots()
        {
			Debug.Log("Bot's step");
			foreach (var obj in _objToChangeStep)
            {
                obj.ChangeEnabledStatus();
            }
            Invoke(nameof(ChangeStepToPlayer), _timeOfStepChanging);
        }

        private void ChangeStepToPlayer()
        {
            Debug.Log("Player's step");
            _foxMover.enabled = true;
        }
    }

}

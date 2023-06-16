using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicLevels
{
    public class FoxMover : MonoBehaviour
    {
        [SerializeField] private PointForMove _currentPoint;
        [SerializeField] private float _speed;
        [SerializeField] private StepsController _stepController;

        private Rigidbody2D _rigidBody;
        private bool _isMoving = false;
        public bool IsMoving => _isMoving;

        private void Start()
		{
            _rigidBody = GetComponent<Rigidbody2D>();
			transform.position = _currentPoint.transform.position;
            _currentPoint.MarkAvailablePoints();
            _currentPoint.gameObject.SetActive(false);
		}

		public bool SetNewCurrentPoint(PointForMove newPoint)
        {
			if (_isMoving == false && enabled == true)
			{
                _currentPoint.gameObject.SetActive(true);

                _currentPoint.MarkUnAvailablePoints();
				_currentPoint = newPoint;
                _currentPoint.MarkAvailablePoints();

                _isMoving = true;
                StartCoroutine(Move());
                return true;
            } 
            else
            {
                return false;
            }
        }

        private IEnumerator Move()
        {
            var pointPos = _currentPoint.transform.position;
            var selfPos = transform;

            while (_isMoving == true)
            {
				var dir =  pointPos - selfPos.position;

                if (dir.magnitude < 0.1f)
                {
                    selfPos.position = pointPos;
					_isMoving = false;
                    _rigidBody.velocity = Vector2.zero;
				}
                else
                {
                    _rigidBody.velocity = dir.normalized * _speed; 
                }
                yield return new WaitForFixedUpdate();
            }
            enabled = false;
            _stepController.ChangeStepToBots();
        }

        public PointForMove GetCurrentPoint()
        {
            return _currentPoint;
        }
    }
}


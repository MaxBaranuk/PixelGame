using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controllers
{
	public class InputHandler : MonoBehaviour
	{
		public static ReactiveCommand<Vector2> PointerDown;
		public static ReactiveCommand<Vector2> PointerUp;
		public static ReactiveCommand<(Vector2, Vector2, Vector2)> PointerMove;
	
		public static ReactiveCommand<Tuple<Vector2, Vector2>> DoublePointerStart;
		public static ReactiveCommand<Tuple<Vector2, Vector2>> DoublePointerMove;
		private static Vector2 _prevTouchPosition;

		public static ReactiveCommand<float> MouseScroll;

		private void Awake () {
			PointerDown = new ReactiveCommand<Vector2>();
			PointerUp = new ReactiveCommand<Vector2>();
			PointerMove = new ReactiveCommand<(Vector2, Vector2, Vector2)>();
			DoublePointerStart = new ReactiveCommand<Tuple<Vector2, Vector2>>();
			DoublePointerMove = new ReactiveCommand<Tuple<Vector2, Vector2>>();
			MouseScroll = new ReactiveCommand<float>();
		}
	
		private void Update ()
		{
        #if UNITY_EDITOR
			EditorInput();
        #else
		    MobileInput();
        #endif				
		}

		private void MobileInput()
		{
			if (Input.touchCount == 1
			    && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
			{
				var touch = Input.GetTouch(0);
				
				switch (touch.phase)
				{
					case TouchPhase.Began:
						PointerDown.Execute(touch.position);
						break;
					case TouchPhase.Moved:
						Vector2 prev = _prevTouchPosition;
						var arg = (current: touch.position, previous:  prev, delta: touch.position - prev);
						PointerMove.Execute(arg);
						break;
					case TouchPhase.Stationary:
						break;
					case TouchPhase.Ended:
						PointerUp.Execute(touch.position);
						break;
					case TouchPhase.Canceled:
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			else if (Input.touchCount == 2)
			{
				var touch1 = Input.GetTouch(0);
				var touch2 = Input.GetTouch(1);
				switch (touch2.phase)
				{
					case TouchPhase.Began:
						DoublePointerStart.Execute(Tuple.Create(touch1.position, touch2.position));
						break;
					case TouchPhase.Moved:
						DoublePointerMove.Execute(Tuple.Create(touch1.position, touch2.position));
						break;
					case TouchPhase.Stationary:
						break;
					case TouchPhase.Ended:
						break;
					case TouchPhase.Canceled:
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		private static void EditorInput()
		{
			if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
			{
				_prevTouchPosition = Input.mousePosition;
				PointerDown.Execute(Input.mousePosition);
			}

			if (Input.GetMouseButtonUp(0))
				PointerUp.Execute(Input.mousePosition);

			if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
			{
				var dist = Vector3.Distance(_prevTouchPosition, Input.mousePosition);
				if (dist < 5)
					return;
				
				Vector2 currPos = Input.mousePosition;
				var arg = (current: currPos, prev: _prevTouchPosition, delta: currPos - _prevTouchPosition);
				PointerMove.Execute(arg);
				_prevTouchPosition = Input.mousePosition;
			}				

			if (Input.mouseScrollDelta != Vector2.zero)
			{
				MouseScroll.Execute(Input.mouseScrollDelta.y);
			}
		}
	}
}

using System;
using System.Linq;
using UniRx;
using UniRx.Diagnostics;
using Unity.Mathematics;
using Unity.Splines.Examples;
using UnityEngine;
using UnityEngine.Splines;

namespace _MeshTest.Scripts
{
    public class SplineMeshTest : MonoBehaviour
    {
        [SerializeField] private SplineContainer splineContainer;
        private Spline Spline => splineContainer.Spline;
        private BezierKnot LastKnot => Spline.Knots.Last();
        private MultipleRoadBehaviour _multipleRoadBehaviour;
        private readonly float3 _targetIn = new(0, 0, -1);
        private readonly float3 _targetOut = new(0, 0, 1);

        private void Start()
        {
            _multipleRoadBehaviour = GetComponent<MultipleRoadBehaviour>();
            Observable.Interval(TimeSpan.FromSeconds(3f))
                .Debug()
                .Subscribe(_ => { AddKnot(); }).AddTo(this);
        }

        private void AddKnot()
        {
            var addKnot = new BezierKnot(new float3(0, 0, LastKnot.Position.z + 3),
                _targetIn, _targetOut, quaternion.identity);
            Spline.Add(addKnot);
            _multipleRoadBehaviour.CreateRoads();
        }
    }
}
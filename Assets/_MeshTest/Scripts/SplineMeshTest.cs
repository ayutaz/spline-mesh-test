using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace _MeshTest.Scripts
{
    public class SplineMeshTest : MonoBehaviour
    {
        [SerializeField] private SplineContainer splineContainer;
        private BezierKnot LastKnot => splineContainer.Splines[0].Knots.Last();
        private readonly float3 _targetIn = new float3(0, 0, -1);
        private readonly float3 _targetOut = new float3(0, 0, 1);

        private void Start()
        {
            Observable.Interval(TimeSpan.FromSeconds(3f))
                .Subscribe(_ =>
                {
                    Debug.Log("Add spline's Knot in runtime");
                    AddKnot();
                }).AddTo(this);
        }

        private void AddKnot()
        {
            var addKnot = new BezierKnot(new float3(0, 0, LastKnot.Position.z + 3), _targetIn, _targetOut, quaternion.identity);
            splineContainer.Splines[0].Add(addKnot);
        }
    }
}
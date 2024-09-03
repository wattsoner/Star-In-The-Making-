using System.Collections.Generic;
using UnityEngine;

public class EnergyWaveAnimation : MonoBehaviour {
    
    [Header("Energy Movement Settings")] 
    [SerializeField] private float waveSpeed = 2f;
    [SerializeField] private float waveHeight = 0.5f; 

    private List<Transform> energyTransforms;
    private Dictionary<Transform, Vector3> initialPositions;

    private void Start() {
        energyTransforms = new List<Transform>();
        initialPositions = new Dictionary<Transform, Vector3>();
    }

    private void Update() {
        AnimateEnergyBalls();
    }

    public void RegisterEnergy(Transform energyTransform) {
        energyTransforms.Add(energyTransform);
        initialPositions[energyTransform] = energyTransform.position;
    }

    public void UnregisterEnergy(Transform energyTransform) {
        energyTransforms.Remove(energyTransform);
        initialPositions.Remove(energyTransform);
    }

    private void AnimateEnergyBalls() {
        var time = Time.time;

        foreach (var energyTransform in energyTransforms) {
            if (energyTransform == null || !initialPositions.TryGetValue(energyTransform, out var initialPosition)) continue;

            energyTransform.position = new Vector3(
                initialPosition.x,
                initialPosition.y + Mathf.Sin(time * waveSpeed + initialPosition.x) * waveHeight,
                initialPosition.z
            );
        }
    }
}
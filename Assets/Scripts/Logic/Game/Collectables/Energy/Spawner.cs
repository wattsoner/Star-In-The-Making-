using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    
    [Header("Attachables")] 
    [SerializeField] private EnergyPool energyPool;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private EnergyWaveAnimation spriteMovementAnimation;
    
    [Header("Radius Settings")] 
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float despawnRadius = 15f;
    [SerializeField] private float seperationRadius = 0.5f;
    
    [Header("Spawn Amount")] 
    [SerializeField] private int maxEnergyCount = 50;
    
    [Header("Energy Size Settings")] 
    [SerializeField] private float minSize = 0.05f;
    [SerializeField] private float maxSize = 0.15f;
    
    [Header("Energy Color Settings")]
    [SerializeField] private Color[] energyColors = {
        new Color(102f/255f, 204f/255f, 255f/255f),  // #66CCFF
        new Color(153f/255f, 255f/255f, 153f/255f),  // #99FF99
        new Color(255f/255f, 178f/255f, 102f/255f),  // #FFB266
        new Color(255f/255f, 153f/255f, 204f/255f),  // #FF99CC
        new Color(255f/255f, 255f/255f, 153f/255f),  // #FFFF99
        new Color(204f/255f, 153f/255f, 255f/255f),  // #CC99FF
        new Color(255f/255f, 102f/255f, 102f/255f)   // #FF6666
    };
    
    private List<GameObject> activeEnergy;

    private Transform cameraTransform;

    private void Start() {
        cameraTransform = mainCamera.transform;
        activeEnergy = new List<GameObject>();
        SpawnInitialEnergy();
    }

    private void Update() {
        SpawnEnergyAroundCamera();
        DespawnEnergyOutOfRange();
    }

    private void SpawnInitialEnergy() {
        for (var i = 0; i < maxEnergyCount; i++) SpawnEnergyAtRandomPosition(true);
    }

    private void SpawnEnergyAroundCamera() {
        if (activeEnergy.Count < maxEnergyCount) SpawnEnergyAtRandomPosition(false);
    }

    private void SpawnEnergyAtRandomPosition(bool isInitialSpawn) {
        Vector3 randomPosition;
        bool isVisible;
        bool isTooClose;

        do {
            randomPosition = cameraTransform.position + Random.insideUnitSphere * spawnRadius;
            randomPosition.z = 0f;

            isVisible = IsPositionVisible(randomPosition);
            
            isTooClose = false;
            foreach (var existingEnergy in activeEnergy) { 
                if (Vector3.Distance(randomPosition, existingEnergy.transform.position) < seperationRadius) {
                    isTooClose = true;
                    break;
                }
            }
        } while ((!isInitialSpawn && isVisible) || isTooClose);

        var newEnergy = energyPool.GetEnergy();
        newEnergy.transform.position = randomPosition;

        var randomSize = Random.Range(minSize, maxSize);
        newEnergy.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

        var randomColor = energyColors[Random.Range(0, energyColors.Length)];
        var energyRenderer = newEnergy.GetComponent<Renderer>();

        if (energyRenderer != null) {
            energyRenderer.material.color = randomColor;
        }

        activeEnergy.Add(newEnergy);
        spriteMovementAnimation.RegisterEnergy(newEnergy.transform);
    }



    private bool IsPositionVisible(Vector3 position) {
        var viewportPoint = mainCamera.WorldToViewportPoint(position);
        return viewportPoint.x > 0 && viewportPoint is { x: < 1, y: > 0 and < 1, z: > 0 };
    }

    private void DespawnEnergyOutOfRange() {
        for (var i = activeEnergy.Count - 1; i >= 0; i--) {
            var energy = activeEnergy[i];
            var distance = Vector3.Distance(cameraTransform.position, energy.transform.position);

            if (!(distance > despawnRadius)) continue;
            
            energyPool.ReturnEnergy(energy);
            spriteMovementAnimation.UnregisterEnergy(energy.transform);
            activeEnergy.RemoveAt(i);
        }
    }
}

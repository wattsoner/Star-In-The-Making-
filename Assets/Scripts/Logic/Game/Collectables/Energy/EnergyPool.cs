using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnergyPool : MonoBehaviour {
    
    [Header("Attachables")] 
    [SerializeField] private GameObject energyPrefab;
    [SerializeField] private GameObject energyContainer;

    [Header("Pool Settings")] 
    [SerializeField] private int poolSize;

    private Queue<GameObject> energyPool;

    private void Start() {
        energyPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++) {
            GameObject energy = Instantiate(energyPrefab, energyContainer.transform, true);
            energy.name = $"Energy {Random.Range(1, 1000)}";
            energy.SetActive(false);
            energyPool.Enqueue(energy);
        }
    }

    public GameObject GetEnergy() {
        if (energyPool.Count > 0) {
            GameObject energy = energyPool.Dequeue();
            energy.SetActive(true);
            return energy;
        } else {
            GameObject energy = Instantiate(energyPrefab);
            return energy;
        }
    }
    
    public void ReturnEnergy(GameObject energy) {
        energy.SetActive(false);
        energyPool.Enqueue(energy);
    }
}
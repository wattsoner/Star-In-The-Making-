using System;
using UnityEngine;

[Serializable]
public class Boost {
    
    public int boostsCount;
    public float rechargeTime;
    
    private int nextBoostToRecharge = -1;
    
    private bool isRecharging = false;
    
    private float[] boostTimers;
    
    private bool[] boostsAvailable;

   

    public Boost(int count, float rechargeTime) {
        boostsCount = count;
        this.rechargeTime = rechargeTime;

        boostTimers = new float[boostsCount];
        boostsAvailable = new bool[boostsCount];

        for (var i = 0; i < boostsCount; i++) boostsAvailable[i] = true;
    }

    public bool IsBoostAvailable(int index) {
        return index >= 0 && index < boostsCount && boostsAvailable[index];
    }

    
    public void UseBoost() {
        for (var i = 0; i < boostsCount; i++) {
            if (!boostsAvailable[i]) continue;
            
            boostsAvailable[i] = false;
            boostTimers[i] = rechargeTime;
            
            if (!isRecharging) {
                nextBoostToRecharge = i;
                isRecharging = true;
            }
            break;
        }
    }

    public void UpdateBoostTimers() {
        if (nextBoostToRecharge == -1) return;
        
        boostTimers[nextBoostToRecharge] -= Time.deltaTime;
            
        if (!(boostTimers[nextBoostToRecharge] <= 0f)) return;
        
        boostsAvailable[nextBoostToRecharge] = true;
        nextBoostToRecharge = FindNextBoostToRecharge();
        
        if (nextBoostToRecharge == -1) {
            isRecharging = false; 
        }
    }

    private int FindNextBoostToRecharge() {
        for (var i = 0; i < boostsCount; i++) {
            if (!boostsAvailable[i] && boostTimers[i] > 0f) {
                return i;
            }
        }
        return -1;
    }

    public int GetAvailableBoosts() {
        var availableCount = 0;

        for (var i = 0; i < boostsCount; i++)
            if (boostsAvailable[i])
                availableCount++;
        return availableCount;
    }
}
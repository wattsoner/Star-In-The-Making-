using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [Header("Boost Settings")]
    [SerializeField] private Boost boost; 
    [SerializeField] private float boostStrength = 5f; 

    [Header("Boost UI Attachables")]
    [SerializeField] private AnimationBoostFade boostUI;
    
    [Header("Particle System")]
    [SerializeField] private ParticleSystem confettiParticleSystem;
    
    private Rigidbody2D rb;

    private AnimationBoostFade abfUI;
    
    private Dictionary<KeyCode, Action> inputActions;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        
        rb.gravityScale = 0;
        rb.drag = 1;
        rb.angularDrag = 1; 
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        boost = new Boost(3, 1); 

        InitializeInputActions();
    }

    private void Update() {
        HandleBoostInput();
        boost.UpdateBoostTimers();
        
        boostUI.UpdateBoostUI(boost.GetAvailableBoosts());
      
    }

    private void InitializeInputActions() {
        inputActions = new Dictionary<KeyCode, Action> {
            { KeyCode.W, () => TryBoost(Vector2.up) },
            { KeyCode.UpArrow, () => TryBoost(Vector2.up) },
            { KeyCode.S, () => TryBoost(Vector2.down) },
            { KeyCode.DownArrow, () => TryBoost(Vector2.down) },
            { KeyCode.A, () => TryBoost(Vector2.left) },
            { KeyCode.LeftArrow, () => TryBoost(Vector2.left) },
            { KeyCode.D, () => TryBoost(Vector2.right) },
            { KeyCode.RightArrow, () => TryBoost(Vector2.right) }
        };
    }

    private void HandleBoostInput() {
        foreach (var action in inputActions.Where(action => Input.GetKeyDown(action.Key))) {
            action.Value.Invoke();
            return;
        }
    }

    private void TryBoost(Vector2 direction) {
        if (boost.GetAvailableBoosts() > 0) {
            BoostInDirection(direction);
            PlayConfettiEffect();
        }
        else
        {
            Debug.Log("No boosts available");
        }
    }

    private void BoostInDirection(Vector2 direction) {
        rb.velocity = direction * boostStrength;
        
        boost.UseBoost();
    }
    
    private void PlayConfettiEffect() {
        if (confettiParticleSystem == null) return;
        
        confettiParticleSystem.transform.position = transform.position;
        confettiParticleSystem.Play();
    }
}

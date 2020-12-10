using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerSystem : MonoBehaviour
{
    private PlayerMovement playerMov;
    // private PlayerAnimationManager animation;
    private float satiety;
    public float hungerThreshold;
    public float satietyMax;
    public float satietyMin;
    public float startSatiety;
    public float PenaltyFactor;

    void Start()
    {
        InvokeRepeating("Penalize", 0.5f, 0.5f);
        playerMov = GetComponent<PlayerMovement>();
    }



    private void OnTriggerEnter(Collider collision)  //aggiungi vento (AGGIUNGERE UNO SMOOTHING) ---- RILEVA QUANDO ATTERRARE E MEMORIZZA SE SI PUò DECOLLARE
    {
        if (collision.gameObject.GetComponent<Fish>())
        {
            satiety += collision.gameObject.GetComponent<Fish>().score;
            Destroy(collision.gameObject);
        }

    }

    void CalculatePenalties(bool divideNotMultiply, ref float parameter)
    {
        if (divideNotMultiply)
        {
            parameter *= PenaltyFactor;
        }
        else
        {
            parameter /= PenaltyFactor;
        }
    }

    void Penalize()
    {
        bool hungry = (satiety < hungerThreshold);
        if (hungry == playerMov.penalty)
        {
            return;
        }
        else
        {
            playerMov.penalty = hungry;
            CalculatePenalties(hungry, ref playerMov.propulsion); //aggiungere altre eventuali penalità
        }
    }
    
}

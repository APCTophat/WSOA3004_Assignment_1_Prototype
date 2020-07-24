using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hamster_Stats_Scripts : MonoBehaviour
{

    //Hamster Core Stats
    public float Health;
    public float HealthMax;
    public float Happiness;
    public float HappinessMax;
    public float Fitness;
    public float FitnessMax;

    public float Food;
    public float FoodMax;
    public float Water;
    public float WaterMax;


    //Stats that change the hamsters stats
    public float _FoodIncreaseAmount;
    public float _foodDecreaseAmount;   

    public float _WaterIncreaseAmount;
    public float _waterDecreaseAmount;   

    public float _happinessIncreaseAmount;
    public float _happinessDecreaseAmount;  

    public float _fitnessIncreaseAmount;
    public float _fitnessDecreaseAmount;

    public float _countdownDeplesionTime;
    public float _countdownStartValue;

    public float _increaseTickRate;
    public float _increaseTickStart;

    //collision
    public string _resourceFood;
    public string _resourceWater;
    public string _resourceFitness;
    public string _resourceHappiness;

    public bool _movingRandomly;
    public bool isDrinking;
    public bool isEating;
    public bool isRunning;
    public bool isPlaying;


    //Ui Elements
    public Image _uiHealth;
    public Image _uiHappiness;
    public Image _uiFitness;

    public GameObject LoseText;

    public string Main_Scene;

    public GameObject EndScreenSound;
    public GameObject MainMusic;

    void Start()
    {
        
    }

    private void Awake()
    {
        _movingRandomly = GetComponent<Hamster_Movement_Script>()._canMoveRandomly;
        LoseText.SetActive(false);
        EndScreenSound.SetActive(false);
    }


    public void ResourseCounter()
    {
        if(_countdownDeplesionTime > 0)
        {
            _countdownDeplesionTime -= Time.deltaTime;
        }
        else
        {
            ConsitentLoss();
            _countdownDeplesionTime = _countdownStartValue;
        }

        if(_increaseTickRate > 0)
        {
            _increaseTickRate -= Time.deltaTime;
        }
        else
        {
            if (isDrinking)     //health
            {
                WaterGain(_WaterIncreaseAmount);
                //FoodLoss(_foodDecreaseAmount);
                //HappinessLoss(_happinessDecreaseAmount);
                //FitnessLoss(_fitnessDecreaseAmount);
            }
            else if (isEating)  //health
            {
                FoodGain(_FoodIncreaseAmount);
                //HappinessLoss(_happinessDecreaseAmount);
                //FitnessLoss(_fitnessDecreaseAmount);
                //WaterLoss(_waterDecreaseAmount);
            }
            else if (isPlaying)   //happiness
            {
                HappinessGain(_happinessIncreaseAmount);
               //FitnessLoss(_fitnessDecreaseAmount);
               //FoodLoss(_foodDecreaseAmount);
               //WaterLoss(_waterDecreaseAmount);
            }
            else if (isRunning)  //fitness
            {
                FitnessGain(_fitnessIncreaseAmount);
                //HappinessLoss(_happinessDecreaseAmount);
                //FoodLoss(_foodDecreaseAmount);
                //WaterLoss(_waterDecreaseAmount);
            }
            
            _increaseTickRate = _increaseTickStart;
        }
    }
   
    public void ConsitentLoss()
    {
        //the stats that decrease consistently stats
        Food = Food - _foodDecreaseAmount;
        Water = Water - _waterDecreaseAmount;
        Happiness = Happiness - _happinessDecreaseAmount;
        Fitness = Fitness - _fitnessDecreaseAmount;
    }
    //fucntions to decrese the stats
    public void FoodLoss(float LossAmount)
    {
        Food = Food - LossAmount;
    }
    public void WaterLoss(float LossAmount)
    {
        Water = Water - LossAmount;
    }
    public void HappinessLoss(float LossAmount)
    {
        Happiness = Happiness - LossAmount;
    }
    public void FitnessLoss(float LossAmount)
    {
        Fitness = Fitness - LossAmount;
    }
    //Functions to increase that stats
    public void FoodGain(float GainAmount)
    {
        Food = Food + GainAmount;
    }
    public void WaterGain(float GainAmount)
    {
        Water = Water + GainAmount;
    }
    public void HappinessGain(float GainAmount)
    {
        Happiness = Happiness + GainAmount;
    }
    public void FitnessGain(float GainAmount)
    {
        Fitness = Fitness + GainAmount;
    }

    void Update()
    {
        _movingRandomly = GetComponent<Hamster_Movement_Script>()._canMoveRandomly;
        isPlaying = GetComponent<Hamster_Movement_Script>().isPlaying;

        Health = (Food + Water) / 2;
        if (isPlaying)
        {
            isRunning = false;
            isEating = false;
            isDrinking = false;
        }
        ResourseCounter();
        CheckLoseCondition();
        UIUpdate();

       
    }

    public void UIUpdate()
    {
        if(Food > FoodMax)
        {
            Food = FoodMax;
        }
        if(Water > WaterMax)
        {
            Water = WaterMax;
        }
        if(Fitness > FitnessMax)
        {
            Fitness = FitnessMax;
        }
        if(Happiness > HappinessMax)
        {
            Happiness = HappinessMax;
        }

        _uiHealth.fillAmount = Health / HealthMax ;
        _uiHappiness.fillAmount = Happiness / HappinessMax;
        _uiFitness.fillAmount = Fitness / FitnessMax;
    }

    public void CheckLoseCondition()
    {
        if(Health <= 0 || Fitness <= 0 || Happiness <= 0)
        {
            Invoke("LoadMenu", 2f);
            LoseText.SetActive(true);
            EndScreenSound.SetActive(true);
            MainMusic.SetActive(false);
        }
        
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(Main_Scene);
    }
    public void OnTriggerEnter(Collider other)
    {
        
        //Debug.Log(other.gameObject.name);
        if (!_movingRandomly)
        {
            if (other.gameObject.name == _resourceFood)
            {
                isEating = true;
            }
            else if (other.gameObject.name == _resourceWater)
            {
                isDrinking = true;
            }
            else if (other.gameObject.name == _resourceFitness)
            {
                isRunning = true;
            }
            
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == _resourceFood)
        {
            isEating = false;
        }
        else if (other.gameObject.name == _resourceWater)
        {
            isDrinking = false;
        }
        else if (other.gameObject.name == _resourceFitness)
        {
            isRunning = false;
        }
    }
}

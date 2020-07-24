﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamster_Movement_Script : MonoBehaviour
{

    //variables relating to "random" movement
    public Vector3[] _moveLocation;
    public Vector3 _tempRandomLocation;

    public bool _canMoveRandomly;
    public bool _ranMoveInstance;

    public float _ranMovementSpeed;
    public float _ranSmoothTime;
    public float _ranMovementIntervalsMin;
    public float _ranMovementIntervalsMax;
    public int _ranMovementArraySize;



    //variables relating to controlled movement
    public Vector3 foodLocation;
    public Vector3 waterLocation;
    public Vector3 fitnessLocation;
    public string foodName;
    public string waterName;
    public string fitnessName;
    public string hamsterName;

    public bool isMoving;
    public bool commandInstance;
    public float hamsterMoveSpeed;
    public float hamserSmoothTime;

    public Vector3 currentPosition;
    public Vector3 targetPosition;

    private IEnumerator coroutine;
    public LayerMask _InteractableObjectLayer;

    //bools to interact with the hamster's stats
    public bool isDrinking;
    public bool isEating;
    public bool isRunning;
    public bool isPlaying;
   

    void Start()
    {
        _InteractableObjectLayer = LayerMask.GetMask("Interactable");
        Invoke("randomHamsterLocation", 3f);
        commandInstance = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            randomHamsterLocation();
        }
    }
    private IEnumerator randomMoveHamsterCoroutine()
    {
        _ranMoveInstance = true;
        //Debug.Log("Random movement start");
        if (_canMoveRandomly)
        {
            while (Vector3.Distance(transform.position, _tempRandomLocation) > 0.05f)
            {
                float TempX = Mathf.SmoothDamp(transform.position.x, _tempRandomLocation.x, ref _ranMovementSpeed, _ranSmoothTime);
               // float TempX = Mathf.Lerp(transform.position.x, _tempRandomLocation.x, _ranMovementSpeed * Time.deltaTime);
                transform.position = new Vector3(TempX, transform.position.y, transform.position.z);
                yield return null;
            }
        }
        _ranMoveInstance = false;
        yield return new WaitForSeconds(Random.Range(_ranMovementIntervalsMin, _ranMovementIntervalsMax));
        
        randomHamsterLocation();
    }


    public void randomHamsterLocation()
    {
        if (_canMoveRandomly)
        {
            if (!_ranMoveInstance)
            {
                isPlaying = false;
                //Debug.Log("New Location to move to");
                int num = Random.Range(0, _ranMovementArraySize);
                _tempRandomLocation = _moveLocation[num];
                StartCoroutine("randomMoveHamsterCoroutine");
            }
        }
               
    }
    private IEnumerator MoveHamsterCoroutine(Vector3 targetPosition)
    {
       // Debug.Log("Command movement start");
        commandInstance = true;
        StopCoroutine("randomMoveHamsterCoroutine");
        _canMoveRandomly = false;
        if (!isMoving)
        {
            while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
            {
                isMoving = true;
                float TempX = Mathf.SmoothDamp(transform.position.x, targetPosition.x, ref hamsterMoveSpeed, hamserSmoothTime);
               // float TempX = Mathf.Lerp(transform.position.x, targetPosition.x, hamsterMoveSpeed * Time.deltaTime);
                transform.position = new Vector3(TempX, transform.position.y, transform.position.z);
                yield return null;
            }
            isMoving = false;
            _canMoveRandomly = true;
        }
         //Debug.Log("Instucted movement done");
        _tempRandomLocation = transform.position;
         StartCoroutine("randomMoveHamsterCoroutine");
        
        yield return null;
        commandInstance = false;
    }

   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointToObject();
        }

        
    }

    public void PointToObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit HIT;
        if (Physics.Raycast(ray, out HIT, _InteractableObjectLayer))
        {
           if(HIT.transform != null)
            {
                if (!commandInstance)
                {
                    HamserMove(HIT.transform.gameObject.name);
                }
                
            }

        }
    }

    public void HamserMove(string ObjectName)
    {
        if(ObjectName == waterName)
        {
            targetPosition = waterLocation;
            StartCoroutine(MoveHamsterCoroutine(targetPosition));
        }
        else if (ObjectName == foodName)
        {
            targetPosition = foodLocation;
            StartCoroutine(MoveHamsterCoroutine(targetPosition));
        }
        else if (ObjectName == fitnessName)
        {   
            targetPosition = fitnessLocation;
            StartCoroutine(MoveHamsterCoroutine(targetPosition));
        }
        else if(ObjectName == hamsterName)
        {
            targetPosition = transform.position;
            StartCoroutine(MoveHamsterCoroutine(targetPosition));
            isPlaying = true;
        }


    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamster_Movement_Script : MonoBehaviour
{

    //variables relating to "random" movement
    public Vector3[] _moveLocation;
    public Vector3 _tempRandomLocation;

    public bool _canMoveRandomly;

    public float _ranMovementSpeed;
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

    public bool isMoving;
    public float hamsterMoveSpeed;

    public Vector3 currentPosition;
    public Vector3 targetPosition;

    private IEnumerator coroutine;
    public LayerMask _InteractableObjectLayer;

    
    void Start()
    {
        _InteractableObjectLayer = LayerMask.GetMask("Interactable");

        randomHamsterLocation();
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
        if (_canMoveRandomly)
        {
            while (Vector3.Distance(transform.position, _tempRandomLocation) > 0.05f)
            {
                float TempX = Mathf.Lerp(transform.position.x, _tempRandomLocation.x, _ranMovementSpeed * Time.deltaTime);
                transform.position = new Vector3(TempX, transform.position.y, transform.position.z);
                yield return null;
            }
        }
        yield return new WaitForSeconds(Random.Range(_ranMovementIntervalsMin, _ranMovementIntervalsMax));

        randomHamsterLocation();
    }


    public void randomHamsterLocation()
    {
        //Debug.Log("New Location to move to");
        int num = Random.Range(0, _ranMovementArraySize);
        _tempRandomLocation = _moveLocation[num];
        StartCoroutine("randomMoveHamsterCoroutine");
        
    }
    private IEnumerator MoveHamsterCoroutine(Vector3 targetPosition)
    {
        StopCoroutine("randomMoveHamsterCoroutine");
        _canMoveRandomly = false;
        if (!isMoving)
        {
            while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
            {
                isMoving = true;
                float TempX = Mathf.Lerp(transform.position.x, targetPosition.x, hamsterMoveSpeed * Time.deltaTime);
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
                HamserMove(HIT.transform.gameObject.name);
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


    }

    
}

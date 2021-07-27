using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LerpBetweenPoints : MonoBehaviour {

    [SerializeField]
    [Tooltip("Locations to lerp between.")]
    private List<Transform> locations;
    [SerializeField]
    [Tooltip("Time to wait before another lerp happens")]
    [Range(0, 1)]
    private List<float> waitTimes;
    [SerializeField]
    [Tooltip("Movement speed towards the new location.")]
    [Range(0, 10)]
    private List<float> movementSpeeds;

    #region Indexes
    private int currentIndexLocations = 1;
    private int currentIndexMovement = 0;
    #endregion

    #region New location calculation
    private Transform newLocation;
    private float movementspeed;
    private float distance;
    private float startTime;
    #endregion

    #region Moving States
    private bool move;
    private bool rotate;
    #endregion

    /// <summary>
    /// Destroys script if there are no locations
    /// Addes wait times and movement speeds if they are empty
    /// </summary>
    private void Awake () {
        if (locations.Count == 0 )
        {
            Destroy(this);
        }
        if (waitTimes.Count == 0)
        {
            waitTimes.Add(0.5f);
            waitTimes.Add(1f);
        }
        if (movementSpeeds.Count == 0)
        {
            movementSpeeds.Add(0.1f);
            movementSpeeds.Add(0.05f);
        }
    }

    /// <summary>
    /// Set starting values
    /// </summary>
    private void Start()
    {
        startTime = Time.time;
        newLocation = locations[currentIndexLocations];
        move = true;
        distance = Vector3.Distance(transform.position, newLocation.position);
    }

    /// <summary>
    /// Lerps to new position
    /// when at the new position set the next target and new movementspeed.
    /// </summary>
    private void Update()
    {

        if (transform.position == newLocation.position)
        {
            move = false;
            StartCoroutine(Wait());
            HelperFunctions.UpdateIndex(locations.Count, ref currentIndexLocations);
            HelperFunctions.UpdateIndex(movementSpeeds.Count, ref currentIndexMovement);
            newLocation = locations[currentIndexLocations];
            movementspeed = movementSpeeds[currentIndexMovement];
            distance = Vector3.Distance(transform.position, newLocation.position);
        }
        if (move)
        {
            if (rotate)
            {
                rotate = false;
                transform.LookAt(newLocation.position);
            }

            float distCovered = (Time.time - startTime) * movementSpeeds[currentIndexMovement];
            float fracJourney = distCovered / distance;
            transform.position = Vector3.Lerp(transform.position, newLocation.position, fracJourney);
        }
    }

    /// <summary>
    /// Wait for a random amount of time.
    /// after the wait set the start time and set rotate and move to true.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Wait()
    {
        float waitTime = waitTimes[Random.Range(0, waitTimes.Count)];
        yield return new WaitForSeconds(waitTime);
        startTime = Time.time;
        rotate = true;
        move = true;
    }
}

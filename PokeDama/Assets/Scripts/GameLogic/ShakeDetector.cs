using UnityEngine;
using System.Collections;

public class ShakeDetector : MonoBehaviour
{
    static float accelerometerUpdateInterval = (float)(1.0 / 60.0f);
    // The greater the value of LowPassKernelWidthInSeconds, the slower the filtered value will converge towards current input sample (and vice versa).
    static float lowPassKernelWidthInSeconds = 1.0f;
    // This next parameter is initialized to 2.0 per Apple's recommendation, or at least according to Brady! ;)
    static float shakeDetectionThreshold = 2.0f;

    static float lowPassFilterFactor = 0;
    static Vector3 lowPassValue;
    static Vector3 acceleration;
    static Vector3 deltaAcceleration;
    static bool is_first = true;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public static bool isShaked(){
        if (is_first)
        {
            lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;

            lowPassValue = Vector3.zero;

            shakeDetectionThreshold *= shakeDetectionThreshold;
            lowPassValue = Input.acceleration;
            is_first = false;
            Debug.Log("is first if statement");
        }

        acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        deltaAcceleration = acceleration - lowPassValue;
        Debug.Log("deltaAcceleration.sqrMagnitude  " + deltaAcceleration.sqrMagnitude);
        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
        {
            // Perform your "shaking actions" here, with suitable guards in the if check above, if necessary to not, to not fire again if they're already being performed.
            Debug.Log("Shake event detected at time " + Time.time);
            return true;
        }

        return false;
	}
}


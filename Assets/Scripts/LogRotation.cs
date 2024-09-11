using System.Collections;
using UnityEngine;

public class LogRotation : MonoBehaviour
{
    [System.Serializable]
    private class RotationElement
    {
        public float speed;
        public float duration;
    }

    [SerializeField]
    private RotationElement[] rotationPattern;
    
    private WheelJoint2D wheelJoint;
    private JointMotor2D motor;

    private void Awake()
    {
        wheelJoint = GetComponent<WheelJoint2D>();
        motor = wheelJoint.motor;
        StartCoroutine("PlayRotationPattern");
    }

    private IEnumerator PlayRotationPattern()
    {
        int rotationIndex = 0;

        while(true)
        {
            yield return new WaitForFixedUpdate();

            motor.motorSpeed = rotationPattern[rotationIndex].speed;
            motor.maxMotorTorque = 10000;
            wheelJoint.motor = motor;

            yield return new WaitForSecondsRealtime(rotationPattern[rotationIndex].duration);
            rotationIndex++;
            rotationIndex = rotationIndex < rotationPattern.Length ? rotationIndex : 0; 
        }
    }

}

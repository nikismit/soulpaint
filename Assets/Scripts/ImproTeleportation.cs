using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ImproTeleportation : VRTK_DashTeleport {

    VRTK_DestinationPoint destinationPoint;
    protected override void DoTeleport(object sender, DestinationMarkerEventArgs e)
    {
        if (enableTeleport && ValidLocation(e.target, e.destinationPosition) && e.enableTeleport)
        {
            if (e.target && e.target.GetComponent<VRTK_DestinationPoint>())
            {
                if(destinationPoint != null)
                    destinationPoint.disabled = false;
                destinationPoint = e.target.GetComponent<VRTK_DestinationPoint>();
                var dest = destinationPoint.destinationLocation ? destinationPoint.destinationLocation.position : e.target.position;
                e.raycastHit.point = dest /*- (Camera.main.transform.position - Camera.main.transform.parent.parent.position)*/;
                e.destinationPosition = e.raycastHit.point;
                destinationPoint.disabled = true;
            }
            else if (destinationPoint != null)
                    destinationPoint.disabled = false;
            
            StartTeleport(sender, e);
            Quaternion updatedRotation = SetNewRotation(e.destinationRotation);
            Vector3 newPosition = GetNewPosition(e.destinationPosition, e.target, e.forceDestinationPosition);
            CalculateBlinkDelay(blinkTransitionSpeed, newPosition);
            Blink(blinkTransitionSpeed);
            Vector3 updatedPosition = SetNewPosition(newPosition, e.target, e.forceDestinationPosition);
            ProcessOrientation(sender, e, updatedPosition, updatedRotation);
            EndTeleport(sender, e);
        }
    }


}

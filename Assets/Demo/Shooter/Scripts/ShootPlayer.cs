using System.Collections;
using System.Collections.Generic;
using TrueSync;
using UnityEngine;

public class ShootPlayer : TrueSyncBehaviour
{
    public override void OnSyncedInput()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement.z = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.z = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1;
        }

        TrueSyncInput.SetFP(0, movement.x);
        TrueSyncInput.SetFP(1, movement.z);
    }


    public override void OnSyncedUpdate()
    {
        FP x = TrueSyncInput.GetFP(0) * TrueSyncManager.DeltaTime;
        FP z = TrueSyncInput.GetFP(1) * TrueSyncManager.DeltaTime;

        tsTransform.Translate(x, 0, z, Space.World);
    }
}

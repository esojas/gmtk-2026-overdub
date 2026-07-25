using System.Collections;
using UnityEngine;

public static class ReplayCollisionUtility
{
    public static IEnumerator SuppressCloneCollisionsBriefly(int steps = 2)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        int cloneLayer = LayerMask.NameToLayer("PlayerClone");
        int obstacleLayer = LayerMask.NameToLayer("Obstacle");

        Physics.IgnoreLayerCollision(playerLayer, cloneLayer, true);
        Physics.IgnoreLayerCollision(obstacleLayer, cloneLayer, true);

        for (int i = 0; i < steps; i++)
            yield return new WaitForFixedUpdate();

        Physics.IgnoreLayerCollision(playerLayer, cloneLayer, false);
        Physics.IgnoreLayerCollision(obstacleLayer, cloneLayer, false);
    }
}

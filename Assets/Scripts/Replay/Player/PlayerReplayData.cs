using UnityEngine;

public class PlayerReplayData : ReplayData
{
    public bool isGrounded { get; private set; }

    public Vector3 movement { get; private set; }

    public Quaternion playerRotation { get; private set; }

    public bool deathThisFrame { get; private set; }

    public bool isVisible { get; private set; }

    public bool isJump { get; private set; }

    public PlayerReplayData(Vector3 position, bool isGrounded, Vector3 movement, Quaternion playerRotation, bool deathThisFrame, bool isVisible, bool isJump)
    {
        this.position = position;
        this.isGrounded = isGrounded;
        this.movement = movement;
        this.playerRotation = playerRotation;
        this.deathThisFrame = deathThisFrame;
        this.isVisible = isVisible;
        this.isJump = isJump;
    }
}

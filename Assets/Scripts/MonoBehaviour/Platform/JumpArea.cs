using UnityEngine;

public class JumpArea : MonoBehaviour
{
    public bool Jumped { get; private set; }

    internal void Start()
    {
        PenguinFSM.Instance.Jumped += OnJumped;
    }

    internal void OnDestory()
    {
        if (PenguinFSM.Instance != null)
        {
            PenguinFSM.Instance.Jumped -= OnJumped;
        }
    }

    private void OnJumped(JumpArea jumpArea)
    {
        if (jumpArea == this)
        {
            Jumped = true;
        }
    }
}
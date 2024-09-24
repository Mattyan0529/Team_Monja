using UnityEngine;

public class InvincibleManagementScript_KH : MonoBehaviour
{
    private bool _isInvincible = false;

    public bool IsInvincible
    {
        get { return _isInvincible; }
    }

    private void StartInvincible()
    {
        _isInvincible = true;
    }

    private void FinishInvincible()
    {
        _isInvincible = false;
    }
}

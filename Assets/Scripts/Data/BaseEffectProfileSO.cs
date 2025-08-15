using UnityEngine;

public abstract class BaseEffectProfileSO : ScriptableObject
{
    public abstract void ToggleExecuteAction(bool doExecute, Interactor interactor);
}

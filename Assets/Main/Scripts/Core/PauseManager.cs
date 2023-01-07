using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour, IPausable
{
    private List<IPausable> _pausablesList = new List<IPausable>();

    public bool IsPaused { get; private set; }

    public void Register(IPausable newPausable)
    {
        _pausablesList.Add(newPausable);
    }

    public void UnRegister(IPausable pausable)
    {
        _pausablesList.Remove(pausable);
    }

    public void SetPause(bool isPaused)
    {
        IsPaused = isPaused;

        foreach (var pausable in _pausablesList)
        {
            pausable.SetPause(IsPaused);
        }
    }
}

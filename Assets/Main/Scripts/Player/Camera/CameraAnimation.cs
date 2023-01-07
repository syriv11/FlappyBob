using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    [Header("Animation default values")]
    [SerializeField] private AnimationCurve _rewindAnimationCurve;
    [SerializeField, Min(0)] private float _rewindAnimationDuration;

    private GameProcess _gameProcess;
    private CameraMovement _cameraMovement;

    public bool IsAnimationRunning { get; private set; }

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    public void StartRewindAnimation()
    {
        StartCoroutine(RewindAnimation());
    }

    private IEnumerator RewindAnimation()
    {
        Debug.Log("Rewind!");

        IsAnimationRunning = true;

        Vector3 startPos = transform.position;
        Vector3 endPos = _cameraMovement.GetCameraPositionNextToPlayer();

        float elapsedTime = 0;
        float lerp = 0;

        while (lerp <= 1)
        {
            transform.position = Vector3.LerpUnclamped(startPos, endPos, _rewindAnimationCurve.Evaluate(lerp));

            elapsedTime += Time.unscaledDeltaTime;
            lerp = elapsedTime / _rewindAnimationDuration;

            yield return null;
        }

        transform.position = endPos;

        IsAnimationRunning = false;
    }

    public void SetCameraMovement(CameraMovement cameraMovement) => _cameraMovement = cameraMovement;

    private void Init()
    {
        // Init variables
        _gameProcess = ProjectContext.Instance.GameProcess;
        IsAnimationRunning = false;

        // Init events
        _gameProcess.GameStateSwitched += OnGameStateSwitched;
    }

    private void OnGameStateSwitched(GameState newGameState)
    {
        if (newGameState is MainMenuState)
            StartRewindAnimation();
    }
}

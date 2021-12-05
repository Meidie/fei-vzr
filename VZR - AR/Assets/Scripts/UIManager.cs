using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public struct UxHandle
{
    public UIManager.InstructionUI InstructionalUI;
    public UIManager.InstructionGoals Goal;

    public UxHandle(UIManager.InstructionUI ui, UIManager.InstructionGoals goal)
    {
        InstructionalUI = ui;
        Goal = goal;
    }
}

public class UIManager : MonoBehaviour
{
    [SerializeField] private bool startWithInstructionalUI = true;

    public bool StartWithInstructionalUI
    {
         get => startWithInstructionalUI;
         set => startWithInstructionalUI = value;
    }

    public enum InstructionUI
    {
        CrossPlatformFindAPlane,
        TapToPlace,
        None
    };

    [SerializeField] private InstructionUI instructionalUI;

    public InstructionUI InstructionalUI
    {
        get => instructionalUI;
        set => instructionalUI = value;
    }

    public enum InstructionGoals
    {
        FoundAPlane,
        FoundMultiplePlanes,
        PlacedAnObject,
        None
    };

    [SerializeField] private InstructionGoals instructionalGoal;
    
    public InstructionGoals InstructionalGoal
    {
        get => instructionalGoal;
        set => instructionalGoal = value;
    }

    [SerializeField] private bool showSecondaryInstructionalUI;
    
    public bool ShowSecondaryInstructionalUI
    {
        get => showSecondaryInstructionalUI;
        set => showSecondaryInstructionalUI = value;
    }

    [SerializeField] private InstructionUI secondaryInstructionUI = InstructionUI.TapToPlace;

    public InstructionUI SecondaryInstructionUI
    {
        get => secondaryInstructionUI;
        set => secondaryInstructionUI = value;
    }

    [SerializeField] private InstructionGoals secondaryGoal = InstructionGoals.PlacedAnObject;

    public InstructionGoals SecondaryGoal
    {
        get => secondaryGoal;
        set => secondaryGoal = value;
    }

    [SerializeField] private GameObject arSessionOrigin;

    public GameObject ARSessionOrigin
    {
        get => arSessionOrigin;
        set => arSessionOrigin = value;
    }

    private Func<bool> _goalReached;
    private bool _secondaryGoalReached;

    private Queue<UxHandle> _uxOrderedQueue;
    private UxHandle _currentHandle;
    private bool _processingInstructions;
    private bool _placedObject;

    [SerializeField] private ARPlaneManager planeManager;
    
    public ARPlaneManager PlaneManager
    {
        get => planeManager;
        set => planeManager = value;
    }

    [SerializeField] private ARUXAnimationManager animationManager;

    public ARUXAnimationManager AnimationManager
    {
        get => animationManager;
        set => animationManager = value;
    }

    private bool _fadedOff;

    private void OnEnable()
    {
        ARUXAnimationManager.ONFadeOffComplete += FadeComplete;
        PlaceObjectsOnPlane.ONPlacedObject += () => _placedObject = true;

        GetManagers();
        _uxOrderedQueue = new Queue<UxHandle>();

        if (startWithInstructionalUI)
        {
            _uxOrderedQueue.Enqueue(new UxHandle(instructionalUI, instructionalGoal));
        }

        if (showSecondaryInstructionalUI)
        {
            _uxOrderedQueue.Enqueue(new UxHandle(secondaryInstructionUI, secondaryGoal));
        }
    }

    private void OnDisable()
    {
        ARUXAnimationManager.ONFadeOffComplete -= FadeComplete;
    }

    private void Update()
    {
        if (_uxOrderedQueue.Count > 0 && !_processingInstructions)
        {
            // pop off
            _currentHandle = _uxOrderedQueue.Dequeue();
            
            // exit instantly, if the goal is already met it will skip showing the first UI and move to the next in the queue 
            _goalReached = GetGoal(_currentHandle.Goal);
            if (_goalReached.Invoke())
            {
                return;
            }

            // fade on
            FadeOnInstructionalUI(_currentHandle.InstructionalUI);
            _processingInstructions = true;
            _fadedOff = false;
        }

        if (_processingInstructions)
        {
            // start listening for goal reached
            if (_goalReached.Invoke())
            {
                // if goal reached, fade off
                if (!_fadedOff)
                {
                    _fadedOff = true;
                    animationManager.FadeOffCurrentUI();
                }
            }
        }
    }

    private void GetManagers()
    {
        if (arSessionOrigin && arSessionOrigin.TryGetComponent(out ARPlaneManager arPlaneManager))
        {
            planeManager = arPlaneManager;
        }
    }

    private Func<bool> GetGoal(InstructionGoals goal)
    {
        return goal switch
        {
            InstructionGoals.FoundAPlane => PlanesFound,
            InstructionGoals.FoundMultiplePlanes => MultiplePlanesFound,
            InstructionGoals.PlacedAnObject => PlacedObject,
            InstructionGoals.None => () => false,
            _ => () => false
        };
    }

    private void FadeOnInstructionalUI(InstructionUI ui)
    {
        switch (ui)
        {
            case InstructionUI.CrossPlatformFindAPlane:
                animationManager.ShowCrossPlatformFindAPlane();
                break;
            case InstructionUI.TapToPlace:
                animationManager.ShowTapToPlace();
                break;
            case InstructionUI.None:
                break;
        }
    }

    private bool PlanesFound() => planeManager && planeManager.trackables.count > 0;

    private bool MultiplePlanesFound() => planeManager && planeManager.trackables.count > 1;

    private void FadeComplete()
    {
        _processingInstructions = false;
    }

    private bool PlacedObject()
    {
        // reset flag to be used multiple times
        if (_placedObject)
        {
            _placedObject = false;
            return true;
        }
        return _placedObject;
    }
}


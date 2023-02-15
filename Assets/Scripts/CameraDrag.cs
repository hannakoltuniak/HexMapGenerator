using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.Events;

public class CameraDrag : InitializableBehaviour
{
    private const float CAMERA_EFFECTIVE_BOUNDS_ADDITIONAL_BORDER_FACTOR = 1.2f;
    
    bool isDragging = false;
    private float _cameraWorldSpaceHeight;
    private float _cameraWorldSpaceWidth;
    private Vector2 _initialMouseClickPosition;
    private Vector2 _mouseCurrentPosition;

    private Vector2 _lastCameraEffectiveBoundsPositionThatCausedCameraDragEvent;
    private Rect _currentCameraEffectiveBounds;
    public Rect CurrentCameraEffectiveBounds
    {
        get => _currentCameraEffectiveBounds;
        private set
        {
            _currentCameraEffectiveBounds = value;
        }
    }

    [HideInInspector]
    public UnityEvent OnCameraDrag = new UnityEvent();
    
    public void AttachNewCameraDragListener(UnityAction listener)
    {
        OnCameraDrag.AddListener(listener);
        OnCameraDrag.Invoke();
    }

    public override void Init()
    {
        SetInitialCameraWorldSpaceSizes();
        UpdateCameraEffectiveBounds();
        SetInitialCameraEffectiveBoundsPositionForEvent();
    }

    private void SetInitialCameraWorldSpaceSizes()
    {
        _cameraWorldSpaceHeight = 2f * Camera.main.orthographicSize;
        _cameraWorldSpaceWidth = _cameraWorldSpaceHeight * Camera.main.aspect;
    }

    private void SetInitialCameraEffectiveBoundsPositionForEvent()
    {
        _lastCameraEffectiveBoundsPositionThatCausedCameraDragEvent = _currentCameraEffectiveBounds.position;
    }

    private void LateUpdate()
    {
        if (GuiStateInfo.IsWindowVisible)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse1) && !isDragging)
            StartDragging();
        else if (isDragging)
            DragAccordingToMouseTravelOffset();
        else if (Input.GetKeyUp(KeyCode.Mouse1))
            FireCameraDragEventWhenCameraMovedSignificantly();

        StopDraggingWhenMouseButtonIsReleased();
    }

    private void UpdateCameraEffectiveBounds()
    {
        _currentCameraEffectiveBounds = new Rect(Camera.main.transform.position - new Vector3(_cameraWorldSpaceWidth / 2f * CAMERA_EFFECTIVE_BOUNDS_ADDITIONAL_BORDER_FACTOR,
                                                                                              _cameraWorldSpaceHeight / 2f * CAMERA_EFFECTIVE_BOUNDS_ADDITIONAL_BORDER_FACTOR),
            new Vector2(_cameraWorldSpaceWidth * CAMERA_EFFECTIVE_BOUNDS_ADDITIONAL_BORDER_FACTOR,
            _cameraWorldSpaceHeight * CAMERA_EFFECTIVE_BOUNDS_ADDITIONAL_BORDER_FACTOR));
    }

    private void StartDragging()
    {
        _initialMouseClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    private void DragAccordingToMouseTravelOffset()
    {
        UpdateCameraEffectiveBounds();
        FireCameraDragEventWhenCameraMovedSignificantly();

        UpdateMousePosition();
        MoveCameraTranformBasedOnMouseTravelledDistance();
    }

    private void UpdateMousePosition()
    {
        _mouseCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void MoveCameraTranformBasedOnMouseTravelledDistance()
    {
        Vector2 distance = _mouseCurrentPosition - _initialMouseClickPosition;
        transform.position += new Vector3(-distance.x, -distance.y, 0);
    }

    private void FireCameraDragEventWhenCameraMovedSignificantly()
    {
        if (MathF.Abs(Vector2.Distance(_lastCameraEffectiveBoundsPositionThatCausedCameraDragEvent, _currentCameraEffectiveBounds.position)) > 0.2f)
        {
            OnCameraDrag.Invoke();
            _lastCameraEffectiveBoundsPositionThatCausedCameraDragEvent = _currentCameraEffectiveBounds.position;
        }
    }


    private void StopDraggingWhenMouseButtonIsReleased()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
            isDragging = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HPBar : MonoBehaviour
{
    [field: SerializeField] public int MaxValue { get; private set; }
    [field: SerializeField] public int Value { get; private set; }

    [field: SerializeField] private RectTransform _topBar;
    [field: SerializeField] private RectTransform _bottomBar;

    [field: SerializeField] private float _animationSpeed = 10f;
    
    private float fullWidth;
    private float TargetWidth => Value * fullWidth / MaxValue;

    private Coroutine _adjustBarWidthCoroutine;
    
    private void Start()
    {
        fullWidth = _topBar.rect.width;
    }
    
    public void Change(int amount)
    {
        Value = Mathf.Clamp(Value + amount, 0, MaxValue);
        if (_adjustBarWidthCoroutine != null)
        {
            StopCoroutine(_adjustBarWidthCoroutine);
        }

        _adjustBarWidthCoroutine = StartCoroutine(AdjustBarWidth(amount));
    }

    private IEnumerator AdjustBarWidth(int amount)
    {
        var suddenChangeBar = amount >= 0 ? _bottomBar : _topBar;
        var slowChangeBar = amount >= 0 ? _topBar : _bottomBar;
        suddenChangeBar.SetWidth(TargetWidth);
        while (Mathf.Abs(suddenChangeBar.rect.width - slowChangeBar.rect.width) > 1f)
        {
            slowChangeBar.SetWidth(Mathf.Lerp(slowChangeBar.rect.width, TargetWidth, Time.deltaTime * _animationSpeed));
            yield return null;
        }

        slowChangeBar.SetWidth(TargetWidth);
    }
//FOR TESTING!!!!
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Change(20);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Change(-20);
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class HealthChangedEvent : UnityEvent<float, float> { } //TODO add a player identifier to this!

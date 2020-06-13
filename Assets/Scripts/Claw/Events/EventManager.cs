using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void EventListener(GameEvent gameEvent);

    public delegate void EventListener<T>(T gameEvent) where T : GameEvent;

    private static readonly Dictionary<Type, EventListener> Listeners = new Dictionary<Type, EventListener>();

    private static readonly Dictionary<Type, List<Delegate>> OneShotListeners = new Dictionary<Type, List<Delegate>>();
    
    private static readonly Dictionary<Delegate, EventListener> ListenerLookup = new Dictionary<Delegate, EventListener>();
    
    private static readonly Queue<GameEvent> Events = new Queue<GameEvent>();

    private static EventManager _instance;

    private static bool _quitting;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    
    private void Update() {
        while (Events.Count > 0) {
            TriggerEvent(Events.Dequeue());
        }
    }

    private void OnDestroy() {
        Listeners.Clear();
        ListenerLookup.Clear();
    }

    private void OnApplicationQuit() {
        _quitting = true;
    }

    public static void AddListener<T>(EventListener<T> listener) where T : GameEvent {
        TryCreateInstance();

        if (ListenerLookup.ContainsKey(listener)) {
            return;
        }

        EventListener genericListener = (e) => listener((T) e);
        ListenerLookup[listener] = genericListener;

        if (Listeners.ContainsKey(typeof(T))) {
            Listeners[typeof(T)] += genericListener;
        }
        else {
            Listeners[typeof(T)] = genericListener;
        }
    }

    public static void AddListenerOneShot<T>(EventListener<T> listener) where T : GameEvent {
        AddListener(listener);
        
        List<Delegate> oneShotListeners;
        if (!OneShotListeners.TryGetValue(typeof(T), out oneShotListeners)) {
            oneShotListeners = new List<Delegate>();
            OneShotListeners.Add(typeof(T), oneShotListeners);
        }
        
        oneShotListeners.Add(listener);
    }

    public static void RemoveListener<T>(EventListener<T> listener) where T : GameEvent {
        TryCreateInstance();

        RemoveListener(typeof(T), listener);
    }

    private static void RemoveListener(Type eventType, Delegate listener) {
        
        EventListener internalListener;
        if (ListenerLookup.TryGetValue(listener, out internalListener)) {
            EventListener tempListener;
            if (Listeners.TryGetValue(eventType, out tempListener)) {
                tempListener -= internalListener;
                if (tempListener == null) {
                    Listeners.Remove(eventType);
                }
                else {
                    Listeners[eventType] = tempListener;
                }
            }

            ListenerLookup.Remove(listener);
        }
    }

    /// <summary>
    /// Queues an event, to be dispatched during the next update.
    /// </summary>
    public static bool QueueEvent(GameEvent gameEvent) {
        TryCreateInstance();

        if (!Listeners.ContainsKey(gameEvent.GetType())) {
            return false;    // No listeners to handle event.
        }

        Events.Enqueue(gameEvent);
        return true;
    }

    /// <summary>
    /// Fires an event instantly.
    /// </summary>
    public static void TriggerEvent(GameEvent gameEvent) {
        TryCreateInstance();

        Type eventType = gameEvent.GetType();
        
        EventListener listener;
        if (Listeners.TryGetValue(eventType, out listener)) {
            listener.Invoke(gameEvent);
        }
        else {
            Debug.LogWarning("Event: " + eventType + " has no listeners");
        }

        if (OneShotListeners.TryGetValue(eventType, out List<Delegate> oneShotListeners)) {
            foreach (Delegate oneShotListener in oneShotListeners) {
                RemoveListener(eventType, oneShotListener);
            }
            oneShotListeners.Clear();
        }
    }

    private static void TryCreateInstance() {
        if (!_quitting && _instance == null) {
            _instance = (new GameObject("Runner_EventManager")).AddComponent<EventManager>();
        }
    }
}


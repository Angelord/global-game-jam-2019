using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void EventListener(GameEvent gameEvent);
    public delegate void EventListener<T>(T gameEvent) where T : GameEvent;

    private static Dictionary<System.Type, EventListener> listeners = new Dictionary<Type, EventListener>();
    private static Dictionary<System.Delegate, EventListener> listenerLookup = new Dictionary<Delegate, EventListener>();

    private static Queue<GameEvent> eventQueue = new Queue<GameEvent>();

    private static EventManager instance;

    private static void TryCreateInstance() {
        if(instance == null) {
            instance = (new GameObject("EventManagerRunner")).AddComponent<EventManager>();
        }
    }

    public static void AddListener<T>(EventListener<T> listener) where T : GameEvent {
        TryCreateInstance();

        AddDelegate<T>(listener);
    }

    private static void AddDelegate<T>(EventListener<T> listener) where T : GameEvent {

        if (listenerLookup.ContainsKey(listener)) {
            return;
        }

        EventListener genericListener = (e) => listener((T)e);
        listenerLookup[listener] = genericListener;

        if (listeners.ContainsKey(typeof(T))) {
            listeners[typeof(T)] += genericListener;
        }
        else {
            listeners[typeof(T)] = genericListener;
        }
    }

    public static void RemoveListener<T>(EventListener<T> listener) where T : GameEvent {
        TryCreateInstance();

        EventListener internalListener;
        if (listenerLookup.TryGetValue(listener, out internalListener)) {
            EventListener tempListener;
            if (listeners.TryGetValue(typeof(T), out tempListener)) {
                tempListener -= internalListener;
                if (tempListener == null) {
                    listeners.Remove(typeof(T));
                }
                else {
                    listeners[typeof(T)] = tempListener;
                }
            }

            listenerLookup.Remove(listener);
        }
    }

    public static bool HasListener<T>(EventListener<T> listener) where T : GameEvent {
        TryCreateInstance();
        return listenerLookup.ContainsKey(listener);
    }

    public static void Clear() {
        TryCreateInstance();
        listeners.Clear();
        listenerLookup.Clear();
    }

    public static bool QueueEvent(GameEvent gameEvent) {
        TryCreateInstance();

        if (!listeners.ContainsKey(gameEvent.GetType())) {
            Debug.LogWarning("QueueEvent failed due to no listeners for event: " + gameEvent.GetType());
            return false;
        }

        eventQueue.Enqueue(gameEvent);
        return true;
    }

    public static void TriggerEvent(GameEvent gameEvent) {
        TryCreateInstance();

        EventListener listener;
        if (listeners.TryGetValue(gameEvent.GetType(), out listener)) {
            listener.Invoke(gameEvent);
        }
        else {
            Debug.LogWarning("Event: " + gameEvent.GetType() + " has no listeners");
        }
    }

    private void Update() {
        while (eventQueue.Count > 0) {
            TriggerEvent(eventQueue.Dequeue());
        }
    }

    private void OnDestroy() {
        Clear();
    }

}


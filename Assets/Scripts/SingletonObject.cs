using UnityEngine;
using System.Collections.Generic;

public class SingletonObject : MonoBehaviour {

    private static readonly Dictionary<string, SingletonObject> instances = new Dictionary<string, SingletonObject>();
    
    public string Identifier;
    
    private void Awake() {
        if (instances.ContainsKey(Identifier)) {
            Destroy(this.gameObject);
            return;
        }

        instances.Add(Identifier, this);
        DontDestroyOnLoad(gameObject);
    }
}

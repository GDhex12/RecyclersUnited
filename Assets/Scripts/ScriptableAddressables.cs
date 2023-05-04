using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ScriptableAddressables<T> : ScriptableObject where T : ScriptableAddressables<T>
{
    private static T Instance;
    public static T i
    {
        get
        {
            if (Instance == null)
            {
                var operation = Addressables.LoadAssetAsync<T>(typeof(T).Name);
                Instance = operation.WaitForCompletion();
            }
            return Instance;
        }
    }
}

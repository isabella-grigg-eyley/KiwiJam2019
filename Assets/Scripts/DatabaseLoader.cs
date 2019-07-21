using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseLoader : MonoBehaviour
{
    public static CarriageImageDatabase Database = null;

    [RuntimeInitializeOnLoadMethod]
    private static void LoadAssets()
    {
        Database = CarriageImageDatabase.Instance;
        Debug.Assert(Database != null, "Database could not load");
        Debug.Log("LOADING CarriageImageDatabase: " + Database);
    }
}

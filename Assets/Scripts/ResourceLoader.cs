using UnityEngine;
using System.IO;

public class ResourceLoader
{
    private const string EnemiesPath = "Enemies";

    public GameObject GetEnemy(string name) {
        string fullPath = Path.Combine(EnemiesPath, name);
        return Resources.Load<GameObject>(fullPath);
    }
}
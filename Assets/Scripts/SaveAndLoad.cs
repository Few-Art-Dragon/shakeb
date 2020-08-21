using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveAndLoad : MonoBehaviour
{
    private Save sv = new Save();
    public string path;

    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "Save.json");
    }
    public void ResetSave()
    {
        path = Path.Combine(Application.dataPath, "Save.json");
        File.Delete(path);
    }

    public void Load()
    {
        path = Path.Combine(Application.persistentDataPath, "Save.json");
        if (File.Exists(path))
        {
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
            
            //listTiles.paints = sv.score;
        }
    }
    public void Save(int score)
    {
        //ResetSave();
        if (File.Exists(path))
        {
            sv = JsonUtility.FromJson<Save>(File.ReadAllText(path));
            int scoreCheck;

            scoreCheck = sv.score;
            if (scoreCheck < score)
            {
                sv.score = score;
                File.WriteAllText(path, JsonUtility.ToJson(sv));
            }
        }
        else
        {
            sv.score = score;
            File.WriteAllText(path, JsonUtility.ToJson(sv));
        }
        
    }
}

[Serializable]
public class Save
{
    public int score;
}

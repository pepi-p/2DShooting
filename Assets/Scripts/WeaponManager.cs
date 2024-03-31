using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WeaponManager : MonoBehaviour
{
    public WeaponSetting[] weapons = new WeaponSetting[4];

    enum bulletType
    {
        none,
        normal,
        diffusion,
        homing
    }

    void Start()
    {
        Load();
    }

    private void Load()
    {
        for(int i = 0; i < weapons.Length; i++)
        {
            string filePath = Application.dataPath + "/SaveData/Bullet" + i.ToString();
            if(File.Exists(filePath))
            {
                StreamReader streamReader = new StreamReader(filePath);
                string json = streamReader.ReadToEnd();
                streamReader.Close();
                weapons[i] = JsonUtility.FromJson<WeaponSetting>(json);
            }
        }
    }

    public void Save()
    {
        for(int i = 0; i < weapons.Length; i++)
        {
            string filePath = Application.dataPath + "/SaveData/Bullet" + i.ToString();
            string json = JsonUtility.ToJson(weapons[i]);
            StreamWriter streamWriter = new StreamWriter(filePath);
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }
    }
}

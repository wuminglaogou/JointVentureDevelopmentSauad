using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : PersistentSingleton<SaveSystem>
{
    public Data data=new Data();
    public void SavePos(Vector3 pos)
    {
        Debug.Log(pos.x + " " + pos.y + " " + pos.z);
        
        data.vector3Data = new SerializeVector3(pos);
    }
    public void LoadPos()
    {
        if(data.vector3Data!=null)
        {
            Debug.Log("enter");
            CharacterManager.Instance.characters[0].gameObject.SetActive(true);
            CharacterManager.Instance.characters[0].gameObject.GetComponent<PlayAnimation>().Save();
            CharacterManager.Instance.characters[0].gameObject.transform.position = data.vector3Data.ToVector3();
        }
             
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : PersistentSingleton<SaveSystem>
{
    public Data data=new Data();
    public AudioData saveSound;
    public SaveAnim currentAnim;
    public SaveAnim possbleToSet;
    public void SavePos(Vector3 pos)
    {
        Debug.Log(pos.x + " " + pos.y + " " + pos.z);
        AudioManager.Instance.PlayAudio(saveSound);
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
    public void SaveAnimation()
    {
        currentAnim.BeginToSave();
    }
    public void SetCurrentAnim( )
    {
        currentAnim = possbleToSet;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : PersistentSingleton<SaveSystem>
{
    public Data data=new Data();
    public AudioData saveSound;
    public SaveAnim currentAnim;
    public SaveAnim animPossbleToSet;
    public Camera currentCamera;
    public Camera cameraPossbleToSet;
    public void SavePos(Vector3 pos)
    {
        AudioManager.Instance.PlayAudio(saveSound);
        data.vector3Data = new SerializeVector3(pos);
    }
    public void SaveCamera( )
    {
        currentCamera = cameraPossbleToSet;
    }
    public void LoadPos()
    {
        if(data.vector3Data!=null)
        {
            Debug.Log("enter");
            CharacterManager.Instance.ClearToOnePlayer();
            LoadCamera();
            CharacterManager.Instance.characters[0].gameObject.SetActive(true);
            CharacterManager.Instance.characters[0].gameObject.GetComponent<PlayAnimation>().Save();
            CharacterManager.Instance.characters[0].gameObject.transform.position = data.vector3Data.ToVector3();
        }            
    }
    public void LoadCamera()
    {
        GameObject previousCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if(previousCamera!=currentCamera.gameObject)
        {
            Debug.Log("enter");
            currentCamera.gameObject.SetActive(true);
            previousCamera.SetActive(false);
            previousCamera.tag = "Untagged";
            currentCamera.gameObject.tag = "MainCamera";
        }
        
    }
    public void SaveAnimation()
    {
        currentAnim.BeginToSave();
    }
    public void SetCurrentAnim( )
    {
        currentAnim = animPossbleToSet;
    }
}

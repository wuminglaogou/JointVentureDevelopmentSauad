using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : PersistentSingleton<CharacterManager>
{
    public AudioData splitSFX;
    public GameObject characterPrefab;
    public List<PlayerController> characters;
    public int activeCharacterIndex = 0;
    public float buffetTime = 0.5f;
    public float vectorForce = 10f;
    public Vector2 speed;
    public void ClearToOnePlayer()
    {
        if(characters.Count > 0)
        {
            for(int i = characters.Count - 1; i >= 0; i--)
            {
                if (characters[i].enabled==false)
                {
                    Destroy(characters[i].gameObject);
                    characters.RemoveAt(i);
                }
            }
            activeCharacterIndex = 0;
            characters[0].enabled = true;
        }
    }
    public void Split(Vector3 postion,Vector2 dir)
    {
        if(characters.Count==3)
        {
            return;
        }
        AudioManager.Instance.PlayAudio(splitSFX);
        characters[activeCharacterIndex].enabled = false;
        // 创建新角色实例
        Vector3 newPos=new Vector3(postion.x,postion.y+1,postion.z);
        GameObject newCharacterObject = Instantiate(characterPrefab, newPos, Quaternion.identity);
        speed = dir * vectorForce;
        PlayerController newCharacter = newCharacterObject.GetComponent<PlayerController>();
        newCharacter.enabled = false;
        // 将新角色添加到角色列表
        Rigidbody2D rb = newCharacterObject.GetComponent<Rigidbody2D>();
        rb.velocity = speed;
        Debug.Log(rb.velocity);
        characters.Add(newCharacter);

        // 切换到新角色
        SwitchToNewCharacter();
    }

    public void SwitchToNewCharacter()
    {      
        activeCharacterIndex = characters.Count-1;
        ActivateActivateCharacterWithIndex();      
    }
    public void SwitchToNextCharacter()
    {
        if (characters.Count > 1)
        {
            characters[activeCharacterIndex].enabled = false;
            activeCharacterIndex = (activeCharacterIndex + 1) % characters.Count; // 循环切换
            ActivateActivateCharacterWithIndex();
        }
    }
    public void DeleteCurrentCharacter()
    {
        if (characters.Count==1)
        {
            return;
        }
        characters[activeCharacterIndex].gameObject.SetActive(false);
        characters.RemoveAt(activeCharacterIndex);
        activeCharacterIndex = 0;
        characters[activeCharacterIndex].enabled = true;
    }
    public void CharacterDie(PlayerController self)
    {
        PlayerController toRemove = null;
        foreach (var character in characters)
        {
            if(character==self)
            {
                toRemove = character;
                break;
            }                    
        }
        if (toRemove!=null)
        {
            if(characters.Count==1)
            {
                toRemove.gameObject.SetActive(false);
                GamePlayUIManager.Instance.OpenDieUi();
                //触发是否回到存档,UI以及禁用输入等等
            }
            else
            {
                characters.Remove(toRemove);
                Destroy(self.gameObject);
                bool isActive = false;
                //foreach (var character in characters)
                //{
                //    if(character.enabled==true)
                //    {
                //        isActive = true;
                //    }
                //}
                for(int i=0; i<characters.Count; i++)
                {
                    if (characters[i].enabled==true)
                    {
                        isActive = true;
                        activeCharacterIndex = i;
                    }
                }
                if(isActive==false)
                {
                    activeCharacterIndex = 0;
                    characters[activeCharacterIndex].enabled = true;
                }          
            }
        }

    }
    public void ActivateActivateCharacterWithIndex()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (i == activeCharacterIndex)
            {
                characters[i].enabled = true;

                //Camera.main.GetComponent<CameraFollow>().Target = characters[i].transform; // 跟随当前角色
            }
        }
    }
    //private void ActivateCurrentCharacterWithBuffet()
    //{
    //    for (int i = 0; i < characters.Count; i++)
    //    {            
    //        if (i == activeCharacterIndex)
    //        {
    //            StartCoroutine(buffetTimeCoroutine(i));
                
    //            //Camera.main.GetComponent<CameraFollow>().Target = characters[i].transform; // 跟随当前角色
    //        }
    //    }
    //}
    //IEnumerator buffetTimeCoroutine(int characterIndex)
    //{
    //    yield return new WaitForSeconds(buffetTime);
    //    characters[characterIndex].enabled = true;
    //}
}

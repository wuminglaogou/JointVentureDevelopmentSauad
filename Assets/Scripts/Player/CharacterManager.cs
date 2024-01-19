using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public GameObject characterPrefab;
    public List<PlayerController> characters;
    public int activeCharacterIndex = 0;
    public float buffetTime = 0.5f;
    public float vectorForce = 10f;
    public Vector2 speed;
    public void Split(Vector3 postion,Vector2 dir)
    {
        if(characters.Count==3)
        {
            return;
        }
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
        SwitchToNextCharacter();
    }

    public void SwitchToNewCharacterWithBuffet()
    {      
        activeCharacterIndex = characters.Count-1;
        ActivateCurrentCharacterWithBuffet();      
    }
    public void SwitchToNextCharacter()
    {
        if (characters.Count > 1)
        {
            activeCharacterIndex = (activeCharacterIndex + 1) % characters.Count; // 循环切换
            ActivateActivateCurrentCharacter();
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
    public void ActivateActivateCurrentCharacter()
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
    private void ActivateCurrentCharacterWithBuffet()
    {
        for (int i = 0; i < characters.Count; i++)
        {            
            if (i == activeCharacterIndex)
            {
                StartCoroutine(buffetTimeCoroutine(i));
                
                //Camera.main.GetComponent<CameraFollow>().Target = characters[i].transform; // 跟随当前角色
            }
        }
    }
    IEnumerator buffetTimeCoroutine(int characterIndex)
    {
        yield return new WaitForSeconds(buffetTime);
        characters[characterIndex].enabled = true;
    }
}

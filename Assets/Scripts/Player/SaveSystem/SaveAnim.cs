using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAnim : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void BeginToSave()
    {
        anim.SetBool("isSaving", true);
    }
    public void EndToSave()
    {
        anim.SetBool("isSaving", false);
    }
    public void Load()
    {
        anim.SetBool("isLoading", true);
    }
    public void OutLoading()
    {
        anim.SetBool("isLoading", false);
    }
}

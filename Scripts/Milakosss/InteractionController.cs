using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

[System.Serializable] public class InteractionModeDictionary: SerializableDictionaryBase<string, GameObject>{}

public class InteractionController : Singelton<InteractionController>
{
    [SerializeField] InteractionModeDictionary interactionModes;
    [SerializeField] string modeName;
    GameObject currentMode;

    protected override void Awake()
    {
        base.Awake();
        ResetAllModes();
    }
    private void Start() 
    {
        _EnableMode(modeName);   
    }


    void ResetAllModes()
    {
        foreach (GameObject mode in interactionModes.Values)
            mode.SetActive(false);
    }
    public static void EnableMode(string name)
    {
        Instance?._EnableMode(name);
    }
    public void _EnableMode(string name)
    {
        GameObject modeObject;
        if (interactionModes.TryGetValue(name, out modeObject))
        {
            StartCoroutine(ChangeMode(modeObject));
        }
        else
        {
            Debug.LogError("undefined mode named " +name);
        }
    }

    IEnumerator ChangeMode(GameObject mode)
    {
        if (mode == currentMode)
        yield break;

        if (currentMode)
        {
            currentMode.SetActive(false);
            yield return null;
        }

        currentMode = mode;
        mode.SetActive(true);
    }
}


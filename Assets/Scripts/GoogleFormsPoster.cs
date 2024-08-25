using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public static class GoogleFormsPoster
{
    public static void Post(string[] answers, string[] formIds, string URL)
    {
        StaticCoroutine.Start(PostCoroutine(answers,formIds,URL));
    }
    static IEnumerator PostCoroutine(string[] answers, string[] formIds, string URL)
    {
        if(answers.Length != formIds.Length)
        {
            Debug.Log("Different number of answers and form ids!");
            yield break;
        }
        WWWForm form = new WWWForm();
        for(int i = 0; i < formIds.Length; i++)
        {
            Debug.Log(formIds[i] + " "  + answers[i]);
            form.AddField(formIds[i], answers[i]);
        }

        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();
    }
}

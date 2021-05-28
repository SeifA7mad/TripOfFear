using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSequense : MonoBehaviour
{

  public DialogInGame dialog;

   public IEnumerator dialogSequence(string txt1, string txt2, string txt3) {
       yield return new WaitUntil(() => dialog.finished);
       dialog.write(txt1);
       yield return new WaitUntil(() => dialog.finished);
       dialog.write(txt2);
       yield return new WaitUntil(() => dialog.finished);
       dialog.write(txt3);
       yield return new WaitUntil(() => dialog.finished);
   }

   public IEnumerator dialogSequence(string txt1, string txt2) {
       yield return new WaitUntil(() => dialog.finished);
       dialog.write(txt1);
       yield return new WaitUntil(() => dialog.finished);
       dialog.write(txt2);
       yield return new WaitUntil(() => dialog.finished);
   }

   public IEnumerator dialogSequence(string txt1) {
       yield return new WaitUntil(() => dialog.finished);
       dialog.write(txt1);
       yield return new WaitUntil(() => dialog.finished);
    }
}

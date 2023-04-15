using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
   private void OnCollisionEnter(Collision collision)
   {
        if(collision.collider.tag == "Can")
        {
            SoundManager.instance.PlayFx(FxTypes.CANHITFX);
        }
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class EnviroFunctions : Singleton<EnviroFunctions>
{
    public void MailPop() {
        Instantiate(Globals.Instance.mailPopup,Globals.instance.computerCanvas.transform);
    }
}

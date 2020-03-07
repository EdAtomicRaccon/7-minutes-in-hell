using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EnviroFunctions : Singleton<EnviroFunctions>
{
    public void MailPop() {
        Instantiate(Globals.Instance.mailPopup,Globals.instance.computerCanvas.transform);
    }
}

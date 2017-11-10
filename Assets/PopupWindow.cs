using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupWindow 
{
    public Rect window;
    public string name;

    public PopupWindow(Rect _window,string myName)
    {
        window = _window;
        name = myName;
    }

}

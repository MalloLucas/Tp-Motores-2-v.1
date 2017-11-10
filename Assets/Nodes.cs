using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Nodes
{
    
    public Rect window;
    public string name = "window";
    public List<Nodes> connections;
    

    public Nodes(string name, int posX = 100, int posY = 100, int width = 200, int height = 50)
    {
        this.name = name;
        window = new Rect(posX, posY, width, height);
        connections = new List<Nodes>();
    }
}
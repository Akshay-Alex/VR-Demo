using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    public int numberOfObjectsOnTray = 0;
    public int trayMaximumCapacity = 3;
    public void ObjectAddedToTray()
    {
        numberOfObjectsOnTray++;
    }
}

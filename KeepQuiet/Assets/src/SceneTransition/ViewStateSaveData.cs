using System;
using System.Collections.Generic;

[Serializable]
public class ViewStateSaveData
{
    public string LocationName;
    public List<string> CluesToHide;

    public ViewStateSaveData(
        string locationName, 
        List<string> cluesToHide)
    {
        LocationName = locationName;
        CluesToHide = cluesToHide;
    }
}


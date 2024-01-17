using System;
using System.Collections.Generic;

[Serializable]
public class ViewStateSaveData
{
    public string LocationName;
    public List<string> CluesToHide;
    // If player added something here, put it here
    public List<string> CluesToAdd;

    public ViewStateSaveData(
        string locationName, 
        List<string> cluesToHide, List<string> cluesToAdd)
    {
        LocationName = locationName;
        CluesToHide = cluesToHide;
        CluesToAdd = cluesToAdd;
    }
}


using System;
using System.Collections.Generic;

[Serializable]
public class DialogData
{
    public string id;
    public string text;
    public List<DialogOptionData> options;
    public string nextDialogId;
}

[Serializable]
public class DialogOptionData
{
    public string optionText;
    public string nextDialogId;
}

[Serializable]
public class DialogTree
{
    public List<DialogData> dialogs;
}
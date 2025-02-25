using UnityEngine;

public class CommunicationData
{
    public string Type;
    public string Data;
}

//Type{
//Transform
//velocity
//Event
//}

public class TransformData
{
    private string objectID;
    Transform transform;

    public TransformData(string objectID, Transform transform)
    {
        this.objectID = objectID;
        this.transform = transform;
    }
}


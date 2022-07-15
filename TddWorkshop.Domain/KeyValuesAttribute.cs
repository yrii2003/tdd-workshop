namespace TddWorkshop.Domain;

public class KeyValuesAttribute : Attribute
{
    public object[] Objects { get; }

    public KeyValuesAttribute(params object[] objects)
    {
        Objects = objects;
    }
}
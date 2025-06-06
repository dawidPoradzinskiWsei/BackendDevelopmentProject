namespace ApplicationCore.Commons.Interfaces;

public interface IIdentity<K> : IComparable<K> where K: IComparable<K>
{
    public K Id
    {
        get;
        set;
    }

    int IComparable<K>.CompareTo(K? other)
    {
        return CompareTo(other);
    }
}
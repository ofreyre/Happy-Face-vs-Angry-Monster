
public class Geometry : Destructible
{
    public bool m_preserve = true;
    public int prefabsIndex;
    public int prefabIndex;
    public GHOST_TYPE m_ghostType;

    public virtual void Activate()
    {
        enabled = true;
    }
}

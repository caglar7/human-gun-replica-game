
/// <summary>
///  collectable handles Move() method differently
///  some collectables jump down after shooting target destroyed, some move towards player
/// </summary>

public interface ICollectableMovable
{
    void Move();
}

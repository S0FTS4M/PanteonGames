using System;
using UnityEngine;

public class MoveableUnitBase : UnitBase, ICloneable
{
    public static event Action<MoveableUnitBase> OnMoveableUnitCreated;
    public event Action<MoveableUnitBase> OnUnitMove;
    public float X => Mathf.Lerp(CurrentTile.X, _nextTile.X, _movementPercentage);

    public float Y => Mathf.Lerp(CurrentTile.Y, _nextTile.Y, _movementPercentage);

    public Tile CurrentTile
    {
        get; protected set;
    }
    private Tile _destTile;  // If we aren't moving, then destTile = currTile
    private Tile _nextTile;  // The next tile in the pathfinding sequence
    private Path_AStar _pathAStar;
    private float _movementPercentage; // Goes from 0 to 1 as we move from currTile to destTile

    private const float Speed = 5f; // Tiles per second

    public MoveableUnitBase(UnitType unitType) : base(unitType)
    {
    }

    public void SetTile(Tile tile)
    {
        CurrentTile = _destTile = _nextTile = tile;

    }
    public object Clone()
    {
        MoveableUnitBase clone = (MoveableUnitBase)MemberwiseClone();
        OnMoveableUnitCreated?.Invoke(clone);
        return clone;
    }
    void Update_DoMovement(float deltaTime)
    {
        if (CurrentTile == _destTile)
        {
            CurrentTile.SetUnit(this);
            _pathAStar = null;
            return; // We're already were we want to be.
        }

        if (_nextTile == null || _nextTile == CurrentTile)
        {
            // Get the next tile from the pathfinder.
            if (_pathAStar == null || _pathAStar.Length() == 0)
            {
                // Generate a path to our destination
                CurrentTile.SetUnit(null);
                _pathAStar = new Path_AStar(CurrentTile.World, CurrentTile, _destTile); // This will calculate a path from curr to dest.
                if (_pathAStar.Length() == 0)
                {
                    Debug.LogError("Path_AStar returned no path to destination!");
                    _destTile = CurrentTile;
                    _pathAStar = null;
                    return;
                }
            }

            // Grab the next waypoint from the pathing system!
            _nextTile = _pathAStar.Dequeue();

        }

        float distToTravel = Mathf.Sqrt(
            Mathf.Pow(CurrentTile.X - _nextTile.X, 2) +
            Mathf.Pow(CurrentTile.Y - _nextTile.Y, 2)
        );
        // How much distance can be travel this Update?
        float distThisFrame = Speed * deltaTime;

        // How much is that in terms of percentage to our destination?
        float percThisFrame = distThisFrame / distToTravel;

        // Add that to overall percentage travelled.
        _movementPercentage += percThisFrame;

        if (_movementPercentage >= 1)
        {
            // We have reached our destination

            //       If there are no more tiles, then we have TRULY
            //       reached our destination.

            CurrentTile = _nextTile;
            _movementPercentage = 0;

        }
    }

    public void Update(float deltaTime)
    {
        Update_DoMovement(deltaTime);

        OnUnitMove?.Invoke(this);
    }

    public void SetDestination(Tile tile)
    {
        _destTile = tile;
    }

}

namespace Assets.Scripts.Interfaces
{
    interface IWorldInput
    {
        int MouseX { get; }
        int MouseY { get; }
        bool IsLeftClicked { get; }
        bool IsRightClicked { get; }



    }
}
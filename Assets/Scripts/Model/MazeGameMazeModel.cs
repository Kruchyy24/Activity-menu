namespace Model
{
    public class MazeGameMazeModel
    {
        public float MazeRotationX { get; set; }
        public float MazeRotationY { get; set; }
        public float MazeRotationZ { get; set; }
        public float MazeRotationMaxAngle = 30;
        public float MazeRotationSensitivity = 0.05f;
        public readonly float ConstantInvertingRotation = -5f;
    }
}
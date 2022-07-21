using UnityEngine;
using Zenject;
using Redbus.Interfaces;
using Model;
using Signals;
using UnityEngine.SceneManagement;

namespace Controller
{
    public class MazeGameController
    {
        private MazeGameMazeModel mazeGameMazeModel;
        private MazeGameBallModel mazeGameBallModel;
        private IEventBus eventBus;

        [Inject]
        private void Init(MazeGameMazeModel mazeGameMazeModel, MazeGameBallModel mazeGameBallModel, IEventBus eventBus)
        {
            this.mazeGameMazeModel = mazeGameMazeModel;
            this.mazeGameBallModel = mazeGameBallModel;
            this.eventBus = eventBus;
            
            Setup();
        }

        private void Setup()
        {
            PublishSignals();
            SubscribeSignals();
        }

        private void PublishSignals()
        {
            eventBus.Publish(new SetBallOnStartPosition());
        }

        private void SubscribeSignals()
        {
            eventBus.Subscribe<GetActualMazeRotationSignal>(SetActualMazeRotation);
            eventBus.Subscribe<ChangeMazeRotationSignal>(ChangeMazeRotation);
            eventBus.Subscribe<ActualVelocitySignal>(GetBallVelocity);
            eventBus.Subscribe<CheckpointSignal>(WhichCheckpoint);
        }

        private void SetActualMazeRotation(GetActualMazeRotationSignal getActualMazeRotationSignal)
        {
            var payload = getActualMazeRotationSignal.Payload;
            mazeGameMazeModel.MazeRotationX = payload.x;
            mazeGameMazeModel.MazeRotationY = payload.y;
            mazeGameMazeModel.MazeRotationZ = payload.z;
        }

        private void ChangeMazeRotation(ChangeMazeRotationSignal vectorDeltaMousePosition)
        {
            var payload = vectorDeltaMousePosition.Payload;

            if ((mazeGameMazeModel.MazeRotationX <= (360 - mazeGameMazeModel.MazeRotationMaxAngle) && mazeGameMazeModel.MazeRotationX >= mazeGameMazeModel.MazeRotationMaxAngle) ||
                (mazeGameMazeModel.MazeRotationY <= (360 - mazeGameMazeModel.MazeRotationMaxAngle) && mazeGameMazeModel.MazeRotationY >= mazeGameMazeModel.MazeRotationMaxAngle) ||
                (mazeGameMazeModel.MazeRotationZ <= (360 - mazeGameMazeModel.MazeRotationMaxAngle) && mazeGameMazeModel.MazeRotationZ >= mazeGameMazeModel.MazeRotationMaxAngle))
            {
                Debug.Log("Rotacja chce wyjsc poza zakres");
                mazeGameMazeModel.MazeRotationX = mazeGameMazeModel.ConstantInvertingRotation*(payload.x * mazeGameMazeModel.MazeRotationSensitivity);
                mazeGameMazeModel.MazeRotationY = mazeGameMazeModel.ConstantInvertingRotation*(payload.y * mazeGameMazeModel.MazeRotationSensitivity);
                mazeGameMazeModel.MazeRotationZ = mazeGameMazeModel.ConstantInvertingRotation*(-payload.z * mazeGameMazeModel.MazeRotationSensitivity);
                
                var newVector = new Vector3(mazeGameMazeModel.MazeRotationX, mazeGameMazeModel.MazeRotationY,
                    mazeGameMazeModel.MazeRotationZ);
                eventBus.Publish(new SetMazeRotationSignal(newVector));
            }
            else
            {
                mazeGameMazeModel.MazeRotationX = payload.x * mazeGameMazeModel.MazeRotationSensitivity;
                mazeGameMazeModel.MazeRotationY = payload.y * mazeGameMazeModel.MazeRotationSensitivity;
                mazeGameMazeModel.MazeRotationZ = -payload.z * mazeGameMazeModel.MazeRotationSensitivity;
                
                var newVector = new Vector3(mazeGameMazeModel.MazeRotationX, mazeGameMazeModel.MazeRotationY,
                    mazeGameMazeModel.MazeRotationZ);
                eventBus.Publish(new SetMazeRotationSignal(newVector));
            }
        }

        private void GetBallVelocity(ActualVelocitySignal ballVelocityVector)
        {
            var payload = ballVelocityVector.Payload;
            mazeGameBallModel.Speed = payload.magnitude;
            eventBus.Publish(new GetActualBallSpeed(mazeGameBallModel.Speed));
        }

        private void WhichCheckpoint(CheckpointSignal checkpointSignal)
        {
            var checkpoint = checkpointSignal.Payload;
            switch (checkpoint)
            {
                case "1":
                {
                    Debug.Log("First checkpoint");
                }
                    break;
                case "2":
                {
                    Debug.Log("Second checkpoint");
                }
                    break;
                case "3":
                {
                    Debug.Log("Third checkpoint");
                }
                    break;
                case "Meta":
                {
                    LoadMainScene();
                }
                    break;
            }
        }

        private void LoadMainScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}


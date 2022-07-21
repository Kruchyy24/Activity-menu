using Redbus.Interfaces;
using Signals;
using TMPro;
using UnityEngine;
using Zenject;

namespace View
{
    public class MazeGameView : MonoBehaviour
    {
        [SerializeField] private GameObject ball;
        [SerializeField] private Canvas ballCanvas;
        [SerializeField] private TextMeshProUGUI speedBallText;
        [SerializeField] private GameObject maze;

        private IEventBus eventBus;
        
        private float mousePositionX;
        private float mousePositionY;
        private float mousePositionXEarlier;
        private float mousePositionYEarlier;

        [Inject]
        private void Init(IEventBus eventBus)
        {
            this.eventBus = eventBus;

            Setup();
        }

        private void Setup()
        {
            eventBus.Subscribe<SetBallOnStartPosition>(a=>SetBallOnStartPosition());
            eventBus.Subscribe<SetMazeRotationSignal>(SetMazeRotation);
            eventBus.Subscribe<GetActualBallSpeed>(SetBallCanvasSpeed);
        }

        private void SetBallOnStartPosition()
        {
            ball.transform.position = new Vector3(-20, 30, -20);
        }

        private void Awake()
        {
            mousePositionX = Input.mousePosition.x;
            mousePositionY = Input.mousePosition.y;
            mousePositionXEarlier = Input.mousePosition.x;
            mousePositionYEarlier = Input.mousePosition.y;
        }

        private void Update()
        {
            BallCanvasFollowingBallPosition();
            GetActualMazeRotation();

            mousePositionX = Input.mousePosition.x;
            mousePositionY = Input.mousePosition.y;
            
            if (mousePositionX != mousePositionXEarlier)
            {
                var deltaMousePosition = mousePositionX - mousePositionXEarlier;
                var vector = new Vector3(0, 0, deltaMousePosition);
                eventBus.Publish(new ChangeMazeRotationSignal(vector));
            }
            
            if (mousePositionY != mousePositionYEarlier)
            {
                var deltaMousePosition = mousePositionY - mousePositionYEarlier;
                var vector = new Vector3(deltaMousePosition, 0, 0);
                eventBus.Publish(new ChangeMazeRotationSignal(vector));
            }
            
            mousePositionXEarlier = Input.mousePosition.x;
            mousePositionYEarlier = Input.mousePosition.y;
        }

        private void FixedUpdate()
        {
            var ballVelocityVector = ball.GetComponent<Rigidbody>().velocity;
            eventBus.Publish(new ActualVelocitySignal(ballVelocityVector));
        }
        
        private void GetActualMazeRotation()
        {
            var vector = maze.transform.rotation.eulerAngles;
            eventBus.Publish(new GetActualMazeRotationSignal(vector));
        }

        private void SetMazeRotation(SetMazeRotationSignal vectorDeltaMazeRotation)
        {
            var vector = vectorDeltaMazeRotation.Payload;
            maze.transform.Rotate(new Vector3(vector.x,vector.y,vector.z), Space.Self);
        }

        private void BallCanvasFollowingBallPosition()
        {
            ballCanvas.transform.position = ball.transform.position;
        }

        private void SetBallCanvasSpeed(GetActualBallSpeed ballSpeed)
        {
            var payload = ballSpeed.Payload;
            speedBallText.text = payload.ToString();
        }
    }
}
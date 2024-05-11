using JuegoProgramacionGrafica;
using OpenTK.Windowing.Common;

namespace juegoProgramacionGrafica
{
    public class ElementController
    {
        private delegate void ThreadKickstart();

        private Game game;
        private List<Action> actions = new();
        private Thread thread;
        bool running = true;

        public float anim = 0;

        public ElementController(Game game)
        {
            this.game = game;
            game.CloseEvent += OnClose;
            game.UpdateFrame += AnimationLoop;

            actions.Add(() => RotateOverTime(0, 0, 180, 0, 0.5f, anim, this.game.delta));
            actions.Add(() => MoveOverTime(0, 0.30f, 0, 0, 0.25f, anim, this.game.delta));
            actions.Add(() => MoveOverTime(0, -0.25f, 0, 0.25f, 0.5f, anim, this.game.delta));
            actions.Add(() => RotateOverTime(0, 0, -180, 0.5f, 0.515f, anim, this.game.delta));
            actions.Add(() => RotateOverTime(0, 0, 180, 0.5f, 1, anim, this.game.delta));
            actions.Add(() => MoveOverTime(0, 0.30f, 0, 0.5f, 0.75f, anim, this.game.delta));
            actions.Add(() => MoveOverTime(0, -0.25f, 0, 0.75f, 1, anim, this.game.delta));

            //thread = new(new ThreadStart(AnimationLoop);
            //thread.Start();
        }

        private void AnimationLoop(FrameEventArgs e = new())
        {
                Console.WriteLine(anim);
                foreach (Action action in actions)
                {
                    action();
                }
                anim += game.delta;
        }

        public void RotateOverTime(float x, float y, float z, float start_time, float end_time, float time, float delta)
        {
            float duration = end_time - start_time;
            if (time > end_time || start_time > time || game.ui_handler.animation_target == null) return;
            game.ui_handler.animation_target.Rotate(x * (delta / duration), y * (delta / duration), z * (delta / duration));
        }

        public void MoveOverTime(float x, float y, float z, float start_time, float end_time, float time, float delta)
        {
            float duration = end_time - start_time;
            if (time > end_time || start_time > time || game.ui_handler.animation_target == null) return;
            game.ui_handler.animation_target.Move(x * (delta / duration), y * (delta / duration), z * (delta / duration));
        }

        private void OnClose()
        {
            running = false;
            //thread.Interrupt();
        }

    }
}


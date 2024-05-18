using JuegoProgramacionGrafica;
using System.Diagnostics;

namespace juegoProgramacionGrafica
{
    public enum AnimationType
    {
        Translation = 0,
        Rotation = 1,
        Scale = 2
    }

    public class Animation
    {
        private ElementController parent;
        AnimationType type;
        public bool started = false, stopped = false;
        private float x, y, z, ix, iy, iz, begin, end;

        public Animation (AnimationType type, float x, float y, float z, float beginTimestamp, float endTimestamp, ElementController parent)
        {
            this.parent = parent;
            this.type = type;
            this.x = x;
            this.y = y;
            this.z = z;
            begin = beginTimestamp;
            end = endTimestamp;
        }
        public Animation(AnimationType type, float x, float y, float z, float beginTimestamp, ElementController parent)
        {
            this.parent = parent;
            this.type = type;
            this.x = x;
            this.y = y;
            this.z = z;
            begin = beginTimestamp;
            end = begin;
        }

        public void execute()
        {
            float duration = end - begin;
            if (stopped || parent.anim < begin) return;
            if (started) {
                switch (type)
                {
                    case AnimationType.Translation:
                        parent.target.Move(x * (parent.delta / duration), y * (parent.delta / duration), z * (parent.delta / duration));
                        if (parent.anim > end) { parent.target.SetPosition(ix + x, iy + y, iz + z); stopped = true; }
                        break;

                    case AnimationType.Rotation:
                        parent.target.Rotate(x * (parent.delta / duration), y * (parent.delta / duration), z * (parent.delta / duration));
                        if (parent.anim > end) { parent.target.SetRotation(ix + x, iy + y, iz + z); stopped = true; }
                        break;

                    case AnimationType.Scale:
                        parent.target.Scale(x * (parent.delta / duration), y * (parent.delta / duration), z * (parent.delta / duration));
                        if (parent.anim > end) { parent.target.SetScale(ix + x, iy + y, iz + z); stopped = true; }
                        break;
                }
            }
            else
            {
                ix = parent.target._position[0];
                iy = parent.target._position[1];
                iz = parent.target._position[2];
                started = true;
            }
        }

        public void reset()
        {
            started = false;
            stopped = false;
        }
    }

    public class ElementController
    {
        private delegate void ThreadKickstart();

        private Game game;
        public GraphicsElement target;
        private List<Animation> animations = new();
        private Thread thread;
        bool running = true;
        bool animating = false;
        public float delta = 0;
        Stopwatch stopwatch = new();
        float lastElapsed = 0;

        float[] oldpos, oldrot, oldscl;

        public float anim = 0;

        public ElementController(Game game)
        {
            this.game = game;
            target = game.elem["main scene"].children["ball"].children["aux"];
            oldpos = target._position;
            oldrot = target._rotation;
            oldscl = target._scale;

            game.Unload += OnUnload;

            animations.Add(new(AnimationType.Rotation,     0,        0.00f, 180, 000, 0500, this));
            animations.Add(new(AnimationType.Rotation,     0,        0.00f, 0,   500, this));
            animations.Add(new(AnimationType.Translation,  -0.270f,  0,     0,   500, this));
            animations.Add(new(AnimationType.Rotation,     0,        0.00f, 180, 500, 1000, this));
            animations.Add(new(AnimationType.Translation,  0,        0.15f, 000, 510, 750,  this));
            animations.Add(new(AnimationType.Translation,  0,       -0.15f, 000, 750, 1000, this));
            animations.Add(new(AnimationType.Rotation,     0,        0.00f, 0, 1000,  this));
            animations.Add(new(AnimationType.Translation, -0.270f,   0,     0, 1000,  this));
            animations.Add(new(AnimationType.Rotation,     0,        0.00f, 180, 1000, 1500, this));
            animations.Add(new(AnimationType.Translation,  0,        0.35f, 000, 1010, 1250, this));
            animations.Add(new(AnimationType.Translation,  0,       -0.35f, 000, 1250, 1500, this));

            thread = new(new ThreadStart(AnimationLoop));
            stopwatch.Start();
            thread.Start();
        }

        private void AnimationLoop()
        {
            while (running)
            {
                float elapsed = stopwatch.ElapsedMilliseconds;
                delta = elapsed - lastElapsed;
                lastElapsed = elapsed;
                Console.WriteLine(anim/1000);
                if (animating) 
                {
                    foreach (Animation animation in animations)
                    {
                        animation.execute();
                    }
                    anim += delta;
                }
            }
        }

        private void OnUnload()
        {
            running = false;
            thread.Interrupt();
        }

        public void ResetAnim()
        {
            animating = false;
            anim = 0;
            target.SetPosition(oldpos[0], oldpos[1], oldpos[2]);
            target.SetRotation(oldrot[0], oldrot[1], oldrot[2]);
            target.SetScale(oldscl[0], oldscl[1], oldscl[2]);
            foreach (Animation animation in animations)
            {
                animation.reset();
            }
        }

        public void StartAnim()
        {
            ResetAnim();
            animating = true;
        }

    }
}


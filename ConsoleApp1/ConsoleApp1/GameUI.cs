using OpenTK.Graphics.OpenGL4;
using Microsoft.VisualBasic;
using ImGuiNET;
using OpenTK.Mathematics;

namespace JuegoProgramacionGrafica
{

    public class GameUI
    {
        Game game;
        GraphicsElement current;
        GraphicsElement animation_target;
        float[] target_pos;
        float[] target_rot;
        float[] target_scl;

        string current_name;
        System.Numerics.Vector3 _pos = new();
        System.Numerics.Vector3 _rot = new();
        System.Numerics.Vector3 _scl = new();
        bool _vis = false;
        bool collapsed = false;
        bool showfaces = false;
        float anim = 0;
        OpenFileDialog openFileDialog;
        SaveFileDialog saveFileDialog;

        public GameUI(Game game)
		{
            this.game = game;
            openFileDialog = new();
		}

        public void Render(float delta)
        {
            if (animation_target != null)
            {
                current.RotateOverTime(0, 0, 180, 0, 0.5f, anim, delta);
                current.MoveOverTime(0, 0.30f, 0, 0, 0.25f, anim, delta);
                current.MoveOverTime(0, -0.25f, 0, 0.25f, 0.5f, anim, delta);
                current.RotateOverTime(0, 0, -180, 0.5f, 0.515f, anim, delta);
                current.MoveOverTime(-0.325f, 0, 0, 0.5f, 0.515f, anim, delta);
                current.RotateOverTime(0, 0, 180, 0.5f, 1, anim, delta);
                current.MoveOverTime(0, 0.30f, 0, 0.5f, 0.75f, anim, delta);
                current.MoveOverTime(0, -0.25f, 0, 0.75f, 1, anim, delta);
                anim += delta;
            }
            ImGui.NewFrame();
            ImGui.Begin("sub", ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoTitleBar);
            ImGui.SetWindowSize(new System.Numerics.Vector2(40f, game.ClientSize.Y));
            ImGui.SetWindowPos(new System.Numerics.Vector2(game.ClientSize.X - 304f + (collapsed ? 264 : 0), 0f));
            if (ImGui.Button("collapse")) collapsed = !collapsed;
            ImGui.End();
            if (!collapsed)
            {
                ImGui.Begin("", ImGuiWindowFlags.NoMove | ImGuiWindowFlags.MenuBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoTitleBar);
                ImGui.SetWindowSize(new System.Numerics.Vector2(264f, game.ClientSize.Y));
                ImGui.SetWindowPos(new System.Numerics.Vector2(game.ClientSize.X - 264f, 0f));
                if (ImGui.BeginMenuBar())
                {
                    if (ImGui.BeginMenu("File"))
                    {
                        if (ImGui.MenuItem("New scene")) game.elem.Add(Interaction.InputBox("Name of the new scene", "New Scene", "New Scene", 0, 0), new());
                        if (ImGui.MenuItem("Load scene"))
                        {
                            if (openFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                game.elem.Add(Interaction.InputBox("Name of the new scene", "New Scene", openFileDialog.SafeFileName[..^5], 0, 0), FileUtils.Deserialize(openFileDialog.FileName));
                            }
                        }
                        if (ImGui.MenuItem("Load object", current != null && current.level == 0))
                        {
                            if (openFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                current.children.Add(Interaction.InputBox("Name of the new scene", "New Scene", openFileDialog.SafeFileName[..^5], 0, 0), FileUtils.Deserialize(openFileDialog.FileName));
                            }
                        }
                        ImGui.EndMenu();
                    }
                    ImGui.EndMenuBar();
                }

                ImGui.Text(game.fps.ToString() + " FPS");

                ImGui.Separator();

                if (ImGui.BeginChild("node_tree", new System.Numerics.Vector2(250f, 250f), true, ImGuiWindowFlags.AlwaysVerticalScrollbar))
                {
                    GenTree(game.elem);
                    ImGui.EndChild();
                }

                if (current != null)
                {
                    _pos = new(current._position[0], current._position[1], current._position[2]);
                    _rot = new(current._rotation[0], current._rotation[1], current._rotation[2]);
                    _scl = new(current._scale[0], current._scale[1], current._scale[2]);
                    _vis = current.visible;

                    ImGui.Text(current_name);
                    ImGui.Checkbox("Visible", ref _vis);
                    ImGui.DragFloat3("Position", ref _pos, 0.01f);
                    ImGui.DragFloat3("Rotation", ref _rot, 1.0f);
                    ImGui.DragFloat3("Scale", ref _scl, 0.01f);
                    if (ImGui.Button("Exec Anim."))
                    {
                        target_pos = current._position;
                        target_rot = current._rotation;
                        target_scl = current._scale;
                        animation_target = current;
                        anim = 0;
                    }

                    if (_rot.X > 360f) _rot.X -= 360f;
                    if (_rot.Y > 360f) _rot.Y -= 360f;
                    if (_rot.Z > 360f) _rot.Z -= 360f;
                    if (_rot.X < 0f) _rot.X += 360f;
                    if (_rot.Y < 0f) _rot.Y += 360f;
                    if (_rot.Z < 0f) _rot.Z += 360f;

                    current.SetPosition(_pos.X, _pos.Y, _pos.Z);
                    current.SetRotation(_rot.X, _rot.Y, _rot.Z);
                    current.SetScale(_scl.X, _scl.Y, _scl.Z);
                    current.visible = _vis;
                }
                ImGui.End();
            }
            game.gui.Render(game.ClientSize);
            GL.Enable(EnableCap.DepthTest);
        }

        private void GenTree(Dictionary<string, GraphicsElement> elem)
        {
            if (!showfaces && elem.Values.First().level == 2) return;
            foreach (string key in elem.Keys)
            {
                ImGuiTreeNodeFlags flags = ImGuiTreeNodeFlags.OpenOnArrow | ImGuiTreeNodeFlags.DefaultOpen;
                if (ImGui.TreeNodeEx(key, flags))
                {
                    if (ImGui.IsItemClicked()) 
                    {
                        if (animation_target != null)
                        {
                            current.SetPosition(target_pos[0], target_pos[1], target_pos[2]);
                            current.SetRotation(target_rot[0], target_rot[1], target_rot[2]);
                            current.SetScale(target_scl[0], target_scl[1], target_scl[2]);
                            animation_target = null;
                        }
                        current = elem[key]; 
                        current_name = key;
                    }
                    GenTree(elem[key].children);
                    ImGui.TreePop();
                }
            }
        }

    }
}

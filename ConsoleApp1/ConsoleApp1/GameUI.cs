using OpenTK.Graphics.OpenGL4;
using Microsoft.VisualBasic;
using ImGuiNET;
using Zenseless.OpenTK.GUI;

namespace JuegoProgramacionGrafica
{

    public class GameUI
    {
        Game game;
        string[] selected_node = { "", "", "" };
        int selected_node_depth = -1;
        System.Numerics.Vector3 _pos = new();
        System.Numerics.Vector3 _rot = new();
        System.Numerics.Vector3 _scl = new();
        bool _vis = false;
        OpenFileDialog openFileDialog;

        public GameUI(Game game)
		{
            this.game = game;
            openFileDialog = new();
		}

        public void Render()
        {
            ImGui.NewFrame();
            ImGui.Begin("Menu", ImGuiWindowFlags.MenuBar);
            if (ImGui.BeginMenuBar())
            {
                if (ImGui.BeginMenu("File"))
                {
                    if (ImGui.MenuItem("New scene")) game.scenes.Add(Interaction.InputBox("Name of the new scene", "New Scene", "New Scene", 0, 0), new());
                    if (ImGui.MenuItem("Load scene"))
                    {
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            game.scenes.Add(Interaction.InputBox("Name of the new scene", "New Scene", openFileDialog.SafeFileName[..^5], 0, 0), ObjectCreation.Deserialize<Scene>(openFileDialog.FileName));
                        }
                    }
                    if (ImGui.MenuItem("Load object"))
                    {
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            game.scenes["main_scene"].Objects.Add(Interaction.InputBox("Name of the new scene", "New Scene", openFileDialog.SafeFileName[..^5], 0, 0), ObjectCreation.Deserialize<Object3D>(openFileDialog.FileName));
                        }
                    }
                    ImGui.EndMenu();
                }
                ImGui.EndMenuBar();
            }

            ImGuiTreeNodeFlags scene_f = ImGuiTreeNodeFlags.DefaultOpen;
            ImGuiTreeNodeFlags objct_f = ImGuiTreeNodeFlags.DefaultOpen;
            ImGuiTreeNodeFlags piece_f = ImGuiTreeNodeFlags.DefaultOpen;
            scene_f |= ImGuiTreeNodeFlags.OpenOnArrow;
            objct_f |= ImGuiTreeNodeFlags.OpenOnArrow;
            piece_f |= ImGuiTreeNodeFlags.OpenOnArrow;

            foreach (string scene in game.scenes.Keys)
            {
                if (ImGui.TreeNodeEx(scene, scene_f))
                {
                    if (ImGui.IsItemClicked()) { selected_node = new string[] { scene, "", "" }; selected_node_depth = 0; }
                    foreach (string object3d in game.scenes[scene].Objects.Keys)
                    {
                        if (ImGui.TreeNodeEx(object3d, objct_f))
                        {
                            if (ImGui.IsItemClicked()) { selected_node = new string[] { object3d, scene, "" }; selected_node_depth = 1; }
                            foreach (string piece in game.scenes[scene].Objects[object3d].Pieces.Keys)
                            {
                                if (ImGui.TreeNodeEx(piece, piece_f))
                                {
                                    if (ImGui.IsItemClicked()) { selected_node = new string[] { piece, object3d, scene }; selected_node_depth = 2; }
                                    ImGui.TreePop();
                                }
                            }
                            ImGui.TreePop();
                        }
                    }
                    ImGui.TreePop();
                }
            }
            switch (selected_node_depth)
            {
                case 0:
                    _pos = new(game.scenes[selected_node[0]]._position[0], game.scenes[selected_node[0]]._position[1], game.scenes[selected_node[0]]._position[2]);
                    _rot = new(game.scenes[selected_node[0]]._rotation[0], game.scenes[selected_node[0]]._rotation[1], game.scenes[selected_node[0]]._rotation[2]);
                    _scl = new(game.scenes[selected_node[0]]._scale[0], game.scenes[selected_node[0]]._scale[1], game.scenes[selected_node[0]]._scale[2]);
                    _vis = game.scenes[selected_node[0]].visible;
                    break;
                case 1:
                    _pos = new(game.scenes[selected_node[1]].Objects[selected_node[0]]._position[0], game.scenes[selected_node[1]].Objects[selected_node[0]]._position[1], game.scenes[selected_node[1]].Objects[selected_node[0]]._position[2]);
                    _rot = new(game.scenes[selected_node[1]].Objects[selected_node[0]]._rotation[0], game.scenes[selected_node[1]].Objects[selected_node[0]]._rotation[1], game.scenes[selected_node[1]].Objects[selected_node[0]]._rotation[2]);
                    _scl = new(game.scenes[selected_node[1]].Objects[selected_node[0]]._scale[0], game.scenes[selected_node[1]].Objects[selected_node[0]]._scale[1], game.scenes[selected_node[1]].Objects[selected_node[0]]._scale[2]);
                    _vis = game.scenes[selected_node[1]].Objects[selected_node[0]].visible;
                    break;
                case 2:
                    _pos = new(game.scenes[selected_node[2]].Objects[selected_node[1]].Pieces[selected_node[0]]._position[0], game.scenes[selected_node[2]].Objects[selected_node[1]].Pieces[selected_node[0]]._position[1], game.scenes[selected_node[2]].Objects[selected_node[1]].Pieces[selected_node[0]]._position[2]);
                    _rot = new(game.scenes[selected_node[2]].Objects[selected_node[1]].Pieces[selected_node[0]]._rotation[0], game.scenes[selected_node[2]].Objects[selected_node[1]].Pieces[selected_node[0]]._rotation[1], game.scenes[selected_node[2]].Objects[selected_node[1]].Pieces[selected_node[0]]._rotation[2]);
                    _scl = new(game.scenes[selected_node[2]].Objects[selected_node[1]].Pieces[selected_node[0]]._scale[0], game.scenes[selected_node[2]].Objects[selected_node[1]].Pieces[selected_node[0]]._scale[1], game.scenes[selected_node[2]].Objects[selected_node[1]].Pieces[selected_node[0]]._scale[2]);
                    _vis = game.scenes[selected_node[2]].Objects[selected_node[1]].Pieces[selected_node[0]].visible;
                    break;
            }

            ImGui.Text(selected_node[0]);
            ImGui.Checkbox("Visible", ref _vis);
            ImGui.DragFloat3("Position", ref _pos);
            ImGui.DragFloat3("Rotation", ref _rot);
            ImGui.DragFloat3("Scale", ref _scl);
            switch (selected_node_depth)
            {
                case 0:
                    game.scenes[selected_node[0]].SetPosition(_pos.X, _pos.Y, _pos.Z);
                    game.scenes[selected_node[0]].SetRotation(_rot.X, _rot.Y, _rot.Z);
                    game.scenes[selected_node[0]].SetScale(_scl.X, _scl.Y, _scl.Z);
                    game.scenes[selected_node[0]].visible = _vis;
                    break;
                case 1:
                    game.scenes[selected_node[1]].Objects[selected_node[0]].SetPosition(_pos.X, _pos.Y, _pos.Z);
                    game.scenes[selected_node[1]].Objects[selected_node[0]].SetRotation(_rot.X, _rot.Y, _rot.Z);
                    game.scenes[selected_node[1]].Objects[selected_node[0]].SetScale(_scl.X, _scl.Y, _scl.Z);
                    game.scenes[selected_node[1]].Objects[selected_node[0]].visible = _vis;
                    break;
                case 2:
                    game.scenes[selected_node[2]].Objects[selected_node[1]].Pieces[selected_node[0]].SetPosition(_pos.X, _pos.Y, _pos.Z);
                    game.scenes[selected_node[2]].Objects[selected_node[1]].Pieces[selected_node[0]].SetRotation(_rot.X, _rot.Y, _rot.Z);
                    game.scenes[selected_node[2]].Objects[selected_node[1]].Pieces[selected_node[0]].SetScale(_scl.X, _scl.Y, _scl.Z);
                    game.scenes[selected_node[2]].Objects[selected_node[1]].Pieces[selected_node[0]].visible = _vis;
                    break;
            }
            ImGui.End();
            game.gui.Render(game.ClientSize);
            GL.Enable(EnableCap.DepthTest);
        }
    }
}

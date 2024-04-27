using System;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Windows.Forms;

namespace JuegoProgramacionGrafica
{
    public partial class Form1 : Form
    {
        Game game;

        public Form1(Game game)
        {
            InitializeComponent();
            this.game = game;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateTreeView();
            TopMost = true;
        }

        private void object_visibility_toggle_CheckStateChanged(object sender, EventArgs e)
        {
            switch (scene_tree.SelectedNode.Level)
            {
                case 0:
                    game.scenes[scene_tree.SelectedNode.Text].visible = object_visibility_toggle.Checked;
                    break;

                case 1:
                    game.scenes[scene_tree.SelectedNode.Parent.Text].Objects[scene_tree.SelectedNode.Text].visible = object_visibility_toggle.Checked;
                    break;
                case 2:
                    game.scenes[scene_tree.SelectedNode.Parent.Parent.Text].Objects[scene_tree.SelectedNode.Parent.Text].Pieces[scene_tree.SelectedNode.Text].visible = object_visibility_toggle.Checked;
                    break;
            }
        }

        private void UpdateTreeView()
        {
            scene_tree.Nodes.Clear();
            foreach (KeyValuePair<string, Scene> scene in game.scenes)
            {
                TreeNode tmpScene = new(scene.Key);
                tmpScene.Checked = scene.Value.visible;
                foreach (KeyValuePair<string, Object3D> object3d in game.scenes[scene.Key].Objects)
                {
                    TreeNode tmpObject = new(object3d.Key);
                    tmpObject.Checked = object3d.Value.visible;
                    foreach (KeyValuePair<string, Piece> piece in game.scenes[scene.Key].Objects[object3d.Key].Pieces)
                    {
                        TreeNode tmpPiece = new(piece.Key);
                        tmpPiece.Checked = piece.Value.visible;
                        tmpObject.Nodes.Add(tmpPiece);
                    }
                    tmpScene.Nodes.Add(tmpObject);
                }
                scene_tree.Nodes.Add(tmpScene);
            }
            scene_tree.ExpandAll();
        }

        private void scene_tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (scene_tree.SelectedNode.Level)
            {
                case 0:
                    object_visibility_toggle.Checked = game.scenes[scene_tree.SelectedNode.Text].visible;
                    break;

                case 1:
                    object_visibility_toggle.Checked = game.scenes[scene_tree.SelectedNode.Parent.Text].Objects[scene_tree.SelectedNode.Text].visible;
                    break;
                case 2:
                    object_visibility_toggle.Checked = game.scenes[scene_tree.SelectedNode.Parent.Parent.Text].Objects[scene_tree.SelectedNode.Parent.Text].Pieces[scene_tree.SelectedNode.Text].visible;
                    break;
            }
            object_visibility_toggle.Enabled = true;
            position_label.Enabled = true;
            rotation_label.Enabled = true;
            scale_label.Enabled = true;
            position_input_x.Enabled = true;
            rotation_input_x.Enabled = true;
            scale_input_x.Enabled = true;
            position_input_y.Enabled = true;
            rotation_input_y.Enabled = true;
            scale_input_y.Enabled = true;
            position_input_z.Enabled = true;
            rotation_input_z.Enabled = true;
            scale_input_z.Enabled = true;
        }

        private void object_visibility_toggle_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void scene_tree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Level)
            {
                case 0:
                    game.scenes[e.Node.Text].visible = e.Node.Checked;
                    break;
                case 1:
                    game.scenes[e.Node.Parent.Text].Objects[e.Node.Text].visible = e.Node.Checked;
                    break;
                case 2:
                    game.scenes[e.Node.Parent.Parent.Text].Objects[e.Node.Parent.Text].Pieces[e.Node.Text].visible = e.Node.Checked;
                    break;
            }
            if (e.Node.IsSelected) object_visibility_toggle.Checked = e.Node.Checked;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

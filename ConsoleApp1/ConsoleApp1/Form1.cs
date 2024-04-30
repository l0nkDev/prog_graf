using Microsoft.VisualBasic;

namespace JuegoProgramacionGrafica
{
    public partial class Form1 : Form
    {
        public delegate void treeUpdate();
        public treeUpdate myDelegate;
        bool changing_selection = false;
        Game game;

        public Form1(Game game)
        {
            InitializeComponent();
            this.game = game;
            myDelegate = new treeUpdate(UpdateTreeView);
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
            scene_tree.SelectedNode.Checked = object_visibility_toggle.Checked;
        }

        public void UpdateTreeView()
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
            changing_selection = true;
            switch (scene_tree.SelectedNode.Level)
            {
                case 0:
                    object_visibility_toggle.Checked = game.scenes[scene_tree.SelectedNode.Text].visible;
                    position_input_x.Value = (decimal)game.scenes[scene_tree.SelectedNode.Text].offset_x;
                    position_input_y.Value = (decimal)game.scenes[scene_tree.SelectedNode.Text].offset_y;
                    position_input_z.Value = (decimal)game.scenes[scene_tree.SelectedNode.Text].offset_z;
                    rotation_input_x.Value = (decimal)game.scenes[scene_tree.SelectedNode.Text].pitch_value;
                    rotation_input_y.Value = (decimal)game.scenes[scene_tree.SelectedNode.Text].yaw_value;
                    rotation_input_z.Value = (decimal)game.scenes[scene_tree.SelectedNode.Text].roll_value;
                    scale_input_x.Value = (decimal)game.scenes[scene_tree.SelectedNode.Text].scale_x;
                    scale_input_y.Value = (decimal)game.scenes[scene_tree.SelectedNode.Text].scale_y;
                    scale_input_z.Value = (decimal)game.scenes[scene_tree.SelectedNode.Text].scale_z;
                    openObjectToolStripMenuItem.Enabled = true;
                    break;

                case 1:
                    object_visibility_toggle.Checked = game.scenes[scene_tree.SelectedNode.Parent.Text].Objects[scene_tree.SelectedNode.Text].visible;
                    position_input_x.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Text].Objects[scene_tree.SelectedNode.Text].offset_x;
                    position_input_y.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Text].Objects[scene_tree.SelectedNode.Text].offset_y;
                    position_input_z.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Text].Objects[scene_tree.SelectedNode.Text].offset_z;
                    rotation_input_x.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Text].Objects[scene_tree.SelectedNode.Text].pitch_value;
                    rotation_input_y.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Text].Objects[scene_tree.SelectedNode.Text].yaw_value;
                    rotation_input_z.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Text].Objects[scene_tree.SelectedNode.Text].roll_value;
                    scale_input_x.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Text].Objects[scene_tree.SelectedNode.Text].scale_x;
                    scale_input_y.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Text].Objects[scene_tree.SelectedNode.Text].scale_y;
                    scale_input_z.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Text].Objects[scene_tree.SelectedNode.Text].scale_z;
                    openObjectToolStripMenuItem.Enabled = false;
                    break;
                case 2:
                    object_visibility_toggle.Checked = game.scenes[scene_tree.SelectedNode.Parent.Parent.Text].Objects[scene_tree.SelectedNode.Parent.Text].Pieces[scene_tree.SelectedNode.Text].visible;
                    position_input_x.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Parent.Text].Objects[scene_tree.SelectedNode.Parent.Text].Pieces[scene_tree.SelectedNode.Text].offset_x;
                    position_input_y.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Parent.Text].Objects[scene_tree.SelectedNode.Parent.Text].Pieces[scene_tree.SelectedNode.Text].offset_y;
                    position_input_z.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Parent.Text].Objects[scene_tree.SelectedNode.Parent.Text].Pieces[scene_tree.SelectedNode.Text].offset_z;
                    rotation_input_x.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Parent.Text].Objects[scene_tree.SelectedNode.Parent.Text].Pieces[scene_tree.SelectedNode.Text].pitch_value;
                    rotation_input_y.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Parent.Text].Objects[scene_tree.SelectedNode.Parent.Text].Pieces[scene_tree.SelectedNode.Text].yaw_value;
                    rotation_input_z.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Parent.Text].Objects[scene_tree.SelectedNode.Parent.Text].Pieces[scene_tree.SelectedNode.Text].roll_value;
                    scale_input_x.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Parent.Text].Objects[scene_tree.SelectedNode.Parent.Text].Pieces[scene_tree.SelectedNode.Text].scale_x;
                    scale_input_y.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Parent.Text].Objects[scene_tree.SelectedNode.Parent.Text].Pieces[scene_tree.SelectedNode.Text].scale_y;
                    scale_input_z.Value = (decimal)game.scenes[scene_tree.SelectedNode.Parent.Parent.Text].Objects[scene_tree.SelectedNode.Parent.Text].Pieces[scene_tree.SelectedNode.Text].scale_z;
                    openObjectToolStripMenuItem.Enabled = false;
                    break;
            }
            object_visibility_toggle.Enabled = true;
            position_label.Enabled = true;
            rotation_label.Enabled = true;
            scale_label.Enabled = true;
            position_input_x.Enabled = true;
            position_input_y.Enabled = true;
            position_input_z.Enabled = true;
            rotation_input_x.Enabled = true;
            rotation_input_y.Enabled = true;
            rotation_input_z.Enabled = true;
            scale_input_x.Enabled = true;
            scale_input_y.Enabled = true;
            scale_input_z.Enabled = true;
            changing_selection = false;
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

        private void UpdatePosition(object sender, EventArgs e)
        {
            float x = (float)position_input_x.Value;
            float y = (float)position_input_y.Value;
            float z = (float)position_input_z.Value;
            if (changing_selection) return;
            switch (scene_tree.SelectedNode.Level)
            {
                case 0:
                    game.scenes[scene_tree.SelectedNode.Text].SetPosition(x, y, z);
                    break;
                case 1:
                    game.scenes[scene_tree.SelectedNode.Parent.Text].Objects[scene_tree.SelectedNode.Text].SetPosition(x, y, z);
                    break;
                case 2:
                    game.scenes[scene_tree.SelectedNode.Parent.Parent.Text].Objects[scene_tree.SelectedNode.Parent.Text].Pieces[scene_tree.SelectedNode.Text].SetPosition(x, y, z);
                    break;
            }
        }

        private void UpdateRotation(object sender, EventArgs e)
        {
            decimal x = rotation_input_x.Value;
            decimal y = rotation_input_y.Value;
            decimal z = rotation_input_z.Value;

            if (changing_selection) return;
            if      (x >= 360) rotation_input_x.Value -= 360;
            else if (y >= 360) rotation_input_y.Value -= 360;
            else if (z >= 360) rotation_input_z.Value -= 360;
            else if (x <    0) rotation_input_x.Value += 360;
            else if (y <    0) rotation_input_y.Value += 360;
            else if (z <    0) rotation_input_z.Value += 360;

            switch (scene_tree.SelectedNode.Level)
            {
                case 0:
                    game.scenes[scene_tree.SelectedNode.Text].SetRotation((float)x, (float)y, (float)z);
                    break;
                case 1:
                    game.scenes[scene_tree.SelectedNode.Parent.Text].Objects[scene_tree.SelectedNode.Text].SetRotation((float)x, (float)y, (float)z);
                    break;
                case 2:
                    game.scenes[scene_tree.SelectedNode.Parent.Parent.Text].Objects[scene_tree.SelectedNode.Parent.Text].Pieces[scene_tree.SelectedNode.Text].SetRotation((float)x, (float)y, (float)z);
                    break;
            }
        }

        private void UpdateScale(object sender, EventArgs e)
        {
            float x = (float)scale_input_x.Value;
            float y = (float)scale_input_y.Value;
            float z = (float)scale_input_z.Value;
            if (changing_selection) return;
            switch (scene_tree.SelectedNode.Level)
            {
                case 0:
                    game.scenes[scene_tree.SelectedNode.Text].SetScale(x, y, z);
                    break;
                case 1:
                    game.scenes[scene_tree.SelectedNode.Parent.Text].Objects[scene_tree.SelectedNode.Text].SetScale(x, y, z);
                    break;
                case 2:
                    game.scenes[scene_tree.SelectedNode.Parent.Parent.Text].Objects[scene_tree.SelectedNode.Parent.Text].Pieces[scene_tree.SelectedNode.Text].SetScale(x, y, z);
                    break;
            }

        }

        private void newSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.scenes.Add(Interaction.InputBox("Name of the new scene", "New Scene", "new_scene", 0, 0), new());
            UpdateTreeView();
        }

        private void openSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                game.queue_is_scene = true;
                game.queue_name = Interaction.InputBox("Name of the new scene", "New Scene", "new_scene", 0, 0);
                game.queue_path = openFileDialog1.FileName;
                game.is_queued = true;
            }
        }

        private void openOnjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                game.queue_is_scene = false;
                game.queue_name = Interaction.InputBox("Name of the new object", "New object", "new_object", 0, 0);
                game.queue_path = openFileDialog1.FileName;
                game.queue_scene = scene_tree.SelectedNode.Text;
                game.is_queued = true;
            }
        }
    }
}

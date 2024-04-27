namespace JuegoProgramacionGrafica
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            object_visibility_toggle = new CheckBox();
            scene_tree = new TreeView();
            scale_input_y = new NumericUpDown();
            position_input_x = new NumericUpDown();
            scale_input_z = new NumericUpDown();
            position_input_y = new NumericUpDown();
            position_input_z = new NumericUpDown();
            rotation_input_x = new NumericUpDown();
            scale_input_x = new NumericUpDown();
            rotation_input_y = new NumericUpDown();
            rotation_input_z = new NumericUpDown();
            position_label = new Label();
            rotation_label = new Label();
            scale_label = new Label();
            ((System.ComponentModel.ISupportInitialize)scale_input_y).BeginInit();
            ((System.ComponentModel.ISupportInitialize)position_input_x).BeginInit();
            ((System.ComponentModel.ISupportInitialize)scale_input_z).BeginInit();
            ((System.ComponentModel.ISupportInitialize)position_input_y).BeginInit();
            ((System.ComponentModel.ISupportInitialize)position_input_z).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rotation_input_x).BeginInit();
            ((System.ComponentModel.ISupportInitialize)scale_input_x).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rotation_input_y).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rotation_input_z).BeginInit();
            SuspendLayout();
            // 
            // object_visibility_toggle
            // 
            object_visibility_toggle.AutoSize = true;
            object_visibility_toggle.Enabled = false;
            object_visibility_toggle.Location = new Point(185, 12);
            object_visibility_toggle.Name = "object_visibility_toggle";
            object_visibility_toggle.Size = new Size(60, 19);
            object_visibility_toggle.TabIndex = 14;
            object_visibility_toggle.Text = "Visible";
            object_visibility_toggle.UseVisualStyleBackColor = true;
            object_visibility_toggle.CheckedChanged += object_visibility_toggle_CheckedChanged;
            object_visibility_toggle.CheckStateChanged += object_visibility_toggle_CheckStateChanged;
            // 
            // scene_tree
            // 
            scene_tree.CheckBoxes = true;
            scene_tree.HideSelection = false;
            scene_tree.Location = new Point(12, 12);
            scene_tree.Name = "scene_tree";
            scene_tree.PathSeparator = ".";
            scene_tree.Size = new Size(167, 230);
            scene_tree.TabIndex = 16;
            scene_tree.AfterCheck += scene_tree_AfterCheck;
            scene_tree.AfterSelect += scene_tree_AfterSelect;
            // 
            // scale_input_y
            // 
            scale_input_y.Enabled = false;
            scale_input_y.Location = new Point(271, 199);
            scale_input_y.Name = "scale_input_y";
            scale_input_y.Size = new Size(120, 23);
            scale_input_y.TabIndex = 17;
            // 
            // position_input_x
            // 
            position_input_x.Enabled = false;
            position_input_x.Location = new Point(271, 41);
            position_input_x.Name = "position_input_x";
            position_input_x.Size = new Size(120, 23);
            position_input_x.TabIndex = 18;
            // 
            // scale_input_z
            // 
            scale_input_z.Enabled = false;
            scale_input_z.Location = new Point(271, 219);
            scale_input_z.Name = "scale_input_z";
            scale_input_z.Size = new Size(120, 23);
            scale_input_z.TabIndex = 19;
            // 
            // position_input_y
            // 
            position_input_y.Enabled = false;
            position_input_y.Location = new Point(271, 61);
            position_input_y.Name = "position_input_y";
            position_input_y.Size = new Size(120, 23);
            position_input_y.TabIndex = 20;
            // 
            // position_input_z
            // 
            position_input_z.Enabled = false;
            position_input_z.Location = new Point(271, 81);
            position_input_z.Name = "position_input_z";
            position_input_z.Size = new Size(120, 23);
            position_input_z.TabIndex = 21;
            // 
            // rotation_input_x
            // 
            rotation_input_x.Enabled = false;
            rotation_input_x.Location = new Point(271, 110);
            rotation_input_x.Name = "rotation_input_x";
            rotation_input_x.Size = new Size(120, 23);
            rotation_input_x.TabIndex = 22;
            // 
            // scale_input_x
            // 
            scale_input_x.Enabled = false;
            scale_input_x.Location = new Point(271, 179);
            scale_input_x.Name = "scale_input_x";
            scale_input_x.Size = new Size(120, 23);
            scale_input_x.TabIndex = 23;
            // 
            // rotation_input_y
            // 
            rotation_input_y.Enabled = false;
            rotation_input_y.Location = new Point(271, 130);
            rotation_input_y.Name = "rotation_input_y";
            rotation_input_y.Size = new Size(120, 23);
            rotation_input_y.TabIndex = 24;
            // 
            // rotation_input_z
            // 
            rotation_input_z.Enabled = false;
            rotation_input_z.Location = new Point(271, 150);
            rotation_input_z.Name = "rotation_input_z";
            rotation_input_z.Size = new Size(120, 23);
            rotation_input_z.TabIndex = 25;
            // 
            // position_label
            // 
            position_label.Enabled = false;
            position_label.Location = new Point(185, 43);
            position_label.Name = "position_label";
            position_label.Size = new Size(50, 15);
            position_label.TabIndex = 26;
            position_label.Text = "Position";
            // 
            // rotation_label
            // 
            rotation_label.Enabled = false;
            rotation_label.Location = new Point(185, 112);
            rotation_label.Name = "rotation_label";
            rotation_label.Size = new Size(52, 15);
            rotation_label.TabIndex = 27;
            rotation_label.Text = "Rotation";
            // 
            // scale_label
            // 
            scale_label.Enabled = false;
            scale_label.Location = new Point(185, 181);
            scale_label.Name = "scale_label";
            scale_label.Size = new Size(34, 15);
            scale_label.TabIndex = 28;
            scale_label.Text = "Scale";
            scale_label.Click += label2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(403, 254);
            Controls.Add(scale_label);
            Controls.Add(rotation_label);
            Controls.Add(position_label);
            Controls.Add(rotation_input_z);
            Controls.Add(rotation_input_y);
            Controls.Add(scale_input_x);
            Controls.Add(rotation_input_x);
            Controls.Add(position_input_z);
            Controls.Add(position_input_y);
            Controls.Add(scale_input_z);
            Controls.Add(position_input_x);
            Controls.Add(scale_input_y);
            Controls.Add(scene_tree);
            Controls.Add(object_visibility_toggle);
            Name = "Form1";
            Text = "GLControl Test Form";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)scale_input_y).EndInit();
            ((System.ComponentModel.ISupportInitialize)position_input_x).EndInit();
            ((System.ComponentModel.ISupportInitialize)scale_input_z).EndInit();
            ((System.ComponentModel.ISupportInitialize)position_input_y).EndInit();
            ((System.ComponentModel.ISupportInitialize)position_input_z).EndInit();
            ((System.ComponentModel.ISupportInitialize)rotation_input_x).EndInit();
            ((System.ComponentModel.ISupportInitialize)scale_input_x).EndInit();
            ((System.ComponentModel.ISupportInitialize)rotation_input_y).EndInit();
            ((System.ComponentModel.ISupportInitialize)rotation_input_z).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private CheckBox object_visibility_toggle;
        private TreeView scene_tree;
        private NumericUpDown scale_input_y;
        private NumericUpDown position_input_x;
        private NumericUpDown scale_input_z;
        private NumericUpDown position_input_y;
        private NumericUpDown position_input_z;
        private NumericUpDown rotation_input_x;
        private NumericUpDown scale_input_x;
        private NumericUpDown rotation_input_y;
        private NumericUpDown rotation_input_z;
        private Label position_label;
        private Label rotation_label;
        private Label scale_label;
    }
}

#endregion
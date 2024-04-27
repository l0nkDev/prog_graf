﻿using Newtonsoft.Json;
using OpenTK.Mathematics;
using System;

namespace JuegoProgramacionGrafica
{
    public class Object3D
    {
        public Dictionary<string, Piece> Pieces = new();

        private Matrix4 pitch, roll, yaw;
        public float offset_x, offset_y, offset_z = 0.0f;
        public float pitch_value, roll_value, yaw_value = 0.0f;
        public bool visible = true;
        public Object3D(float offset_x = 0.0f, float offset_y = 0.0f, float offset_z = 0.0f)
		{
			this.offset_x = offset_x;
			this.offset_y = offset_y;
			this.offset_z = offset_z;

            pitch = roll = yaw = Matrix4.Identity;
        }

        public Object3D()
        {
            pitch = roll = yaw = Matrix4.Identity;
        }

		public void Draw(Shader shader, Matrix4 model, Matrix4 view, Matrix4 projection, double time)
		{
            if (visible) 
            { 
			    foreach (Piece piece in Pieces.Values)
			    {
				    piece.Draw(shader, roll * pitch * yaw * Matrix4.CreateTranslation(offset_x, offset_y, offset_z) * model, view, projection, time);
			    }
            }
        }

        public void SetRotation(float pitch, float yaw, float roll)
        {
            this.yaw = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(yaw));
            this.pitch = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(pitch));
            this.roll = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(roll));
            yaw_value = yaw;
            pitch_value = pitch;
            roll_value = roll;
        }

        public void Rotate(float pitch, float yaw, float roll)
        {
            this.yaw *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(yaw));
            this.pitch *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(pitch));
            this.roll *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(roll));
            yaw_value += yaw;
            pitch_value += pitch;
            roll_value += roll;
        }

        public void SetPosition(float x, float y, float z)
        {
            offset_x = x;
            offset_y = y;
            offset_z = z;
        }

        public void Move(float x, float y, float z)
        {
            offset_x += x;
            offset_y += y;
            offset_z += z;
        }
    }
}

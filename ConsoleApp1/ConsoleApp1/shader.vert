#version 330 core
layout (location = 0) in vec3 aPos;   // the position variable has attribute position 0
layout (location = 1) in vec2 aTexCoord; // the color variable has attribute position 1
  
out vec2 TexCoord; // output a color to the fragment shader

uniform mat4 transform;

void main()
{   
    TexCoord = aTexCoord;
    gl_Position = vec4(aPos, 1.0) * transform;
}    
#version 330

uniform vec3 inColor;
out vec4 outputColor;

void main()
{
    outputColor = vec4(inColor.x, inColor.y, inColor.z, 1.0);
}

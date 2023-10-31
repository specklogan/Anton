#version 330

uniform sampler2D texture0;
in vec2 texCoord;
out vec4 outputColor;

void main()
{
    outputColor = texture(texture0, texCoord);
}

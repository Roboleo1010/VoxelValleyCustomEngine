#version 330

in vec3 vPosition;
in vec3 vColor;

uniform mat4 modelview;

out vec3 color;

void
main()
{
    gl_Position=modelview*vec4(vPosition,1.);
    color=vColor;
}
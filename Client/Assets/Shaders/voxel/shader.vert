#version 330

in vec3 vPosition;
in vec3 vColor;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec3 color;

void main() {
  gl_Position = projection * view * model * vec4(vPosition, 1.);
  color = vColor;
}
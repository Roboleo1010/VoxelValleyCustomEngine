#version 330

in vec3 vPosition;
in vec4 vColor;
in vec3 vNormal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec3 objectColor;
out vec3 normal;
out vec3 fragPosInWorld;

void main() {
  gl_Position = projection * view * model * vec4(vPosition, 1.);
  objectColor = vec3(vColor.r/255, vColor.g/255, vColor.b/255);
  normal = vNormal;
  fragPosInWorld = vec3(model * vec4(vPosition, 1));
}
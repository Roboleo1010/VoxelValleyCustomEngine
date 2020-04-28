#version 330

in vec3 objectColor;
in vec3 normal;
in vec3 fragPosInWorld;

uniform vec3 lightColor;

out vec4 FragColor;

// PHONG Configuration
float ambientStrength = 0.1;
uniform vec3 lightPos = vec3(0, 500, 0);

void main() {
  // calculate ambuient strength
  vec3 ambient = ambientStrength * lightColor;

  // calculate is the direction vector between the light source and the
  // fragment's position
  vec3 norm = normalize(normal);
  vec3 lightDirection = normalize(lightPos - fragPosInWorld);

  // calculate diffuse Impact
  float diff = max(dot(norm, lightDirection), 0.0);
  vec3 diffuse = diff * lightColor;

  vec3 result = (ambient + diffuse) * objectColor;

  FragColor = vec4(result, 1);
}
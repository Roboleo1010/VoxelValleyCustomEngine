#version 330

// #define NR_POINT_LIGHTS 4

struct DirectionalLight {
  vec3 direction;

  vec3 ambient;
  vec3 diffuse;
  vec3 specular;
};

struct PointLight {
  vec3 position;

  float constant;
  float linear;
  float quadratic;

  vec3 ambient;
  vec3 diffuse;
  vec3 specular;
};

in vec3 objectColor;
in vec3 normal;
in vec3 fragPosInWorld;

uniform int numPointLights;
uniform vec3 viewPos;
uniform DirectionalLight directionalLight;
// uniform PointLight pointLights[NR_POINT_LIGHTS];

out vec4 FragColor;

vec3 CalculateDirectionalLight(DirectionalLight light, vec3 normal,
                               vec3 viewDirection);

vec3 CalculatePointLight(PointLight light, vec3 normal, vec3 fragPos,
                         vec3 viewDirection);

void main() {
  vec3 norm = normalize(normal);
  vec3 viewDirection = normalize(viewPos - fragPosInWorld);

  // 1. Directional light
  vec3 result =
      CalculateDirectionalLight(directionalLight, norm, viewDirection);

  // 2. Point lights
  // for (int i = 0; i < NR_POINT_LIGHTS; i++)
  //  result += CalculatePointLight(pointLights[i], norm, fragPosInWorld,
  //  viewDirection);

  // 3. Spot lights TODO: Get From
  // https://github.com/inputusernamehere/LearnOpenTK/blob/feature/4.0/Chapter2/6-MultipleLights/Shaders/lighting.frag

  FragColor = vec4(result, 1.0);
}

vec3 CalculateDirectionalLight(DirectionalLight light, vec3 normal,
                               vec3 viewDirection) {
  vec3 lightDirection = normalize(-light.direction);

  // diffuse shading
  float diff = max(dot(normal, lightDirection), 0.0);

  // Specular shading
  vec3 reflectDirection = reflect(-lightDirection, normal);
  float spec = pow(max(dot(viewDirection, reflectDirection), 0.0),
                   1); // Where 0 = shinyness

  // combine results
  vec3 ambient = light.ambient;
  vec3 diffuse = light.diffuse * diff;
  vec3 specular = light.specular * spec;

  return (ambient + diffuse + specular) * objectColor;
}

vec3 CalculatePointLight(PointLight light, vec3 normal, vec3 fragPos,
                         vec3 viewDirection) {
  vec3 lightDirection = normalize(light.position - fragPos);

  // diffuse shading
  float diff = max(dot(normal, lightDirection), 0.0);

  // specular shading
  vec3 reflectDirection = reflect(-lightDirection, normal);
  float spec = pow(max(dot(viewDirection, reflectDirection), 0.0),
                   32); // Where 32 = shinyness

  // attenuation
  float distance = length(light.position - fragPos);
  float attenuation = 1.0 / (light.constant + light.linear * distance +
                             light.quadratic * (distance * distance));

  // combine results
  vec3 ambient = light.ambient;
  vec3 diffuse = light.diffuse * diff;
  vec3 specular = light.specular * spec;

  ambient *= attenuation;
  diffuse *= attenuation;
  specular *= attenuation;

  return (ambient + diffuse + specular);
}
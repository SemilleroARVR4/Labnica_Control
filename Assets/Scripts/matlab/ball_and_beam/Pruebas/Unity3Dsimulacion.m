clear all, close all, clc;
%%
% Modelo
[G Xbar Ubar] = get_linear_model();
% Tiempo de muestreo
h = 0.1;
% Modelo discreto
Gz = c2d(G,h,'zoh');

% Referencia
s = tf('s');
r = 1/s;
rz = c2d(ss(r),h,'zoh');

%Controlador
Controller = lqr_controller_design(Gz,rz,0.0125);
MLc = canon(Controller,'modal')

% Generador de codigo.
generateCode(MLc);

% Condición inicial.
X0 = [0 0 0 0];
% Momento de inercia.
[m,J,r,g]           = deal(0.1,0.025,0.01,9.807);
L = sqrt((2*J) /(m))

% Datos simulink graficas.
resultados = sim('Simul_LQR',50)
setPoint = resultados.Ref.signals.values;
pos = resultados.estados.signals.values(:,2);
angulo = resultados.estados.signals.values(:,1);

% Animación
ball_and_beam_animation(angulo,pos)

plot(setPoint,'LineWidth',2); hold on; grid on; plot(pos,'LineWidth',2);
figure;
plot(rad2deg(angulo),'LineWidth',2);

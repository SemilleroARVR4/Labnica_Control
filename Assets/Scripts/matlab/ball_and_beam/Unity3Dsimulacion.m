clear all, close all, clc;
%%
% planta
[G Xbar Ubar] = get_linear_model();
h = 0.05;
Gz = c2d(G,h,'zoh');
% set point
s = tf('s');
r = 1/s;
rz = c2d(ss(r),h,'zoh');
% diseno controlador
Controller = lqr_controller_design(Gz,rz,0.01);
MLc = canon(Controller,'modal');
% condiciones de simulacion
X0 = [0 0 0 0];
SP = 0.3;
% generador de codigo
generateCode(MLc);
% simulacion en simulink
out = sim('Simul_LQR',100);
% variables de simulink
teta = out.estados.signals.values(:,1);
x = out.estados.signals.values(:,2);
ref = mean(out.Ref.signals.values());
u = out.U.signals.values;
t2 = out.U.time;
t = out.estados.time;
% plot de resultados
plot(t,x);
figure;
plot(t,rad2deg(teta));
figure;
plot(t2,u);
figure;
ball_and_beam_animation(teta,x);
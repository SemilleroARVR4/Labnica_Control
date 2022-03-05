clear all, close all, clc;
%%
[G Xbar Ubar] = get_linear_model();
h = 0.01;
Gz = c2d(G,h,'zoh');

s = tf('s');
r = 1/s;
rz = c2d(ss(r),h,'zoh');

Controller = lqr_controller_design(Gz,rz,100);
generateCode(Controller);
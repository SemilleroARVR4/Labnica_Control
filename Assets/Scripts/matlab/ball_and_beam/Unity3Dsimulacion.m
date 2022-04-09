clear all, close all, clc;
%%
[G Xbar Ubar] = get_linear_model();
h = 0.05;
Gz = c2d(G,h,'zoh');

s = tf('s');
r = 1/s;
rz = c2d(ss(r),h,'zoh');

Controller = lqr_controller_design(Gz,rz,1);
MLc = canon(Controller,'modal')

generateCode(MLc);

X0 = [0 0 0 0 0]

%Momento de inercia
[m,J,r,g]           = deal(0.1,0.025,0.01,9.807);  

L = sqrt((2*J) /(m))







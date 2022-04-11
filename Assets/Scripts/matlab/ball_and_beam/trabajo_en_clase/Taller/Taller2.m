close all;
clear;
clc;

% Declaración matrices

A = [0 0 1 0; 0 0 0 1;0 0 0 0;0 0 0 0];
B = [0 0;0 0;1 0;0 1];
C = [1 0 0 0];
D = 0;

% Cm = [1 0 0 0;0 1 0 0];
% Dm = zeros(2,2);
%% 
h = 0.1;
Mt = ss(A,B,C,D);
Mk = c2d(Mt,h,'zoh');

[A,B,C,D] = deal(Mk.A,Mk.B,Mk.C,Mk.D);
%% Referencia

s = tf('s');
fg = 1/s;

Ref = c2d(ss(fg),h,'zoh');
[Az,Bz] = deal(Ref.A,Ref.B);

%% Matriz extendida

Znx_nz = zeros(length(B),length(Bz));
Znz_nu = zeros(length(Bz),2)

Ae = [A,Znx_nz; -Bz*C, Az];
Be = [B; Znz_nu];

Q = diag([1,1,1,1,100]);

R = 1;
L = dlqr(Ae,Be,Q,R)

VP = eig(Ae-Be*L)
%eig(Ae*Be*L)
abs(VP)
%%
X0 = [10; -pi/4; 0; 0];

resultados = sim('SimulTaller',10)

setPoint = resultados.Ref.signals.values;
pos1 = resultados.estados.signals.values(:,1);
vel1 = resultados.estados.signals.values(:,2);
pos2 = resultados.estados.signals.values(:,3);
vel2 = resultados.estados.signals.values(:,4);

Uf = resultados.U.signals.values(:,1);
Ut = resultados.U.signals.values(:,2);

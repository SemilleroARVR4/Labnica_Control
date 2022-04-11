close all;
clear;
clc;

%Declaración simbolos.
syms x4 x1 x2 x3 m f1 f2 g r J uf ut

%Declaración ecuaciónes en diferencia.
x1p = x3;
x2p = x4;
x3p = -g + (1/m)*cos(x2)*(f1 + f2);
x4p = (r/J)*(f1- f2);

Y = [x1;x2];

%Jacobianas
A = jacobian([x1p; x2p; x3p; x4p], [x1; x2; x3; x4]);
B = jacobian([x1p; x2p; x3p; x4p],[f1;f2]);
C = jacobian(Y,[x1; x2; x3; x4]);
D = jacobian(Y,[f1;f2]);


%% Punto 2

%Definción de parametros
x2 = 0;
x3 = pi/2; %pi/2
x4 = 0;

m  = 10;
r  = 0.5;
J  = 0.1;
g  = 9.8;
f1 = (m*r*uf+J*cos(x2)*ut+m*g*r)/(2*cos(x2)*r);
f2 = (m*r*uf-J*cos(x2)*ut+m*g*r)/(2*cos(x2)*r);

Ac = eval(A);
Bc = eval(B);

Cc = eye(4);
Dc = zeros(4,2);

Mt = ss(Ac,Bc,Cc,Dc);
Mk = c2d(Mt,0.1,'zoh');

[A,B,C,D] = deal(Mk.A,Mk.B,Mk.C,Mk.D);

s = tf('s');
fg = 1/s;

Ref = c2d(ss(fg),0.1,'zoh');
[Az,Bz] = deal(Ref.A,Ref.B);

Znx_nz = zeros(length(B),length(Bz));
Znz_nu = zeros(length(Bz),2)

Ae = [A,Znx_nz; -Bz*C, Az];
Be = [B; Znz_nu];

Q = eye(4);
R = 1;
L = dlqr(Ae,Be,Q,R)


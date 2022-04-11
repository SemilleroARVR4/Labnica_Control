close all;
clear all;
clc;

%% Punto1

h = 0.01;

[MLint,XBar,UBar]  = get_linear_model();

Mk = c2d(MLint,h,'zoh')

[A,B,C,D] = deal(Mk.A,Mk.B,Mk.C,Mk.D);
%% Punto 2

valorG = 1;
s = tf('s');
fref = valorG/s;
Ref = c2d(ss(fref),h,'zoh')

[Az,Bz] = deal(Ref.A,Ref.B);

Znx_nz = zeros(length(B),length(Bz));
Znz_nu = zeros(length(Bz),1);
Ae = [A,Znx_nz; -Bz*C, Az];
Be = [B; Znz_nu];

Q = diag([1,1,1,1,1]);
R = 1;
L = dlqr(Ae,Be,Q,R)

%X0 = [deg2rad(30) 0.5 0 0]
X0 = [0 0.5 0 0]

resultados = sim('Simul1',5)

setPoint = resultados.Ref.signals.values;
pos = resultados.estados.signals.values(:,2);
angulo = resultados.estados.signals.values(:,1);
%ball_and_beam_animation(mean(setPoint),pos)
ball_and_beam_animation(angulo,pos)

[m,J,r,g]           = deal(0.1,0.025,0.01,9.807);
L = sqrt((2*J) /(m))

%plot(setPoint,'LineWidth',2); hold on; plot(pos,'LineWidth',2);

close all, clear all, clc;
%% laplace
K = 2;
Tao = 40;
s = tf('s');
G = K/(Tao*s + 1);
step(G);
%% tiempo 
syms s Tao K;
Y = (K/(Tao*s + 1))*(5/s);
y = ilaplace(Y);
%%
U = 3;
K = 2;
Tao = 40;
t = [1:1:400];
y = U*K - U*K*exp(-t/Tao);
figure;
plot(t,y);
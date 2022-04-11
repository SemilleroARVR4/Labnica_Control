function [MLint,XBar,UBar]  = get_linear_model()
%% Constants
m                   = sym('m','positive');
J                   = sym('J','positive');
r                   = sym('r','positive');
g                   = sym('g','positive');
a                   = sym('a','positive');

%% States
% x1 = theta, x2  = x, x3 = thetadot, x4 = xdot
x1                  = sym('x1','real');
x2                  = sym('x2','real');
x3                  = sym('x3','real');
x4                  = sym('x4','real');
U                   = sym('U','real');

%% Nonlinear model
Th                  = x1;
x                   = x2;
Thdot               = x3;
xdot                = x4;                                                
% Inertia matrix
D                   = [m*r^2+m*x^2+J, -m*r; -m*r, m];
% Coriolis and centrifugal forces
H                   = [2*m*x*xdot*Thdot; -m*x*Thdot^2];
% Gravity force
G                   = [m*g*x*cos(Th)-m*g*r*sin(Th); m*g*sin(Th)];
% Inputs
E                   = [U; 0];                                               

%% Linearized model state space model
% x1 = theta, x2  = x, x3 = thetadot, x4 = xdot
Accel               = D\(E-H-G);
XDot                = [x3; x4; Accel];                                      

% Measured output
Ym                  = x2;

% Jacobians
X                   = [x1; x2; x3; x4];
ASym                = jacobian(XDot,X);
BSym                = jacobian(XDot,U);
CmSym               = jacobian(Ym,X);
DmSym               = jacobian(Ym,U);

%% Equilibrium point
x1bar               = 0.;
x2bar               = 0;
x3bar               = 0;
x4bar               = 0;
XBar                = [x1bar; x2bar; x3bar; x4bar];
UBar                = 0;
[x1,x2,x3,x4,U]     = deal(x1bar,x2bar,x3bar,x4bar,UBar);                   %#ok<ASGLU>
[m,J,r,g]           = deal(0.1,0.025,0.01,9.807);                           %#ok<ASGLU>

%% Numeric model
A                   = eval(ASym);
B                   = eval(BSym);
C                   = eval(CmSym);
D                   = eval(DmSym);
MLint               = ss(A,B,C,D);
end











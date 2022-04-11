function XDot = ball_and_beam_model(U,X)
%#codegen
% Constants of the model
[m,J,r,g]           = deal(0.1,0.025,0.01,9.807);

%% State space model
% x1 = theta, x2  = x, x3 = thetadot, x4 = xdot
[X1,X2,X3,X4]= deal(X(1),X(2),X(3),X(4));

% Inertia matrix
D           = [m*r^2+m*X2^2+J, -m*r; -m*r, m];

% Coriolis and centrifugal forces
H           = [2*m*X2*X4*X3; -m*X2*X3^2];

% Gravity forces
G           = [m*g*X2*cos(X1)-m*g*r*sin(X1); m*g*sin(X1)];

% Generalized effort
E           = [U; 0];

% Derivatives of states
XDot        = [X3; X4; D\(E-H-G)];
end
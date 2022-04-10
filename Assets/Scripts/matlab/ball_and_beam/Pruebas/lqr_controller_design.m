function [Controller,Process] = lqr_controller_design(Gz,Reference,Speed)
% Gz         is a state space discrete time system.
% REFERENCES is a one-dimensional array of discrete time transfer functions.
% FACTOR     is a real number multiplying the R matrix of the LQR problem.

% By default MATLAB does not consider time delays as states
Gznodelay       = delays2states(Gz);
Process         = ss(Gznodelay);
Process         = minreal(Process);

% Additional states are obtained from the reference signal.
Reference       = ss(Reference);

% Number of states of the process to control
nx              = size(Process.A,1);
% Number of states of the reference signals
nz              = size(Reference.A,1);
% Number of inputs to the process to control
nu              = size(Process.B,2);
% Number of outputs of the process to control
ny              = size(Process.C,1);

%% LQR for the model with additional states
Ae              = [Process.A, zeros(nx,nz); -Reference.B*Process.C, Reference.A];
Be              = [Process.B; zeros(nz,nu)];
R               = eye(nu);
Q               = blkdiag(eye(nx),Speed*eye(nz));
L               = dlqr(Ae,Be,Q,R);

%% Observer
Qo              = eye(nx);
Ro              = Speed*eye(ny);
K               = dlqr(Process.A',Process.C',Qo,Ro)';
%% Controller
Lx              = L(:,1:nx);
Lz              = L(:,nx+1:end);
AControl        = [Process.A-K*Process.C-Process.B*Lx,  -Process.B*Lz; 
                                         zeros(nz,nx),  Reference.A];
BControl        = [zeros(nx,nu),             K; 
                   Reference.B, -Reference.B];
CControl        = [-Lx, -Lz];
DControl        = zeros(nu,2*ny);
Controller      = ss(AControl, BControl, CControl, DControl, Process.Ts);
%Controller      = canon(Controller,'modal');
end

function Gznodelay = delays2states(Gz)
if any(Gz.InputDelay > 0) || any(any(Gz.IODelay > 0)) || any(Gz.OutputDelay > 0)
    % Dimensions of the transfer function matrix
    [row,col]   = size(Gz.Denominator);
    
    % Matrix of static transfer functions
    Gznodelay   = tf(zeros(row,col));
    
    % Delays are replaced by poles at z = 0
    for r = 1:row
        for c = 1:col
            Gzsiso          = Gz(r,c);
            totaldelay      = Gzsiso.InputDelay + Gzsiso.IODelay + Gzsiso.OutputDelay;
            Gznodelay(r,c)  = tf(Gzsiso.Numerator{1},[Gzsiso.Denominator{1}, zeros(1,totaldelay)],Gz.Ts);
        end   
    end
else
    Gznodelay = Gz;
end      
end

clc;
clear variables;
close all;

%% Get linear model
[modelt,xbar,ubar]  = get_linear_model();

%% Controlled output. 
% ycontrol = 1 means that the controlled output is the first output of
% "modelt"
ycontrol           = 2;

%% Discrete time model
h                   = 0.01;
modelk              = c2d(modelt,h);

%% States gain and extended model
r                   = 1;
q                   = diag([1; 1; 1; 1; 0.01]);
[lstates,add_states]= get_states_gain(modelk,ycontrol,q,r);

%% Optimal observer
qobs                = 100*diag([1;1;1;1]);
robs                = eye(2);
kobs                = dlqr(modelk.A',modelk.C',qobs,robs)';
observer            = ss(modelk.A-kobs*modelk.C,...
                        [kobs,modelk.B],eye(4),zeros(4,3),h);

%% Simulation
% Initial state
% x1 = theta, x2  = x, x3 = thetadot, x4 = xdot
X0                  = [0; 0; 0; 0];

%% Call to Simulink
setpoint            = -0.5;
compact             = 1;
if compact == 1
    control             = get_controller(modelk,ycontrol,add_states,lstates,kobs);
    results             = sim('ball_and_beam_compact',6);    
else    
    results             = sim('ball_and_beam',6);
end


%% Plot simresults
Time                = results.States.time;
Th                  = results.States.signals.values(:,1);
Pos                 = results.States.signals.values(:,2);
Torque              = results.U.signals.values;
plot_simresults(Time,Th,Pos,Torque);

%% Animation
ball_and_beam_animation(Th,Pos);

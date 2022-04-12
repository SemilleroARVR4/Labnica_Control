function gpid = pid_design(gs,type,tau)
% This function calculates the coefficients kp, ki and kd of the
% controller: 
%
%               GPID(s) = kp + ki/s + kd*s
%
% Such that the closed loop transfer for "gs" is:
%
%               Gcl(s)  = 1/(tau*s+1)^r

%% Stability
if ~(isstable(gs) && isstable(1/gs))
    error('Poles and zeros of the transfer function must be in the left-half plane');
end

%% Symbolic transfer function
s               = sym('s');
num             = poly2sym(gs.num{1},s);
den             = poly2sym(gs.den{1},s);

%% Order of the closed loop transfer 1/(L*s+1)^r
npolos          = length(roots(gs.den{1}));
nzeros          = length(roots(gs.num{1}));
r               = npolos - nzeros;

%% Taylor expansion of the controller
imc_s           = s * den / (num*((tau*s + 1)^r - 1));
staylor         = taylor(imc_s,s,'ExpansionPoint',0,'Order',3);

%% f(s0 + s) = f(s0) + f'(s0)*(s) + f''(s0)*(s)^2/2! + f'''(s0)*(s)^3/3! + .. +
% s*gc(s) = c0 + c1*s + c2*s^2
%   gc(s) = c0/s + c1 + c2*s
ctaylor         = double(coeffs(staylor));
type            = lower(string(type));
ki              = ctaylor(1);
kp              = ctaylor(2);
s               = tf('s');
if length(ctaylor) >= 3 && type == "pid"
    kd          = ctaylor(3);
    gpid        = kp + ki/s + kd*s/(0.1*tau*s+1);
else
    gpid        = kp + ki/s;
end    
end

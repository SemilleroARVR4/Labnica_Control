function generateCode(Controller)
%GENERATECODE Summary of this function goes here
%   Detailed explanation goes here
fprintf('// ecuacion de estados \n');
% ecuacion de estados MISO
for i=1:1:length(Controller.A)
    fprintf('x%dnext = ',i)
    for j=1:1:length(Controller.A)
        fprintf('%0.2ff*x%d + ',Controller.A(i,j),j)
    end 
    
    fprintf('%0.2ff*r + %0.2ff*y',Controller.B(i,1),Controller.B(i,2))
    
    fprintf('; \n')
end
fprintf('\n')
% ecuacion de salida
fprintf('// ecuacion de salida \n');
fprintf('u = ')
for i=1:1:length(Controller.C)
    fprintf('%0.2ff*x%d + ',Controller.C(1,i),i)
end 
fprintf('; \n')
end


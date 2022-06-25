function plot_simresults(Time,Th,Pos,Torque)
figure;

%% Angle
subplot(2,1,1);
plot(Time,rad2deg(Th),'Linewidth',2);
grid on;
title('Ball and beam closed loop response','Fontsize',12);
xlabel('Time [seconds]','Fontsize',12);
ylabel('Angle [degres]','Fontsize',12);

%% Position
subplot(2,1,2);
plot(Time,Pos,'Linewidth',2);
grid on;
title('Linear position','Fontsize',12);
xlabel('Time [seconds]','Fontsize',12);
ylabel('[meters]','Fontsize',12);

%% Torque
figure;
plot(Time,Torque,'Linewidth',2);
grid on;
title('Torque around the joint','Fontsize',12);
xlabel('Time [seconds]','Fontsize',12);
ylabel('[N m]','Fontsize',12);
end

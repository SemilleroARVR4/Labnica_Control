function ball_and_beam_animation(Q,X)
%% Rectangle representing the beam
XRect                           = 1.00*[+1; +1; -1; -1];
YRect                           = 0.05*[-1; +1; +1; -1];
PosRect                         = [XRect'; YRect'];

%% Triangle
XTriangle                       = 0.1*[+1;  0; -1];
YTriangle                       = 0.1*[-1; +1; -1];

for i = 1:length(Q)
    % Rotation matrix
    Angle                           = Q(i);
    disp(Angle)
    Rot                             = [cos(Angle), -sin(Angle); sin(Angle), cos(Angle)];
    disp(Rot)
    % Rotated coordinates
    PosRectNew                      = Rot * PosRect;
    XRect                           = PosRectNew(1,:)';
    YRect                           = PosRectNew(2,:)' + 0.1;
    % Rectangle representing the ball
    XBall                           = 0.10*[+1; +1; -1; -1] + X(i);
    YBall                           = 0.10*[-1; +1; +1; -1] + 0.25;
    PosBall                         = [XBall'; YBall'];
    PosBallNew                      = Rot * PosBall;
    XBall                           = PosBallNew(1,:)';
    YBall                           = PosBallNew(2,:)';

    %% Draw rotated object
    if i == 1
        HRect                           = fill(XRect,YRect,'b'); hold on;
        HBall                           = fill(XBall,YBall,'g');        
        fill(XTriangle,YTriangle,'r');
        axis equal;
        grid on;
    else
        set(HBall,'XData',XBall,'YData',YBall);
        set(HRect,'XData',XRect,'YData',YRect);       
    end
    pause(0.02);
    drawnow();
end
end
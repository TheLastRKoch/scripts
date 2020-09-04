echo ¨Seting iPad 10 inches mode¨
sudo xrandr --newmode "2048x1536_60.00"  267.25  2048 2208 2424 2800  1536 1539 1543 1592 -hsync +vsync
xrandr --addmode LVDS-1 "2048x1536_60.00"
xrandr --output LVDS-1 --scale 0.50x0.50
xrandr -o left

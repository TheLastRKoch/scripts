#!/bin/bash

#Run Start Sound
mpg123 "/home/rkoch/Sound Efects/Cyber start.mp3"

[Unit]
Description=Run AfterPowerOptions
After=suspend.target hibernate.target hybrid-sleep.target suspend-then-hibernate.target


[Service]
ExecStart=/home/rkoch/Scripts/StopHibernate.sh
#User=my_user_name
#Environment=DISPLAY=:0

[Install]
WantedBy=suspend.target hibernate.target hybrid-sleep.target suspend-then-hibernate.target
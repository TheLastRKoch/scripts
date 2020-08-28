echo "Digite el nombre de usuario"
read username

val1="useradd -m $username"
eval $val1

val2="passwd $username"
eval $val2

val3="usermod -a -G sudo $username"
eval $val3

val4="chsh -s /bin/bash $username"
eval $val4

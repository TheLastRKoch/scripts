echo "Digite el nombre de usuario"
echo"$(read username)"

echo "$(useradd -m $username)"

echo "$(passwd $username)"

echo "$(usermod -a -G sudo $username)"

echo "$(chsh -s /bin/bash $username)"




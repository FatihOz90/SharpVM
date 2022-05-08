name=\$(ls /sys/class/net | head -n 1)
d=\"IP_NETMASK\"
a=\"IP_GATEWAY\"
if [ \"\$d\" == \"255.255.255.0\" ];then
masked=\"24\"
elif [ \"\$d\" == \"255.255.255.128\" ];then
masked=\"25\"
elif [ \"\$d\" == \"255.255.255.192\" ];then
masked=\"26\"
elif [ \"\$d\" == \"255.255.255.224\" ];then
masked=\"27\"
elif [ \"\$d\" == \"255.255.255.240\" ];then
masked=\"28\"
elif [ \"\$d\" == \"255.255.255.248\" ];then
masked=\"29\"
elif [ \"\$d\" == \"255.255.255.252\" ];then
masked=\"30\"
elif [ \"\$d\" == \"255.255.255.254\" ];then
masked=\"31\"
elif [ \"\$d\" == \"255.255.255.255\" ];then
masked=\"32\"
fi

cat > /etc/netplan/config.yaml <<EOL
network:
  version: 2
  renderer: networkd
  ethernets:
   \$name:
    dhcp4: no
    addresses: [IP_ADDRESS/\$masked]
    gateway4: \$a
    nameservers:
      addresses: [8.8.8.8,8.8.4.4]
EOL
sudo netplan apply

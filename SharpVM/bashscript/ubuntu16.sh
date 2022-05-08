cat > /etc/network/interfaces << EOF
source /etc/network/interfaces.d/*
auto lo
iface lo inet loopback
auto NETNAME
iface NETNAME inet static
address "IP_ADDRESS"
gateway "IP_GATEWAY"
netmask "IP_NETMASK"
dns-nameservers 4.2.2.4
dns-search google.com
EOF

cat > /etc/resolv.conf <<EOL
nameserver "8.8.8.8"
nameserver "8.8.4.4"
search google.com
EOL

sed -i "\'s@NETNAME@$(ls /sys/class/net | head -n 1)@\'" /etc/network/interfaces >/dev/null 2>&1
service networking restart

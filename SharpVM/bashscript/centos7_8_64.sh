#!/bin/bash
name=\$(ls /sys/class/net | head -n 1)

cat > /etc/sysconfig/network-scripts/ifcfg-\$name << EOF
DEVICE=\$name
TYPE=Ethernet
ONBOOT=yes
IPADDR="IP_ADDRESS"
GATEWAY="IP_GATEWAY"
NETMASK="IP_NETMASK"
DNS1=4.2.2.4
dns-search google.com
EOF
sleep 0.5
chmod 777 /etc/sysconfig/network-scripts/ifcfg-\$name
sleep 0.5

if [ -d "/etc/sysconfig/network-scripts/ifcfg-\$name" ] then
systemctl restart network
# samba setup

    # installation
    yum install samba samba-client samba-common -y

    # backup original config file
    cp -pf /etc/samba/smb.conf /etc/samba/smb.conf.bak

    # add samba group and user
    groupadd smbgroup
    useradd smbusr0 -G smbgroup
    smbpasswd -a smbusr0   # Test123

    chmod -R 755 /srv/share
    chown -R root:smbgroup /srv/share

    # SELinux config
    chcon -t samba_share_t /srv/share/

    # config
    [share]
    path = /srv/share
    valid users = @smbgroup
    browseable = yes
    writeable = yes
    guest ok = no

    
    # enable smb service
    systemctl enable smb.service
    systemctl enable nmb.service
    systemctl restart smb.service
    systemctl restart nmb.service

    # firewall-cmd configuration
    firewall-cmd --permanent --zone=public --add-service=samba
    firewall-cmd --reload


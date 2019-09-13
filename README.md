# Orienteering-TV-Results

## Run on Raspberry Pi

### Install docker
https://docs.docker.com/v17.09/engine/installation/linux/docker-ce/debian/

### Instyall git
sudo apt-get install -y git

###
git clone https://github.com/Jocke-G/Orienteering-TV-Results.git

cd Orienteering-TV-Results

docker-compose -f docker-compose.rpi.yml --build up

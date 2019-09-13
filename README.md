# Orienteering-TV-Results

## Run on Raspberry Pi

### Install docker
https://docs.docker.com/v17.09/engine/installation/linux/docker-ce/debian/

### Install git
sudo apt-get install -y git

### Clone git repository
git clone https://github.com/Jocke-G/Orienteering-TV-Results.git

### Move inte folder
cd Orienteering-TV-Results

### Build and start docker containers
docker-compose -f docker-compose.rpi.yml up --build

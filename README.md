# Orienteering-TV-Results

## Run on Raspberry Pi

### 1. Install docker

Follow Docker guide:  
https://docs.docker.com/v17.09/engine/installation/linux/docker-ce/debian/

### 2. Install git

`sudo apt-get install -y git`

### 3. Clone git repository

`git clone https://github.com/Jocke-G/Orienteering-TV-Results.git`

### 4. Move inte folder

`cd Orienteering-TV-Results`

### 5. Build and start docker containers

`docker-compose -f docker-compose.rpi.yml up --build`

## Usage

Edit docker-compose file to your needs, with your favourite text editor.
`nano docker-compose.rpi.yml`

Open a browser to http://your-pi\
Press c to open config/command/conf whatever it should be called.

## Docker Cheat Sheet

* Start all containers

* Stop all containers\
`docker-compose -f docker-compose.rpi.yml down`

* Start single container\
-d for --detached\
`docker-compose -f docker-compose.rpi.yml up -d client`

* Stop single container\
`docker-compose -f docker-compose.rpi.yml kill layout-service`

* Show running containers\
`docker ps`

* Build single container\
``

* Show logs\
Last -f means follow\
docker-compose -f docker-compose.rpi.yml logs -f

* Logs for single service\
`docker-compose -f docker-compose.rpi.yml logs client`

#!/bin/bash

# Recreate config file
rm -rf ./config.json
touch ./config.json

# Add assignment 
echo "{" >> ./config.json

varname="mqtt_host"
value=$(printf '%s\n' "${!varname}")
[[ -z $value ]] && value="missing"
echo "  \"$varname\": \"$value\"," >> ./config.json

varname="mqtt_port"
value=$(printf '%s\n' "${!varname}")
[[ -z $value ]] && value=8000
echo "  \"$varname\": $value," >> ./config.json

varname="layouts_rest_host"
value=$(printf '%s\n' "${!varname}")
[[ -z $value ]] && value="missing"
echo "  \"$varname\": \"$value\"," >> ./config.json

varname="layouts_rest_port"
value=$(printf '%s\n' "${!varname}")
[[ -z $value ]] && value=8081
echo "  \"$varname\": $value," >> ./config.json

varname="results_rest_host"
value=$(printf '%s\n' "${!varname}")
[[ -z $value ]] && value="missing"
echo "  \"$varname\": \"$value\"," >> ./config.json

varname="results_rest_port"
value=$(printf '%s\n' "${!varname}")
[[ -z $value ]] && value=8081
echo "  \"$varname\": $value" >> ./config.json

echo "}" >> ./config.json

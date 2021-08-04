#!/usr/bin/env bash

for id in $(docker ps -q --filter ancestor=${1})
do
    echo "stopping container ${id}"
    docker stop "${id}"
done

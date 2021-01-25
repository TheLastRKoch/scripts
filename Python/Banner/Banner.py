import requests
import json
import os

hostname = os.popen("hostname").read().strip()

try:

	#Process Json data
	json_file = open("config.json")
	data = json.load(json_file)
	child = (data["Repositories"])
	setting = child[hostname]
	host_url = setting["url"]
	host_os = setting["os"]

	os.system("clear")
	print("...")

	req = requests.get(host_url)
	os.system("clear")
	if host_os == "Linux":
		print(os.popen("neofetch").read())
	print("")
	print(req.text)

except KeyError:
	print ("Error: there's no configuration for the hostname:  "+str(hostname))

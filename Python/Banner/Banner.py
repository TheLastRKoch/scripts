import requests
import json
import os

def clear(host_os):
	keyword = ""
	if host_os == "Windows":
		keyword = "cls"
	else:
		keyword = "clear"
	os.system(keyword)

def main():
	hostname = os.popen("hostname").read().strip()

	try:

		#Process Json data
		json_file = open("config.json")
		data = json.load(json_file)
		child = (data["Repositories"])
		setting = child[hostname]
		host_url = setting["url"]
		host_os = setting["os"]

		clear(host_os)
		print("Processing ...")

		req = requests.get(host_url)
		clear(host_os)
		if host_os == "Linux":
			print(os.popen("neofetch").read())
		print("")
		print(req.text)

	except KeyError:
		print ("Error: there's no configuration for the hostname:  "+str(hostname))

if __name__ == '__main__':
    main()

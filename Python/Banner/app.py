from pathlib import Path
import requests
import json
import sys
import os


def get_host_name():
	return os.popen("hostname").read().strip()


def get_configuration(hostname):
	try:
		#Process Json data
		json_file = open("config.json")
		data = json.load(json_file)
		child = (data["Repositories"])
		setting = child[hostname]
		return {
					"os":setting["os"], 
					"url":setting["url"], 
					"has_neofetch":bool(setting["has_neofetch"])
				}

	except KeyError:
		print (f"Error: there's no configuration for the hostname: {+str(hostname)}")


def clear_screen(os_type):
	keyword = ""
	if os_type == "Windows":
		keyword = "cls"
	else:
		keyword = "clear"
	os.system(keyword)


def show_processing_message(host_os):
	clear_screen(host_os)
	username = os.popen("whoami").read().strip()
	print (f"Welcome back {username} !!!")
	print("Processing ...")


def get_banner(conf):
	req = requests.get(conf["url"])
	clear_screen(conf["os"])
	if conf["has_neofetch"]:
		print(os.popen("neofetch").read()+"\n")
	return req.text

def get_banner_file_content(file_path):
	with open(file_path, 'r') as file:
		return file.read()

def create_file(file_path, body):
	with open(file_path, "w") as file:
		file.write(body)

if __name__ == '__main__':
	update = False
	
	if len(sys.argv)>1:
		if sys.argv[1] == "update":
			update = True
	
	banner_path = os.path.join(str(Path.home()),"banner.info") 
	if os.path.exists(banner_path) and not update:
		banner_body = get_banner_file_content(banner_path)
	else:
		hostname = get_host_name()
		conf = get_configuration(hostname)
		print(conf)
		clear_screen(conf["os"])
		show_processing_message(conf["os"])
		banner_body = get_banner(conf)
		create_file(banner_path,banner_body)
	print(banner_body)

'''
Created on 08-05-2012

@author: Malwin
'''

import sys
import ConfigParser
import subprocess

class Usage(Exception):
    def __init__(self, msg):
        self.msg = msg

def main(argv=None):
    config = ConfigParser.RawConfigParser(allow_no_value=True)
    config.readfp(open('config'))
    cfg_dict = {}
    cfg_dict['user'] = config.get('General', 'user')
    cfg_dict['password'] = config.get('General', 'password')
    cfg_dict['dbname'] = config.get('General', 'dbname')
    cfg_dict['sqlbin'] = config.get('General', 'sqlbin')
    cfg_dict['path_dest'] =config.get('General', 'path_dest')
    
    mysqldump_command = '\"' + cfg_dict['sqlbin'] + 'mysqldump.exe' + '\"' + ' ' + '--user=' + cfg_dict['user'] + ' ' + cfg_dict['dbname'] + ' > ' + cfg_dict['path_dest']
    p = subprocess.Popen(mysqldump_command, stdout=subprocess.PIPE, stderr=subprocess.PIPE, shell=True)
    p.wait()
    if p.returncode != 0:
        print 'something goes wrong'
        return 1;
    print 'DUMP DONE'
    return 0;

if __name__ == "__main__":
    sys.exit(main())